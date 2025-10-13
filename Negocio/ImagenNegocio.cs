using System;
using System.Collections.Generic;
using System.Data;
using Dominio;

namespace Negocio
{
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

        public void agregarImagenesPorProducto(int idProducto, List<string> imagenUrls)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Validar que el producto existe
                ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                Articulo producto = articuloNegocio.obtenerArticuloPorId(idProducto);
                
                if (producto == null)
                {
                    throw new Exception($"No se encontró el producto con ID: {idProducto}");
                }

                // Validar y agregar cada imagen
                foreach (string url in imagenUrls)
                {
                    if (!validarUrlImagen(url))
                    {
                        throw new Exception($"URL de imagen inválida: {url}");
                    }

                    Imagen nuevaImagen = new Imagen();
                    nuevaImagen.IdArticulo = idProducto;
                    nuevaImagen.ImagenUrl = url;

                    agregarImagen(nuevaImagen);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
