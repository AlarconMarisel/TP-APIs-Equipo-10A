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
    public partial class FrmAgregarImagen : Form
    {
        private Articulo articulo;
        private Imagen imagen;
        private ImagenNegocio imagenNegocio;

        public FrmAgregarImagen()
        {
            InitializeComponent();
            imagenNegocio = new ImagenNegocio();
        }

        public FrmAgregarImagen(Articulo articulo) : this()
        {
            this.articulo = articulo;
            Text = $"Gestionar Imágenes - {articulo.NombreArticulo}";
        }

        public FrmAgregarImagen(Articulo articulo, Imagen imagen) : this(articulo)
        {
            this.imagen = imagen;
            Text = "Modificar Imagen";
        }

        private void FrmAgregarImagen_Load(object sender, EventArgs e)
        {
            try
            {
                txtUrlImagen.Text = "";
                btnAgregar.Text = "Agregar";
                btnEliminar.Visible = false;
                LimpiarPictureBox();

                if (articulo != null)
                {
                    CargarImagenes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarImagenes()
        {
            try
            {
                if (articulo.Imagenes == null)
                {
                    articulo.Imagenes = imagenNegocio.listarImagenesPorArticulo(articulo.IdArticulo);
                }

                if (articulo.Imagenes == null)
                {
                    articulo.Imagenes = new List<Imagen>();
                }

                dgvImagenes.DataSource = articulo.Imagenes;
                
                if (dgvImagenes.Columns.Count > 0)
                {
                    if (dgvImagenes.Columns["Id"] != null)
                        dgvImagenes.Columns["Id"].Visible = false;
                    if (dgvImagenes.Columns["IdArticulo"] != null)
                        dgvImagenes.Columns["IdArticulo"].Visible = false;
                    if (dgvImagenes.Columns["ImagenUrl"] != null)
                    {
                        dgvImagenes.Columns["ImagenUrl"].HeaderText = "URL de Imagen";
                        dgvImagenes.Columns["ImagenUrl"].Width = 300;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar imágenes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUrlImagen.Text))
                {
                    MessageBox.Show("Por favor ingrese una URL de imagen válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!imagenNegocio.validarUrlImagen(txtUrlImagen.Text))
                {
                    MessageBox.Show("La URL ingresada no es válida. Debe ser una URL HTTP o HTTPS.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (articulo == null || articulo.IdArticulo <= 0)
                {
                    MessageBox.Show("Error: No se puede agregar imagen. El artículo no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Usar el nuevo método con detección de duplicados
                var urlsParaAgregar = new List<string> { txtUrlImagen.Text.Trim() };
                var resultado = imagenNegocio.agregarImagenesPorProducto(articulo.IdArticulo, urlsParaAgregar);

                // Mostrar resultado al usuario
                string mensaje = "";
                
                if (resultado.Agregadas.Count > 0)
                {
                    mensaje += $"✅ Se agregaron {resultado.Agregadas.Count} imagen(es) nueva(s).\n";
                }
                
                if (resultado.Duplicadas.Count > 0)
                {
                    mensaje += "No se permiten URLs repetidas.\n";
                }

                if (resultado.Agregadas.Count == 0 && resultado.Duplicadas.Count > 0)
                {
                    MessageBox.Show(mensaje, "Imágenes Duplicadas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                txtUrlImagen.Clear();
                articulo.Imagenes = null;
                CargarImagenes();
                imagen = null;
                LimpiarPictureBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (imagen != null)
                {
                    DialogResult resultado = MessageBox.Show("¿Está seguro que desea eliminar esta imagen?", 
                                                           "Confirmar eliminación", 
                                                           MessageBoxButtons.YesNo, 
                                                           MessageBoxIcon.Warning);

                    if (resultado == DialogResult.Yes)
                    {
                        imagenNegocio.eliminarImagen(imagen.Id);
                        MessageBox.Show("Imagen eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txtUrlImagen.Clear();
                        articulo.Imagenes = null;
                        CargarImagenes();
                        btnAgregar.Text = "Agregar";
                        imagen = null;
                        LimpiarPictureBox();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvImagenes_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvImagenes.CurrentRow != null && dgvImagenes.CurrentRow.DataBoundItem != null)
                {
                    Imagen imagenSeleccionada = (Imagen)dgvImagenes.CurrentRow.DataBoundItem;
                    imagen = imagenSeleccionada;
                    btnEliminar.Visible = true;
                    MostrarImagenSeleccionada(imagenSeleccionada.ImagenUrl);
                }
                else
                {
                    btnEliminar.Visible = false;
                    LimpiarPictureBox();
                }
            }
            catch (Exception ex)
            {
                btnEliminar.Visible = false;
                LimpiarPictureBox();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUrlImagen.Clear();
            imagen = null;
            btnEliminar.Visible = false;
            dgvImagenes.ClearSelection();
            LimpiarPictureBox();
        }

        private void MostrarImagenSeleccionada(string urlImagen)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(urlImagen) && Uri.IsWellFormedUriString(urlImagen, UriKind.Absolute))
                {
                    pictureBoxImagen.Load(urlImagen);
                    pictureBoxImagen.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    LimpiarPictureBox();
                }
            }
            catch (Exception ex)
            {
                LimpiarPictureBox();
            }
        }

        private void LimpiarPictureBox()
        {
            pictureBoxImagen.Image = null;
            pictureBoxImagen.BackColor = Color.LightGray;
        }
    }
}
