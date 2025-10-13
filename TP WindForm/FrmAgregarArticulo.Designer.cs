namespace TP_WindForm
{
    partial class FrmAgregarArticulo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblCodArticulo = new System.Windows.Forms.Label();
            this.txtCodigoArticulo = new System.Windows.Forms.TextBox();
            this.LblNombreArticulo = new System.Windows.Forms.Label();
            this.LblDescripcion = new System.Windows.Forms.Label();
            this.LblMarcaArticulo = new System.Windows.Forms.Label();
            this.LblCategoriaArticulo = new System.Windows.Forms.Label();
            this.LblPrecioArticulo = new System.Windows.Forms.Label();
            this.txtNombreArticulo = new System.Windows.Forms.TextBox();
            this.txtDescripcionArticulo = new System.Windows.Forms.TextBox();
            this.cboMarcaArticulo = new System.Windows.Forms.ComboBox();
            this.cboCategoriaArticulo = new System.Windows.Forms.ComboBox();
            this.txtPrecioArticulo = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.PtbImagenArticulo = new System.Windows.Forms.PictureBox();
            this.LblImagenArticulo = new System.Windows.Forms.Label();
            this.btnAnteriorImagen = new System.Windows.Forms.Button();
            this.lblContadorImagenes = new System.Windows.Forms.Label();
            this.btnSiguienteImagen = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PtbImagenArticulo)).BeginInit();
            this.SuspendLayout();
            // 
            // LblCodArticulo
            // 
            this.LblCodArticulo.AutoSize = true;
            this.LblCodArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCodArticulo.Location = new System.Drawing.Point(53, 35);
            this.LblCodArticulo.Name = "LblCodArticulo";
            this.LblCodArticulo.Size = new System.Drawing.Size(54, 16);
            this.LblCodArticulo.TabIndex = 0;
            this.LblCodArticulo.Text = "Código ";
            // 
            // txtCodigoArticulo
            // 
            this.txtCodigoArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoArticulo.Location = new System.Drawing.Point(113, 29);
            this.txtCodigoArticulo.Name = "txtCodigoArticulo";
            this.txtCodigoArticulo.Size = new System.Drawing.Size(70, 22);
            this.txtCodigoArticulo.TabIndex = 0;
            // 
            // LblNombreArticulo
            // 
            this.LblNombreArticulo.AutoSize = true;
            this.LblNombreArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNombreArticulo.Location = new System.Drawing.Point(51, 75);
            this.LblNombreArticulo.Name = "LblNombreArticulo";
            this.LblNombreArticulo.Size = new System.Drawing.Size(56, 16);
            this.LblNombreArticulo.TabIndex = 2;
            this.LblNombreArticulo.Text = "Nombre";
            // 
            // LblDescripcion
            // 
            this.LblDescripcion.AutoSize = true;
            this.LblDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDescripcion.Location = new System.Drawing.Point(28, 115);
            this.LblDescripcion.Name = "LblDescripcion";
            this.LblDescripcion.Size = new System.Drawing.Size(79, 16);
            this.LblDescripcion.TabIndex = 3;
            this.LblDescripcion.Text = "Descripción";
            // 
            // LblMarcaArticulo
            // 
            this.LblMarcaArticulo.AutoSize = true;
            this.LblMarcaArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMarcaArticulo.Location = new System.Drawing.Point(62, 155);
            this.LblMarcaArticulo.Name = "LblMarcaArticulo";
            this.LblMarcaArticulo.Size = new System.Drawing.Size(45, 16);
            this.LblMarcaArticulo.TabIndex = 4;
            this.LblMarcaArticulo.Text = "Marca";
            // 
            // LblCategoriaArticulo
            // 
            this.LblCategoriaArticulo.AutoSize = true;
            this.LblCategoriaArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCategoriaArticulo.Location = new System.Drawing.Point(41, 195);
            this.LblCategoriaArticulo.Name = "LblCategoriaArticulo";
            this.LblCategoriaArticulo.Size = new System.Drawing.Size(66, 16);
            this.LblCategoriaArticulo.TabIndex = 5;
            this.LblCategoriaArticulo.Text = "Categoría";
            // 
            // LblPrecioArticulo
            // 
            this.LblPrecioArticulo.AutoSize = true;
            this.LblPrecioArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPrecioArticulo.Location = new System.Drawing.Point(61, 235);
            this.LblPrecioArticulo.Name = "LblPrecioArticulo";
            this.LblPrecioArticulo.Size = new System.Drawing.Size(46, 16);
            this.LblPrecioArticulo.TabIndex = 6;
            this.LblPrecioArticulo.Text = "Precio";
            // 
            // txtNombreArticulo
            // 
            this.txtNombreArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreArticulo.Location = new System.Drawing.Point(113, 68);
            this.txtNombreArticulo.Name = "txtNombreArticulo";
            this.txtNombreArticulo.Size = new System.Drawing.Size(162, 22);
            this.txtNombreArticulo.TabIndex = 1;
            // 
            // txtDescripcionArticulo
            // 
            this.txtDescripcionArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcionArticulo.Location = new System.Drawing.Point(113, 107);
            this.txtDescripcionArticulo.Name = "txtDescripcionArticulo";
            this.txtDescripcionArticulo.Size = new System.Drawing.Size(303, 22);
            this.txtDescripcionArticulo.TabIndex = 2;
            // 
            // cboMarcaArticulo
            // 
            this.cboMarcaArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMarcaArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMarcaArticulo.FormattingEnabled = true;
            this.cboMarcaArticulo.Location = new System.Drawing.Point(113, 146);
            this.cboMarcaArticulo.Name = "cboMarcaArticulo";
            this.cboMarcaArticulo.Size = new System.Drawing.Size(162, 24);
            this.cboMarcaArticulo.TabIndex = 3;
            // 
            // cboCategoriaArticulo
            // 
            this.cboCategoriaArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategoriaArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategoriaArticulo.FormattingEnabled = true;
            this.cboCategoriaArticulo.Location = new System.Drawing.Point(113, 187);
            this.cboCategoriaArticulo.Name = "cboCategoriaArticulo";
            this.cboCategoriaArticulo.Size = new System.Drawing.Size(162, 24);
            this.cboCategoriaArticulo.TabIndex = 4;
            // 
            // txtPrecioArticulo
            // 
            this.txtPrecioArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioArticulo.Location = new System.Drawing.Point(113, 228);
            this.txtPrecioArticulo.Name = "txtPrecioArticulo";
            this.txtPrecioArticulo.Size = new System.Drawing.Size(123, 22);
            this.txtPrecioArticulo.TabIndex = 5;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(216, 361);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(145, 38);
            this.btnAceptar.TabIndex = 7;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(475, 361);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(145, 38);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cerrar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // PtbImagenArticulo
            // 
            this.PtbImagenArticulo.Location = new System.Drawing.Point(475, 29);
            this.PtbImagenArticulo.MaximumSize = new System.Drawing.Size(311, 262);
            this.PtbImagenArticulo.MinimumSize = new System.Drawing.Size(311, 262);
            this.PtbImagenArticulo.Name = "PtbImagenArticulo";
            this.PtbImagenArticulo.Size = new System.Drawing.Size(311, 262);
            this.PtbImagenArticulo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PtbImagenArticulo.TabIndex = 14;
            this.PtbImagenArticulo.TabStop = false;
            // 
            // LblImagenArticulo
            // 
            this.LblImagenArticulo.AutoSize = true;
            this.LblImagenArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblImagenArticulo.Location = new System.Drawing.Point(53, 275);
            this.LblImagenArticulo.Name = "LblImagenArticulo";
            this.LblImagenArticulo.Size = new System.Drawing.Size(52, 16);
            this.LblImagenArticulo.TabIndex = 7;
            this.LblImagenArticulo.Text = "Imagen";
            // 
            // btnAnteriorImagen
            // 
            this.btnAnteriorImagen.Location = new System.Drawing.Point(475, 297);
            this.btnAnteriorImagen.Name = "btnAnteriorImagen";
            this.btnAnteriorImagen.Size = new System.Drawing.Size(75, 23);
            this.btnAnteriorImagen.TabIndex = 18;
            this.btnAnteriorImagen.Text = "Anterior";
            this.btnAnteriorImagen.UseVisualStyleBackColor = true;
            this.btnAnteriorImagen.Click += new System.EventHandler(this.btnAnteriorImagen_Click);
            // 
            // lblContadorImagenes
            // 
            this.lblContadorImagenes.AutoSize = true;
            this.lblContadorImagenes.Location = new System.Drawing.Point(612, 307);
            this.lblContadorImagenes.Name = "lblContadorImagenes";
            this.lblContadorImagenes.Size = new System.Drawing.Size(37, 13);
            this.lblContadorImagenes.TabIndex = 20;
            this.lblContadorImagenes.Text = "1 de 1";
            this.lblContadorImagenes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSiguienteImagen
            // 
            this.btnSiguienteImagen.Location = new System.Drawing.Point(711, 297);
            this.btnSiguienteImagen.Name = "btnSiguienteImagen";
            this.btnSiguienteImagen.Size = new System.Drawing.Size(75, 23);
            this.btnSiguienteImagen.TabIndex = 21;
            this.btnSiguienteImagen.Text = "Siguiente";
            this.btnSiguienteImagen.UseVisualStyleBackColor = true;
            this.btnSiguienteImagen.Click += new System.EventHandler(this.btnSiguienteImagen_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(113, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Agregar/Eliminar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnAgregarImagen_Click);
            // 
            // FrmAgregarArticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSiguienteImagen);
            this.Controls.Add(this.lblContadorImagenes);
            this.Controls.Add(this.btnAnteriorImagen);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.PtbImagenArticulo);
            this.Controls.Add(this.txtPrecioArticulo);
            this.Controls.Add(this.cboCategoriaArticulo);
            this.Controls.Add(this.cboMarcaArticulo);
            this.Controls.Add(this.txtDescripcionArticulo);
            this.Controls.Add(this.txtNombreArticulo);
            this.Controls.Add(this.LblImagenArticulo);
            this.Controls.Add(this.LblPrecioArticulo);
            this.Controls.Add(this.LblCategoriaArticulo);
            this.Controls.Add(this.LblMarcaArticulo);
            this.Controls.Add(this.LblDescripcion);
            this.Controls.Add(this.LblNombreArticulo);
            this.Controls.Add(this.txtCodigoArticulo);
            this.Controls.Add(this.LblCodArticulo);
            this.MaximumSize = new System.Drawing.Size(850, 489);
            this.MinimumSize = new System.Drawing.Size(850, 489);
            this.Name = "FrmAgregarArticulo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuevo Articulo";
            this.Load += new System.EventHandler(this.FrmAgregarArticulo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PtbImagenArticulo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblCodArticulo;
        private System.Windows.Forms.TextBox txtCodigoArticulo;
        private System.Windows.Forms.Label LblNombreArticulo;
        private System.Windows.Forms.Label LblDescripcion;
        private System.Windows.Forms.Label LblMarcaArticulo;
        private System.Windows.Forms.Label LblCategoriaArticulo;
        private System.Windows.Forms.Label LblPrecioArticulo;
        private System.Windows.Forms.TextBox txtNombreArticulo;
        private System.Windows.Forms.TextBox txtDescripcionArticulo;
        private System.Windows.Forms.ComboBox cboMarcaArticulo;
        private System.Windows.Forms.ComboBox cboCategoriaArticulo;
        private System.Windows.Forms.TextBox txtPrecioArticulo;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.PictureBox PtbImagenArticulo;
        private System.Windows.Forms.Label LblImagenArticulo;
        private System.Windows.Forms.Button btnAnteriorImagen;
        private System.Windows.Forms.Label lblContadorImagenes;
        private System.Windows.Forms.Button btnSiguienteImagen;
        private System.Windows.Forms.Button button1;
    }
}