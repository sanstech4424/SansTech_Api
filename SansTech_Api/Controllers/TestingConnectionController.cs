using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using SansTech_Api.Config.Db_Connection;
using SansTech_Api.Config.Master_Reusable_Void;
using SansTech_Api.Config.System_Models;
using System.Data;
using System.Security.Cryptography;

namespace SansTech_Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestingConnectionController : ControllerBase
	{
		[HttpGet]
		[Route("/Testing")]
		public IActionResult Testing() 
		{
			string? fetchData;
			List<object> results = new List<object>();
			DataTable dataTable = new DataTable();
			try
			{
				Postgres_Db_Connection.OpenConnection();
				Console.WriteLine("Connection to PostgreSQL database opened successfully!");
				string query = @"SELECT ""Id"", ""Name"" FROM public.""TestingTable""";
				using (var command = new NpgsqlCommand(query, Db_Connection_Models.Connection))
				{
					using (var adapter = new NpgsqlDataAdapter(command))
					{
						adapter.Fill(dataTable);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
			fetchData = JsonConvert.SerializeObject(dataTable);
			return Ok(fetchData);
		}

		[HttpGet]
		[Route("/TestingEncrpy")]
		public IActionResult Encrpy()
		{
			string key = "1234567890123456"; // 16-byte key for AES-128 (128 bits)
			string originalText = "Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Username=postgres.bqyvogqxzdngwqudlufp;Password=Cans4424!;Database=postgres;";

			// Encrypt the text
			string encryptedText = Encryp_Decryp_Strings.Encrypt(originalText, key);
			Console.WriteLine("Encrypted: " + encryptedText);
			return Ok(encryptedText);
		}
	}
}
