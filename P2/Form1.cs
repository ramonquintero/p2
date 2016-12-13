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
    public partial class Form1 : Form
    {
        AccesoADatos acceso;
        int usuario;

        public Form1()
        {
            InitializeComponent();
            acceso = new AccesoADatos(Application.StartupPath);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            label1.Left = (this.Width / 2) - (label1.Width/2);
            label2.Left = (this.Width / 2) - (label2.Width / 2);
            pictureBox1.Left = (this.Width / 2) - ((pictureBox1.Width/2)*4);
            label3.Left = (this.Width / 2) - (label3.Width / 2);
            label4.Left = (this.Width / 2) - ((label4.Width / 2));
            label8.Left = (this.Width / 2) - ((label8.Width / 2));
            
            tableLayoutPanel1.Left = (this.Width / 2) - (tableLayoutPanel1.Width / 2);
            tableLayoutPanel2.Left = (this.Width / 2) - (tableLayoutPanel2.Width / 2);
            tableLayoutPanel3.Left = (this.Width / 2) - (tableLayoutPanel3.Width / 2);
            tableLayoutPanel4.Left = (this.Width / 2) - (tableLayoutPanel4.Width / 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Length > 0) && (textBox2.Text.Length > 0))
            {
                if (acceso.autenticacion(textBox1.Text, textBox2.Text, out usuario))
                {
                    this.Hide();
                    FormAdmin f = new FormAdmin(usuario);
                    f.ShowDialog(this);
                    this.Close();
                }
                else
                    MessageBox.Show("El usuario "+textBox1.Text+" no existe o la clave no es la correcta.");
            }
            else
                MessageBox.Show("Faltan datos...");
        }
    }
}
