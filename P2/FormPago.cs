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
    public partial class FormPago : Form
    {
        private int usuario;
        private string periodo;
        private string pais;
        ImagenProceso imagen;
        PictureBox img;
        DataTable dt;
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        public FormPago(int usr, string per, string pa)
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

        private void FormPago_Load(object sender, EventArgs e)
        {
            acceso.accesapais(Application.StartupPath, periodo, pais);
            acceso.cuentas(ref comboBox1);
            textBox4.Text = periodo;
            acceso.grid_pagos(ref dt, ref dataGridView1, periodo);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.Enabled)
            {
                button3.Enabled = false;
                textBox1.Text = textBox2.Text =  textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = "";
                checkBox1.Checked =  false;
                comboBox1.Text = "";
                button2.Text = "Cancelar";
            }
            else
            {
                if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                {
                    imagen.espere();
                    string pagado = "0";
                    if (checkBox1.Checked) pagado = "1";
                    if (comboBox2.Text.Equals("Cheque"))
                        acceso.agregar_pago(textBox1.Text, periodo, textBox2.Text, textBox5.Text, dateTimePicker1.Value.ToShortDateString(),comboBox1.Text,
                        "",textBox6.Text,textBox7.Text,pagado);
                    else
                        acceso.agregar_pago(textBox1.Text, periodo, textBox2.Text, "", dateTimePicker1.Value.ToShortDateString(), comboBox1.Text,
                        textBox5.Text, textBox6.Text, textBox7.Text, pagado);
                    textBox1.ReadOnly = true;
                    button3.Enabled = true;
                    button2.Text = "Eliminar";
                    acceso.grid_pagos(ref dt, ref dataGridView1, periodo);
                    imagen.fin_espere();
                }
                else
                {
                    MessageBox.Show("Debe escribir un código y un concepto");
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!(button3.Enabled))
            {
                if (textBox1.Text.Length < 4)
                    textBox1.Text = textBox1.Text.PadLeft(4, '0');

                acceso.titular(ref textBox2, textBox1.Text, periodo);
                if (textBox2.Text.Length > 0)
                {
                    MessageBox.Show("Ya existe ese expediente en el registro de pagos");
                    button3.Enabled = true;
                    button2.Text = "Eliminar";
                    acceso.grid_pagos(ref dt, ref dataGridView1, periodo);
                }
                else
                {
                    acceso.titular_en_expediente(ref textBox2, textBox1.Text, periodo);
                    if (textBox2.Text.Length == 0)
                    {
                        MessageBox.Show("No existe ese expediente ");
                        button3.Enabled = true;
                        button2.Text = "Eliminar";
                        acceso.grid_pagos(ref dt, ref dataGridView1, periodo);
                    }
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                imagen.espere();
                string pagado = "0";
                if (checkBox1.Checked) pagado = "1";
                if (comboBox2.Text.Equals("Cheque"))
                    acceso.modificar_pago(textBox1.Text, periodo, textBox2.Text, textBox5.Text, dateTimePicker1.Value.ToShortDateString(), comboBox1.Text,
                    "", textBox6.Text, textBox7.Text, pagado);
                else
                    acceso.modificar_pago(textBox1.Text, periodo, textBox2.Text, "", dateTimePicker1.Value.ToShortDateString(), comboBox1.Text,
                    textBox5.Text, textBox6.Text, textBox7.Text, pagado);
                acceso.grid_pagos(ref dt, ref dataGridView1, periodo);
                imagen.fin_espere();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.Equals("Cancelar"))
            {
                textBox1.ReadOnly = true;
                button3.Enabled = true;
                button2.Text = "Eliminar";
                acceso.grid_pagos(ref dt, ref dataGridView1, periodo);
            }
            else
            {
                if (MessageBox.Show("El registro que se muestra será eliminado. Está seguro?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    imagen.espere();
                    acceso.eliminar_pago(textBox1.Text,periodo);
                    acceso.grid_pagos(ref dt, ref dataGridView1, periodo);
                    imagen.fin_espere();
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!(dataGridView1.CurrentRow == null))
            {
                try
                {
                    button2.Enabled = button3.Enabled = true;
                    button2.Text = "Eliminar";
                    textBox2.ReadOnly = true;
                    textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells[2].Value.ToString().Length > 0)
                    {
                        comboBox2.Text = "Cheque";
                        textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    }
                    else
                    {
                        if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Length > 0)
                        {
                            comboBox2.Text = "Depósito";
                            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                        }
                        else
                        {
                            comboBox2.Text = "";
                            textBox5.Text = "";
                        }
                    }
                    try
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                    }
                    catch (Exception ex) { }
                    comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    checkBox1.Checked = (Boolean)dataGridView1.CurrentRow.Cells[9].Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error inesperado: " + ex.Message);
                }
            }
        }
    }
}
