using System;
using System.Data;

namespace Restaurant_Management_App
{
    public class LoyaltyService
    {
        public sealed class PromotionMatch
        {
            public int PromotionId { get; set; }
            public string PromotionName { get; set; }
            public int MinPoints { get; set; }
            public double DiscountPercent { get; set; }
        }

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
        pointsUsed INT NOT NULL DEFAULT 0,
        promotionId INT NULL,
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
END

IF COL_LENGTH('dbo.Bill', 'discountPercent') IS NULL
    ALTER TABLE dbo.Bill ADD discountPercent FLOAT NULL;

IF COL_LENGTH('dbo.Bill', 'discountAmount') IS NULL
    ALTER TABLE dbo.Bill ADD discountAmount DECIMAL(18,2) NULL;

IF COL_LENGTH('dbo.Bill', 'finalAmount') IS NULL
    ALTER TABLE dbo.Bill ADD finalAmount DECIMAL(18,2) NULL;

IF COL_LENGTH('dbo.Bill', 'idPromotion') IS NULL
    ALTER TABLE dbo.Bill ADD idPromotion INT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Bill_PromotionProgram')
BEGIN
    ALTER TABLE dbo.Bill WITH NOCHECK
    ADD CONSTRAINT FK_Bill_PromotionProgram FOREIGN KEY (idPromotion) REFERENCES dbo.PromotionProgram(id);
END

IF COL_LENGTH('dbo.LoyaltyPointHistory', 'pointsUsed') IS NULL
    ALTER TABLE dbo.LoyaltyPointHistory ADD pointsUsed INT NOT NULL DEFAULT 0;

IF COL_LENGTH('dbo.LoyaltyPointHistory', 'promotionId') IS NULL
    ALTER TABLE dbo.LoyaltyPointHistory ADD promotionId INT NULL;";

            Database.Instance.ExecuteNonQuery(query);
        }

        public decimal GetBillTotal(int billId)
        {
            object amountObj = Database.Instance.ExecuteScalar(@"SELECT ISNULL(SUM(f.price * bi.quantity),0)
                                                                FROM BillInfo bi
                                                                JOIN Food f ON bi.idFood = f.id
                                                                WHERE bi.idBill = " + billId);
            return Convert.ToDecimal(amountObj);
        }

        public PromotionMatch GetBestPromotionForCustomer(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                return null;
            }

            int points = GetCustomerPoints(customerName);
            string escapedName = Escape(customerName);

            DataTable dt = Database.Instance.ExecuteQuery($@"SELECT TOP 1 id, promoName, minPoints, discountPercent
                                                             FROM PromotionProgram
                                                             WHERE isActive = 1
                                                               AND GETDATE() BETWEEN startDate AND endDate
                                                               AND minPoints <= {points}
                                                             ORDER BY discountPercent DESC, minPoints DESC");
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];
            return new PromotionMatch
            {
                PromotionId = Convert.ToInt32(row["id"]),
                PromotionName = row["promoName"].ToString(),
                MinPoints = Convert.ToInt32(row["minPoints"]),
                DiscountPercent = Convert.ToDouble(row["discountPercent"])
            };
        }

        public DataTable GetEligiblePromotionsForCustomer(string customerName)
        {
            int points = GetCustomerPoints(customerName);
            return Database.Instance.ExecuteQuery($@"SELECT id, promoName, minPoints, discountPercent
                                                     FROM PromotionProgram
                                                     WHERE isActive = 1
                                                       AND GETDATE() BETWEEN startDate AND endDate
                                                       AND minPoints <= {points}
                                                     ORDER BY discountPercent DESC, minPoints DESC");
        }

        public void ApplyPaymentAndPoints(int billId, string customerName, decimal payableAmount, int pointsUsed, int? promotionId)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                return;
            }

            int pointsEarned = (int)(payableAmount / 10000m);
            string escapedName = Escape(customerName);

            int customerId;
            object customerIdObj = Database.Instance.ExecuteScalar($@"SELECT TOP 1 id FROM CustomerLoyalty
                                                                      WHERE customerName = N'{escapedName}'
                                                                      ORDER BY id DESC");
            if (customerIdObj == null || customerIdObj == DBNull.Value)
            {
                object insertedId = Database.Instance.ExecuteScalar($@"INSERT INTO CustomerLoyalty(customerName, phone, points, totalSpent, updatedAt)
                                                                       VALUES (N'{escapedName}', N'', 0, 0, GETDATE());
                                                                       SELECT SCOPE_IDENTITY();");
                customerId = Convert.ToInt32(insertedId);
            }
            else
            {
                customerId = Convert.ToInt32(customerIdObj);
            }

            int netPointChange = pointsEarned - pointsUsed;
            Database.Instance.ExecuteNonQuery($@"UPDATE CustomerLoyalty
                                                 SET points = CASE WHEN points + ({netPointChange}) < 0 THEN 0 ELSE points + ({netPointChange}) END,
                                                     totalSpent = totalSpent + {payableAmount.ToString(System.Globalization.CultureInfo.InvariantCulture)},
                                                     updatedAt = GETDATE()
                                                 WHERE id = {customerId}");

            string promoValue = promotionId.HasValue ? promotionId.Value.ToString() : "NULL";
            Database.Instance.ExecuteNonQuery($@"INSERT INTO LoyaltyPointHistory(customerId, billId, amount, pointsEarned, pointsUsed, promotionId)
                                                 VALUES ({customerId}, {billId}, {payableAmount.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {pointsEarned}, {pointsUsed}, {promoValue})");
        }

        public int GetCustomerPoints(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                return 0;
            }

            string escapedName = Escape(customerName);
            object pointsObj = Database.Instance.ExecuteScalar($@"SELECT TOP 1 points
                                                                  FROM CustomerLoyalty
                                                                  WHERE customerName = N'{escapedName}'
                                                                  ORDER BY id DESC");
            if (pointsObj == null || pointsObj == DBNull.Value)
            {
                return 0;
            }

            return Convert.ToInt32(pointsObj);
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
