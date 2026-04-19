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
    userId NVARCHAR(20) PRIMARY KEY, 	
	password NVARCHAR(100) NOT NULL,
	RoleId INT NOT NULL,
	fullname nvarchar(50) NOT NULL,
	birthday date NOT NULL,
	email nvarchar(50) NOT NULL,
	phone nvarchar(20) NOT NULL,
    address NVARCHAR(100) NOT NULL, -- Địa chỉ của người dùng
    ward NVARCHAR(50) NOT NULL,-- Phường/xã
    district NVARCHAR(50) NOT NULL,-- Quận/huyện
    city NVARCHAR(50) NOT NULL,-- Thành phố
	salary decimal(18,2) default 0  NOT NULL, -- Lương của người dùng 
    FOREIGN KEY (RoleId) REFERENCES Role(Id)
);


-- 1. Bảng Tỉnh / Thành Phố
CREATE TABLE city (
    cityId NVARCHAR(10) PRIMARY KEY,
    cityName NVARCHAR(100) NOT NULL
);

-- 2. Bảng Quận / Huyện
CREATE TABLE district (
	cityId NVARCHAR(10) NOT NULL,
    districtID NVARCHAR(10) PRIMARY KEY NOT NULL,
    districtName NVARCHAR(50) NOT NULL,
	FOREIGN KEY (cityId) REFERENCES city(cityId)
);

-- 3. Bảng Phường / Xã
CREATE TABLE ward (
	districtID NVARCHAR(10) NOT NULL,
    wardID NVARCHAR(10) PRIMARY KEY,
    wardName NVARCHAR(100) NOT NULL,
    FOREIGN KEY (districtID) REFERENCES district(districtID)
);

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
	idTable INT  NOT NULL,
	id int PRIMARY KEY NOT NULL,
	dateCheckIn DATETIME NOT NULL DEFAULT GETDATE(),
	dateCheckOut DATETIME,
	customerName NVARCHAR(100),   -- NAME
    caseName NVARCHAR(50),        -- CASE
    payMethod NVARCHAR(50),       -- PAY METHOD
    note NVARCHAR(255),            -- NOTE
	status INT NOT NULL DEFAULT 0, --0: Unpaid, 1: Paid
	FOREIGN KEY (idTable) REFERENCES tableFood(id)
)
GO

CREATE TABLE BillInfo
(
	id INT IDENTITY PRIMARY KEY NOT NULL,
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	quantity INT NOT NULL DEFAULT 1,
	FOREIGN KEY (idBill) REFERENCES Bill(id),
	FOREIGN KEY (idFood) REFERENCES Food(id)
)
GO

CREATE TABLE PasswordHistory (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    userId NVARCHAR(20) NOT NULL,
    oldPassword VARCHAR(100),
    newPassword VARCHAR(100),
    changeDate DATETIME DEFAULT GETDATE(),
    -- Lưu lại ai là người thực hiện (Chính chủ hay Admin đổi hộ)
    changedBy VARCHAR(50), 
    FOREIGN KEY (userId) REFERENCES Account(userId)
);







