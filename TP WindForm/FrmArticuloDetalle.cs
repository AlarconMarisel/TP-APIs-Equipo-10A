using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TP_WindForm
{
    public partial class FrmArticuloDetalle : Form
    {
        private Articulo articulo;
        private int indiceImagenActual = 0;

        public FrmArticuloDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void FrmArticuloDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                lblTituloArticulo.Text = articulo.NombreArticulo;
                this.Text = $"Detalles - {articulo.NombreArticulo}";

                CargarDatosArticulo();

                CargarImagen();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los detalles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatosArticulo()
        {
            try
            {
                txtCodigo.Text = articulo.CodigoArticulo;
                txtNombre.Text = articulo.NombreArticulo;
                txtDescripcion.Text = articulo.DescripcionArticulo;
                txtMarca.Text = articulo.MarcaArticulo.Descripcion;
                txtCategoria.Text = articulo.CategoriaArticulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString("C");
                txtId.Text = articulo.IdArticulo.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del artículo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarImagen()
        {
            try
            {
                if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
                {
                    if (indiceImagenActual >= 0 && indiceImagenActual < articulo.Imagenes.Count)
                    {
                        string urlImagen = articulo.Imagenes[indiceImagenActual].ImagenUrl;
                        if (!string.IsNullOrWhiteSpace(urlImagen) && Uri.IsWellFormedUriString(urlImagen, UriKind.Absolute))
                        {
                            pictureBoxImagen.Load(urlImagen);
                            pictureBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                        else
                        {
                            MostrarSinImagen();
                        }
                    }
                    else
                    {
                        MostrarSinImagen();
                    }

                    lblContadorImagenes.Text = $"{indiceImagenActual + 1} de {articulo.Imagenes.Count}";

                    btnAnterior.Enabled = indiceImagenActual > 0;
                    btnSiguiente.Enabled = indiceImagenActual < articulo.Imagenes.Count - 1;
                }
                else
                {
                    MostrarSinImagen();
                    lblContadorImagenes.Text = "Sin imágenes";
                    btnAnterior.Enabled = false;
                    btnSiguiente.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MostrarSinImagen();
                lblContadorImagenes.Text = "Error al cargar";
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
            }
        }

        private void MostrarSinImagen()
        {
            try
            {
                pictureBoxImagen.Image = null;
                pictureBoxImagen.BackColor = Color.LightGray;
                
                pictureBoxImagen.Controls.Clear();
                
                Label lblSinImagen = new Label();
                lblSinImagen.Text = "No hay imágenes\ndel artículo";
                lblSinImagen.Font = new Font("Arial", 12, FontStyle.Italic);
                lblSinImagen.ForeColor = Color.Gray;
                lblSinImagen.TextAlign = ContentAlignment.MiddleCenter;
                lblSinImagen.Dock = DockStyle.Fill;
                lblSinImagen.Name = "lblSinImagen";
                lblSinImagen.AutoSize = false;

                pictureBoxImagen.Controls.Add(lblSinImagen);
            }
            catch (Exception ex)
            {
                pictureBoxImagen.Image = null;
                pictureBoxImagen.BackColor = Color.LightGray;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
                {
                    if (indiceImagenActual > 0)
                    {
                        indiceImagenActual--;
                        CargarImagen();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al navegar a la imagen anterior: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (articulo.Imagenes != null && articulo.Imagenes.Count > 0)
                {
                    if (indiceImagenActual < articulo.Imagenes.Count - 1)
                    {
                        indiceImagenActual++;
                        CargarImagen();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al navegar a la imagen siguiente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}