using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dominio;

namespace Negocio
{
    public class ResultadoAgregarImagenes
    {
        public List<string> Agregadas { get; set; } = new List<string>();
        public List<string> Duplicadas { get; set; } = new List<string>();
    }

    public class ImagenNegocio
    {
        public List<Imagen> listarImagenesPorArticulo(int idArticulo)
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo = @IdArticulo");
                datos.SetearParametro("@IdArticulo", idArticulo);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarImagen(Imagen nuevaImagen)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                datos.SetearParametro("@IdArticulo", nuevaImagen.IdArticulo);
                datos.SetearParametro("@ImagenUrl", nuevaImagen.ImagenUrl);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificarImagen(Imagen imagenModificada)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("UPDATE IMAGENES SET ImagenUrl = @ImagenUrl WHERE Id = @Id");
                datos.SetearParametro("@Id", imagenModificada.Id);
                datos.SetearParametro("@ImagenUrl", imagenModificada.ImagenUrl);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarImagen(int idImagen)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("DELETE FROM IMAGENES WHERE Id = @Id");
                datos.SetearParametro("@Id", idImagen);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarImagenesPorArticulo(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.SetearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @IdArticulo");
                datos.SetearParametro("@IdArticulo", idArticulo);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool validarUrlImagen(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return url.StartsWith("http://") || url.StartsWith("https://");
        }

        public ResultadoAgregarImagenes agregarImagenesPorProducto(int idProducto, List<string> imagenUrls)
        {
            AccesoDatos datos = new AccesoDatos();
            var resultado = new ResultadoAgregarImagenes();

            try
            {
                // Validar que el producto existe
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                Articulo producto = articuloNegocio.obtenerArticuloPorId(idProducto);
                
                if (producto == null)
                {
                    throw new ArgumentException($"No se encontró el producto con ID: {idProducto}");
                }

                // Verificar URLs existentes para detectar duplicados
                var imagenesExistentes = listarImagenesPorArticulo(idProducto);
                var urlsExistentes = imagenesExistentes.Select(i => i.ImagenUrl).ToList();

                // Separar URLs nuevas de duplicadas
                var urlsNuevas = new List<string>();
                var urlsDuplicadas = new List<string>();

                foreach (string url in imagenUrls)
                {
                    if (urlsExistentes.Contains(url))
                    {
                        urlsDuplicadas.Add(url);
                    }
                    else
                    {
                        urlsNuevas.Add(url);
                    }
                }

                // Agregar solo las URLs nuevas
                foreach (string url in urlsNuevas)
                {
                    Imagen nuevaImagen = new Imagen();
                    nuevaImagen.IdArticulo = idProducto;
                    nuevaImagen.ImagenUrl = url;

                    agregarImagen(nuevaImagen);
                }

                resultado.Agregadas = urlsNuevas;
                resultado.Duplicadas = urlsDuplicadas;

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic agregarImagenesPorProductoConEstadisticas(int idProducto, List<string> imagenUrls)
        {
            int agregadas = 0;
            int duplicadas = 0;
            var urlsAgregadas = new List<string>();
            var urlsDuplicadas = new List<string>();

            try
            {
                // Validar que el producto existe
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                Articulo producto = articuloNegocio.obtenerArticuloPorId(idProducto);
                
                if (producto == null)
                {
                    throw new ArgumentException($"No se encontró el producto con ID: {idProducto}");
                }

                // Obtener imágenes existentes para detectar duplicados
                var imagenesExistentes = listarImagenesPorArticulo(idProducto);
                var urlsExistentes = imagenesExistentes.Select(i => i.ImagenUrl).ToList();

                // Procesar cada URL
                foreach (string url in imagenUrls)
                {
                    if (urlsExistentes.Contains(url))
                    {
                        duplicadas++;
                        urlsDuplicadas.Add(url);
                    }
                    else
                    {

                        Imagen nuevaImagen = new Imagen();
                        nuevaImagen.IdArticulo = idProducto;
                        nuevaImagen.ImagenUrl = url;

                        agregarImagen(nuevaImagen);
                        
                        agregadas++;
                        urlsAgregadas.Add(url);
                        urlsExistentes.Add(url);
                    }
                }

                return new
                {
                    Agregadas = agregadas,
                    Duplicadas = duplicadas,
                    UrlsAgregadas = urlsAgregadas,
                    UrlsDuplicadas = urlsDuplicadas
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
