//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Venta_y_Gestion.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class productos
    {
        // crea el constructor de productos y define los atributos
        public int productoId { get; set; }
        public string productoNombre { get; set; }
        public int tipoProductoId { get; set; }
        public int productoPrecio { get; set; }
        public int productoStock { get; set; }
        public byte[] productoImagen { get; set; }
        public virtual tipoProducto tipoProducto { get; set; }
    }
}