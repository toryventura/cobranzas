using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cobranzas.Entidades
{
	public class SEGURIDAD
	{
		private static readonly string key = "Key0b3li$c0"; //"Key0b3li$c0"
		private static readonly string IV = "C0NTRUCT0R@0B3LI$C0"; //"C0NTRUCT0R@0B3LI$C0"

		public static string EncriptarAES128(string texto)
		{
			byte[] clearBytes = Encoding.UTF8.GetBytes(texto);
			using (Aes encryptor = Aes.Create())
			{
				encryptor.Key = Encoding.UTF8.GetBytes(key);
				encryptor.IV = Encoding.UTF8.GetBytes(IV);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(clearBytes, 0, clearBytes.Length);
						cs.Close();
					}
					texto = Convert.ToBase64String(ms.ToArray());
				}
			}
			return texto;
		}

		public static string DesencriptarAES128(string textoEncriptado)
		{
			byte[] cipherBytes = Convert.FromBase64String(textoEncriptado);
			using (Aes encryptor = Aes.Create())
			{
				encryptor.Key = Encoding.UTF8.GetBytes(key);
				encryptor.IV = Encoding.UTF8.GetBytes(IV);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(cipherBytes, 0, cipherBytes.Length);
						cs.Close();
					}
					textoEncriptado = Encoding.UTF8.GetString(ms.ToArray());
				}
			}
			return textoEncriptado;
		}

		// Hash an input string and return the hash as
		// a 32 character hexadecimal string.
		public static string encriptarMD5(string input)
		{
			// Create a new instance of the MD5CryptoServiceProvider object.
			MD5 md5Hasher = MD5.Create();

			// Convert the input string to a byte array and compute the hash.
			byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data
			// and format each one as a hexadecimal string.
			for (int i = 0 ; i < data.Length ; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		// Verify a hash against a string.
		public static bool verificarMD5(string input, string hash)
		{
			// Hash the input.
			string hashOfInput = encriptarMD5(input);

			// Create a StringComparer an comare the hashes.
			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			if (0 == comparer.Compare(hashOfInput, hash))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
