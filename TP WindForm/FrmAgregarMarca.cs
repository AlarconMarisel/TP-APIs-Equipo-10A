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
    public partial class FrmAgregarMarca : Form
    {
        private Marca marca = null;
        public FrmAgregarMarca()
        {
            InitializeComponent();
        }
        public FrmAgregarMarca(Marca marca)
        {
            InitializeComponent();
            this.marca = marca;
            txtNombreMarca.Text = marca.Descripcion;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();

            string nuevoNombre = txtNombreMarca.Text.Trim();

            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                MessageBox.Show("Debe ingresar un nombre para la Marca.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreMarca.Focus();
                return;
            }


            try
            {
                if (marca == null) 
                {
                    Marca nuevo = new Marca();
                    nuevo.Descripcion = txtNombreMarca.Text;
                    negocio.agregarMarca(nuevo);
                    MessageBox.Show("Agregado exitosamente");
                }
                else 
                {
                    if (marca.Descripcion == nuevoNombre)
                    {
                        MessageBox.Show("No se realizaron cambios en la Marca.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;

                    }

                        marca.Descripcion = txtNombreMarca.Text;
                    negocio.modificarMarca(marca);                
                    MessageBox.Show("Modificado exitosamente");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
