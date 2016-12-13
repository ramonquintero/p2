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
    public partial class FormFicha : Form
    {
        private int usuario;
        private string periodo;
        private string pais;
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        FormListaDeFichas f2;
        public FormFicha(int usr, string per, string pa)
        {
            InitializeComponent();
            //img = pictureBox1;
            //imagen = new ImagenProceso(img, this);
            //imagen.fin_espere();
            usuario = usr;
            periodo = per;
            pais = pa;
            acceso.accesapais(Application.StartupPath,periodo, pais);
        }

        private void FormFicha_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Nombre y Apellido";
            dataGridView1.Columns[1].Name = "Ingresos ùltimo año";
            dataGridView1.Columns[0].Width = dataGridView1.Width/2;
            dataGridView1.Columns[1].Width = dataGridView1.Width / 2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int qtyrows = dataGridView1.Rows.Count;
            double total = 0;
            for (int i = 0; i < qtyrows; i++)
            {
                try
                {
                    dataGridView1.Rows[i].Cells[1].Value = dataGridView1.Rows[i].Cells[1].Value.ToString().Replace(".", "");
                    total += Double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                }
                catch (Exception ex)
                {
                }
            }
            textBox12.Text = total.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f2 = new FormListaDeFichas(this, periodo, pais);
            f2.MdiParent = this.MdiParent;
            f2.Show();
            button1.Visible = false;
        }
    }
}
