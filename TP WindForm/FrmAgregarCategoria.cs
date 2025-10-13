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
    public partial class FrmAgregarCategoria : Form
    {
        private Categoria categoria = null;
        public FrmAgregarCategoria()
        {
            InitializeComponent();
            this.Text = "Agregar Categoría";
            btnAceptar.Text = "Agregar";
        }
        public FrmAgregarCategoria(Categoria categoria)
        {
            InitializeComponent();
            this.categoria = categoria;
            txtNombreCategoria.Text = categoria.Descripcion;
            this.Text = "Modificar Categoría";
            btnAceptar.Text = "Modificar";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nuevoNombre = txtNombreCategoria.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                MessageBox.Show("Debe ingresar un nombre para la categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCategoria.Focus();
                return;
            }

            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                if (categoria == null)
                {
                    Categoria nuevo = new Categoria();
                    nuevo.Descripcion = nuevoNombre;
                    negocio.agregarCategoria(nuevo);
                    MessageBox.Show("Categoría agregada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (categoria.Descripcion == nuevoNombre)
                    {
                        MessageBox.Show("No se realizaron cambios en la categoría.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    categoria.Descripcion = nuevoNombre;
                    negocio.modificarCategoria(categoria);                
                    MessageBox.Show("Categoría modificada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
