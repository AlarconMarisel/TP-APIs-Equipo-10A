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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listadoDeArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmArticuloListado formArticulos = new FrmArticuloListado();
            //formArticulos.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaMarca formMarca = new FrmListaMarca();
           {
                formMarca.ShowDialog();
            }
          
        }
        private void articulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmArticuloListado formArticulos = new FrmArticuloListado();
            formArticulos.ShowDialog();

        }

        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaCategoria formCategoria = new FrmListaCategoria();
            formCategoria.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Saliendo del Sistema, ¿Esta seguro que desea Salir?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resultado == DialogResult.Yes)
            {
                Close();
            }
        }
    }

}
