using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using SansTech_Api.Config.Master_Reusable_Void;

namespace SansTech_Api.Config.Db_Connection
{
	public class Getting_Connection_Strings
	{


		public static string Decrypt()
		{
			try
			{
				// The encrypted string in Base64 format (replace with your actual encrypted string)
				string key = "1234567890123456";
				string encryptedText = "3aWICNJnhwk6/o3eQtLrd+f8ajmOyITIGOrB8SuCMaq6a9HVjn4u3ixOzcMNiMCzHBsaWI7fjxQgr9lgbPgi7aoOq1Yb6TMiEcQ2Ol7DYX94FOtTujvJU5diyaVQ5MKz24Yv7twD4InM4pRibvAEc50LEWD6uG2m4BEI4ZMSc1g9mo8RmJjoZmd0r0Ghi74FBpvsii8s4L/yj+paK6h27w==";
				string decryptedText = Encryp_Decryp_Strings.Decrypt(encryptedText, key);
				return (decryptedText);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred during decryption: {ex.Message}");
				return string.Empty;
			}
		}
	}
}
