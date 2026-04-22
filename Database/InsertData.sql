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
(N'Trà đào', 1, 30000, 'Available', 'Images/Food/tra_dao.jpg'),
(N'Cà phê sữa', 1, 25000, 'Available', 'Images/Food/ca_phe.jpg'),
(N'Nước cam', 1, 20000, 'Available', 'Images/Food/nuoc_cam.jpg'),
(N'Cơm gà', 2, 50000, 'Available', 'Images/Food/com_ga.jpg'),
(N'Bún bò', 2, 45000, 'Available', 'Images/Food/bun_bo.jpg'),
(N'Bánh flan', 3, 15000, 'Available', 'Images/Food/flan.jpg')

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
INSERT INTO Account (userId, Password, RoleId, fullname, birthday, email, phone, address, ward, district, city, salary)
VALUES ('ADM01', '123', 1, 'Nguyen Van A', '2005/04/19', 'avn1@gmail.com', '0901234567', '141 DPB', 'Phuong Ben Nghe', 'Quan 1', 'TP. Ho Chi Minh','25000');

INSERT INTO Account (userId, Password, RoleId, fullname, birthday, email, phone, address, ward, district, city, salary)
VALUES ('MNG01', '123', 2, 'Le Hoang B', '2005/12/01', 'blh1@gmail.com', '0801674567', '141 DPB', 'Phuong Phuc Xa', 'Quan Ba Dinh', 'Ha Noi','25000');

INSERT INTO Account (userId, Password, RoleId, fullname, birthday, email, phone, address, ward, district, city, salary)
VALUES ('CHF01', '123', 3, 'Tran Thi C', '2005/03/25', 'ctt1@gmail.com', '0909804567', '141 DPB', 'Phuong Thach Thang', 'Quan Hai Chau', 'Da Nang','25000');

INSERT INTO Account (userId, Password, RoleId, fullname, birthday, email, phone, address, ward, district, city, salary)
VALUES ('STF01', '123', 4, 'Pham Thanh D', '2005/04/07', 'dpt1@gmail.com', '0907654567', '141 DPB', 'Phuong Ben Nghe', 'Quan 1', 'TP. Ho Chi Minh','25000');


	-- INSERT Tỉnh/Thành phố
INSERT INTO city (cityId, cityName) 
VALUES ('79', 'TP. Ho Chi Minh'), ('01', 'Ha Noi'), ('48', 'Da Nang');

	-- INSERT Quận/Huyện
-- HCM
INSERT INTO district (districtId, districtName, cityId)
VALUES ('760', 'Quan 1', '79'), ('764', 'Quan Go Vap', '79'), ('761', 'Quan 12', '79');
-- Ha Noi
INSERT INTO district (districtId, districtName, cityId)
VALUES ('001', 'Quan Ba Dinh', '01'), ('002', 'Quan Hoan Kiem', '01');
-- Da Nang
INSERT INTO district (districtId, districtName, cityId)
VALUES ('490', 'Quan Hai Chau', '48'), ('492', 'Quan Thanh Khe', '48');

	-- INSERT Phường/Xã
-- Quan 1 - HCM
INSERT INTO ward (wardId, wardName, districtId) 
VALUES ('26734', 'Phuong Ben Nghe', '760'), ('26737', 'Phuong Ben Thanh', '760');
-- Quan Go Vap - HCM
INSERT INTO ward (wardId, wardName, districtId)
VALUES ('26896', 'Phuong 1', '764'), ('26902', 'Phuong 10', '764');
-- Quan 12 - HCM
INSERT INTO ward (wardId, wardName, districtId) 
VALUES ('26800', 'Phuong Tan Thoi Nhat', '761'), ('26801', 'Phuong Hiep Thanh', '761');
-- Quan Ba Dinh - HN
INSERT INTO ward (wardId, wardName, districtId)
VALUES ('00001', 'Phuong Phuc Xa', '001'), ('00004', 'Phuong Truc Bach', '001');
-- Quan Hoan Kiem - HN
INSERT INTO ward (wardId, wardName, districtId) 
VALUES ('00015', 'Phuong Hang Bac', '002'), ('00021', 'Phuong Trang Tien', '002');
-- Quan Hai Chau - DN
INSERT INTO ward (wardId, wardName, districtId) 
VALUES ('32101', 'Phuong Thach Thang', '490'), ('32102', 'Phuong Hai Chau I', '490');
-- Quan Thanh Khe - DN
INSERT INTO ward (wardId, wardName, districtId) 
VALUES ('32103', 'Phuong Chinh Gian', '492'), ('32104', 'Phuong Thac Gian', '492');

-- Thêm món
INSERT INTO Food(name, idCategory, price)
VALUES (N'Cơm chiên', 1, 30000),
       (N'Nước ngọt', 1, 15000)


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
