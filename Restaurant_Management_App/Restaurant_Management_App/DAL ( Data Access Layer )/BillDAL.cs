using System;
using System.Data;

namespace Restaurant_Management_App
{
    public class BillDAL
    {
        private static BillDAL instance;
        public static BillDAL Instance
        {
            get { if (instance == null) instance = new BillDAL(); return BillDAL.instance; }
            private set { BillDAL.instance = value; }
        }

        private BillDAL() { }

        public int GetUncheckBillIDByTableID(int idTable) //lấy id hóa đơn chưa thanh toán của bàn đó, nếu chưa có thì trả về -1
        {
            // Gọi qua lớp Database bạn vừa cập nhật ở trên
            string query = "SELECT * FROM dbo.Bill WHERE idTable = " + idTable + " AND status = 0";
            DataTable data = Database.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                return (int)data.Rows[0]["id"];
            }
            return -1;
        }

        public void InsertBill(int idTable, string customerName, string caseName, string payMethod)//hàm InsertBill với đầy đủ 4 tham số như giao diện của bạn
        {
            // Câu lệnh SQL này sẽ lưu cả Tên khách, Loại đơn và Cách thanh toán vào DB
            string query = string.Format("INSERT dbo.Bill (idTable, dateCheckIn, status, customerName, caseName, payMethod) " +
                                 "VALUES ({0}, GETDATE(), 0, N'{1}', N'{2}', N'{3}')",
                                 idTable, customerName, caseName, payMethod);

            Database.Instance.ExecuteNonQuery(query);
        }

        public void CheckOut(int idBill)
        {
            // Đây chính là nơi bạn để câu lệnh SQL đó
            // status = 1 nghĩa là đã thanh toán
            string query = "UPDATE dbo.Bill SET status = 1, dateCheckOut = GETDATE() WHERE id = " + idBill;

            Database.Instance.ExecuteNonQuery(query);
        }
        public int GetMaxIDBill()
        {
            try
            {
                // Sử dụng ISNULL để nếu bảng trống thì trả về 0, giúp mã đơn tiếp theo là 1
                string query = "SELECT ISNULL(MAX(id), 0) FROM dbo.Bill";
                return (int)Database.Instance.ExecuteScalar(query);
            }
            catch
            {
                return 1;
            }
        }

    }
}