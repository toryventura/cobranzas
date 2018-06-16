using System.Web.Mvc;

namespace Cobranzas.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.fase1 = 1;
			ViewBag.fase2 = 5;
			ViewBag.fase3 = 56;
			ViewBag.fase4 = 45;
			ViewBag.fase5 = 5;
			ViewBag.total = 10;
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}