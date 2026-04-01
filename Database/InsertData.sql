USE QuanLyNhaHang
GO

-- ========================
-- TableFood
-- ========================
INSERT INTO tableFood(name, status)
VALUES 
(N'Bàn 1', N'Available'),
(N'Bàn 2', N'Available'),
(N'Bàn 3', N'Available'),
(N'Bàn 4', N'Unavailable')
-- ========================
-- FoodCategory
-- ========================
INSERT INTO FoodCategory(name)
VALUES 
(N'Đồ uống'),
(N'Đồ ăn'),
(N'Tráng miệng')
-- ========================
-- Food
-- ========================
INSERT INTO Food(name, idCategory, price)
VALUES 
(N'Trà đào', 1, 30000),
(N'Cà phê sữa', 1, 25000),
(N'Nước cam', 1, 20000),
(N'Cơm gà', 2, 50000),
(N'Bún bò', 2, 45000),
(N'Bánh flan', 3, 15000)
-- ========================
-- Account (PHÂN QUYỀN)
-- ========================
INSERT INTO Account(username, password, displayName, type)
VALUES 
(N'admin', N'123', N'Quản lý', 1),   -- Admin
(N'staff1', N'123', N'Nhân viên 1', 0),
(N'staff2', N'123', N'Nhân viên 2', 0)
-- ========================
-- Bill
-- ========================
INSERT INTO Bill(idTable, dateCheckIn, status)
VALUES 
(1, GETDATE(), 0),
(2, GETDATE(), 0),
(3, GETDATE(), 1)
-- ========================
-- BillInfo
-- ========================
INSERT INTO BillInfo(idBill, idFood, quantity)
VALUES 
(1, 1, 2),
(1, 2, 1),
(2, 4, 1),
(2, 5, 2),
(3, 6, 1)