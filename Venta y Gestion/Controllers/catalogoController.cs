using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Venta_y_Gestion.Models;

namespace Venta_y_Gestion.Controllers
{
    public class catalogoController : Controller
    {
        private BDEntities db = new BDEntities();

        // GET: catalogo
        
        public ActionResult Index()
        { 
            // obtener todos los tipos de productos
            var productos = db.productos.Include(p => p.tipoProducto);
            return View(productos.ToList());
        }

        // GET: catalogo/Details/5
        
        public ActionResult Details(int? id)
        {
            // si el id es nulo, redirigir a la vista de error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            productos productos = db.productos.Find(id);
            // si no se encuentra el producto, redirigir a la vista de error
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: catalogo/Create
        public ActionResult Create()
        {
            
            ViewBag.tipoProductoId = new SelectList(db.tipoProducto, "tipoProductoId", "tipoProductoNombre");
            // se crea SelectList que es una lista de opciones desde la db para el tipo de producto, obteniendo el id y el nombre
            return View();
        }
        //funcion para crear un producto en la db
        // POST: catalogo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productoId,productoNombre,tipoProductoId,productoPrecio,productoStock,productoImagen")] productos productos)
        {
            if (ModelState.IsValid)
            {
                //si es valido el producto, guardarlo en la base de datos
                db.productos.Add(productos);
                // guardar los cambios en la base de datos
                db.SaveChanges();
                // redirigir al index de catalogo
                return RedirectToAction("Index");
            }
                //se inicia el ViewBag con el tipo de producto y el id del producto
            ViewBag.tipoProductoId = new SelectList(db.tipoProducto, "tipoProductoId", "tipoProductoNombre", productos.tipoProductoId);
            return View(productos);
        }
        // funcion para editar un producto

        // GET: catalogo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            //siel id del catalogp es nulo, redirigir a la vista de error
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

        // POST: catalogo/Edit/5
        //funcion para guardar los cambios en la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productoId,productoNombre,tipoProductoId,productoPrecio,productoStock,productoImagen")] productos productos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tipoProductoId = new SelectList(db.tipoProducto, "tipoProductoId", "tipoProductoNombre", productos.tipoProductoId);
            return View(productos);
        }

        // GET: catalogo/Delete/5
        // funcion para eliminar un producto del catalogo
        public ActionResult Delete(int? id)
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

        // POST: catalogo/Delete/5
        //funcion para eliminar un producto del catalogo
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productos productos = db.productos.Find(id);
            db.productos.Remove(productos);
            db.SaveChanges();
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
