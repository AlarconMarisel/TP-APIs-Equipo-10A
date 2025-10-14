using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_CatalogoProductos.Models
{
    public class ProductoDto
    {
        public string CodigoArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public string DescripcionArticulo { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }
        public decimal Precio { get; set; }
        public List<string> Imagenes { get; set; }
    }

}