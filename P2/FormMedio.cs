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
    public partial class FormMedio : Form
    {
        private int usuario;
        private string periodo;
        private string pais;
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        ImagenProceso imagen;
        PictureBox img;
        DataTable datos_solicitante;
        DataTable datos_familiares;
        int qtyregistros;
        int regactual;
        DataRow linea;
        System.Drawing.Font texto_normal;
        System.Drawing.Font titulo_informe;
        System.Drawing.Font titulo_tabla;
        System.Drawing.Font etiquetas;
        public FormMedio(int usr, string per, string pa)
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

        private void button1_Click(object sender, EventArgs e)
        {
            int valor=0, valor1 = 0;
            if (radioButton1.Checked)
            {
                if ((textBox1.Text.Length == 0) ||
                    (textBox2.Text.Length == 0))
                {
                    MessageBox.Show("Debe indicar los limites de los expedientes a procesar");
                    return;
                }
                if (!(int.TryParse(textBox1.Text, out valor)) ||
                    !(int.TryParse(textBox2.Text, out valor1)))
                {
                    MessageBox.Show("Debe indicar valores numéricos en los limites de los expedientes a procesar");
                    return;
                }
                if (valor > valor1)
                {
                    MessageBox.Show("El valor 'Desde' debe ser el numero mas bajo de los expedientes a procesar");
                    return;
                }

            }
            if (radioButton2.Checked)
            {

                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MessageBox.Show("La fecha 'Desde' debe ser la fecha mas baja de los expedientes a procesar");
                    return;
                }
            }
            if (!((radioButton5.Checked)||(radioButton6.Checked)||(radioButton7.Checked)||(radioButton8.Checked)||
                (radioButton9.Checked)||(radioButton10.Checked)||(radioButton11.Checked)||(radioButton12.Checked)))
            {
                MessageBox.Show("Indique el tipo de documento");
                return;
            }

            if (radioButton5.Checked)
            {
                imagen.espere();
                acceso.favorables_filtro(valor, valor1, dateTimePicker1.Value, dateTimePicker2.Value, periodo, pais, saveFileDialog1);
                //System.Diagnostics.Process.Start("winword.exe", Application.StartupPath + "\\Documentos\\" + System.Configuration.ConfigurationManager.AppSettings["Resoluciones_favorables_"+pais]);
                imagen.fin_espere();
            }
            if (radioButton6.Checked)
            {
                imagen.espere();
                acceso.Justificante_filtro(valor, valor1, dateTimePicker1.Value, dateTimePicker2.Value, periodo, pais, saveFileDialog1);
                //System.Diagnostics.Process.Start("winword.exe", Application.StartupPath + "\\Documentos\\" + System.Configuration.ConfigurationManager.AppSettings["Justificantes_" + pais]);
                imagen.fin_espere();
            }
            if (radioButton7.Checked)
            {
                imagen.espere();
                acceso.Resoluciones_desfavorables(valor, valor1, dateTimePicker1.Value, dateTimePicker2.Value, periodo, pais, saveFileDialog1);
                //System.Diagnostics.Process.Start("winword.exe", Application.StartupPath + "\\Documentos\\" + System.Configuration.ConfigurationManager.AppSettings["Desfavorable_" + pais]);
                imagen.fin_espere();
            }
            if (radioButton8.Checked)
            {
                MessageBox.Show("No implementado");
                return;
            }
            if (radioButton9.Checked)
            {
                System.Diagnostics.Process.Start("winword.exe", Application.StartupPath + "\\Documentos\\" + System.Configuration.ConfigurationManager.AppSettings["Desestimiento_" + pais]);
            }
            if (radioButton10.Checked)
            {
                System.Diagnostics.Process.Start("winword.exe", Application.StartupPath + "\\Documentos\\" + System.Configuration.ConfigurationManager.AppSettings["Revision_Favorable_" + pais]);
            }
            if (radioButton12.Checked)
            {
                System.Diagnostics.Process.Start("winword.exe", Application.StartupPath + "\\Documentos\\" + System.Configuration.ConfigurationManager.AppSettings["Solicitud_Duplicada_" + pais]);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            label1.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = false;
            label4.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            label1.Enabled = false;
            label2.Enabled = false;
            label3.Enabled = true;
            label4.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }
    }
}
