using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Venta_y_Gestion.Models;

namespace Venta_y_Gestion.Controllers
{
    public class gestionController : Controller
    {
        private BDEntities db = new BDEntities();

        // GET: gestion
        public ActionResult Index()
        {
            var productos = db.productos.Include(p => p.tipoProducto);
            return View(productos.ToList());
        }

        public ActionResult ConvertirImagen(int productoId)
        {   
            var imagen = db.productos.Where(x => x.productoId == productoId).FirstOrDefault();

            return File(imagen.productoImagen, "image/jpeg");
        }

        // GET: gestion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: gestion/Create
        public ActionResult Create()
        {
            ViewBag.tipoProductoId = new SelectList(db.tipoProducto, "tipoProductoId", "tipoProductoNombre");
            return View();
        }

        // POST: gestion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productoId,productoNombre,tipoProductoId,productoPrecio,productoStock")] productos productos, HttpPostedFileBase Imagen)
        {
            if (Imagen != null && Imagen.ContentLength > 0)
            {
                byte[] imageData = null;
                using (var binaryreader = new BinaryReader(Imagen.InputStream))
                {
                    imageData = binaryreader.ReadBytes(Imagen.ContentLength);
                }
                productos.productoImagen = imageData;
            }

            if (ModelState.IsValid)
            {
                db.productos.Add(productos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tipoProductoId = new SelectList(db.tipoProducto, "tipoProductoId", "tipoProductoNombre", productos.tipoProductoId);
            return View(productos);
        }

        // GET: gestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productos productos = db.productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            ViewBag.tipoProductoId = new SelectList(db.tipoProducto, "tipoProductoId", "tipoProductoNombre", productos.tipoProductoId);
            return View(productos);
        }

        // POST: gestion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productoId,productoNombre,tipoProductoId,productoPrecio,productoStock")] productos productos, HttpPostedFileBase Imagen)
        {
            //si la imagen no es nula y tiene un tamaño mayor a 0 (existe) se guarda la imagen en una variable como byte[] y se guarda en la base de datos
            if (Imagen != null && Imagen.ContentLength > 0)
            {
                byte[] imageData = null;
                using (var binaryreader = new BinaryReader(Imagen.InputStream))
                {
                    imageData = binaryreader.ReadBytes(Imagen.ContentLength);
                }
                productos.productoImagen = imageData;
            }

            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tipoProductoId = new SelectList(db.tipoProducto, "tipoProductoId", "tipoProductoNombre", productos.tipoProductoId);
            return View(productos);
        }

        // GET: gestion/Delete/5
        public ActionResult Delete(int? id)
        {
            //si el id es nulo, retorna una pagina de error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // si no hay errores, busca el producto con el id que se le pasa por parametro
            productos productos = db.productos.Find(id);
            //si el producto no existe, retorna una pagina de error
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: gestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //se elimina el producto de la base de datos 
            //se busca el producto por id y se elimina
            productos productos = db.productos.Find(id);
            db.productos.Remove(productos);
            //se guardan los cambios en la base de datos
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //se libera la memoria de la base de datos
                db.Dispose();
                
            }
            base.Dispose(disposing);
        }
    }
}
