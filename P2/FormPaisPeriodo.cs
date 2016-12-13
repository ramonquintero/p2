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
    public partial class FormPaisPeriodo : Form
    {
        AccesoADatos acceso;
        int usuario;
        string db;
        string periodo;
        public FormPaisPeriodo(int u, ref string dbpais, ref string per)
        {
            InitializeComponent();
            acceso = new AccesoADatos(Application.StartupPath);
            usuario = u;
            db = dbpais;
            periodo = per;
        }

        private void FormPaisPeriodo_Load(object sender, EventArgs e)
        {
            acceso.annios(ref comboBox2);
            acceso.paises(ref comboBox1, ref db, ref periodo,comboBox2.Text);

            string[] d = MdiParent.Text.Split('-');
            if (d.Length>1)
            {
                comboBox1.Text = db;
                comboBox2.Text = periodo;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length == 0)
                MessageBox.Show("Debe indicar un pais");
            else
                if (comboBox2.Text.Length == 0)
                    MessageBox.Show("Debe indicar un año");
                else
                {
                    db = 
                    MdiParent.Text = "Programa 2" + " [ " + comboBox1.Text + " - " + comboBox2.Text+ " ] ";
                    ((FormAdmin)MdiParent).afectarPaisPeriodo(comboBox1.Text, comboBox2.Text);
                    acceso.accesapais(Application.StartupPath,comboBox2.Text, comboBox1.Text);
                    acceso.modificar_tablas();
                }
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            acceso.paises(ref comboBox1, ref db, ref periodo, comboBox2.Text);
        }
    }
}
