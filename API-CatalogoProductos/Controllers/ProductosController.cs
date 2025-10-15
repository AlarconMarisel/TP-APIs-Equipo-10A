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
            var producto = negocio.obtenerArticuloPorId(id);

            if (producto == null)
            { 
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "No se encontró el Producto solicitado."));
            }

            return producto;

        }

        // POST: api/Productos
        [HttpPost]
        public HttpResponseMessage Post([FromBody] ProductoDto producto)
        {
            try
            {
                if (producto == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No se enviaron datos del producto.");

                if (string.IsNullOrWhiteSpace(producto.CodigoArticulo) || string.IsNullOrWhiteSpace(producto.NombreArticulo) ||
                    producto.IdMarca <= 0 || producto.IdCategoria <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Faltan datos obligatorios del producto.");
                }

                MarcaNegocio marcaNeg = new MarcaNegocio();
                CategoriaNegocio categoriaNeg = new CategoriaNegocio();

                Marca marca = marcaNeg.listarMarca().Find(m => m.Id == producto.IdMarca);
                Categoria categoria = categoriaNeg.listarCategoria().Find(c => c.Id == producto.IdCategoria);

                if (marca == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La marca especificada no existe.");

                if (categoria == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "La categoría especificada no existe.");

                Articulo nuevo = new Articulo
                {
                    CodigoArticulo = producto.CodigoArticulo,
                    NombreArticulo = producto.NombreArticulo,
                    DescripcionArticulo = producto.DescripcionArticulo,
                    MarcaArticulo = marca,
                    CategoriaArticulo = categoria,
                    Precio = producto.Precio
                };

                ArticuloNegocio negocio = new ArticuloNegocio();
                int idNuevo = negocio.agregarArticulo(nuevo);

                if (idNuevo > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, $"Producto agregado correctamente con ID {idNuevo}.");
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No se pudo insertar el producto.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al agregar el producto. " + ex.Message);
            }
        }


        // PUT: api/Productos/5
        public HttpResponseMessage Put(int id, [FromBody]ProductoDto producto)
        {          
            var negocio = new ArticuloNegocio();
            var marcaNeg = new MarcaNegocio();
            var categoriaNeg = new CategoriaNegocio();

            // Validaciones
            if( negocio.obtenerArticuloPorId(id) == null)
            {
               return Request.CreateResponse(HttpStatusCode.NotFound, "No se encontró el Producto a Modificar.");
            }
            
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

            try
            {
                negocio.modificarArticulo(produc);
            
                return Request.CreateResponse(HttpStatusCode.OK, "Producto Modificado Correctamente.");

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al Modificar el Producto. " + ex.Message);
            }
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
