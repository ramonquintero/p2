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
    public partial class FormManStatus : Form
    {
        AccesoADatos acceso;
        ImagenProceso imagen;
        PictureBox img;
        DataTable dt;
        public FormManStatus()
        {
            InitializeComponent();
            acceso = new AccesoADatos(Application.StartupPath);
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
        }

        private void FormManStatus_Load(object sender, EventArgs e)
        {
            acceso.grid_manstatus(ref dt, ref dataGridView1);
            dataGridView1.Columns[0].Width = dataGridView1.Width / 5;
            dataGridView1.Columns[1].Width = dataGridView1.Width-150;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Enabled)
            {
                textBox1.ReadOnly = false;
                button2.Enabled = false;
                textBox1.Text = textBox2.Text = "";
                button3.Text = "Cancelar";
            }
            else
            {
                if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                {
                    imagen.espere();
                    acceso.agregar_status(textBox1.Text, textBox2.Text);
                    textBox1.ReadOnly = true;
                    button2.Enabled = true;
                    button3.Text = "Eliminar";
                    acceso.grid_manstatus(ref dt, ref dataGridView1);
                    imagen.fin_espere();
                }
                else
                {
                    MessageBox.Show("Debe escribir un código y un concepto");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                imagen.espere();
                acceso.modificar_status(textBox1.Text, textBox2.Text);
                acceso.grid_manstatus(ref dt, ref dataGridView1);
                imagen.fin_espere();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text.Equals("Cancelar"))
            {
                textBox1.ReadOnly=true;
                textBox2.Text="";
            }
            else
            {
                if (MessageBox.Show("El registro que se muestra será eliminado. Está seguro?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    imagen.espere();
                    acceso.eliminar_status(textBox1.Text);
                    acceso.grid_manstatus(ref dt, ref dataGridView1);
                    imagen.fin_espere();
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!(dataGridView1.CurrentRow == null))
            {
                try
                {
                    button2.Enabled = button3.Enabled = true;
                    button3.Text = "Eliminar";
                    textBox1.ReadOnly = true;
                    textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error inesperado: " + ex.Message);
                }
            }
        }
    }
}
