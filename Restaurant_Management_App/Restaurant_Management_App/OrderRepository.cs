using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Restaurant_Management_App
{
    public class OrderRepository
    {
        /// <summary>
        /// Inserts a Bill and its BillInfo rows inside a transaction.
        /// If status == 1 (paid) dateCheckOut will be set to GETDATE(); otherwise NULL.
        /// Returns the new Bill id.
        /// </summary>
        public int InsertBillWithItems(int tableId, List<Tuple<int, int>> foodIdAndQty,
                                      string customerName = null, string caseName = null,
                                      string payMethod = null, string note = null,
                                      int status = 0)
        {
            if (foodIdAndQty == null || foodIdAndQty.Count == 0)
                throw new ArgumentException("No items provided.", nameof(foodIdAndQty));

            using (var conn = new SqlConnection(Database.connStr))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;
                    try
                    {
                        cmd.CommandText = @"
INSERT INTO Bill (idTable, dateCheckIn, dateCheckOut, status, customerName, caseName, payMethod, note)
OUTPUT INSERTED.id
VALUES (@idTable, GETDATE(), CASE WHEN @status = 1 THEN GETDATE() ELSE NULL END, @status, @customerName, @caseName, @payMethod, @note)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idTable", tableId);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@customerName", (object)customerName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@caseName", (object)caseName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@payMethod", (object)payMethod ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@note", (object)note ?? DBNull.Value);

                        var newBillIdObj = cmd.ExecuteScalar();
                        int billId = Convert.ToInt32(newBillIdObj);

                        cmd.CommandText = "INSERT INTO BillInfo (idBill, idFood, quantity) VALUES (@billId, @foodId, @qty)";
                        foreach (var pair in foodIdAndQty)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@billId", billId);
                            cmd.Parameters.AddWithValue("@foodId", pair.Item1);
                            cmd.Parameters.AddWithValue("@qty", pair.Item2);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                        return billId;
                    }
                    catch
                    {
                        try { tx.Rollback(); } catch { /* ignore */ }
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Loads table list (id, name).
        /// </summary>
        public List<Tuple<int, string>> GetTableList()
        {
            var list = new List<Tuple<int, string>>();
            using (var conn = new SqlConnection(Database.connStr))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT id, name FROM tableFood ORDER BY id";
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(Tuple.Create(rdr.GetInt32(0), rdr.GetString(1)));
                    }
                }
            }
            return list;
        }
    }
}