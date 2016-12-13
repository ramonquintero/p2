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
    public partial class FormMarca : Form
    {
        private int usuario;
        private string periodo;
        private string pais;
        ImagenProceso imagen;
        PictureBox img;
        DataTable dt;
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        public FormMarca(int usr, string per, string pa)
        {
            InitializeComponent();
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
            usuario = usr;
            periodo = per;
            pais = pa;
            acceso.accesapais(Application.StartupPath, periodo, pais);
        }

        private void FormMarca_Load(object sender, EventArgs e)
        {
            acceso.accesapais(Application.StartupPath, periodo, pais);
            acceso.marcas(ref comboBox1, "");
            acceso.marcas(ref comboBox2, "");
            acceso.requisitorias(ref comboBox3, "");
            acceso.requisitorias(ref comboBox4, "");
            acceso.requisitorias(ref comboBox5, "");
            acceso.requisitorias(ref comboBox6, "");
            acceso.grid_marca(ref dt, ref dataGridView1, periodo);
            textBox3.Text = dataGridView1.Rows.Count.ToString();
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                string cod_marca = "";
                string concepto_marca = "";
                acceso.id_marca_en_solicitud(periodo, dataGridView1.CurrentRow.Cells[0].Value.ToString(), ref cod_marca, ref concepto_marca);
                acceso.requisitorias_en_solicitud(dataGridView1.CurrentRow.Cells[8].Value.ToString(), ref comboBox3);
                acceso.requisitorias_en_solicitud(dataGridView1.CurrentRow.Cells[9].Value.ToString(), ref comboBox4);
                acceso.requisitorias_en_solicitud(dataGridView1.CurrentRow.Cells[10].Value.ToString(), ref comboBox5);
                acceso.requisitorias_en_solicitud(dataGridView1.CurrentRow.Cells[11].Value.ToString(), ref comboBox6);
                try
                {
                    dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[12].Value.ToString());
                }
                catch (Exception ex) { }
                textBox1.Text = cod_marca;
                comboBox1.Text = concepto_marca;
            }
        }

        private void FormMarca_Resize(object sender, EventArgs e)
        {
            groupBox1.Top = this.Height - 150;
            dataGridView1.Height = this.Height -400;
            dataGridView1.Width = this.Width - 50;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Width>500)
                    dataGridView1.Columns[i].Width = dataGridView1.Width / 9;
                else
                    dataGridView1.Columns[i].Width = 100;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filtro = "";

            acceso.grid_marca(ref dt, ref dataGridView1, periodo);

            if (textBox2.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "(Nombres like '%" + textBox2.Text + "%'";
                filtro += " OR Apellidos like '%" + textBox2.Text + "%')";
            }
            
            if (comboBox2.Text.Length > 0)
            {
                string cod_marca = "0";
                acceso.id_marca(ref cod_marca, comboBox2.Text);
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += " Marca like '%" + cod_marca + "%'";
            }
            try
            {
                dt.DefaultView.RowFilter = filtro;
            }
            catch (Exception ex)
            {
                dt.DefaultView.RowFilter = "";
            }
            textBox3.Text = dataGridView1.Rows.Count.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cod_marca = "0";
            acceso.id_marca(ref cod_marca, comboBox1.Text);
            textBox1.Text = cod_marca;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            imagen.espere();
            acceso.modificar_marca_en_expediente(periodo, dataGridView1.CurrentRow.Cells[0].Value.ToString(), textBox1.Text);
            acceso.grid_marca(ref dt, ref dataGridView1, periodo);
            imagen.fin_espere();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            acceso.modificar_requisitoria(periodo, dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                comboBox3.Text, comboBox4.Text, comboBox5.Text, comboBox6.Text,dateTimePicker1.Value.ToShortDateString());
            acceso.grid_marca(ref dt, ref dataGridView1, periodo);
        }
    }
}
