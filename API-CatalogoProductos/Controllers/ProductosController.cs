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
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                // Validaciones de entrada
                if (id <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El ID del producto debe ser mayor a 0");
                }

                ArticuloNegocio negocio = new ArticuloNegocio();
                
                // Validar que el producto existe
                Articulo producto = negocio.obtenerArticuloPorId(id);
                if (producto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No se encontró el producto a eliminar");
                }

                // Eliminar imágenes asociadas primero
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                try
                {
                    imagenNegocio.eliminarImagenesPorArticulo(id);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, 
                        "Error al eliminar las imágenes del producto: " + ex.Message);
                }

                // Eliminar el producto
                try
                {
                    negocio.eliminarArticulo(id);
                    
                    var response = new
                    {
                        message = "Producto eliminado correctamente",
                        productoId = id,
                        nombreProducto = producto.NombreArticulo,
                        codigoProducto = producto.CodigoArticulo
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, 
                        "Error al eliminar el producto: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, 
                    "Error interno del servidor: " + ex.Message);
            }
        }

        // POST: api/Productos/{id}/imagenes
        [HttpPost]
        [Route("api/Productos/{id}/imagenes")]
        public HttpResponseMessage AgregarImagenes(int id, [FromBody] List<string> imagenUrls)
        {
            try
            {
                // Debug: Log para ver qué se está recibiendo
                System.Diagnostics.Debug.WriteLine($"ID recibido: {id}");
                System.Diagnostics.Debug.WriteLine($"imagenUrls es null: {imagenUrls == null}");
                if (imagenUrls != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Cantidad de URLs: {imagenUrls.Count}");
                    for (int i = 0; i < imagenUrls.Count; i++)
                    {
                        System.Diagnostics.Debug.WriteLine($"URL[{i}]: {imagenUrls[i]?.Length} caracteres");
                    }
                }

                // Validaciones de entrada
                if (imagenUrls == null || imagenUrls.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Debe proporcionar al menos una URL de imagen");
                }

                if (id <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "El ID del producto debe ser mayor a 0");
                }

                // Limite de URLs por request
                const int maxUrls = 20;
                if (imagenUrls.Count > maxUrls)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, $"Se permiten hasta {maxUrls} imágenes por petición");
                }

                // Validación temprana de URLs muy largas
                for (int i = 0; i < imagenUrls.Count; i++)
                {
                    if (imagenUrls[i] != null && imagenUrls[i].Length > 1000)
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, $"URL en posición {i} excede la longitud máxima de 1000 caracteres. Longitud actual: {imagenUrls[i].Length}");
                    }
                }

                // Normalizar y validar URLs
                var urlsValidas = new List<string>();
                var urlsInvalidas = new List<string>();
                var errores = new List<object>();

                for (int i = 0; i < imagenUrls.Count; i++)
                {
                    var url = imagenUrls[i]?.Trim();
                    
                    if (string.IsNullOrEmpty(url))
                    {
                        errores.Add(new { field = $"imagenUrls[{i}]", error = "URL no puede estar vacía" });
                        continue;
                    }

                    if (url.Length > 1000)
                    {
                        errores.Add(new { field = $"imagenUrls[{i}]", error = "URL excede la longitud máxima de 1000 caracteres" });
                        urlsInvalidas.Add(url);
                        continue;
                    }

                    if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                    {
                        errores.Add(new { field = $"imagenUrls[{i}]", error = "URL debe comenzar con http:// o https://" });
                        urlsInvalidas.Add(url);
                        continue;
                    }

                    if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                    {
                        errores.Add(new { field = $"imagenUrls[{i}]", error = "URL tiene formato inválido" });
                        urlsInvalidas.Add(url);
                        continue;
                    }

                    urlsValidas.Add(url);
                }

                // Si hay URLs invalidas, devuelve error 400 con detalles
                if (errores.Count > 0)
                {
                    var errorResponse = new
                    {
                        message = "Datos inválidos",
                        errors = errores
                    };
                    return Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
                }

                // Lógica de negocio
                ImagenNegocio imagenNegocio = new ImagenNegocio();
                
                // Validar que el producto existe
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                Articulo producto = articuloNegocio.obtenerArticuloPorId(id);
                if (producto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Producto no encontrado");
                }
                
                // Obtener imágenes existentes para detectar duplicados
                var imagenesExistentes = imagenNegocio.listarImagenesPorArticulo(id);
                var urlsExistentes = imagenesExistentes.Select(i => i.ImagenUrl).ToList();
                
                // Usar el nuevo método con detección de duplicados
                var resultado = imagenNegocio.agregarImagenesPorProducto(id, urlsValidas);

                // Respuesta estructurada
                var response = new
                {
                    productoId = id,
                    totalSolicitadas = urlsValidas.Count,
                    agregadas = resultado.Agregadas.Count,
                    duplicadas = resultado.Duplicadas.Count,
                    urlsAgregadas = resultado.Agregadas,
                    urlsDuplicadas = resultado.Duplicadas
                };

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (ArgumentException ex) when (ex.Message.Contains("No se encontró el producto"))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Producto no encontrado");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error interno del servidor: " + ex.Message);
            }
        }
    }
}
