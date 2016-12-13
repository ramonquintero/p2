using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2
{
    public partial class FormListaRegistro : Form
    {
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        string periodo = "";
        string pais = "";
        FormRegistro padre;
        DataTable dt;
        ImagenProceso imagen;
        PictureBox img;
        string filtro;
        public FormListaRegistro(FormRegistro f,string per,string pa)
        {
            InitializeComponent();
            periodo = per;
            pais = pa;
            padre = f;
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
            filtro = "";
        }

        private void FormListaRegistro_Load(object sender, EventArgs e)
        {
            acceso.accesapais(Application.StartupPath,periodo, pais);
            
            acceso.grid_expedientes(ref dt,ref dataGridView1, periodo,"");
            textBox5.Text = dataGridView1.Rows.Count.ToString();
            acceso.medio(ref comboBox1);
            acceso.annios(ref comboBox2);
            comboBox2.Items.Add("Todos");
            comboBox2.Text = periodo;

        }

        private void FormListaRegistro_FormClosing(object sender, FormClosingEventArgs e)
        {
            padre.mostrar_registros();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imagen.espere();
            filtro = "";
            /*if (textBox1.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "NumExp like '%" + textBox1.Text + "%'";
            }
            if (textBox2.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "Pasaporte like '%" + textBox2.Text + "%'";
            }
            if (textBox3.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "Apellidos like '%" + textBox3.Text + "%'";
            }
            if (textBox4.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "Nombres like '%" + textBox4.Text + "%'";
            }
            if (comboBox1.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "Medio like '%" + comboBox1.Text + "%'";
            }*/

            if (textBox1.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "solicitudes.NumExp like '%" + textBox1.Text + "%' ";
            }
            if (textBox2.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "solicitudes.Pasaporte like '%" + textBox2.Text + "%' ";
            }
            if (textBox3.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "solicitudes.Apellidos like '%" + textBox3.Text + "%' ";
            }
            if (textBox4.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "solicitudes.Nombres like '%" + textBox4.Text + "%' ";
            }
            if (comboBox1.Text.Length > 0)
            {
                if (filtro.Length > 0)
                    filtro += " AND ";
                filtro += "solicitudes.MedioSolicitud like '%" + comboBox1.Text + "%' ";
            }
            if (radioButton1.Checked)
                acceso.grid_expedientes(ref dt, ref dataGridView1, comboBox2.Text,filtro,1);
            if (radioButton2.Checked)
                acceso.grid_expedientes(ref dt, ref dataGridView1, comboBox2.Text, filtro, 2);
            if (radioButton3.Checked)
                acceso.grid_expedientes(ref dt, ref dataGridView1, comboBox2.Text, filtro, 3);
            /*try
            {
                dt.DefaultView.RowFilter = filtro;
            }
            catch (Exception ex) 
            {
                dt.DefaultView.RowFilter = "";
            }*/
            textBox5.Text = dataGridView1.Rows.Count.ToString();
            imagen.fin_espere();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            padre.mostrar_registros_cambio_registro(dataGridView1.CurrentRow,true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            padre.mostrar_registros_cambio_registro(dataGridView1.CurrentRow, false);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            acceso.grid_expedientes(ref dt, ref dataGridView1, periodo,filtro);
            textBox5.Text = dataGridView1.Rows.Count.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            /*imagen.espere();
            acceso.grid_expedientes(ref dt, ref dataGridView1, periodo,filtro,1);
            textBox5.Text = dataGridView1.Rows.Count.ToString();
            imagen.fin_espere();*/
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            /*imagen.espere();
            acceso.grid_expedientes(ref dt, ref dataGridView1, periodo,filtro,2);
            textBox5.Text = dataGridView1.Rows.Count.ToString();
            imagen.fin_espere();*/
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            /*imagen.espere();
            acceso.grid_expedientes(ref dt, ref dataGridView1, periodo,filtro);
            textBox5.Text = dataGridView1.Rows.Count.ToString();
            imagen.fin_espere();*/
        }
    }
}
