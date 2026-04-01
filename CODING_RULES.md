# 📏 Coding Rules

## 1. Quy tắc đặt tên Control (WinForms)

| Control  | Prefix |
| -------- | ------ |
| Form     | frm    |
| Button   | btn    |
| TextBox  | txt    |
| ListBox  | lst    |
| Label    | lbl    |
| CheckBox | chk    |
| GroupBox | gbx    |
| ComboBox | cmb    |
| DataGrid | dg     |
| Dataset  | ds     |
| GridView | gv     |

Ví dụ:

* btnLogin, btnAddFood
* txtUsername, txtPassword
* lblTitle
* dgvFoodList

---

## 2. Quy tắc đặt tên biến & hàm

* Biến: camelCase → `userName`, `tableId`
* Hàm: PascalCase → `LoadData()`, `InsertBill()`
* Class: PascalCase → `Account`, `FoodDAO`

---

## 3. Quy tắc đặt tên file

* Form: `LoginForm.cs`, `MainForm.cs`
* DAO: `AccountDAO.cs`, `FoodDAO.cs`
* Model: `Account.cs`, `Food.cs`

---

## 4. Quy tắc code

* Không hardcode dữ liệu
* Tách riêng DAO (data access)
* Sử dụng Stored Procedure thay vì viết SQL trực tiếp
* Mỗi hàm chỉ làm 1 nhiệm vụ

---

## 5. Quy tắc Git

* Commit message rõ ràng (English hoặc Vietnamese)
* Ví dụ:

  * "Add login form"
  * "Fix InsertData.sql"
* Không commit file rác (bin, obj)

---

## 6. Quy tắc làm việc nhóm

* Mỗi người làm 1 feature riêng (branch)
* Không code trực tiếp trên main
* Pull code trước khi push

---

## 7. Quy tắc UI

* Tên control phải có prefix
* Không để tên mặc định như button1, textBox1
* Giao diện rõ ràng, dễ nhìn

---

## 8. Quy tắc Database

* Tên bảng: PascalCase → `Food`, `Bill`
* Tên cột: camelCase → `idTable`, `dateCheckIn`
* Sử dụng khóa chính và khóa ngoại đầy đủ
* Dùng Stored Procedure cho các thao tác chính

---

## 9. Ghi chú

Tuân thủ các quy tắc trên để đảm bảo code dễ đọc, dễ bảo trì và làm việc nhóm hiệu quả.
