using System.Security.Cryptography;
using System.Text;

namespace SansTech_Api.Config.Master_Reusable_Void
{
	public class Encryp_Decryp_Strings
	{
		public static string Encrypt(string plainText, string key)
		{
			using (Aes aesAlg = Aes.Create())
			{
				// Generate a random IV (Initialization Vector)
				aesAlg.GenerateIV();
				aesAlg.Key = Encoding.UTF8.GetBytes(key); // Key should be 16, 24, or 32 bytes long (AES-128, AES-192, AES-256)

				// Create an encryptor from the AES instance
				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
					}

					// Concatenate the IV and the encrypted data (IV is needed for decryption)
					byte[] iv = aesAlg.IV;
					byte[] encrypted = msEncrypt.ToArray();
					byte[] result = new byte[iv.Length + encrypted.Length];
					Array.Copy(iv, 0, result, 0, iv.Length);
					Array.Copy(encrypted, 0, result, iv.Length, encrypted.Length);

					// Return the result as Base64 encoded string
					return Convert.ToBase64String(result);
				}
			}
		}




		public static string Decrypt(string cipherText, string key)
		{
			using (Aes aesAlg = Aes.Create())
			{
				// Convert the Base64 encoded cipher text to byte array
				byte[] fullCipher = Convert.FromBase64String(cipherText);

				// Extract the IV (Initialization Vector) from the cipher text
				byte[] iv = new byte[16]; // AES block size is 16 bytes
				Array.Copy(fullCipher, 0, iv, 0, iv.Length);

				byte[] cipherBytes = new byte[fullCipher.Length - iv.Length];
				Array.Copy(fullCipher, iv.Length, cipherBytes, 0, cipherBytes.Length);

				aesAlg.Key = Encoding.UTF8.GetBytes(key); // Key should be 16, 24, or 32 bytes long
				aesAlg.IV = iv;

				// Create a decryptor from the AES instance
				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							return srDecrypt.ReadToEnd();
						}
					}
				}
			}
		}
	}
}
