using System;
using System.Collections.Generic;
using System.Data;

namespace Restaurant_Management_App
{
    public class BuffetService
    {
        private readonly Database _db = Database.Instance;

        public void EnsureSchema()
        {
            string sql = @"
IF OBJECT_ID('dbo.BuffetCustomerAccount', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.BuffetCustomerAccount
    (
        userName NVARCHAR(50) PRIMARY KEY,
        [password] NVARCHAR(100) NOT NULL,
        fullName NVARCHAR(100) NULL,
        createdAt DATETIME NOT NULL DEFAULT GETDATE()
    )
END

IF OBJECT_ID('dbo.BuffetSession', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.BuffetSession
    (
        id INT IDENTITY PRIMARY KEY,
        billId INT NOT NULL,
        customerUserName NVARCHAR(50) NOT NULL,
        packageCode NVARCHAR(10) NOT NULL,
        pricePerHead DECIMAL(18,2) NOT NULL,
        guestCount INT NOT NULL,
        isLocked BIT NOT NULL DEFAULT 1,
        status NVARCHAR(20) NOT NULL DEFAULT N'Active',
        createdBy NVARCHAR(50) NULL,
        createdAt DATETIME NOT NULL DEFAULT GETDATE(),
        closedAt DATETIME NULL,
        CONSTRAINT FK_BuffetSession_Bill FOREIGN KEY (billId) REFERENCES Bill(id),
        CONSTRAINT FK_BuffetSession_Customer FOREIGN KEY (customerUserName) REFERENCES BuffetCustomerAccount(userName)
    )
END

IF OBJECT_ID('dbo.BuffetMenuScope', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.BuffetMenuScope
    (
        id INT IDENTITY PRIMARY KEY,
        idFood INT NOT NULL,
        packageCode NVARCHAR(10) NOT NULL,
        CONSTRAINT FK_BuffetMenuScope_Food FOREIGN KEY (idFood) REFERENCES Food(id)
    )
END

IF OBJECT_ID('dbo.BuffetOrderBatch', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.BuffetOrderBatch
    (
        id INT IDENTITY PRIMARY KEY,
        sessionId INT NOT NULL,
        batchNo INT NOT NULL,
        submittedAt DATETIME NOT NULL DEFAULT GETDATE(),
        status NVARCHAR(20) NOT NULL DEFAULT N'Pending',
        CONSTRAINT FK_BuffetOrderBatch_Session FOREIGN KEY (sessionId) REFERENCES BuffetSession(id)
    )
END

IF OBJECT_ID('dbo.BuffetOrderItem', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.BuffetOrderItem
    (
        id INT IDENTITY PRIMARY KEY,
        batchId INT NOT NULL,
        idFood INT NOT NULL,
        quantity INT NOT NULL,
        status NVARCHAR(20) NOT NULL DEFAULT N'Pending',
        CONSTRAINT FK_BuffetOrderItem_Batch FOREIGN KEY (batchId) REFERENCES BuffetOrderBatch(id),
        CONSTRAINT FK_BuffetOrderItem_Food FOREIGN KEY (idFood) REFERENCES Food(id)
    )
END

IF NOT EXISTS (SELECT 1 FROM dbo.BuffetMenuScope)
BEGIN
    INSERT INTO dbo.BuffetMenuScope(idFood, packageCode)
    SELECT id,
           CASE WHEN price <= 299000 THEN N'299K' ELSE N'599K' END
    FROM dbo.Food

    INSERT INTO dbo.BuffetMenuScope(idFood, packageCode)
    SELECT id, N'599K' FROM dbo.Food WHERE price <= 599000
END";

            _db.ExecuteNonQuery(sql);
        }

        public DataTable GetActiveSessions()
        {
            return _db.ExecuteQuery(@"
SELECT s.id, s.billId, s.customerUserName, s.packageCode, s.pricePerHead, s.guestCount, s.status, s.createdAt
FROM dbo.BuffetSession s
WHERE s.status = N'Active'
ORDER BY s.createdAt DESC");
        }

        public int CreateOrStartSession(string customerUserName, string customerPassword, string fullName, int billId, string packageCode, int guestCount, string createdBy)
        {
            if (string.IsNullOrWhiteSpace(customerUserName) || string.IsNullOrWhiteSpace(customerPassword))
                throw new Exception("Tài khoản và mật khẩu khách hàng không được để trống.");

            object existing = _db.ExecuteScalar($"SELECT COUNT(1) FROM dbo.BuffetCustomerAccount WHERE userName = N'{Escape(customerUserName)}'");
            int exists = Convert.ToInt32(existing);

            if (exists == 0)
            {
                _db.ExecuteNonQuery($@"INSERT INTO dbo.BuffetCustomerAccount(userName, [password], fullName)
                                      VALUES (N'{Escape(customerUserName)}', N'{Escape(customerPassword)}', N'{Escape(fullName)}')");
            }
            else
            {
                _db.ExecuteNonQuery($@"UPDATE dbo.BuffetCustomerAccount
                                      SET [password] = N'{Escape(customerPassword)}', fullName = N'{Escape(fullName)}'
                                      WHERE userName = N'{Escape(customerUserName)}'");
            }

            object activeSession = _db.ExecuteScalar($@"SELECT TOP 1 id FROM dbo.BuffetSession 
                                                        WHERE billId = {billId} AND status = N'Active' ORDER BY id DESC");
            if (activeSession != null && activeSession != DBNull.Value)
            {
                throw new Exception("Bill này đã có phiên buffet đang hoạt động và không thể đổi hạn mức trước khi thanh toán.");
            }

            decimal pricePerHead = packageCode == "599K" ? 599000 : 299000;
            object sessionId = _db.ExecuteScalar($@"INSERT INTO dbo.BuffetSession(billId, customerUserName, packageCode, pricePerHead, guestCount, isLocked, status, createdBy)
                                                   VALUES ({billId}, N'{Escape(customerUserName)}', N'{Escape(packageCode)}', {pricePerHead}, {guestCount}, 1, N'Active', N'{Escape(createdBy)}');
                                                   SELECT SCOPE_IDENTITY();");

            return Convert.ToInt32(sessionId);
        }

        public DataRow LoginCustomer(string customerUserName, string customerPassword)
        {
            DataTable dt = _db.ExecuteQuery($@"
SELECT TOP 1 s.id AS sessionId, s.billId, s.customerUserName, s.packageCode, s.guestCount, s.pricePerHead, s.status
FROM dbo.BuffetSession s
JOIN dbo.BuffetCustomerAccount c ON c.userName = s.customerUserName
WHERE c.userName = N'{Escape(customerUserName)}'
  AND c.[password] = N'{Escape(customerPassword)}'
  AND s.status = N'Active'
ORDER BY s.id DESC");

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public DataTable GetBuffetMenuBySession(int sessionId)
        {
            return _db.ExecuteQuery($@"
SELECT f.id AS foodId, f.name, f.price, CAST(0 AS INT) AS quantity
FROM dbo.BuffetSession s
JOIN dbo.BuffetMenuScope ms ON ms.packageCode = s.packageCode
JOIN dbo.Food f ON f.id = ms.idFood
WHERE s.id = {sessionId}
  AND f.status = N'Available'
GROUP BY f.id, f.name, f.price
ORDER BY f.id");
        }

        public int SubmitBatch(int sessionId, Dictionary<int, int> foodQty)
        {
            if (foodQty == null || foodQty.Count == 0)
                throw new Exception("Vui lòng chọn ít nhất 1 món để submit.");

            object maxNo = _db.ExecuteScalar($"SELECT ISNULL(MAX(batchNo),0) FROM dbo.BuffetOrderBatch WHERE sessionId = {sessionId}");
            int nextNo = Convert.ToInt32(maxNo) + 1;

            object batchIdObj = _db.ExecuteScalar($@"INSERT INTO dbo.BuffetOrderBatch(sessionId, batchNo, status)
                                                     VALUES ({sessionId}, {nextNo}, N'Pending');
                                                     SELECT SCOPE_IDENTITY();");

            int batchId = Convert.ToInt32(batchIdObj);
            foreach (KeyValuePair<int, int> item in foodQty)
            {
                _db.ExecuteNonQuery($@"INSERT INTO dbo.BuffetOrderItem(batchId, idFood, quantity, status)
                                       VALUES ({batchId}, {item.Key}, {item.Value}, N'Pending')");
            }

            return batchId;
        }

        public DataTable GetBatchHistory(int sessionId)
        {
            return _db.ExecuteQuery($@"
SELECT b.id AS batchId, b.batchNo, b.submittedAt, b.status AS batchStatus,
       f.name AS foodName, i.quantity, i.status AS itemStatus
FROM dbo.BuffetOrderBatch b
JOIN dbo.BuffetOrderItem i ON i.batchId = b.id
JOIN dbo.Food f ON f.id = i.idFood
WHERE b.sessionId = {sessionId}
ORDER BY b.batchNo DESC, i.id ASC");
        }

        public void MarkBatchServed(int batchId)
        {
            _db.ExecuteNonQuery($@"UPDATE dbo.BuffetOrderBatch SET status = N'Served' WHERE id = {batchId};
                                  UPDATE dbo.BuffetOrderItem SET status = N'Served' WHERE batchId = {batchId};");
        }

        public decimal GetEstimatedBuffetTotal(int sessionId)
        {
            object totalObj = _db.ExecuteScalar($"SELECT ISNULL(pricePerHead * guestCount,0) FROM dbo.BuffetSession WHERE id = {sessionId}");
            return Convert.ToDecimal(totalObj);
        }

        private string Escape(string value)
        {
            return (value ?? string.Empty).Replace("'", "''");
        }
    }
}
