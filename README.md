# 🍽️ Restaurant Management System

## Giới thiệu

Ứng dụng quản lý nhà hàng xây dựng bằng C# WinForms và SQL Server 2022, hỗ trợ quản lý bàn, món ăn, hóa đơn và tài khoản người dùng (Admin/Staff).

## Công nghệ

* C# WinForms (.NET Framework)
* SQL Server 2022
* GitHub

## Cấu trúc project

Restaurant-App/
├── Database/ (CreateDatabase.sql, InsertData.sql, StoredProcedures.sql)
├── WinformApp/ (Forms, Models, DAO)
├── Docs/
└── README.md

## Cách chạy

1. Mở SSMS và chạy: CreateDatabase.sql → InsertData.sql → StoredProcedures.sql
2. Mở file .sln bằng Visual Studio 2022 → Build → Run

## Tài khoản test

* admin / 123 (Admin)
* staff1 / 123 (Staff)

## Chức năng

Admin: quản lý món, danh mục, báo cáo
Staff: chọn bàn, thêm món, thanh toán

## Stored Procedure

Sử dụng các procedure như USP_Login, USP_InsertBill, USP_InsertBillInfo, USP_CheckOut để xử lý logic hệ thống.

## Thành viên

* Trần Quốc Duy Khôi
* Nguyễn Lê Đàm Văn
* Nguyễn Hoàng Trọng Phúc
* Nguyễn Trần Công Hậu

## Trạng thái

Đang phát triển
