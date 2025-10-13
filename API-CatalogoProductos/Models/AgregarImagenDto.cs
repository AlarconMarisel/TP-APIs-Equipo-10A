using System.Collections.Generic;

namespace API_CatalogoProductos.Models
{
    public class AgregarImagenDto
    {
        public int IdProducto { get; set; }
        public List<string> ImagenUrls { get; set; }
    }
}
