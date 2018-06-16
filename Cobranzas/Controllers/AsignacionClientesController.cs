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
    public class AsignacionClientesController : Controller
    {
        private DB_A3C345_cobranzasEntities db = new DB_A3C345_cobranzasEntities();

        // GET: AsignacionClientes
        public  ActionResult Index()
        {
			/* var asignacionClientes = db.AsignacionClientes.Include(a => a.cliente).Include(a => a.Usuario);*/
			//return View(await asignacionClientes.ToListAsync());
			return View();
        }
		[HttpPost]
		public async Task<ActionResult>List()
		{
			var asignacionClientes = db.AsignacionClientes.Include(a => a.cliente).Include(a => a.Usuario).ToListAsync();
			return PartialView("_List", await asignacionClientes) ;
		}

        // GET: AsignacionClientes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionCliente asignacionCliente = await db.AsignacionClientes.FindAsync(id);
            if (asignacionCliente == null)
            {
                return HttpNotFound();
            }
            return View(asignacionCliente);
        }

        // GET: AsignacionClientes/Create
        public ActionResult Create()
        {
            ViewBag.clienteID = new SelectList(db.clientes, "clienteID", "nombre");
            ViewBag.usuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre");
            return PartialView("_Create",new AsignacionCliente());
        }

        // POST: AsignacionClientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "usuarioID,asignacionclienteID,clienteID,estado,fechaAsignacion")] AsignacionCliente asignacionCliente)
        {
            if (ModelState.IsValid)
            {
                db.AsignacionClientes.Add(asignacionCliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.usuarioID = new SelectList(db.clientes, "clienteID", "nombre", asignacionCliente.usuarioID);
            ViewBag.usuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", asignacionCliente.usuarioID);
            return View(asignacionCliente);
        }

        // GET: AsignacionClientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionCliente asignacionCliente = await db.AsignacionClientes.FindAsync(id);
            if (asignacionCliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuarioID = new SelectList(db.clientes, "clienteID", "nombre", asignacionCliente.usuarioID);
            ViewBag.usuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", asignacionCliente.usuarioID);
            return View(asignacionCliente);
        }

        // POST: AsignacionClientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "usuarioID,asignacionclienteID,clienteID,estado,fechaAsignacion")] AsignacionCliente asignacionCliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asignacionCliente).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.usuarioID = new SelectList(db.clientes, "clienteID", "nombre", asignacionCliente.usuarioID);
            ViewBag.usuarioID = new SelectList(db.Usuarios, "UsuarioID", "UsuarioNombre", asignacionCliente.usuarioID);
            return View(asignacionCliente);
        }

        // GET: AsignacionClientes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionCliente asignacionCliente = await db.AsignacionClientes.FindAsync(id);
            if (asignacionCliente == null)
            {
                return HttpNotFound();
            }
            return View(asignacionCliente);
        }

        // POST: AsignacionClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AsignacionCliente asignacionCliente = await db.AsignacionClientes.FindAsync(id);
            db.AsignacionClientes.Remove(asignacionCliente);
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
