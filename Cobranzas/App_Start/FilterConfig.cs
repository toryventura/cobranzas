using Cobranzas.Clases;
using System.Web.Mvc;

namespace Cobranzas
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			//filters.Add(new HandleErrorAttribute());
			filters.Add(new AutorizarAtributo());
		}
	}
}
