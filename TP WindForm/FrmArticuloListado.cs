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
    public partial class FrmArticuloListado : Form
    {   
        List<Articulo> listaArticulos;
        public FrmArticuloListado()
        {
            InitializeComponent();
        }

        private void FrmArticuloListado_Load(object sender, EventArgs e)
        {
            CargarArticulos();
            cboFiltroArticulos.Items.Add("Código");
            cboFiltroArticulos.Items.Add("Nombre");
            cboFiltroArticulos.Items.Add("Marca");
            cboFiltroArticulos.Items.Add("Categoría");
            cboFiltroArticulos.Visible = false;
        }

        private void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            FrmAgregarArticulo agregar = new FrmAgregarArticulo();
            agregar.ShowDialog();
            CargarArticulos();
        }

        private void btnModificarArticulo_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            FrmAgregarArticulo modificar = new FrmAgregarArticulo(seleccionado);
            modificar.ShowDialog(); 
            CargarArticulos();
        }

        private void btnEliminarArticulo_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult resultado = MessageBox.Show("¿Esta seguro que desea eliminar este Articulo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {              
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminarArticulo(seleccionado.IdArticulo);
                    CargarArticulos();
                }
            }
            catch (Exception ex)    
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void CargarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulos = negocio.listarArticulo();
                dgvArticulos.DataSource = listaArticulos;
                OcultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OcultarColumnas()
        {
            dgvArticulos.Columns["IdArticulo"].Visible = false;
            dgvArticulos.Columns["DescripcionArticulo"].Visible = false;

        }

        private void CargarArticulosFiltrados(List<Articulo>listaFiltrada)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = listaFiltrada;
                OcultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string opcion = "";
            string filtro = txtFiltro.Text.ToLower();

            if(cboFiltroArticulos.SelectedItem != null)
            {
                opcion = cboFiltroArticulos.SelectedItem.ToString();
            }    
            

            try
            {

                if (filtro != "")
                {

                    if (chbFiltroAvanzado.Checked == false)
                    {               
                        listaFiltrada = listaArticulos.FindAll(x => x.CodigoArticulo.ToLower().Contains(txtFiltro.Text.ToLower()) || x.NombreArticulo.ToLower().Contains(txtFiltro.Text.ToLower()) || x.MarcaArticulo.Descripcion.ToLower().Contains(txtFiltro.Text.ToLower()) || x.CategoriaArticulo.Descripcion.ToLower().Contains(txtFiltro.Text.ToLower()));
                        CargarArticulosFiltrados(listaFiltrada);
                        return;
                    }

                    switch (opcion)
                    {
                        case "Código":

                            listaFiltrada = listaArticulos.FindAll(x => x.CodigoArticulo.ToLower().Contains(filtro));
                            CargarArticulosFiltrados(listaFiltrada);
                            break;
                        case "Nombre":

                            listaFiltrada = listaArticulos.FindAll(x => x.NombreArticulo.ToLower().Contains(filtro));
                            CargarArticulosFiltrados(listaFiltrada);
                            break;
                        case "Marca":

                            listaFiltrada = listaArticulos.FindAll(x => x.MarcaArticulo.Descripcion.ToLower().Contains(filtro));
                            CargarArticulosFiltrados(listaFiltrada);
                            break;
                        case "Categoría":

                            listaFiltrada = listaArticulos.FindAll(x => x.CategoriaArticulo.Descripcion.ToLower().Contains(filtro));
                            CargarArticulosFiltrados(listaFiltrada);
                            break;
                        default:

                            txtFiltro.Text = "";
                            MessageBox.Show("Debe seleccionar una opción de filtrado");
                            break;

                    }
                }
                else
                {
                    listaFiltrada = listaArticulos;
                    CargarArticulosFiltrados(listaFiltrada);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private void chbFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            if(chbFiltroAvanzado.Checked==true)
            {
                txtFiltro.Text = "";
                cboFiltroArticulos.Visible = true;
            
            }
            else
            {
                txtFiltro.Text = "";
                cboFiltroArticulos.Visible = false;

            }

        }

        private void btnVerDetalleArticulo_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvArticulos.CurrentRow != null)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo articuloSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    
                    Articulo articuloCompleto = negocio.obtenerArticuloPorId(articuloSeleccionado.IdArticulo);
                    
                    if (articuloCompleto != null)
                    {
                        FrmArticuloDetalle detalle = new FrmArticuloDetalle(articuloCompleto);
                        detalle.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo cargar los detalles del artículo seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un artículo para ver sus detalles.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir los detalles del artículo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
