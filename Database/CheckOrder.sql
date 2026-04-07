SELECT 
    b.id AS [Mã Hóa Đơn], 
    f.name AS [Tên Món], 
    bi.quantity AS [Số Lượng], -- Đổi quantity thành count cho giống lệnh INSERT
    f.price AS [Đơn Giá], 
    (bi.quantity * f.price) AS [Thành Tiền]
FROM dbo.Bill AS b
JOIN dbo.BillInfo AS bi ON b.id = bi.idBill
JOIN dbo.Food AS f ON bi.idFood = f.id
ORDER BY b.id DESC
SELECT 
    b.id AS [Mã Hóa Đơn], 
    b.DateCheckIn AS [Ngày Vào],
    SUM(f.price * bi.quantity) AS [Tổng Tiền]
FROM dbo.Bill AS b
JOIN dbo.BillInfo AS bi ON b.id = bi.idBill
JOIN dbo.Food AS f ON bi.idFood = f.id
GROUP BY b.id, b.DateCheckIn
SELECT * FROM Account