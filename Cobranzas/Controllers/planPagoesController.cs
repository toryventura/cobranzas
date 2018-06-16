using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cobranzas.Data;

namespace Cobranzas.Controllers
{
    public class planPagoesController : Controller
    {
        private DB_A3C345_cobranzasEntities db = new DB_A3C345_cobranzasEntities();

        // GET: planPagoes
        public async Task<ActionResult> Index()
        {
            var planPagoes = db.planPagoes.Include(p => p.NotaVenta);
            return View(await planPagoes.ToListAsync());
        }

        // GET: planPagoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planPago planPago = await db.planPagoes.FindAsync(id);
            if (planPago == null)
            {
                return HttpNotFound();
            }
            return View(planPago);
        }

        // GET: planPagoes/Create
        public ActionResult Create()
        {
            ViewBag.notaVentaID = new SelectList(db.NotaVentas, "notaVentaID", "nombre");
            return View();
        }

        // POST: planPagoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "totalVenta,ncuotas,fecha,monto,planPagoID,notaVentaID")] planPago planPago)
        {
            if (ModelState.IsValid)
            {
                db.planPagoes.Add(planPago);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.notaVentaID = new SelectList(db.NotaVentas, "notaVentaID", "nombre", planPago.notaVentaID);
            return View(planPago);
        }

        // GET: planPagoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planPago planPago = await db.planPagoes.FindAsync(id);
            if (planPago == null)
            {
                return HttpNotFound();
            }
            ViewBag.notaVentaID = new SelectList(db.NotaVentas, "notaVentaID", "nombre", planPago.notaVentaID);
            return View(planPago);
        }

        // POST: planPagoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "totalVenta,ncuotas,fecha,monto,planPagoID,notaVentaID")] planPago planPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planPago).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.notaVentaID = new SelectList(db.NotaVentas, "notaVentaID", "nombre", planPago.notaVentaID);
            return View(planPago);
        }

        // GET: planPagoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planPago planPago = await db.planPagoes.FindAsync(id);
            if (planPago == null)
            {
                return HttpNotFound();
            }
            return View(planPago);
        }

        // POST: planPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            planPago planPago = await db.planPagoes.FindAsync(id);
            db.planPagoes.Remove(planPago);
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
