USE QuanLyNhaHang
GO
-- =========================
-- TABLE: tableFood
-- =========================
INSERT INTO tableFood(name)
VALUES 
(N'Bàn 1'),
(N'Bàn 2'),
(N'Bàn 3'),
(N'Bàn 4'),
(N'Bàn 5')
-- =========================
-- TABLE: FoodCategory
-- =========================
INSERT INTO FoodCategory(name)
VALUES
(N'Nước uống'),
(N'Món chính'),
(N'Món ăn vặt')
-- =========================
-- TABLE: Food
-- =========================
INSERT INTO Food(name, idCategory, price)
VALUES
(N'Coca Cola', 1, 15000),
(N'Pepsi', 1, 15000),
(N'Trà sữa', 1, 25000),

(N'Cơm gà', 2, 35000),
(N'Phở bò', 2, 40000),

(N'Khoai tây chiên', 3, 20000),
(N'Gà rán', 3, 30000)
-- =========================
-- TABLE: Account
-- =========================
INSERT INTO Account(username, password, displayName, type)
VALUES
('admin', '123', N'Quản trị', 1),
('staff', '123', N'Nhân viên', 0)
