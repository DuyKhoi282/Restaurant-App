# 🍽️ Restaurant Management App

Ứng dụng quản lý nhà hàng viết bằng **C# WinForms** và **SQL Server**, phục vụ các nghiệp vụ bán hàng tại quầy như đăng nhập, gọi món, quản lý hóa đơn và quản lý danh mục món ăn.

## 1) Mục tiêu dự án

- Hỗ trợ nhân viên thao tác order nhanh theo bàn.
- Hỗ trợ quản lý theo dõi hóa đơn và doanh thu cơ bản.
- Chuẩn hóa dữ liệu người dùng, món ăn, bàn và hóa đơn trong SQL Server.

## 2) Công nghệ sử dụng

- **C# WinForms** (.NET Framework)
- **SQL Server** (khuyến nghị SQL Server 2022)
- **Visual Studio 2022**

## 3) Cấu trúc thư mục chính

```text
Restaurant-App/
├── Database/
│   ├── CreateDatabase.sql
│   ├── InsertData.sql
│   ├── ClearData.sql
│   └── CheckOrder.sql
├── Restaurant_Management_App/
│   ├── Restaurant_Management_App.sln
│   └── Restaurant_Management_App/
│       ├── FORM/
│       ├── DAL ( Data Access Layer )/
│       ├── DTO ( Data Transfer Object )/
│       └── ...
└── README.md
```

## 4) Hướng dẫn cài đặt & chạy ứng dụng

### Bước 1: Khởi tạo cơ sở dữ liệu

1. Mở SQL Server Management Studio (SSMS).
2. Chạy file theo thứ tự:
   - `Database/CreateDatabase.sql`
   - `Database/InsertData.sql`
3. (Tuỳ chọn) Dùng `Database/ClearData.sql` để reset dữ liệu test.

### Bước 2: Chạy ứng dụng WinForms

1. Mở file solution:  
   `Restaurant_Management_App/Restaurant_Management_App.sln`
2. Build solution trong Visual Studio 2022.
3. Chạy project `Restaurant_Management_App`.

## 5) Tài khoản mẫu

- **Admin**: `admin / 123`
- **Staff**: `staff1 / 123`

> Nếu dữ liệu seed thay đổi, vui lòng kiểm tra lại script trong thư mục `Database/`.

## 6) Tính năng chính

### Dành cho Admin

- Quản lý món ăn và danh mục món.
- Quản lý tài khoản và phân quyền cơ bản.
- Theo dõi danh sách hóa đơn và báo cáo doanh thu cơ bản.

### Dành cho Staff

- Chọn bàn và tạo order.
- Thêm/xóa món trong hóa đơn.
- Thanh toán và in hóa đơn.

## 7) Một số thành phần quan trọng trong mã nguồn

- **FORM/**: chứa các màn hình giao diện (đăng nhập, tạo order, quản lý món, báo cáo...).
- **DAL/**: lớp truy xuất dữ liệu.
- **DTO/**: đối tượng truyền dữ liệu giữa các tầng.

## 8) Nhóm phát triển

- Trần Quốc Duy Khôi
- Nguyễn Lê Đàm Văn
- Nguyễn Hoàng Trọng Phúc
- Nguyễn Trần Công Hậu

---

Nếu bạn muốn, mình có thể viết thêm phần **“Hướng dẫn đóng góp (Contributing)”** và **“Ảnh minh họa giao diện”** để README đầy đủ hơn.
