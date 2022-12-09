using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CurrencyGetterApp
{
    public static class Program
    {

        static void Main(string[] args)
        {
            ExchangeOperations.LoadRates();

            string connetionString = null; 
            SqlConnection cnn;
            connetionString = "Data Source=PELL\\MSSQLSERVER2;Initial Catalog=PAYMENT_SYSTEM;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                Console.WriteLine("Connection Open ! ");

                //SqlCommand myCommand = new SqlCommand("select * from MERCHANT_INFO", cnn);
                //myCommand.Parameters.AddWithValue("@name", name);
                //myCommand.Parameters.AddWithValue("@last_name", last_name);

                // IDataReader data = myCommand.ExecuteReader();

                //while (data.Read())
                //{
                //   var MERCHANT= data["MERCHANT_NAME"].ToString();
                //}
                //
                SqlCommand deleteCmd = new SqlCommand("DELETE CURRENCY_RATE WHERE CURRENCY_DATE= @current_date", cnn);
                deleteCmd.Parameters.AddWithValue("@current_date", DateTime.Today);
                deleteCmd.ExecuteNonQuery();


                for (int i=0; i< ExchangeOperations.ExchangeRates.Count; i++)
                {
                    SqlCommand myCommand = new SqlCommand("INSERT INTO CURRENCY_RATE VALUES (@status, @currency_date," +
                  " @currency_code, @currency_name, @currency_rate)", cnn);
                    myCommand.Parameters.AddWithValue("@status",1);
                    myCommand.Parameters.AddWithValue("@currency_date", DateTime.Today);
                    myCommand.Parameters.AddWithValue("@currency_code", "");
                    myCommand.Parameters.AddWithValue("@currency_name", ExchangeOperations.ExchangeRates.ElementAt(i).Key);
                    myCommand.Parameters.AddWithValue("@currency_rate", ExchangeOperations.ExchangeRates.ElementAt(i).Value);
                    myCommand.ExecuteNonQuery();
                } 
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection ! ");
                throw ex;
            }

        }



    }
}
