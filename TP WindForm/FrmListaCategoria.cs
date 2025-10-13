using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TP_WindForm
{
    public partial class FrmListaCategoria : Form
    {
        private DataTable dataTable;
        private BindingSource bindingSource;
        private bool ordenDescendente = false;

        public FrmListaCategoria()
        {
            InitializeComponent();
            configurarDataTable();
            configurarFiltro();
        }

        private void configurarDataTable()
        {            
            dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Descripcion", typeof(string));
          
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;
            DgvCategorias.DataSource = bindingSource;
            
            DgvCategorias.AllowUserToAddRows = false;
        }

        private void cargar()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                List<Categoria> listaCategorias = negocio.listarCategoria();
                               
                dataTable.Clear();
                              
                foreach (Categoria categoria in listaCategorias)
                {
                    dataTable.Rows.Add(categoria.Id, categoria.Descripcion);
                }
                               
                dataTable.DefaultView.Sort = "Descripcion ASC";
                DgvCategorias.Columns["Descripcion"].HeaderText = "Categoría ↑";
                ordenDescendente = false;
                               
                DgvCategorias.Columns["Id"].Visible = false;
                
                DgvCategorias.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FrmListaCategoria_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void BtnAgregarCategoria_Click(object sender, EventArgs e)
        {
            FrmAgregarCategoria agregar = new FrmAgregarCategoria();
            agregar.ShowDialog();
            cargar();
        }

        private void BtnModificarCategoria_Click(object sender, EventArgs e)
        {
            if (DgvCategorias.CurrentRow != null)
            {
                DataRowView rowView = (DataRowView)DgvCategorias.CurrentRow.DataBoundItem;
                Categoria seleccionada = new Categoria();
                seleccionada.Id = (int)rowView["Id"];
                seleccionada.Descripcion = (string)rowView["Descripcion"];
                
                FrmAgregarCategoria modificar = new FrmAgregarCategoria(seleccionada);
                modificar.ShowDialog();
                cargar();
            }
        }

        private void BtnEliminarCategoria_Click(object sender, EventArgs e)
        {
            if (DgvCategorias.CurrentRow != null)
            {
                DataRowView rowView = (DataRowView)DgvCategorias.CurrentRow.DataBoundItem;
                int idCategoria = (int)rowView["Id"];
                string nombreCategoria = (string)rowView["Descripcion"];
                
                CategoriaNegocio negocio = new CategoriaNegocio();
                try
                {
                    DialogResult respuesta = MessageBox.Show($"¿Está seguro que desea eliminar la categoría '{nombreCategoria}'?", 
                                                           "Eliminar Categoría", 
                                                           MessageBoxButtons.YesNo, 
                                                           MessageBoxIcon.Warning);
                    
                    if (respuesta == DialogResult.Yes)
                    {
                        negocio.eliminarCategoria(idCategoria);
                        MessageBox.Show("Categoría eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cargar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DgvCategorias_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == DgvCategorias.Columns["Descripcion"].Index)
            {
                ordenDescendente = !ordenDescendente;
                
                if (ordenDescendente)
                {
                    dataTable.DefaultView.Sort = "Descripcion DESC";
                    DgvCategorias.Columns["Descripcion"].HeaderText = "Categoría ↓";
                }
                else
                {
                    dataTable.DefaultView.Sort = "Descripcion ASC";
                    DgvCategorias.Columns["Descripcion"].HeaderText = "Categoría ↑";
                }
            }
        }

        private void DgvCategorias_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvCategorias.Columns["Descripcion"].Index && e.RowIndex == -1)
            {
                DgvCategorias.Cursor = Cursors.Hand;
            }
        }

        private void DgvCategorias_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvCategorias.Columns["Descripcion"].Index && e.RowIndex == -1)
            {
                DgvCategorias.Cursor = Cursors.Default;
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltro.Text == "Escriba para filtrar categorías...")
            {
                dataTable.DefaultView.RowFilter = "";
                return;
            }

            string filtro = txtFiltro.Text.Trim();
            
            if (string.IsNullOrEmpty(filtro))
            {
                dataTable.DefaultView.RowFilter = "";
            }
            else
            {
                string filtroEscapado = filtro.Replace("'", "''");
                dataTable.DefaultView.RowFilter = $"Descripcion LIKE '%{filtroEscapado}%'";
            }
        }

        private void configurarFiltro()
        {
            txtFiltro.Text = "Escriba para filtrar categorías...";
            txtFiltro.ForeColor = System.Drawing.Color.Gray;
            
            txtFiltro.Enter += txtFiltro_Enter;
            txtFiltro.Leave += txtFiltro_Leave;
        }

        private void txtFiltro_Enter(object sender, EventArgs e)
        {
            if (txtFiltro.Text == "Escriba para filtrar categorías...")
            {
                txtFiltro.Text = "";
                txtFiltro.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtFiltro_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                txtFiltro.Text = "Escriba para filtrar categorías...";
                txtFiltro.ForeColor = System.Drawing.Color.Gray;
            }
        }

    }
}
