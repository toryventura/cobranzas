using Cobranzas.Data;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System;
using Cobranzas.Clases;

namespace Cobranzas.Controllers
{
	public class NotaVentasController : Controller
	{
		private DB_A3C345_cobranzasEntities db = new DB_A3C345_cobranzasEntities();
		List<KeyValuePair<string, string>> mensaje = new List<KeyValuePair<string, string>>();
		// GET: NotaVentas
		public ActionResult Index()
		{

			return View();
		}
		public async Task<ActionResult> List()
		{
			var notaVentas = db.NotaVentas.Include(n => n.cliente).Include(n => n.Usuario);
			return PartialView("_List", await notaVentas.ToListAsync());

		}
		// GET: NotaVentas/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			NotaVenta notaVenta = await db.NotaVentas.FindAsync(id);
			if (notaVenta == null)
			{
				return HttpNotFound();
			}
			return View(notaVenta);
		}

		// GET: NotaVentas/Create
		public ActionResult Create()
		{
			ViewBag.clienteID = new SelectList(db.clientes, "clienteID", "nombre");
			ViewBag.vendedorID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre");
			List<producto> listpro = new List<producto>();
			listpro = db.productoes.ToList();
			ViewBag.productos = listpro;
			return View();
		}

		// POST: NotaVentas/Create
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		public async Task<ActionResult> Create(NotaVenta notaVenta, string list)
		{
			try
			{
				int i = db.NotaVentas.Count() + 1;
				notaVenta.notaVentaID = i;
				db.NotaVentas.Add(notaVenta);
				await db.SaveChangesAsync();
				System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
				List<DetalleVenta> nota = js.Deserialize<List<DetalleVenta>>(list);
				foreach (var item in nota)
				{
					int nt = db.DetalleVentas.Count();
					item.detalleVentaID = nt + 1;
					item.notaVentaID = i;
					db.DetalleVentas.Add(item);
					await db.SaveChangesAsync();
				}
				if (notaVenta.formapago.Value)
				{
					double totalc = notaVenta.total.Value - notaVenta.cuotainicial.Value;
					double monto = totalc / notaVenta.cantidadcouta.Value;
					int p = db.planPagoes.Count() + 1;
					planPago pl = new planPago
					{
						planPagoID = p,
						totalVenta = Convert.ToInt32(totalc),
						ncuotas = notaVenta.cantidadcouta,
						fecha = notaVenta.fecha,
						monto = Convert.ToInt32(monto),
						notaVentaID = i

					};
					db.planPagoes.Add(pl);
					await db.SaveChangesAsync();
					int total = pl.totalVenta.Value;
					int mon = pl.monto.Value;

					for (int j = 0; j < notaVenta.cantidadcouta; j++)
					{
						int co = db.cuotas.Count() + 1;
						DateTime dt = notaVenta.fecha.Value;
						cuota c = new cuota()
						{
							cuotasID = co,
							saldo = total - mon,
							haber = mon,
							debe = 0,
							fecha = dt.AddDays(30),
							planPagoID = p,
							pagado = 0

						};

						db.cuotas.Add(c);
						await db.SaveChangesAsync();
					}
				}

				mensaje.Clear();
				mensaje.Add(Util.mensaje(Util.OK, Util.OKMENSAJE));
				return Json(mensaje);
			}
			catch (Exception ex)
			{
				mensaje.Clear();
				mensaje.Add(Util.mensaje(Util.ERROR, ex.Message));
				return Json(mensaje);
			}


		}



		// GET: NotaVentas/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			NotaVenta notaVenta = await db.NotaVentas.FindAsync(id);
			if (notaVenta == null)
			{
				return HttpNotFound();
			}
			ViewBag.clienteID = new SelectList(db.clientes, "clienteID", "nombre", notaVenta.clienteID);
			ViewBag.vendedorID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", notaVenta.vendedorID);
			return View(notaVenta);
		}

		// POST: NotaVentas/Edit/5
		// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
		// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "fecha,nombre,nit,total,notaVentaID,clienteID,vendedorID,cuotainicial,cantidadcouta,formapago")] NotaVenta notaVenta)
		{
			if (ModelState.IsValid)
			{
				db.Entry(notaVenta).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			ViewBag.clienteID = new SelectList(db.clientes, "clienteID", "nombre", notaVenta.clienteID);
			ViewBag.vendedorID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", notaVenta.vendedorID);
			return View(notaVenta);
		}

		// GET: NotaVentas/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			NotaVenta notaVenta = await db.NotaVentas.FindAsync(id);
			if (notaVenta == null)
			{
				return HttpNotFound();
			}
			return View(notaVenta);
		}

		// POST: NotaVentas/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			NotaVenta notaVenta = await db.NotaVentas.FindAsync(id);
			db.NotaVentas.Remove(notaVenta);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
