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
    public partial class FormListadoSolicitudes : Form
    {
        private int usuario;
        private string periodo;
        private string pais;
        DataTable dt;
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        ImagenProceso imagen;
        PictureBox img;
        string filtro;
        public FormListadoSolicitudes(int usr, string per, string pa)
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

        private void FormListadoSolicitudes_Load(object sender, EventArgs e)
        {
            acceso.accesapais(Application.StartupPath, periodo, pais);

            acceso.grid_expedientes(ref dt, ref dataGridView1, periodo, "");

            label2.Text = dataGridView1.Rows.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imagen.espere();
            filtro = "";

            if (textBox1.Text.Length > 0) //lugar de nacimiento
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "solicitudes.LugarNac like '%" + textBox1.Text + "%' ";
            }
            if (textBox2.Text.Length > 0) //lugar de residencia
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "(solicitudes.Domicilio like '%" + textBox2.Text + "%' OR ";
                filtro += "solicitudes.Localidad like '%" + textBox2.Text + "%' OR ";
                filtro += "solicitudes.Ciudad like '%" + textBox2.Text + "%' OR ";
                filtro += "solicitudes.Estado like '%" + textBox2.Text + "%') ";
            }
            if (comboBox1.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                string nummes = Convert.ToString(comboBox1.SelectedIndex);
                if (nummes.Length==1) nummes="0"+nummes;

                filtro += "Month(solicitudes.FSolic)=" + nummes;
            }
            if (radioButton1.Checked == true) //aprobadas
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "(solicitudes.IdStatus='06' OR solicitudes.IdStatus='08') ";
            }
            if (radioButton2.Checked == true) //denegadas
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "solicitudes.IdStatus<>'06' AND solicitudes.IdStatus<>'08' ";
            }
            acceso.grid_expedientes(ref dt, ref dataGridView1, periodo, filtro, 4);
            label2.Text = dataGridView1.Rows.Count.ToString();
            imagen.fin_espere();
        }

        private void FormListadoSolicitudes_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width;
            groupBox1.Top = this.Height - groupBox1.Height-50;
            dataGridView1.Height = groupBox1.Top - dataGridView1.Top;
        }
    }
}
