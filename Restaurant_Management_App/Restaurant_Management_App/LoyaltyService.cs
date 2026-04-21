using System;
using System.Data;

namespace Restaurant_Management_App
{
    public class LoyaltyService
    {
        public LoyaltyService()
        {
            EnsureTables();
        }

        private void EnsureTables()
        {
            string query = @"
IF OBJECT_ID('dbo.CustomerLoyalty', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.CustomerLoyalty
    (
        id INT IDENTITY PRIMARY KEY,
        customerName NVARCHAR(100) NOT NULL,
        phone NVARCHAR(20) NULL,
        points INT NOT NULL DEFAULT 0,
        totalSpent DECIMAL(18,2) NOT NULL DEFAULT 0,
        createdAt DATETIME NOT NULL DEFAULT GETDATE(),
        updatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END

IF OBJECT_ID('dbo.LoyaltyPointHistory', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.LoyaltyPointHistory
    (
        id INT IDENTITY PRIMARY KEY,
        customerId INT NOT NULL,
        billId INT NOT NULL,
        amount DECIMAL(18,2) NOT NULL,
        pointsEarned INT NOT NULL,
        createdAt DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (customerId) REFERENCES dbo.CustomerLoyalty(id),
        FOREIGN KEY (billId) REFERENCES dbo.Bill(id)
    );
END

IF OBJECT_ID('dbo.PromotionProgram', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PromotionProgram
    (
        id INT IDENTITY PRIMARY KEY,
        promoName NVARCHAR(120) NOT NULL,
        description NVARCHAR(300) NULL,
        startDate DATETIME NOT NULL,
        endDate DATETIME NOT NULL,
        minPoints INT NOT NULL DEFAULT 0,
        discountPercent FLOAT NOT NULL DEFAULT 0,
        isActive BIT NOT NULL DEFAULT 1,
        createdAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END";

            Database.Instance.ExecuteNonQuery(query);
        }

        public void AwardPointsByBill(int billId, string customerName, string phone = null)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                return;
            }

            object amountObj = Database.Instance.ExecuteScalar(@"SELECT ISNULL(SUM(f.price * bi.quantity),0)
                                                                FROM BillInfo bi
                                                                JOIN Food f ON bi.idFood = f.id
                                                                WHERE bi.idBill = " + billId);
            decimal amount = Convert.ToDecimal(amountObj);
            int pointsEarned = (int)(amount / 10000m); // 10.000đ = 1 điểm

            if (pointsEarned <= 0)
            {
                return;
            }

            string escapedName = Escape(customerName);
            string escapedPhone = Escape(phone ?? string.Empty);

            int customerId;
            object customerIdObj = Database.Instance.ExecuteScalar($@"SELECT TOP 1 id FROM CustomerLoyalty
                                                                      WHERE customerName = N'{escapedName}'
                                                                      ORDER BY id DESC");
            if (customerIdObj == null || customerIdObj == DBNull.Value)
            {
                object insertedId = Database.Instance.ExecuteScalar($@"INSERT INTO CustomerLoyalty(customerName, phone, points, totalSpent, updatedAt)
                                                                       VALUES (N'{escapedName}', N'{escapedPhone}', 0, 0, GETDATE());
                                                                       SELECT SCOPE_IDENTITY();");
                customerId = Convert.ToInt32(insertedId);
            }
            else
            {
                customerId = Convert.ToInt32(customerIdObj);
            }

            Database.Instance.ExecuteNonQuery($@"UPDATE CustomerLoyalty
                                                 SET points = points + {pointsEarned},
                                                     totalSpent = totalSpent + {amount.ToString(System.Globalization.CultureInfo.InvariantCulture)},
                                                     updatedAt = GETDATE(),
                                                     phone = CASE WHEN LEN(N'{escapedPhone}') = 0 THEN phone ELSE N'{escapedPhone}' END
                                                 WHERE id = {customerId}");

            Database.Instance.ExecuteNonQuery($@"INSERT INTO LoyaltyPointHistory(customerId, billId, amount, pointsEarned)
                                                 VALUES ({customerId}, {billId}, {amount.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {pointsEarned})");
        }

        public DataTable GetCustomerPoints()
        {
            return Database.Instance.ExecuteQuery(@"SELECT id, customerName, phone, points, totalSpent, updatedAt
                                                    FROM CustomerLoyalty
                                                    ORDER BY points DESC, updatedAt DESC");
        }

        public DataTable GetPromotions()
        {
            return Database.Instance.ExecuteQuery(@"SELECT id, promoName, description, startDate, endDate, minPoints, discountPercent, isActive
                                                    FROM PromotionProgram
                                                    ORDER BY startDate DESC");
        }

        public void CreatePromotion(string name, string description, DateTime startDate, DateTime endDate, int minPoints, double discountPercent)
        {
            string escapedName = Escape(name);
            string escapedDesc = Escape(description ?? string.Empty);

            Database.Instance.ExecuteNonQuery($@"INSERT INTO PromotionProgram(promoName, description, startDate, endDate, minPoints, discountPercent, isActive)
                                                 VALUES (N'{escapedName}', N'{escapedDesc}', '{startDate:yyyy-MM-dd HH:mm:ss}', '{endDate:yyyy-MM-dd HH:mm:ss}', {minPoints}, {discountPercent.ToString(System.Globalization.CultureInfo.InvariantCulture)}, 1)");
        }

        private static string Escape(string input)
        {
            return (input ?? string.Empty).Replace("'", "''");
        }
    }
}
