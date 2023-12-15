using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace MySqlDataAccessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = MySqlClientFactory.Instance;

            var conn = factory.CreateConnection();
            conn.ConnectionString = "server=localhost;port=3306;database=mysql;uid=root;pwd=mysql";

            conn.Open();

            try
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "select * from table1";

                using var dataAdapter = factory.CreateDataAdapter();
                dataAdapter.SelectCommand = cmd;

                var dataSet = new DataSet();
                dataAdapter.Fill(dataSet);

                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        Console.Write(row[column] + "   ");
                    }
                    Console.WriteLine();
                }
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
