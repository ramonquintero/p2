namespace P2
{
    partial class FormListaDeFichas
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.venezuelaDataSet4 = new P2.VenezuelaDataSet4();
            this.infSocialBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.infSocialTableAdapter = new P2.VenezuelaDataSet4TableAdapters.InfSocialTableAdapter();
            this.numExpDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asuntoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.situtacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.justificacionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uniSocFamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alquilerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.luzDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aguaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saludDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.condominioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefonoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.laboralDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viviendaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sanitariaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observacionesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.concederDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mantenerDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.denegarDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.suspenderDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.extinguirDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fregDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuarioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.venezuelaDataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infSocialBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numExpDataGridViewTextBoxColumn,
            this.asuntoDataGridViewTextBoxColumn,
            this.fechaDataGridViewTextBoxColumn,
            this.situtacionDataGridViewTextBoxColumn,
            this.justificacionDataGridViewTextBoxColumn,
            this.uniSocFamDataGridViewTextBoxColumn,
            this.alquilerDataGridViewTextBoxColumn,
            this.luzDataGridViewTextBoxColumn,
            this.aguaDataGridViewTextBoxColumn,
            this.saludDataGridViewTextBoxColumn,
            this.condominioDataGridViewTextBoxColumn,
            this.telefonoDataGridViewTextBoxColumn,
            this.laboralDataGridViewTextBoxColumn,
            this.viviendaDataGridViewTextBoxColumn,
            this.sanitariaDataGridViewTextBoxColumn,
            this.observacionesDataGridViewTextBoxColumn,
            this.concederDataGridViewCheckBoxColumn,
            this.mantenerDataGridViewCheckBoxColumn,
            this.denegarDataGridViewCheckBoxColumn,
            this.suspenderDataGridViewCheckBoxColumn,
            this.extinguirDataGridViewCheckBoxColumn,
            this.fregDataGridViewTextBoxColumn,
            this.usuarioDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.infSocialBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(778, 298);
            this.dataGridView1.TabIndex = 0;
            // 
            // venezuelaDataSet4
            // 
            this.venezuelaDataSet4.DataSetName = "VenezuelaDataSet4";
            this.venezuelaDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // infSocialBindingSource
            // 
            this.infSocialBindingSource.DataMember = "InfSocial";
            this.infSocialBindingSource.DataSource = this.venezuelaDataSet4;
            // 
            // infSocialTableAdapter
            // 
            this.infSocialTableAdapter.ClearBeforeFill = true;
            // 
            // numExpDataGridViewTextBoxColumn
            // 
            this.numExpDataGridViewTextBoxColumn.DataPropertyName = "NumExp";
            this.numExpDataGridViewTextBoxColumn.HeaderText = "NumExp";
            this.numExpDataGridViewTextBoxColumn.Name = "numExpDataGridViewTextBoxColumn";
            this.numExpDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // asuntoDataGridViewTextBoxColumn
            // 
            this.asuntoDataGridViewTextBoxColumn.DataPropertyName = "Asunto";
            this.asuntoDataGridViewTextBoxColumn.HeaderText = "Asunto";
            this.asuntoDataGridViewTextBoxColumn.Name = "asuntoDataGridViewTextBoxColumn";
            this.asuntoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            this.fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            this.fechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
            this.fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            this.fechaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // situtacionDataGridViewTextBoxColumn
            // 
            this.situtacionDataGridViewTextBoxColumn.DataPropertyName = "Situtacion";
            this.situtacionDataGridViewTextBoxColumn.HeaderText = "Situtacion";
            this.situtacionDataGridViewTextBoxColumn.Name = "situtacionDataGridViewTextBoxColumn";
            this.situtacionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // justificacionDataGridViewTextBoxColumn
            // 
            this.justificacionDataGridViewTextBoxColumn.DataPropertyName = "Justificacion";
            this.justificacionDataGridViewTextBoxColumn.HeaderText = "Justificacion";
            this.justificacionDataGridViewTextBoxColumn.Name = "justificacionDataGridViewTextBoxColumn";
            this.justificacionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // uniSocFamDataGridViewTextBoxColumn
            // 
            this.uniSocFamDataGridViewTextBoxColumn.DataPropertyName = "UniSocFam";
            this.uniSocFamDataGridViewTextBoxColumn.HeaderText = "UniSocFam";
            this.uniSocFamDataGridViewTextBoxColumn.Name = "uniSocFamDataGridViewTextBoxColumn";
            this.uniSocFamDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // alquilerDataGridViewTextBoxColumn
            // 
            this.alquilerDataGridViewTextBoxColumn.DataPropertyName = "alquiler";
            this.alquilerDataGridViewTextBoxColumn.HeaderText = "alquiler";
            this.alquilerDataGridViewTextBoxColumn.Name = "alquilerDataGridViewTextBoxColumn";
            this.alquilerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // luzDataGridViewTextBoxColumn
            // 
            this.luzDataGridViewTextBoxColumn.DataPropertyName = "luz";
            this.luzDataGridViewTextBoxColumn.HeaderText = "luz";
            this.luzDataGridViewTextBoxColumn.Name = "luzDataGridViewTextBoxColumn";
            this.luzDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aguaDataGridViewTextBoxColumn
            // 
            this.aguaDataGridViewTextBoxColumn.DataPropertyName = "agua";
            this.aguaDataGridViewTextBoxColumn.HeaderText = "agua";
            this.aguaDataGridViewTextBoxColumn.Name = "aguaDataGridViewTextBoxColumn";
            this.aguaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // saludDataGridViewTextBoxColumn
            // 
            this.saludDataGridViewTextBoxColumn.DataPropertyName = "salud";
            this.saludDataGridViewTextBoxColumn.HeaderText = "salud";
            this.saludDataGridViewTextBoxColumn.Name = "saludDataGridViewTextBoxColumn";
            this.saludDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // condominioDataGridViewTextBoxColumn
            // 
            this.condominioDataGridViewTextBoxColumn.DataPropertyName = "condominio";
            this.condominioDataGridViewTextBoxColumn.HeaderText = "condominio";
            this.condominioDataGridViewTextBoxColumn.Name = "condominioDataGridViewTextBoxColumn";
            this.condominioDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // telefonoDataGridViewTextBoxColumn
            // 
            this.telefonoDataGridViewTextBoxColumn.DataPropertyName = "telefono";
            this.telefonoDataGridViewTextBoxColumn.HeaderText = "telefono";
            this.telefonoDataGridViewTextBoxColumn.Name = "telefonoDataGridViewTextBoxColumn";
            this.telefonoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // laboralDataGridViewTextBoxColumn
            // 
            this.laboralDataGridViewTextBoxColumn.DataPropertyName = "laboral";
            this.laboralDataGridViewTextBoxColumn.HeaderText = "laboral";
            this.laboralDataGridViewTextBoxColumn.Name = "laboralDataGridViewTextBoxColumn";
            this.laboralDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // viviendaDataGridViewTextBoxColumn
            // 
            this.viviendaDataGridViewTextBoxColumn.DataPropertyName = "vivienda";
            this.viviendaDataGridViewTextBoxColumn.HeaderText = "vivienda";
            this.viviendaDataGridViewTextBoxColumn.Name = "viviendaDataGridViewTextBoxColumn";
            this.viviendaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sanitariaDataGridViewTextBoxColumn
            // 
            this.sanitariaDataGridViewTextBoxColumn.DataPropertyName = "sanitaria";
            this.sanitariaDataGridViewTextBoxColumn.HeaderText = "sanitaria";
            this.sanitariaDataGridViewTextBoxColumn.Name = "sanitariaDataGridViewTextBoxColumn";
            this.sanitariaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // observacionesDataGridViewTextBoxColumn
            // 
            this.observacionesDataGridViewTextBoxColumn.DataPropertyName = "observaciones";
            this.observacionesDataGridViewTextBoxColumn.HeaderText = "observaciones";
            this.observacionesDataGridViewTextBoxColumn.Name = "observacionesDataGridViewTextBoxColumn";
            this.observacionesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // concederDataGridViewCheckBoxColumn
            // 
            this.concederDataGridViewCheckBoxColumn.DataPropertyName = "Conceder";
            this.concederDataGridViewCheckBoxColumn.HeaderText = "Conceder";
            this.concederDataGridViewCheckBoxColumn.Name = "concederDataGridViewCheckBoxColumn";
            this.concederDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // mantenerDataGridViewCheckBoxColumn
            // 
            this.mantenerDataGridViewCheckBoxColumn.DataPropertyName = "mantener";
            this.mantenerDataGridViewCheckBoxColumn.HeaderText = "mantener";
            this.mantenerDataGridViewCheckBoxColumn.Name = "mantenerDataGridViewCheckBoxColumn";
            this.mantenerDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // denegarDataGridViewCheckBoxColumn
            // 
            this.denegarDataGridViewCheckBoxColumn.DataPropertyName = "denegar";
            this.denegarDataGridViewCheckBoxColumn.HeaderText = "denegar";
            this.denegarDataGridViewCheckBoxColumn.Name = "denegarDataGridViewCheckBoxColumn";
            this.denegarDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // suspenderDataGridViewCheckBoxColumn
            // 
            this.suspenderDataGridViewCheckBoxColumn.DataPropertyName = "suspender";
            this.suspenderDataGridViewCheckBoxColumn.HeaderText = "suspender";
            this.suspenderDataGridViewCheckBoxColumn.Name = "suspenderDataGridViewCheckBoxColumn";
            this.suspenderDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // extinguirDataGridViewCheckBoxColumn
            // 
            this.extinguirDataGridViewCheckBoxColumn.DataPropertyName = "extinguir";
            this.extinguirDataGridViewCheckBoxColumn.HeaderText = "extinguir";
            this.extinguirDataGridViewCheckBoxColumn.Name = "extinguirDataGridViewCheckBoxColumn";
            this.extinguirDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // fregDataGridViewTextBoxColumn
            // 
            this.fregDataGridViewTextBoxColumn.DataPropertyName = "Freg";
            this.fregDataGridViewTextBoxColumn.HeaderText = "Freg";
            this.fregDataGridViewTextBoxColumn.Name = "fregDataGridViewTextBoxColumn";
            this.fregDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usuarioDataGridViewTextBoxColumn
            // 
            this.usuarioDataGridViewTextBoxColumn.DataPropertyName = "Usuario";
            this.usuarioDataGridViewTextBoxColumn.HeaderText = "Usuario";
            this.usuarioDataGridViewTextBoxColumn.Name = "usuarioDataGridViewTextBoxColumn";
            this.usuarioDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // FormListaDeFichas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 406);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormListaDeFichas";
            this.Text = "FormListaDeFichas";
            this.Load += new System.EventHandler(this.FormListaDeFichas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.venezuelaDataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infSocialBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private VenezuelaDataSet4 venezuelaDataSet4;
        private System.Windows.Forms.BindingSource infSocialBindingSource;
        private VenezuelaDataSet4TableAdapters.InfSocialTableAdapter infSocialTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn numExpDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn asuntoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn situtacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn justificacionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uniSocFamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alquilerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn luzDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aguaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn saludDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn condominioDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefonoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn laboralDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn viviendaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sanitariaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn observacionesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn concederDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn mantenerDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn denegarDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn suspenderDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn extinguirDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fregDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuarioDataGridViewTextBoxColumn;
    }
}