-- Buffet feature schema
-- Run this script once after CreateDatabase.sql

IF OBJECT_ID('dbo.BuffetCustomerAccount', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.BuffetCustomerAccount
    (
        userName NVARCHAR(50) PRIMARY KEY,
        [password] NVARCHAR(100) NOT NULL,
        fullName NVARCHAR(100) NULL,
        createdAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

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
    );
END
GO

IF OBJECT_ID('dbo.BuffetMenuScope', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.BuffetMenuScope
    (
        id INT IDENTITY PRIMARY KEY,
        idFood INT NOT NULL,
        packageCode NVARCHAR(10) NOT NULL,
        CONSTRAINT FK_BuffetMenuScope_Food FOREIGN KEY (idFood) REFERENCES Food(id)
    );
END
GO

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
    );
END
GO

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
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.BuffetMenuScope)
BEGIN
    -- default scope by price: <=299k in both 299K and 599K, <=599k in 599K
    INSERT INTO dbo.BuffetMenuScope(idFood, packageCode)
    SELECT id, N'299K' FROM dbo.Food WHERE price <= 299000;

    INSERT INTO dbo.BuffetMenuScope(idFood, packageCode)
    SELECT id, N'599K' FROM dbo.Food WHERE price <= 599000;
END
GO
