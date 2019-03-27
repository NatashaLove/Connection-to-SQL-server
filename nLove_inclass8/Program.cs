using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace nLove_inclass8
{
    class Program
    {
        static void Main(string[] args)
        {
            //connection to sql server
            SqlConnection conn;
            // fills outt the form of connection:
            conn = new SqlConnection("Data Source = 184.168.194.55; " +// the string parses on the ;
                "Initial Catalog = classroom; " +// puts each string into a designated field
                "User ID = profmorris; " +
           // see sql-connection string-builder class (has properties and methods for sql connection)

                "Password = Lec2g#08");

            try
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Students WHERE Level_ID = @level"; // sql query wll be sent to the db
                // @ - means that it is a variable - it'll be replaced later with a value - good for security reasons

                SqlCommand command = new SqlCommand(query, conn); // needs a query and the connection vars

                Console.Write("What level? ");
                string level = Console.ReadLine();
                command.Parameters.AddWithValue("@level", level); // level value will populate the @level var
                
                int number = (int)command.ExecuteScalar();// (int) - turns the value of command into intenger

                Console.WriteLine(number);// will return the number of students with level=@ - from the db
                
                
     // command method - returns the results of the query!

                Console.WriteLine("Good Connection.");

                string query2 = "INSERT INTO Students " +
                    "(Student_FName, Student_Lname) " +
                    "VALUES (@fname, @lname)"; // @- placeholder for data we don't know yet- will be replaced later

                SqlCommand comm2 = new SqlCommand(query2, conn);
                string fname = "Jefferson";
                string lname = "Davis";

                comm2.Parameters.AddWithValue("@fname", fname);
                comm2.Parameters.AddWithValue("@lname", lname);

                int rows = comm2.ExecuteNonQuery(); // returns number of rows affected by the query

                if (rows >0)
                {
                    Console.WriteLine("{0} row(s) inserted.", rows);
                }
                else
                {
                    Console.WriteLine("No rows inserted.");
                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Connection closed.");
            }

            Console.WriteLine("Press any key..");
            Console.ReadKey();
        }
    }
}
