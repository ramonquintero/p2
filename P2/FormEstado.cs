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
    public partial class FormEstado : Form
    {
        AccesoADatos acceso;
        ImagenProceso imagen;
        PictureBox img;
        DataTable dt;
        public FormEstado()
        {
            InitializeComponent();
            acceso = new AccesoADatos(Application.StartupPath);
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
        }

        private void FormEstado_Load(object sender, EventArgs e)
        {
            acceso.paises(ref comboBox1);
            acceso.grid_estado(ref dt, ref dataGridView1);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!(dataGridView1.CurrentRow == null))
            {
                try
                {
                    button2.Enabled = button3.Enabled = true;
                    comboBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error inesperado: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Enabled == true)
            {
                button2.Enabled = false;
                button3.Enabled = false;
                comboBox1.Text = textBox1.Text = textBox2.Text = "";
            }
            else
            {
                if (comboBox1.Text.Length > 0 && textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                {
                    imagen.espere();
                    acceso.agregar_estado(comboBox1.Text, textBox1.Text, textBox2.Text);
                    acceso.grid_estado(ref dt, ref dataGridView1);
                    button2.Enabled = button3.Enabled = true;
                    imagen.fin_espere();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            imagen.espere();
            acceso.modificar_estado(comboBox1.Text, textBox1.Text, textBox2.Text);
            acceso.grid_estado(ref dt, ref dataGridView1);
            imagen.fin_espere();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((dataGridView1.RowCount > 0))
            {
                if (MessageBox.Show("El registro que se muestra será eliminado. Está seguro?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    imagen.espere();
                    acceso.eliminar_estado(comboBox1.Text, textBox1.Text);
                    acceso.grid_estado(ref dt, ref dataGridView1);
                    imagen.fin_espere();
                }
            }
        }
    }
}
