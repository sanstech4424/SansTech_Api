using Npgsql;
using SansTech_Api.Config.System_Models;

namespace SansTech_Api.Config.Db_Connection
{

	public class Postgres_Db_Connection
	{
		public static void OpenConnection()
		{
			try
			{
				string connectionString = Getting_Connection_Strings.Decrypt();
				Db_Connection_Models.Connection = new NpgsqlConnection(connectionString);
				Db_Connection_Models.Connection.Open();
				Console.WriteLine("Connection to PostgreSQL database opened successfully!");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while connecting: {ex.Message}");
			}
		}

		public static void CloseConnection()
		{
			try
			{
				if (Db_Connection_Models.Connection != null && Db_Connection_Models.Connection.State != System.Data.ConnectionState.Closed)
				{
					Db_Connection_Models.Connection.Close();
					Console.WriteLine("Connection to PostgreSQL database closed.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while closing the connection: {ex.Message}");
			}
		}

	}
}
