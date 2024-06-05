/*
uses https://learn.microsoft.com/en-us/azure/azure-sql/database/connect-query-dotnet-visual-studio?view=azuresql
as reference.  had to add in code for TrustServerCertificate=true;
*/

using Microsoft.Data.SqlClient;

namespace sqltest
{
    class Program
    {
        static void Main(string[] args)
        {
            try 
            { 
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "KAB-SQL\\KABSQL22"; 
                builder.UserID = "csharp";            
                builder.Password = "BlueCat12";     
                builder.InitialCatalog = "MusicStore";
                builder.TrustServerCertificate=true;
         
                using SqlConnection connection = new SqlConnection(builder.ConnectionString);
                Console.WriteLine("\nQuery data example:");
                Console.WriteLine("=========================================\n");
                
                connection.Open();       

                String sql = "SELECT AlbumId,Title,ArtistId FROM Album";

                using SqlCommand command = new SqlCommand(sql, connection);
                
                using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()){
                        Console.WriteLine("{0} {1} {2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    }               
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine(); 
        }
    }
}