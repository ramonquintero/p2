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
    public partial class FormPrintSol : Form
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
        public FormPrintSol(int usr, string per, string pa)
        {
            InitializeComponent();
            //img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            //imagen.fin_espere();
            usuario = usr;
            periodo = per;
            pais = pa;
            acceso.accesapais(Application.StartupPath,periodo, pais);
        }

        private void FormPrintSol_Load(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            int valor,valor1=0;
            if (radioButton1.Checked)
            {
                if ((textBox1.Text.Length == 0) ||
                    (textBox2.Text.Length == 0))
                {
                    MessageBox.Show("Debe indicar los limites de los expedientes a procesar");
                    return;
                }
                if (!(int.TryParse(textBox1.Text,out valor)) ||
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

            texto_normal = new System.Drawing.Font("Courier New", 12);
            titulo_informe = new System.Drawing.Font("Courier New", 14, System.Drawing.FontStyle.Bold);
            titulo_tabla = new System.Drawing.Font("Courier New", 12, System.Drawing.FontStyle.Bold);
            etiquetas = new System.Drawing.Font("Courier New", 12);

            datos_solicitante = new DataTable();
            datos_familiares = new DataTable();

            if (radioButton1.Checked)
                acceso.datos_personales_por_numexp(periodo, textBox1.Text, textBox2.Text, ref datos_solicitante);
            else
                acceso.datos_personales_por_fecha(periodo, dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString(), ref datos_solicitante);

            qtyregistros = datos_solicitante.Rows.Count;
            regactual = 0;
            if (radioButton4.Checked)
                printDocument1.Print();
            else
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            datos_solicitante.Rows.Clear();
        }

        private void encabezado(System.Drawing.Printing.PrintPageEventArgs e, System.Drawing.Font titulo_informe)
        {
            e.Graphics.DrawString("CONSEJERIA DE TRABAJO", titulo_informe, Brushes.Black, 20, 50);
            e.Graphics.DrawString("Y ASUNTOS SOCIALES", titulo_informe, Brushes.Black, 20, 80);
            e.Graphics.DrawString("Programa1", titulo_informe, Brushes.Black, e.MarginBounds.Right-140, 20);
            e.Graphics.DrawString("Hoja de Cálculo", titulo_informe, Brushes.Black, e.MarginBounds.Right - 140, 50);
            e.Graphics.DrawString("Fecha: "+DateTime.Now.ToShortDateString(), titulo_informe, Brushes.Black, e.MarginBounds.Right - 140, 80);
            
        }

        private void datos_personales(System.Drawing.Printing.PrintPageEventArgs e, System.Drawing.Font fuente, System.Drawing.Font titulo,
                                      string nombre, string pasaporte, string ingreso, string edad, string inscripcion)
        {
            if (nombre.Length > 40)
                nombre = nombre.Substring(0, 40);
            if (ingreso.Length > 12)
                ingreso = ingreso.Substring(0, 12);
            if (edad.Length > 2)
                edad = edad.Substring(0, 2);
            if (pasaporte.Length > 12)
                pasaporte = pasaporte.Substring(0, 12);
            if (inscripcion.Length > 10)
                inscripcion = inscripcion.Substring(0, 10);
            e.Graphics.DrawString("Datos Personales:", titulo, Brushes.Black, 20, 140);
            e.Graphics.DrawString("Nombres:", fuente, Brushes.Black, 60, 180);
            e.Graphics.DrawString(nombre, fuente, Brushes.Black, 150, 180);
            e.Graphics.DrawString("Ingresos:", fuente, Brushes.Black, e.MarginBounds.Right - 165, 180);
            e.Graphics.DrawString(ingreso, fuente, Brushes.Black, e.MarginBounds.Right - 70, 180);
            e.Graphics.DrawString("Edad:", fuente, Brushes.Black, 60, 210);
            e.Graphics.DrawString(edad, fuente, Brushes.Black, 110, 210);
            e.Graphics.DrawString("Pasaporte:", fuente, Brushes.Black, 140, 210);
            e.Graphics.DrawString(pasaporte, fuente, Brushes.Black, 250, 210);
            e.Graphics.DrawString("Nª Inscripción Consular:", fuente, Brushes.Black, e.MarginBounds.Right - 330, 210);
            e.Graphics.DrawString(inscripcion, fuente, Brushes.Black, e.MarginBounds.Right - 80, 210);

        }

        private void encabezado_familiares(System.Drawing.Printing.PrintPageEventArgs e, 
                                           System.Drawing.Font fuente, System.Drawing.Font encabezado_de_tabla)
        {
            e.Graphics.DrawString("Datos de la Unidad Económica Familiar:", fuente, Brushes.Black, 20, 250);
            e.Graphics.DrawString("Apellidos y Nombres", encabezado_de_tabla, Brushes.Black, 150, 290);
            e.Graphics.DrawString("Parentesco", encabezado_de_tabla, Brushes.Black, 450, 290);
            e.Graphics.DrawString("Ingresos", encabezado_de_tabla, Brushes.Black, e.MarginBounds.Right - 115, 290);
        }

        private void detalle_familiares(System.Drawing.Printing.PrintPageEventArgs e,
                                           System.Drawing.Font fuente, int numlinea,
                                        string nombre, string parentesco, string ingresos)
        {
            if (nombre.Length > 40)
                nombre = nombre.Substring(0, 40);
            if (ingresos.Length > 12)
                ingresos = ingresos.Substring(0, 12);
            if (parentesco.Length > 15)
                parentesco = parentesco.Substring(0, 15);
            e.Graphics.DrawString(nombre, fuente, Brushes.Black, 40, 290 + (30 * numlinea));
            e.Graphics.DrawString(parentesco, fuente, Brushes.Black, 450, 290 + (30 * numlinea));
            e.Graphics.DrawString(ingresos, fuente, Brushes.Black, e.MarginBounds.Right - 115, 290 + (30 * numlinea));
        }

        private void incapacidad(System.Drawing.Printing.PrintPageEventArgs e,
                                           System.Drawing.Font titulo, System.Drawing.Font fuente, ref int numlinea,
                                        string discapacidad)
        {
            if (discapacidad.Length > 40)
                discapacidad = discapacidad.Substring(0, 40);
            e.Graphics.DrawString("Incapacidad:", titulo, Brushes.Black, 20, 290 + (30 * numlinea));
            numlinea++;
            e.Graphics.DrawString(discapacidad, fuente, Brushes.Black, 100, 290 + (30 * numlinea));
            numlinea++;
        }

        private void ayuda(System.Drawing.Printing.PrintPageEventArgs e,
                                           System.Drawing.Font titulo, System.Drawing.Font fuente, ref int numlinea)
        {
            e.Graphics.DrawString("Ayuda:", titulo, Brushes.Black, 20, 290 + (30 * numlinea));
            numlinea++;
        }

        private void status(System.Drawing.Printing.PrintPageEventArgs e,
                            System.Drawing.Font titulo, System.Drawing.Font fuente, ref int numlinea,
                            string visita_emision,string visita_ejecucion,
                            string revision_emision,string revision_ejecucion,
                            string requisitoria_emision,string reqiusitoria_ejecucion)
        {
            e.Graphics.DrawString("Status del Expediente:", titulo, Brushes.Black, 20, 290 + (30 * numlinea));
            numlinea++;
            e.Graphics.DrawString("Fecha de Emisión", titulo, Brushes.Black, 300, 290 + (30 * numlinea));
            e.Graphics.DrawString("Fecha de Ejecución", titulo, Brushes.Black, 500, 290 + (30 * numlinea));
            numlinea++;
            e.Graphics.DrawString("VISITA SOCIAL", titulo, Brushes.Black, 100, 290 + (30 * numlinea));
            e.Graphics.DrawString(visita_emision, fuente, Brushes.Black, 300, 290 + (30 * numlinea));
            e.Graphics.DrawString(visita_ejecucion, fuente, Brushes.Black, 500, 290 + (30 * numlinea));
            numlinea++;
            e.Graphics.DrawString("REVISIÓN MEDICA", titulo, Brushes.Black, 100, 290 + (30 * numlinea));
            e.Graphics.DrawString(revision_emision, fuente, Brushes.Black, 300, 290 + (30 * numlinea));
            e.Graphics.DrawString(revision_ejecucion, fuente, Brushes.Black, 500, 290 + (30 * numlinea));
            numlinea++;
            e.Graphics.DrawString("REQUISITORIA", titulo, Brushes.Black, 100, 290 + (30 * numlinea));
            e.Graphics.DrawString(requisitoria_emision, fuente, Brushes.Black, 300, 290 + (30 * numlinea));
            e.Graphics.DrawString(reqiusitoria_ejecucion, fuente, Brushes.Black, 500, 290 + (30 * numlinea));
            numlinea++;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            int i=1;
            
            linea = datos_solicitante.Rows[regactual];
            
            encabezado(e, titulo_informe);
            datos_personales(e, texto_normal, titulo_tabla, linea["nombre"].ToString(),linea["Pasaporte"].ToString(),
                linea["Ingresos"].ToString(), "99", linea["FInsCon"].ToString());
            encabezado_familiares(e, titulo_tabla, etiquetas);
            acceso.datos_familiares_por_registro(periodo, linea["NumExp"].ToString(), ref datos_familiares);
            foreach (DataRow linea_familiar in datos_familiares.Rows)
            {
                detalle_familiares(e, texto_normal, i, linea_familiar["nombre"].ToString(), linea_familiar["parentesco"].ToString(), linea_familiar["Ingresos"].ToString());
                i++;
            }
            datos_familiares.Rows.Clear();
            incapacidad(e, titulo_tabla, texto_normal, ref i, " ");
            ayuda(e, titulo_tabla, texto_normal, ref i);
            status(e, titulo_tabla, texto_normal, ref i, " ", " ", " ", " ", " ", " ");
            regactual++;
            if (regactual < qtyregistros) e.HasMorePages = true;
            
        }

    }
}
