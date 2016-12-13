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
    public partial class FormCiudad : Form
    {
        AccesoADatos acceso;
        ImagenProceso imagen;
        PictureBox img;
        DataTable dt;
        public FormCiudad()
        {
            InitializeComponent();
            acceso = new AccesoADatos(Application.StartupPath);
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
        }

        private void FormCiudad_Load(object sender, EventArgs e)
        {
            acceso.grid_ciudad(ref dt, ref dataGridView1);
            acceso.paises(ref comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            acceso.estados(ref comboBox2,comboBox1.Text);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Enabled)
            {
                button2.Enabled = false;
                button3.Enabled = false;
                comboBox1.Text = comboBox2.Text = textBox1.Text = textBox2.Text = "";
            }
            else
            {
                if (comboBox1.Text.Length > 0 && comboBox2.Text.Length > 0 && textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                {
                    imagen.espere();
                    acceso.agregar_ciudad(comboBox1.Text,comboBox2.Text, textBox1.Text, textBox2.Text);
                    acceso.grid_ciudad(ref dt, ref dataGridView1);
                    button2.Enabled = button3.Enabled = true;
                    imagen.fin_espere();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length > 0 && comboBox2.Text.Length > 0 && textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                imagen.espere();
                acceso.modificar_ciudad(comboBox1.Text,comboBox2.Text, textBox1.Text, textBox2.Text);
                acceso.grid_ciudad(ref dt, ref dataGridView1);
                imagen.fin_espere();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((dataGridView1.RowCount > 0))
            {
                if (MessageBox.Show("El registro que se muestra será eliminado. Está seguro?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    imagen.espere();
                    acceso.eliminar_ciudad(comboBox1.Text, comboBox2.Text,textBox1.Text);
                    acceso.grid_ciudad(ref dt, ref dataGridView1);
                    imagen.fin_espere();
                }
            }
        }
    }
}