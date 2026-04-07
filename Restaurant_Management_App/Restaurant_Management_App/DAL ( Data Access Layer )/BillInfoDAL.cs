using System;
using System.Data;

namespace Restaurant_Management_App
{
    public class BillInfoDAL
    {
        private static BillInfoDAL instance;
        public static BillInfoDAL Instance
        {
            get { if (instance == null) instance = new BillInfoDAL(); return BillInfoDAL.instance; }
            private set { BillInfoDAL.instance = value; }
        }

        private BillInfoDAL() { }

        // Hàm thêm món ăn vào chi tiết hóa đơn
        public void InsertBillInfo(int idBill, int idFood, int quantity)
        {
            // Thực thi lệnh INSERT vào bảng BillInfo
            string query = "INSERT INTO dbo.BillInfo (idBill, idFood, quantity) VALUES ( @idBill , @idFood , @quantity )";
            Database.Instance.ExecuteNonQuery(query, new object[] { idBill, idFood, quantity });
        }
    }
}