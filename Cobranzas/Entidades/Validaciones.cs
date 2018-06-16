using System;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Cobranzas.Entidades
{
	public class Validaciones
	{
		public static readonly string _CaracteresEspeciales = @"@°#$%&;,:._\-<>\(\)\[\]\{\}'\*\+\¬\!\\\/\=\?\¡\¡\¿\¨\~\^\´\`" + "\"";// "°¬!”\#$%&/()=?¡’\¡¿¨´*+~^[{]}`;,:._-><";

		public static bool ValidarContrasena(string pwd)
		{
			if (EsNulaVacia(pwd))
			{
				return false;
			}
			//^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$
			return Regex.IsMatch(pwd, @"(?=^[a-zA-Z0-9-_@]{8,12}$)(^(?=.*[A-Z])(?=.*[0-9].*[0-9])(?=.*[a-z]).{8,12}$)");
		}

		public static bool EsNulaVacia(string data)
		{
			string str = string.Empty;
			try
			{
				str = data.Trim();
			}
			catch
			{
				str = string.Empty;
			}
			if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Valida alfanumérico, retorna true si cumple
		/// </summary>
		/// <param name="data">String a validar</param>
		/// <param name="specialChars">Si hay que permitir algunos caracteres especiales, poner "-CE-"(caracteres especiales) o "-CEE-"(CE con espacio) para caracteres especiales definidos</param>
		/// <param name="maxLength">Maximo de la cadena</param>
		/// <param name="minLength">Minimo de la cadena</param>
		/// <returns>Retorna True si es válido</returns>
		public static bool ValidarAlfanumerico(string data, string specialChars = "-CE-", bool Tildes = true, int maxLength = 0, int minLength = 0)
		{
			if (Validaciones.EsNulaVacia(data))
			{
				return false;
			}
			StringBuilder expresionBuilder = new StringBuilder();
			expresionBuilder.AppendFormat(@"^[a-zA-ZñÑ0-9{0}{1}]", specialChars.Equals("-CE-") ? _CaracteresEspeciales : specialChars.Equals("-CEE-") ? string.Concat(_CaracteresEspeciales, " ") : specialChars, Tildes ? "áéíóúÁÉÍÓÚäëïöüÄËÏÖÜ" : "");

			if ((maxLength > 0 && minLength >= 0) && (maxLength >= minLength))
			{
				expresionBuilder.Append(string.Format("{{{0},{1}}}", minLength, maxLength));
			}
			else
			{
				expresionBuilder.Append("+");
			}
			expresionBuilder.Append("$");
			string expresion = expresionBuilder.ToString();
			return Regex.IsMatch(data, expresion);
		}

		public static bool ValidarAlfabetico(string data, string specialChars = "", int maxLength = 0, int minLength = 0)
		{
			if (Validaciones.EsNulaVacia(data))
			{
				return false;
			}
			StringBuilder expresionBuilder = new StringBuilder();
			//expresionBuilder.AppendFormat(@"^[a-zA-Z{0}]", specialChars);
			expresionBuilder.AppendFormat(@"^[a-zA-Z{0}]", specialChars.Equals("-CE-") ? _CaracteresEspeciales : specialChars.Equals("-CEE-") ? string.Concat(_CaracteresEspeciales, " ") : specialChars);

			if ((maxLength > 0 && minLength >= 0) && (maxLength >= minLength))
			{
				expresionBuilder.Append(string.Format("{{{0},{1}}}", minLength, maxLength));
			}
			else
			{
				expresionBuilder.Append("+");
			}
			expresionBuilder.Append("$");
			string expresion = expresionBuilder.ToString();
			return Regex.IsMatch(data, expresion);
		}

		public static bool ValidarNumerico(string data, string specialChars = "", int maxLength = 0, int minLength = 0)
		{
			if (Validaciones.EsNulaVacia(data))
			{
				return false;
			}
			StringBuilder expresionBuilder = new StringBuilder();
			expresionBuilder.AppendFormat(@"^[0-9{0}]", specialChars);

			if ((maxLength > 0 && minLength >= 0) && (maxLength >= minLength))
			{
				expresionBuilder.Append(string.Format("{{{0},{1}}}", minLength, maxLength));
			}
			else
			{
				expresionBuilder.Append("+");
			}
			expresionBuilder.Append("$");
			string expresion = expresionBuilder.ToString();
			return Regex.IsMatch(data, expresion);
		}

		public static bool ValidarEmail(string emailaddress)
		{
			try
			{
				if (Validaciones.EsNulaVacia(emailaddress))
				{
					return false;
				}
				MailAddress m = new MailAddress(emailaddress);
				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

		public static bool ValidarIP(string ip, TipoIP tipoIP)
		{
			//
			try
			{

				if (Validaciones.EsNulaVacia(ip))
				{
					return false;
				}
				IPAddress dirip;
				if (!IPAddress.TryParse(ip, out dirip))
				{
					return false;
				}
				switch (tipoIP)
				{
					case TipoIP.V4:
						StringBuilder expresionBuilder = new StringBuilder();
						expresionBuilder.AppendFormat(@"^([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])$");
						string expresion = expresionBuilder.ToString();
						if (Regex.IsMatch(ip, expresion))
						{
							return dirip.AddressFamily == AddressFamily.InterNetwork;
						}
						else
						{
							return false;
						}
					case TipoIP.V6:
						return dirip.AddressFamily == AddressFamily.InterNetworkV6;
					default:
						return false;
				}

			}
			catch
			{
				return false;
			}
		}

	}

	public enum TipoIP
	{
		V4 = 1,
		V6 = 2
	}
}
