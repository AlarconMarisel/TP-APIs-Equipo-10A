using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocio;
using Dominio;
using API_CatalogoProductos.Models;

namespace API_CatalogoProductos.Controllers
{
    public class ProductosController : ApiController
    {
        // GET: api/Productos
        public IEnumerable<Articulo> Get()
        {
            ArticuloNegocio datos = new ArticuloNegocio();


            return datos.listarArticulo();
        }

        // GET: api/Productos/5
        public Articulo Get(int id)
        {

            ArticuloNegocio negocio = new ArticuloNegocio();

            return negocio.obtenerArticuloPorId(id);

        }

        // POST: api/Productos
        [HttpPost]
        public IHttpActionResult Post([FromBody] ProductoDto producto)
        {
            try
            {
                if (producto == null)
                    return BadRequest("No se enviaron datos del producto.");

                Articulo nuevo = new Articulo
                {
                    CodigoArticulo = producto.CodigoArticulo,
                    NombreArticulo = producto.NombreArticulo,
                    DescripcionArticulo = producto.DescripcionArticulo,
                    MarcaArticulo = new Marca { Id = producto.IdMarca },
                    CategoriaArticulo = new Categoria { Id = producto.IdCategoria },
                    Precio = producto.Precio
                };

                
                ArticuloNegocio negocio = new ArticuloNegocio();
                int idNuevo = negocio.agregarArticulo(nuevo);

                if (idNuevo > 0)
                    return Ok($"Producto agregado correctamente con ID {idNuevo}");
                else
                    return BadRequest("No se pudo insertar el producto.");

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Productos/5
        public HttpResponseMessage Put(int id, [FromBody]ProductoDto producto)
        {          
            var negocio = new ArticuloNegocio();
            var marcaNeg = new MarcaNegocio();
            var categoriaNeg = new CategoriaNegocio();

            // Validaciones
            Marca marca = marcaNeg.listarMarca().Find(m => m.Id == producto.IdMarca);
            Categoria categoria = categoriaNeg.listarCategoria().Find(c => c.Id == producto.IdCategoria);

            if (marca == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "La Marca especificada no existe.");
            }
            if (categoria == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "La Categoria especificada no existe.");
            }

            // Producto a Modificar
            var produc = new Articulo();
            if (producto != null)
            { 
                produc.CodigoArticulo = producto.CodigoArticulo;
                produc.NombreArticulo = producto.NombreArticulo;
                produc.DescripcionArticulo = producto.DescripcionArticulo;
                produc.MarcaArticulo = new Marca { Id =producto.IdMarca };
                produc.CategoriaArticulo = new Categoria { Id=producto.IdCategoria }; 
                produc.Precio = producto.Precio;
                produc.IdArticulo = id;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error en los datos del producto a Modificar.");
            };
                      
            negocio.modificarArticulo(produc);
            
            return Request.CreateResponse(HttpStatusCode.OK, "Producto Modificado Correctamente.");

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
