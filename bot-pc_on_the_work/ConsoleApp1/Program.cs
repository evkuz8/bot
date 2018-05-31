using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string symbols = "`~!@#$%^&*(){}[]_-+=|\\|/?.,<>'\"№0 ";
            List<string> updates = new List<string>();

            string conStr = @"Data Source = .\SQLEXPRESS; Initial Catalog = Dictionaries; Integrated Security = true;";
            SqlConnection sqlConnection = new SqlConnection(conStr);

            SqlCommand command = new SqlCommand("SELECT BadWord FROM BadWords", sqlConnection);

            sqlConnection.Open();

            SqlDataReader reader = command.ExecuteReader();
            int r = 0;
            while (reader.Read())
            {
                Console.WriteLine(reader[0]);
                string cmd ="UPDATE BadWords SET BadWord = '"+ Exclude(reader[0].ToString(), symbols.ToCharArray())+"' WHERE Badword = '"+reader[0]+"'" ;
                //r++;
                //if (r == 657)
                //{
                //    Console.WriteLine();
                //}
                updates.Add(cmd);
                

            }

            reader.Close();
            foreach (string update in updates)
            {
                SqlCommand updateCommmand = new SqlCommand(update, sqlConnection);
                updateCommmand.ExecuteNonQuery();
            }
            

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
        static string Exclude(string q, char[] symbols)
        {
            foreach (char symbol in symbols)
            {
                q = q.Replace(symbol.ToString(), string.Empty);
            }

            return q; // при проверке стоит поставить точку останова
        }
    }
}
