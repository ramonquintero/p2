using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2
{
    public partial class FormEstadis : Form
    {
        private int usuario;
        private string periodo;
        private string pais;
        ImagenProceso imagen;
        PictureBox img;
        DataTable dt;
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        public FormEstadis()
        {
            InitializeComponent();
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
            /*usuario = usr;
            periodo = per;
            pais = pa;
            acceso.accesapais(Application.StartupPath, periodo, pais);*/
        }

        private void FormEstadis_Load(object sender, EventArgs e)
        {
            acceso.annios(ref comboBox1);
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Pais";
            dataGridView1.Columns[1].Name = "Favorables";
            dataGridView1.Columns[2].Name = "Euros";
            dataGridView1.Columns[3].Name = "Dólares";
            dataGridView1.Columns[4].Name = "Moneda local";
            dataGridView1.Columns[0].Width = dataGridView1.Width / 5;
            dataGridView1.Columns[1].Width = dataGridView1.Width / 5;
            dataGridView1.Columns[2].Width = dataGridView1.Width / 5;
            dataGridView1.Columns[3].Width = dataGridView1.Width / 5;
            dataGridView1.Columns[4].Width = dataGridView1.Width / 5;

            dataGridView2.ColumnCount = 6;
            dataGridView2.Columns[0].Name = "Pais";
            dataGridView2.Columns[1].Name = "Solicitudes";
            dataGridView2.Columns[2].Name = "Favorables";
            dataGridView2.Columns[3].Name = "%";
            dataGridView2.Columns[4].Name = "Desfavorables";
            dataGridView2.Columns[5].Name = "%";
            dataGridView2.Columns[0].Width = dataGridView1.Width / 6;
            dataGridView2.Columns[1].Width = dataGridView1.Width / 6;
            dataGridView2.Columns[2].Width = dataGridView1.Width / 6;
            dataGridView2.Columns[3].Width = dataGridView1.Width / 6;
            dataGridView2.Columns[4].Width = dataGridView1.Width / 6;
            dataGridView2.Columns[5].Width = dataGridView1.Width / 6;

            tabControl1.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length > 0)
            {
                imagen.espere();
                tabControl1.Enabled = true;
                dataGridView2.Rows.Clear();
                acceso.cuantitativo(ref dataGridView2, comboBox1.Text);
                dataGridView1.Rows.Clear();
                acceso.administrativo(ref dataGridView1, comboBox1.Text);
                imagen.fin_espere();
            }
            else
                tabControl1.Enabled = false;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
