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
INSERT INTO Food(name, idCategory, price, status, image)
VALUES 
(N'Trà đào', 1, 30000, 'Available', 'Images/Food/tra_dao.jpg'),
(N'Cà phê sữa', 1, 25000, 'Available', 'Images/Food/ca_phe.jpg'),
(N'Nước cam', 1, 20000, 'Available', 'Images/Food/nuoc_cam.jpg'),
(N'Cơm gà', 2, 50000, 'Available', 'Images/Food/com_ga.jpg'),
(N'Bún bò', 2, 45000, 'Available', 'Images/Food/bun_bo.jpg'),
(N'Bánh flan', 3, 15000, 'Available', 'Images/Food/flan.jpg'),
(N'Cơm chiên', 1, 30000, 'Available', 'Images/Food/com_chien.jpg'),
(N'Nước ngọt', 1, 15000, 'Available', 'Images/Food/nuoc_ngot.jpg')

-- ========================
-- Role 
-- ========================
INSERT INTO Role (RoleName)
VALUES 
('Admin'),
('Manager'),
('Chef'),
('Staff'),
('Customer');			  

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
('MANAGE_PERMISSION'),	  -- phân quyền
('RATING_SERVICE');       -- đánh giá 

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
	-- Role Khách hàng chỉ có Permission liên quan đến đặt hàng và đánh giá dịch vụ
INSERT INTO RolePermission
SELECT r.Id, p.Id
FROM Role r, Permission p
WHERE r.RoleName = 'Customer'
AND p.PermissionName IN ('CREATE_ORDER','RATING_SERVICE');

-- ========================
-- Account (Dữ liệu tài khoản)
-- ========================

INSERT INTO Account (userId, Password, RoleId, fullname, birthday, email, phone, address, ward, district, city, salary, isDeleted, imagePath)
VALUES ('ADM01', '123', 1, N'Nguyen Van A', '2005-04-19', 'avn1@gmail.com', '0901234567', N'141 DPB', N'Phường Bến Nghé', N'Quận 1', N'TP. Hồ Chí Minh', 25000, 0, 'ADM01.jpg');

INSERT INTO Account (userId, Password, RoleId, fullname, birthday, email, phone, address, ward, district, city, salary, isDeleted, imagePath)
VALUES ('MNG01', '123', 2, N'Le Hoang B', '2005-12-01', 'blh1@gmail.com', '0801674567', N'141 DPB', N'Phường Phúc Xá', N'Quận Ba Đình', N'Hà Nội', 25000, 0, 'MNG01.jpg');

INSERT INTO Account (userId, Password, RoleId, fullname, birthday, email, phone, address, ward, district, city, salary, isDeleted, imagePath)
VALUES ('CHF01', '123', 3, N'Tran Thi C', '2005-03-25', 'ctt1@gmail.com', '0909804567', N'141 DPB', N'Phường Thạch Thang', N'Quận Hải Châu', N'Đà Nẵng', 25000, 0, 'CHF01.jpg');

INSERT INTO Account (userId, Password, RoleId, fullname, birthday, email, phone, address, ward, district, city, salary, isDeleted, imagePath)
VALUES ('STF01', '123', 4, N'Pham Thanh D', '2005-04-07', 'dpt1@gmail.com', '0907654567', N'141 DPB', N'Phường Bến Nghé', N'Quận 1', N'TP. Hồ Chí Minh', 25000, 0, 'STF01.jpg');

INSERT INTO Account (userId, Password, RoleId, fullname, birthday, email, phone, address, ward, district, city, salary, isDeleted, imagePath)
VALUES ('CUSTOMER', '123',5, N'Hoang Anh Tuan', '2003-05-15', 'tna@gmail.com', '0932154347', N'141 DPB', N'Phường Bến Nghé', N'Quận 1', N'TP. Hồ Chí Minh', 0, 0, 'CUSTOMER.jpg');

-- 1. INSERT Tỉnh/Thành phố
INSERT INTO city (cityId, cityName) VALUES 
('79', N'TP. Hồ Chí Minh'), 
('01', N'Hà Nội'), 
('48', N'Đà Nẵng');

-- 2. INSERT Quận/Huyện
-- HCM
INSERT INTO district (districtId, districtName, cityId) VALUES 
('760', N'Quận 1', '79'), 
('764', N'Quận Gò Vấp', '79'), 
('761', N'Quận 12', '79');
-- Hà Nội
INSERT INTO district (districtId, districtName, cityId) VALUES 
('001', N'Quận Ba Đình', '01'), 
('002', N'Quận Hoàn Kiếm', '01');
-- Đà Nẵng
INSERT INTO district (districtId, districtName, cityId) VALUES 
('490', N'Quận Hải Châu', '48'), 
('492', N'Quận Thanh Khê', '48');

-- 3. INSERT Phường/Xã
-- Quận 1 - HCM
INSERT INTO ward (wardId, wardName, districtId) VALUES 
('26734', N'Phường Bến Nghé', '760'), 
('26737', N'Phường Bến Thành', '760');
-- Quận Gò Vấp - HCM
INSERT INTO ward (wardId, wardName, districtId) VALUES 
('26896', N'Phường 1', '764'), 
('26902', N'Phường 10', '764');
-- Quận 12 - HCM
INSERT INTO ward (wardId, wardName, districtId) VALUES 
('26800', N'Phường Tân Thới Nhất', '761'), 
('26801', N'Phường Hiệp Thành', '761');
-- Quận Ba Đình - HN
INSERT INTO ward (wardId, wardName, districtId) VALUES 
('00001', N'Phường Phúc Xá', '001'), 
('00004', N'Phường Trúc Bạch', '001');
-- Quận Hoàn Kiếm - HN
INSERT INTO ward (wardId, wardName, districtId) VALUES 
('00015', N'Phường Hàng Bạc', '002'), 
('00021', N'Phường Tràng Tiền', '002');
-- Quận Hải Châu - DN
INSERT INTO ward (wardId, wardName, districtId) VALUES 
('32101', N'Phường Thạch Thang', '490'), 
('32102', N'Phường Hải Châu I', '490');
-- Quận Thanh Khê - DN
INSERT INTO ward (wardId, wardName, districtId) VALUES 
('32103', N'Phường Chính Gián', '492'), 
('32104', N'Phường Thạc Gián', '492');

-- ========================
-- TẠO BILL
-- ========================
DECLARE @BillId INT;

INSERT INTO Bill (idTable, status, customerName, caseName, payMethod)
VALUES (1, 0, N'Khách lẻ', N'Tại chỗ', N'Tiền mặt');

SET @BillId = SCOPE_IDENTITY();

-- ========================
-- BILL INFO
-- ========================
INSERT INTO BillInfo (idBill, idFood, quantity)
VALUES
(@BillId, 1, 2),
(@BillId, 2, 1);


