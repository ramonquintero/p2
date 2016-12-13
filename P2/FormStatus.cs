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
    public partial class FormStatus : Form
    {
        private int usuario;
        private string periodo;
        private string pais;
        DataTable dt;
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        public FormStatus(int usr, string per, string pa)
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

        private void FormStatus_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'programa2DataSet2.Status' Puede moverla o quitarla según sea necesario.
            this.statusTableAdapter.Fill(this.programa2DataSet2.Status);
            // TODO: esta línea de código carga datos en la tabla 'venezuelaDataSet5.Solicitudes' Puede moverla o quitarla según sea necesario.
            //this.solicitudesTableAdapter.Fill(this.venezuelaDataSet5.Solicitudes);
            acceso.accesapais(Application.StartupPath,periodo, pais);

            acceso.grid_expedientes(ref dt, ref dataGridView1, periodo,"");

            textBox5.Text = dataGridView1.Rows.Count.ToString();
            acceso.medio(ref comboBox1);
            acceso.annios(ref comboBox2);
            comboBox2.Items.Add("Todos");
            comboBox2.Text = periodo;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string filtro = "";
            if (textBox1.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "NumExp like '%" + textBox1.Text + "%' ";
            }
            if (textBox2.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "Pasaporte like '%" + textBox2.Text + "%' ";
            }
            if (textBox3.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "Apellidos like '%" + textBox3.Text + "%' ";
            }
            if (textBox4.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "Nombres like '%" + textBox4.Text + "%' ";
            }
            if (comboBox1.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "Medio like '%" + comboBox1.Text + "%' ";
            }
            acceso.grid_expedientes(ref dt, ref dataGridView1, comboBox2.Text,filtro);
            /*try
            {
                dt.DefaultView.RowFilter = filtro;
            }
            catch (Exception ex)
            {
                dt.DefaultView.RowFilter = "";
            }*/
            textBox5.Text = dataGridView1.Rows.Count.ToString();
        }
    }
}
