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
-- Role 
-- ========================
INSERT INTO Role (RoleName)
VALUES 
('Admin'),
('Manager'),
('Chef'),
('Staff');

-- ========================
-- Permission
-- ========================
INSERT INTO Permission (PermissionName)
VALUES
('MANAGE_PRODUCTS'),      -- quản lý menu
('CREATE_ORDER'),         -- tạo đơn
('MANAGE_ORDER'),         -- quản lý đơn
('CUSTOMER_CARE'),        -- chăm sóc khách hàng
('MANAGE_REVENUE'),       -- quản lý doanh thu
('MANAGE_STAFF'),         -- quản lý nhân viên
('MANAGE_ACCOUNT'),       -- tạo tài khoản
('MANAGE_PERMISSION');    -- phân quyền

-- ========================
-- Gán Permission cho Role
-- ========================
		-- Role Admin có tất cả các Permission
INSERT INTO RolePermission
SELECT r.Id, p.Id
FROM Role r, Permission p
WHERE r.RoleName = 'Admin';
		-- Role Quản lý có một số Permission liên quan đến quản lý menu và bill
INSERT INTO RolePermission
SELECT r.Id, p.Id
FROM Role r, Permission p
WHERE r.RoleName = 'Manager'
AND p.PermissionName IN 
('MANAGE_PRODUCTS','CREATE_ORDER','MANAGE_ORDER','CUSTOMER_CARE','MANAGE_REVENUE','MANAGE_STAFF');
		-- Role Nhân viên có	Permission liên quan đến xem menu và tạo bill, chăm sóc khách hàng 
INSERT INTO RolePermission
SELECT r.Id, p.Id
FROM Role r, Permission p
WHERE r.RoleName = 'Staff'
AND p.PermissionName IN ('CREATE_ORDER','MANAGE_ORDER','CUSTOMER_CARE');
	-- Role Đầu bếp chỉ có Permission liên quan đến quản lý menu
INSERT INTO RolePermission
SELECT r.Id, p.Id
FROM Role r, Permission p
WHERE r.RoleName = 'Chef'
AND p.PermissionName = 'MANAGE_PRODUCTS';	

-- ========================
-- Account (Dữ liệu tài khoản)
-- ========================
	-- 
INSERT INTO Account (userId, Password, DisplayName, RoleId, fullname, birthday, email, phone, address, ward, district, city)
VALUES ('ADM01', '123', N'Admin', 1, 'Nguyen Van A', '2005/04/19', 'avn1@gmail.com', '0901234567', '141 DPB', 'Phuong 2', 'Quan Binh Thanh', ' TPHCM ');
	-- 
INSERT INTO Account (userId, Password, DisplayName, RoleId, fullname, birthday, email, phone, address, ward, district, city)
VALUES ('MNG01', '123', N'Quản lý', 2, 'Le Hoang B', '2005/12/01', 'blh1@gmail.com', '0801674567', '141 DPB', 'Phuong 2', 'Quan Binh Thanh', ' TPHCM ');
	--
INSERT INTO Account (userId, Password, DisplayName, RoleId, fullname, birthday, email, phone, address, ward, district, city)
VALUES ('CHF01', '123', N'Bếp trưởng', 3, 'Tran Thi C', '2005/03/25', 'ctt1@gmail.com', '0909804567', '141 DPB', 'Phuong 2', 'Quan Binh Thanh', ' TPHCM ');
	--
INSERT INTO Account (userId,  Password, DisplayName, RoleId, fullname, birthday, email, phone, address, ward, district, city)
VALUES ('STF01', '123', N'Nhân viên', 4, 'Pham Thanh D', '2005/04/07', 'dpt1@gmail.com', '0907654567', '141 DPB', 'Phuong 2', 'Quan Binh Thanh', ' TPHCM ');




-- Thêm món
INSERT INTO Food(name, idCategory, price)
VALUES (N'Cơm chiên', 1, 30000),
       (N'Nước ngọt', 1, 15000)



DECLARE @BillId INT
SET @BillId = SCOPE_IDENTITY()

INSERT INTO BillInfo (idBill, idFood, quantity)
VALUES
(@BillId, 1, 2),  -- 2 cơm chiên
(@BillId, 2, 1)   -- 1 nước ngọt








