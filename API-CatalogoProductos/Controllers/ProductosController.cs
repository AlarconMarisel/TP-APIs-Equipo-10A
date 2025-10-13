using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio;
using API_CatalogoProductos.Models;

namespace API_CatalogoProductos.Controllers
{
    public class ProductosController : ApiController
    {
        // GET: api/Productos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Productos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Productos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Productos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Productos/5
        public void Delete(int id)
        {
        }

        // POST: api/Productos/{id}/imagenes
        [HttpPost]
        [Route("api/Productos/{id}/imagenes")]
        public IHttpActionResult AgregarImagenes(int id, [FromBody] List<string> imagenUrls)
        {
            try
            {
                // Validaciones
                if (imagenUrls == null || imagenUrls.Count == 0)
                {
                    return BadRequest("Debe proporcionar al menos una URL de imagen");
                }

                if (id <= 0)
                {
                    return BadRequest("El ID del producto debe ser mayor a 0");
                }

                // Lógica de negocio
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                imagenNegocio.agregarImagenesPorProducto(id, imagenUrls);

                return Ok($"Se agregaron {imagenUrls.Count} imágenes al producto con ID {id}");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
