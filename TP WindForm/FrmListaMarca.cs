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
    public partial class FrmListaMarca : Form
    {
        public FrmListaMarca()
        {
            InitializeComponent();
        }
        private List<Marca> listaMarcas;

        private void FrmListaMarca_Load_1(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            MarcaNegocio datos = new MarcaNegocio();
            try
            {
                listaMarcas = datos.listarMarca();
                ocultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()
        {
            DgvListaDeMarcas.DataSource = listaMarcas;
            DgvListaDeMarcas.Columns["Id"].Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAgregarMarca alta = new FrmAgregarMarca();
            alta.ShowDialog();
            cargar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Marca seleccionado;
            seleccionado = (Marca)DgvListaDeMarcas.CurrentRow.DataBoundItem;

            FrmAgregarMarca modificar = new FrmAgregarMarca(seleccionado);
            modificar.ShowDialog();
            cargar();

        }
        private void Eliminar_Click(object sender, EventArgs e)
        {
            try
            {

                Marca seleccionado = (Marca)DgvListaDeMarcas.CurrentRow.DataBoundItem;

                var confirm = MessageBox.Show("¿Está seguro de eliminar esta marca?",
                                              "Confirmar eliminación",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.eliminarMarca(seleccionado);
                    MessageBox.Show("Marca eliminada correctamente.");
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBuscarMarca_Click(object sender, EventArgs e)
        {
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Marca> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro.Length >= 3)
            {
                listaFiltrada = listaMarcas.FindAll(x => x.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaMarcas;
            }

            DgvListaDeMarcas.DataSource = null;
            DgvListaDeMarcas.DataSource = listaFiltrada;
            DgvListaDeMarcas.Columns["Id"].Visible = false;


        }

        private void lblFiltroMarca_Click(object sender, EventArgs e)
        {

        }
    }
}
