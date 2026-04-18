using System;
using System.Data;
using System.Data.SqlClient;

namespace Restaurant_Management_App.DAL
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return instance;
            }
        }

        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyNhaHang;Integrated Security=True";

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
            }

            return data;
        }
    }
}