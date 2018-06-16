using System.Web.Optimization;

namespace Cobranzas
{
	public class BundleConfig
	{
		// Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Content/login")
				.Include("~/Content/login/login.css", new CssRewriteUrlTransform())
				.Include("~/Content/login/login.css", new CssRewriteUrlTransform()));

			bundles.Add(new ScriptBundle("~/Scripts/js").Include(
				"~/Scripts/jquery-{version}.js",
				"~/Scripts/bootstrap.js",
				"~/Scripts/jquery.blockUI.js",
				"~/Scripts/jquery.unobtrusive-ajax.js",
				"~/Scripts/jquery.validate.js",
				"~/Scripts/blckUI.js",
				"~/Scripts/CasasFuncionesMenus.js",
				"~/Scripts/CasasFuncionesGlobales.js"
				));
			
			//bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
			//			"~/Scripts/jquery-{version}.js",
			//			"~/bower_components/jquery/dist/jquery.min.js"));

			//bundles.Add(new ScriptBundle("~/bundles/js").Include(
			//			"~/bower_components/bootstrap/dist/js/bootstrap.min.js",
			//			"~/plugins/input-mask/jquery.inputmask.js",
			//			"~/plugins/input-mask/jquery.inputmask.date.extensions.js",
			//			"~/plugins/input-mask/jquery.inputmask.extensions.js",
			//			"~/bower_components/moment/min/moment.min.js",
			//			"~/bower_components/bootstrap-daterangepicker/daterangepicker.js",
			//			"~/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js",
			//			"~/bower_components/bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js",
			//			"~/plugins/timepicker/bootstrap-timepicker.min.js",
			//			"~/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
			//			"~/plugins/iCheck/icheck.min.js",
			//			"~/bower_components/datatables.net/js/jquery.dataTables.min.js",
			//			"~/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js",
			//			"~/bower_components/fastclick/lib/fastclick.js",
			//			"~/dist/js/adminlte.min.js",
			//			"~/bower_components/ckeditor/ckeditor.js",
			//			"~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
			//			"~/Scripts/jquery.unobtrusive-ajax.js",
			//			"~/Scripts/jquery-ui.js",
			//			"~/Scripts/respond.js",
			//			"~/Scripts/respond.min.js",
			//			"~/bower_components/ag-grid-enterprise/dist/ag-grid-enterprise.js",
			//			"~/Scripts/jquery.blockUI.js",
			//			"~/Scripts/blckUI.js",
			//			"~/plugins/alertify/alertify.min.js",
			//			"~/Scripts/mensajes.js",
			//			"~/Scripts/FuncionesGlobalAg-grid.js",
			//			"~/bower_components/chart.js/Chart.js"
			//			));
			
			bundles.Add(new StyleBundle("~/Content/css")
				.Include("~/bower_components/bootstrap/dist/css/bootstrap.min.css", new CssRewriteUrlTransform()).Include(
				"~/bower_components/ag-grid-enterprise/dist/styles/ag-theme-bootstrap.css", new CssRewriteUrlTransform()).Include(
				"~/bower_components/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()).Include(
				"~/bower_components/Ionicons/css/ionicons.min.css", new CssRewriteUrlTransform()).Include(
				"~/bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css", new CssRewriteUrlTransform()).Include(
				"~/dist/css/AdminLTE.min.css", new CssRewriteUrlTransform()).Include(
				"~/dist/css/skins/_all-skins.min.css", new CssRewriteUrlTransform()).Include(
				"~/plugins/timepicker/bootstrap-timepicker.min.css", new CssRewriteUrlTransform()).Include(
				"~/bower_components/ag-grid-enterprise/dist/styles/ag-theme-balham.css", new CssRewriteUrlTransform()).Include(
				"~/plugins/alertify/alertify.core.css", new CssRewriteUrlTransform()).Include(
				"~/plugins/alertify/alertify.default.css", new CssRewriteUrlTransform()));
			//bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
			//			"~/Scripts/modernizr-*"));
		
		}
	}
}
