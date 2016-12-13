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
    public partial class ResoluciónDelExpediente : Form
    {
        FormRegistro frm;
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        string periodo;
        string expediente;
        ImagenProceso imagen;
        PictureBox img;
        string usuario;
        string pais;
        public ResoluciónDelExpediente(FormRegistro f,string pais)
        {
            InitializeComponent();
            frm = f;
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
            this.pais = pais;
        }

        public void ActualizarDataInterna(string[] resolucion)
        {
            acceso.accesapais(Application.StartupPath,resolucion[2], resolucion[10]);
            textBox9.Text = resolucion[0];
            acceso.marcas(ref comboBox5, resolucion[0]);
            try {
                dateTimePicker5.Value = Convert.ToDateTime(resolucion[1]);
            }
            catch (Exception ex)
            {
            }
            acceso.cuantia_en_euros(ref textBox10, resolucion[2], resolucion[3]);
            acceso.cuantia_en_dolares(ref textBox12, resolucion[2], resolucion[3]);
            acceso.cuantia_en_moneda_local(ref textBox14, resolucion[2], resolucion[3]);
            textBox11.Text = resolucion[8];
            textBox13.Text = resolucion[9];
            acceso.fecha_resolucion(ref dateTimePicker6, resolucion[2], resolucion[3]);
            textBox15.Text = resolucion[4];
            acceso.status(ref comboBox6, resolucion[4]);
            try
            {
                dateTimePicker7.Value = Convert.ToDateTime(resolucion[5]);
            }
            catch (Exception ex)
            {
            }
            try
            {
                dateTimePicker8.Value = Convert.ToDateTime(resolucion[6]);
            }
            catch (Exception ex)
            {
            }
            textBox16.Text = resolucion[7];
            periodo = resolucion[2];
            expediente = resolucion[3];
            usuario = resolucion[11];
        }

        private void ResoluciónDelExpediente_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.mostrar_resolucion();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void ResoluciónDelExpediente_Load(object sender, EventArgs e)
        {
            acceso.marcas(ref comboBox5,"");
            acceso.status(ref comboBox6, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imagen.espere();
            Application.DoEvents();
            if (!(acceso.guardar_resolucion(periodo, expediente, textBox9.Text, dateTimePicker5.Value.ToShortDateString(),
                textBox10.Text, textBox11.Text, textBox12.Text, textBox13.Text, textBox14.Text,
                dateTimePicker6.Value.ToShortDateString(), textBox15.Text, dateTimePicker7.Value.ToShortDateString(),
                dateTimePicker8.Value.ToShortDateString(), textBox16.Text,usuario,pais)))
                MessageBox.Show("Ocurrió un error guardando la resolución. Revise que el expediente esté creado.");
            Application.DoEvents();
            imagen.fin_espere();
        }
    }
}
