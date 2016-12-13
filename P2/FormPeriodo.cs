using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace P2
{
    public partial class FormPeriodo : Form
    {
        AccesoADatos acceso;
        ImagenProceso imagen;
        PictureBox img;
        int usuario;

        public FormPeriodo(int us)
        {
            InitializeComponent();
            acceso = new AccesoADatos(Application.StartupPath);
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
            usuario = us;
        }

        private void FormPeriodo_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'programa2DataSet1.Paises' Puede moverla o quitarla según sea necesario.
            this.paisesTableAdapter.Fill(this.programa2DataSet1.Paises);
            // TODO: esta línea de código carga datos en la tabla 'programa2DataSet.Periodo' Puede moverla o quitarla según sea necesario.
            this.periodoTableAdapter.Fill(this.programa2DataSet.Periodo);

        }

        private void FormPeriodo_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width - 50;
            dataGridView2.Width = this.Width - 50;
            dataGridView2.Top = this.Height - dataGridView2.Height - 40;
            //dataGridView1.Height = this.Height/5;
            dataGridView1.Height = dataGridView2.Top-350;
            
            
            label12.Top = dataGridView1.Top + dataGridView1.Height + 10;
            label13.Top = dataGridView1.Top + dataGridView1.Height + 10;
            label14.Top = dataGridView1.Top + dataGridView1.Height + 10;
            textBox19.Top = dataGridView1.Top + dataGridView1.Height + 10;
            textBox20.Top = dataGridView1.Top + dataGridView1.Height + 10;
            textBox21.Top = dataGridView1.Top + dataGridView1.Height + 10;
            label15.Top = dataGridView1.Top + dataGridView1.Height + 50;
            label16.Top = dataGridView1.Top + dataGridView1.Height + 50;
            label17.Top = dataGridView1.Top + dataGridView1.Height + 100;
            textBox22.Top = dataGridView1.Top + dataGridView1.Height + 50;
            textBox23.Top = dataGridView1.Top + dataGridView1.Height + 50;
            textBox24.Top = dataGridView1.Top + dataGridView1.Height + 100;
            label18.Top = dataGridView1.Top + dataGridView1.Height + 100;
            label19.Top = dataGridView1.Top + dataGridView1.Height + 150;
            label20.Top = dataGridView1.Top + dataGridView1.Height + 150;
            textBox25.Top = dataGridView1.Top + dataGridView1.Height + 100;
            textBox26.Top = dataGridView1.Top + dataGridView1.Height + 150;
            textBox27.Top = dataGridView1.Top + dataGridView1.Height + 150;
            tableLayoutPanel2.Top = dataGridView1.Top + dataGridView1.Height + 50;
            button4.Top = dataGridView2.Top - 20;
            button5.Top = dataGridView2.Top - 20;
            button6.Top = dataGridView2.Top - 20;
            button2.Left = dataGridView1.Width / 2;
            button3.Left = dataGridView1.Width - 60;
            button5.Left = dataGridView2.Width / 2;
            button6.Left = dataGridView2.Width - 60;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                    textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    if (textBox3.Text.Trim().Length == 0)
                        textBox3.Text = "0.0";
                    textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    if (textBox4.Text.Trim().Length == 0)
                        textBox4.Text = "0.0";
                    textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    if (textBox5.Text.Trim().Length == 0)
                        textBox5.Text = "0.0";
                    textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    if (textBox6.Text.Trim().Length == 0)
                        textBox6.Text = "0.0";
                    textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    if (textBox7.Text.Trim().Length == 0)
                        textBox7.Text = "0.0";
                    textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    if (textBox8.Text.Trim().Length == 0)
                        textBox8.Text = "0.0";
                    textBox9.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    if (textBox9.Text.Trim().Length == 0)
                        textBox9.Text = "0.0";
                    textBox10.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                    if (textBox10.Text.Trim().Length == 0)
                        textBox10.Text = "0.0";
                    textBox11.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                    if (textBox11.Text.Trim().Length == 0)
                        textBox11.Text = "0.0";
                    textBox12.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                    if (textBox12.Text.Trim().Length == 0)
                        textBox12.Text = "0.0";
                    textBox13.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
                    if (textBox13.Text.Trim().Length == 0)
                        textBox13.Text = "0.0";
                    textBox14.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
                    if (textBox14.Text.Trim().Length == 0)
                        textBox14.Text = "0.0";
                    textBox15.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();
                    if (textBox15.Text.Trim().Length == 0)
                        textBox15.Text = "0.0";
                    textBox16.Text = dataGridView1.CurrentRow.Cells[15].Value.ToString();
                    if (textBox16.Text.Trim().Length == 0)
                        textBox16.Text = "0.0";
                    textBox17.Text = dataGridView1.CurrentRow.Cells[16].Value.ToString();
                    if (textBox17.Text.Trim().Length == 0)
                        textBox17.Text = "0.0";
                    textBox18.Text = dataGridView1.CurrentRow.Cells[17].Value.ToString();
                    if (textBox18.Text.Trim().Length == 0)
                        textBox18.Text = "0.0";
                    if (dataGridView1.CurrentRow.Cells[18].Value.ToString().Equals("1"))
                        radioButton1.Checked = true;
                    else
                        radioButton1.Checked = false;
                    if (dataGridView1.CurrentRow.Cells[18].Value.ToString().Equals("2"))
                        radioButton2.Checked = true;
                    else
                        radioButton2.Checked = false;
                    if (dataGridView1.CurrentRow.Cells[18].Value.ToString().Equals("3"))
                        radioButton3.Checked = true;
                    else
                        radioButton3.Checked = false;
                    if (dataGridView1.CurrentRow.Cells[18].Value.ToString().Equals("4"))
                        radioButton4.Checked = true;
                    else
                        radioButton4.Checked = false;
                    if (dataGridView1.CurrentRow.Cells[19].Value.ToString().Equals("True"))
                        checkBox1.Checked = true;
                    else
                        checkBox1.Checked = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error inesperado: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv;
            DataSet ds = programa2DataSet1;
            dv = new DataView(ds.Tables[0], "Periodo = '"+textBox1.Text+"' ", "", DataViewRowState.CurrentRows); 
            dataGridView2.DataSource = dv;
            
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null)
            {
                textBox19.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                textBox20.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                textBox21.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                textBox22.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
                textBox23.Text = (Double.Parse(textBox22.Text) * 0.25).ToString();
                textBox24.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();
                textBox25.Text = dataGridView2.CurrentRow.Cells[9].Value.ToString();
                textBox26.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
                textBox27.Text = dataGridView2.CurrentRow.Cells[19].Value.ToString();

                textBox28.Text = dataGridView2.CurrentRow.Cells[10].Value.ToString();
                textBox29.Text = dataGridView2.CurrentRow.Cells[11].Value.ToString();
                textBox30.Text = dataGridView2.CurrentRow.Cells[12].Value.ToString();
                textBox31.Text = dataGridView2.CurrentRow.Cells[13].Value.ToString();
                textBox32.Text = dataGridView2.CurrentRow.Cells[14].Value.ToString();
                textBox33.Text = dataGridView2.CurrentRow.Cells[15].Value.ToString();
                textBox34.Text = dataGridView2.CurrentRow.Cells[16].Value.ToString();
                textBox35.Text = dataGridView2.CurrentRow.Cells[17].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button3.Text = "Cancelar";
            textBox1.Enabled = true;
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text =
                textBox7.Text = textBox8.Text = textBox9.Text = textBox10.Text = textBox11.Text = textBox12.Text =
                textBox13.Text = textBox14.Text = textBox15.Text = textBox16.Text = textBox17.Text = textBox18.Text = "";
            radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.Enabled)
            {
                //Vamos a modificar el registro
                imagen.espere();
                string activo = "";
                if (radioButton1.Checked) activo = "1";
                if (radioButton2.Checked) activo = "2";
                if (radioButton3.Checked) activo = "3";
                if (radioButton4.Checked) activo = "4";
                try
                {
                    acceso.modificar_periodo(textBox1.Text, textBox2.Text, Double.Parse(textBox3.Text), Double.Parse(textBox4.Text), Double.Parse(textBox5.Text), Double.Parse(textBox6.Text),
                        Double.Parse(textBox7.Text), Double.Parse(textBox8.Text), Double.Parse(textBox9.Text), Double.Parse(textBox10.Text),
                        Double.Parse(textBox11.Text), Double.Parse(textBox12.Text), Double.Parse(textBox13.Text), Double.Parse(textBox14.Text),
                        Double.Parse(textBox15.Text), Double.Parse(textBox16.Text), Double.Parse(textBox17.Text), Double.Parse(textBox18.Text), activo, checkBox1.Checked, usuario.ToString(), DateTime.Now.ToShortDateString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error. "+ex.Message);
                }
                this.periodoTableAdapter.Fill(this.programa2DataSet.Periodo);
                dataGridView1_SelectionChanged(sender, e);
                imagen.fin_espere();
            }
            else
            {
                //Vamos a agregar un registro
                imagen.espere();
                string activo="";
                if (radioButton1.Checked) activo="1";
                if (radioButton2.Checked) activo="2";
                if (radioButton3.Checked) activo="3";
                if (radioButton4.Checked) activo="4";
                try
                {
                    acceso.agregar_periodo(textBox1.Text, textBox2.Text, Double.Parse(textBox3.Text), Double.Parse(textBox4.Text), Double.Parse(textBox5.Text), Double.Parse(textBox6.Text),
                        Double.Parse(textBox7.Text), Double.Parse(textBox8.Text), Double.Parse(textBox9.Text), Double.Parse(textBox10.Text),
                        Double.Parse(textBox11.Text), Double.Parse(textBox12.Text), Double.Parse(textBox13.Text), Double.Parse(textBox14.Text),
                        Double.Parse(textBox15.Text), Double.Parse(textBox16.Text), Double.Parse(textBox17.Text), Double.Parse(textBox18.Text), activo, checkBox1.Checked, usuario.ToString(), DateTime.Now.ToShortDateString());
                    button3.Text = "Eliminar";
                    textBox1.Enabled = false;
                    button1.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error. "+ex.Message);
                }
                this.periodoTableAdapter.Fill(this.programa2DataSet.Periodo);
                
                dataGridView1_SelectionChanged(sender, e);
                imagen.fin_espere();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text.Equals("Cancelar"))
            {
                button1.Enabled = true;
                textBox1.Enabled = false;
                button3.Text = "Eliminar";
                dataGridView1_SelectionChanged(sender, e);
            }
            else
            {
                if (MessageBox.Show("El registro que se muestra será eliminado. Está seguro?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    imagen.espere();
                    acceso.eliminar_periodo(textBox1.Text);
                    dataGridView1_SelectionChanged(sender, e);
                    this.periodoTableAdapter.Fill(this.programa2DataSet.Periodo);
                    imagen.fin_espere();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button6.Text = "Cancelar";
            textBox19.Enabled = true;

            textBox19.Text = textBox20.Text = textBox21.Text = textBox22.Text = textBox23.Text = textBox24.Text =
                textBox25.Text = textBox26.Text = textBox27.Text = textBox28.Text = textBox29.Text = textBox30.Text =
                textBox31.Text = textBox32.Text = textBox33.Text = textBox34.Text = textBox35.Text="";

            textBox22.Text = textBox23.Text = textBox24.Text =
            textBox25.Text = textBox26.Text = textBox27.Text = textBox28.Text = textBox29.Text = textBox30.Text =
            textBox31.Text = textBox32.Text = textBox33.Text = textBox34.Text = textBox35.Text = "0.0";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double d;
            if (button4.Enabled)
            {
                //Vamos a modificar el registro
                imagen.espere();
                string activo = "";
                /*if (radioButton1.Checked) activo = "1";
                if (radioButton2.Checked) activo = "2";
                if (radioButton3.Checked) activo = "3";
                if (radioButton4.Checked) activo = "4";*/
                if (!(Double.TryParse(textBox22.Text, out d)))
                    textBox22.Text = "0";
                if (!(Double.TryParse(textBox24.Text, out d)))
                    textBox24.Text = "0";
                if (!(Double.TryParse(textBox25.Text, out d)))
                    textBox25.Text = "0";
                if (!(Double.TryParse(textBox28.Text, out d)))
                    textBox28.Text = "0";
                if (!(Double.TryParse(textBox29.Text, out d)))
                    textBox29.Text = "0";
                if (!(Double.TryParse(textBox30.Text, out d)))
                    textBox30.Text = "0";
                if (!(Double.TryParse(textBox31.Text, out d)))
                    textBox31.Text = "0";
                if (!(Double.TryParse(textBox32.Text, out d)))
                    textBox32.Text = "0";
                if (!(Double.TryParse(textBox33.Text, out d)))
                    textBox33.Text = "0";
                if (!(Double.TryParse(textBox34.Text, out d)))
                    textBox34.Text = "0";
                if (!(Double.TryParse(textBox35.Text, out d)))
                    textBox35.Text = "0";
                try
                {
                    acceso.modificar_pais_en_periodo(textBox19.Text,
                        textBox1.Text,textBox20.Text,textBox21.Text, 
                        textBox26.Text, 
                        Double.Parse(textBox22.Text), 
                        Double.Parse(textBox24.Text), 
                        Double.Parse(textBox25.Text),
                        Double.Parse(textBox28.Text), 
                        Double.Parse(textBox29.Text), 
                        Double.Parse(textBox30.Text), 
                        Double.Parse(textBox31.Text),
                        Double.Parse(textBox32.Text), 
                        Double.Parse(textBox33.Text), 
                        Double.Parse(textBox34.Text), 
                        Double.Parse(textBox35.Text),
                        usuario.ToString(), 
                        DateTime.Now.ToShortDateString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error. " + ex.Message);
                }
                this.paisesTableAdapter.Fill(this.programa2DataSet1.Paises);
                dataGridView2_SelectionChanged(sender, e);
                imagen.fin_espere();
            }
            else
            {
                //Vamos a agregar un registro
                imagen.espere();
                try
                {
                    string rutadatos = ConfigurationManager.AppSettings["rutaDatos"];
                    string res= acceso.crear_bd_pais_periodo(textBox19.Text, textBox1.Text, rutadatos, Application.StartupPath);
                    if (res.Length==0)
                    {
                        acceso.agregar_pais_en_periodo(textBox19.Text,
                            textBox1.Text,textBox20.Text,textBox21.Text, 
                            textBox26.Text, 
                            Double.Parse(textBox22.Text), 
                            Double.Parse(textBox24.Text), 
                            Double.Parse(textBox25.Text),
                            Double.Parse(textBox28.Text), 
                            Double.Parse(textBox29.Text), 
                            Double.Parse(textBox30.Text), 
                            Double.Parse(textBox31.Text),
                            Double.Parse(textBox32.Text), 
                            Double.Parse(textBox33.Text), 
                            Double.Parse(textBox34.Text), 
                            Double.Parse(textBox35.Text),
                            usuario.ToString(), 
                            DateTime.Now.ToShortDateString());
                        button6.Text = "Eliminar";
                        button4.Enabled = true;
                        textBox19.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error. " + ex.Message);
                }
                this.paisesTableAdapter.Fill(this.programa2DataSet1.Paises);
                dataGridView2_SelectionChanged(sender, e);
                imagen.fin_espere();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string rutadatos = ConfigurationManager.AppSettings["rutaDatos"];
            if (button6.Text.Equals("Cancelar"))
            {
                button4.Enabled = true;
                textBox19.Enabled = false;
                button6.Text = "Eliminar";
                dataGridView2_SelectionChanged(sender, e);
            }
            else
            {
                if (MessageBox.Show("El registro y la base de datos asociada al pais seleccionado será eliminado. Está seguro?", "Eliminar País de período", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    imagen.espere();
                    string res = acceso.eliminar_bd_pais_periodo(textBox19.Text, textBox1.Text, rutadatos);
                    if (res.Length == 0)
                    {
                        acceso.eliminar_pais_de_periodo(textBox1.Text, textBox19.Text);
                        //dataGridView2_SelectionChanged(sender, e);
                        this.paisesTableAdapter.Fill(this.programa2DataSet1.Paises);
                    }
                    else
                        MessageBox.Show(res);
                    imagen.fin_espere();
                }
            }
        }
    }
}
