USE master
GO

IF DB_ID('QuanLyNhaHang') IS NOT NULL -- Kiểm tra nếu cơ sở dữ liệu đã tồn tại
BEGIN
    ALTER DATABASE QuanLyNhaHang SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyNhaHang;
END
GO

CREATE DATABASE QuanLyNhaHang
GO

USE QuanLyNhaHang
GO

CREATE TABLE tableFood
(
	id INT IDENTITY PRIMARY KEY NOT NULL,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
	status NVARCHAR(20) NOT NULL DEFAULT N'Available', --Tình trạng bàn (Available, Unavailable)
)
GO



-- Bảng role dùng để phân loại người dùng theo chức vụ 
-- ( vd Rolename = Admin, Manager, Chef, Stafff )
CREATE TABLE Role (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL
);

-- Bảng permission dùng để lưu các các hành động cụ thể ( tính năng ) mà hệ thống cho phép 
--( vd PermissionName = ViewMenu, EditMenu, ViewBill, EditBill, ManageAccount,... )
CREATE TABLE Permission (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PermissionName NVARCHAR(100) NOT NULL
);

-- Bảng liên kết giữa Role và Permission (Many-to-Many) để xác định quyền hạn của từng Role trong hệ thống 
--( vd Role Admin có tất cả các Permission, Role Manager có một số Permission liên quan đến quản lý menu và bill, 
-- Role Staff chỉ có Permission liên quan đến xem menu và tạo bill,...)
CREATE TABLE RolePermission (
    RoleId INT,
    PermissionId INT,
    PRIMARY KEY (RoleId, PermissionId),
    FOREIGN KEY (RoleId) REFERENCES Role(Id),
    FOREIGN KEY (PermissionId) REFERENCES Permission(Id)
);

-- Bảng dùng để lưu thông tin tài khoản đăng nhập hệ thống , có liên kết với bảng Role để phân quyền
-- ( vd username = admin, password = 123, displayName = Quản lý, RoleId = 1 (Admin) )
CREATE TABLE Account
(
	username NVARCHAR(50) PRIMARY KEY NOT NULL,
	password NVARCHAR(100) NOT NULL,
	displayName NVARCHAR(100) NOT NULL,
	RoleId INT NOT NULL,
	fullname nvarchar(50) NOT NULL,
	birthday date NOT NULL,
	email nvarchar(50) NOT NULL,
	phone nvarchar(20) NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Role(Id)
)
GO

CREATE TABLE FoodCategory
(
	id INT IDENTITY PRIMARY KEY NOT NULL,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
)
GO

CREATE TABLE Food
(
	id INT IDENTITY PRIMARY KEY NOT NULL,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0,
	status NVARCHAR(20) DEFAULT N'Available',--Tình trạng món ăn (Available, Out of stock)
	image NVARCHAR(255), --hình ảnh món ăn
	FOREIGN KEY (idCategory) REFERENCES FoodCategory(id)
)
GO

CREATE TABLE Bill
(
	id INT IDENTITY PRIMARY KEY NOT NULL,
	idTable INT NOT NULL,
	dateCheckIn DATETIME NOT NULL DEFAULT GETDATE(),
	dateCheckOut DATETIME,
	status INT NOT NULL, --0: Unpaid, 1: Paid
	FOREIGN KEY (idTable) REFERENCES tableFood(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY NOT NULL,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	quantity INT NOT NULL DEFAULT 0,
	FOREIGN KEY (idBill) REFERENCES Bill(id),
	FOREIGN KEY (idFood) REFERENCES Food(id)
)
GO

ALTER TABLE Bill
ADD 
    customerName NVARCHAR(100),   -- NAME
    caseName NVARCHAR(50),        -- CASE
    payMethod NVARCHAR(50),       -- PAY METHOD
    note NVARCHAR(255)            -- NOTE

GO
CREATE PROCEDURE USP_GetOrderList
AS
BEGIN
    SELECT 
        b.id AS [NO.],
        b.id AS [ID ORDER],
        CONVERT(DATE, b.dateCheckIn) AS [DATE],
        CONVERT(TIME, b.dateCheckIn) AS [TIME],
        b.caseName AS [CASE],
        tf.name AS [TABLE],
        b.customerName AS [NAME],
        SUM(f.price * bi.quantity) AS [TOTAL PRICE],
        b.payMethod AS [PAY METHOD],
        b.note AS [NOTE]
    FROM Bill b
    JOIN tableFood tf ON b.idTable = tf.id
    LEFT JOIN BillInfo bi ON b.id = bi.idBill
    LEFT JOIN Food f ON bi.idFood = f.id
    GROUP BY 
        b.id, b.dateCheckIn, b.caseName, tf.name,
        b.customerName, b.payMethod, b.note
    ORDER BY b.id DESC
END