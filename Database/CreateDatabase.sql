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

CREATE TABLE Account
(
	username NVARCHAR(50) PRIMARY KEY NOT NULL,
	password NVARCHAR(100) NOT NULL,
	displayName NVARCHAR(100) NOT NULL,
	type INT NOT NULL DEFAULT 0 --1: Admin, 0: Staff
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
