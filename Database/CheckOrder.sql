SELECT 
    id AS [Mã HĐ], 
    idTable AS [Bàn], 
    customerName AS [Tên Khách], 
    caseName AS [Loại Đơn], 
    payMethod AS [P.Thức TToán], 
    status AS [Trạng Thái], -- 0: Chưa thanh toán, 1: Đã thanh toán
    dateCheckIn AS [Giờ Vào]
FROM dbo.Bill
ORDER BY dateCheckIn DESC;
SELECT 
    bi.idBill AS [Mã HĐ],
    f.name AS [Tên Món],
    bi.quantity AS [Số Lượng], -- Kiểm tra đúng cột quantity bạn vừa sửa
    f.price AS [Đơn Giá],
    (f.price * bi.quantity) AS [Thành Tiền]
FROM dbo.BillInfo bi
JOIN dbo.Food f ON bi.idFood = f.id
WHERE bi.idBill = 1; -- <<< THAY SỐ ORDER NO CỦA BẠN VÀO ĐÂY