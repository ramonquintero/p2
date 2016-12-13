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
    public partial class FormPaises : Form
    {
        AccesoADatos acceso;
        ImagenProceso imagen;
        PictureBox img;
        public FormPaises()
        {
            InitializeComponent();
            acceso = new AccesoADatos(Application.StartupPath);
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
        }

        private void FormPaises_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'programa2DataSet4.Pais' Puede moverla o quitarla según sea necesario.
            this.paisTableAdapter.Connection.ConnectionString = acceso.stringdeconexion;
            this.paisTableAdapter.Fill(this.programa2DataSet4.Pais);
            textBox1.ReadOnly = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!(dataGridView1.CurrentRow == null))
            {
                try
                {
                    button2.Enabled = button3.Enabled = true;

                    textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
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
                textBox1.Enabled = false;
                textBox1.Text = textBox2.Text = "";
                textBox2.Focus();
            }
            else
            {
                if (textBox2.Text.Length > 0)
                {
                    imagen.espere();
                    acceso.agregar_pais(textBox2.Text);
                    this.paisTableAdapter.Fill(this.programa2DataSet4.Pais);
                    button2.Enabled = button3.Enabled = true;
                    textBox1.Enabled = true;
                    imagen.fin_espere();
                }
                else
                {
                    MessageBox.Show("Debe indicar el nombre del país");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            imagen.espere();
            acceso.modificar_pais(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()), textBox2.Text);
            this.paisTableAdapter.Fill(this.programa2DataSet4.Pais);
            imagen.fin_espere();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((dataGridView1.RowCount > 0))
            {
                if (MessageBox.Show("El registro que se muestra será eliminado. Está seguro?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    imagen.espere();
                    acceso.eliminar_pais(int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                    this.paisTableAdapter.Fill(this.programa2DataSet4.Pais);
                    imagen.fin_espere();
                }
            }
        }
    }
}
