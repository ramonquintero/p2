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
    public partial class FormUsuariosYPerfiles : Form
    {
        AccesoADatos acceso;
        int usuario;
        int primeravez = 0;
        PictureBox img;
        public FormUsuariosYPerfiles(int user)
        {
            InitializeComponent();
            acceso = new AccesoADatos(Application.StartupPath);
            usuario = user;
            img = pictureBox1;
            fin_espere();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            acceso.perfiles(ref comboBox1);
            acceso.usuarios(ref listBox1);
            //acceso.modificar_menu(ref menuStrip1);
        }

        private void espere()
        {
            
            img.Left = 0;
            img.Top = 0;
            img.Width = this.Width;
            img.Height = this.Height;
            img.Image = Image.FromFile(Application.StartupPath + "\\loading01.gif");
            img.Refresh();
        }

        private void fin_espere()
        {
            img.Left = 2000;
            img.Top = 2000;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            espere();
            primeravez = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {

                checkedListBox1.SetItemChecked(i, false);

            }
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {

                checkedListBox2.SetItemChecked(i, false);

            }
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {

                checkedListBox3.SetItemChecked(i, false);

            }
            for (int i = 0; i < checkedListBox4.Items.Count; i++)
            {

                checkedListBox4.SetItemChecked(i, false);

            }
            for (int i = 0; i < checkedListBox5.Items.Count; i++)
            {

                checkedListBox5.SetItemChecked(i, false);

            }
            for (int i = 0; i < checkedListBox6.Items.Count; i++)
            {

                checkedListBox6.SetItemChecked(i, false);

            }
            for (int i = 0; i < checkedListBox7.Items.Count; i++)
            {

                checkedListBox7.SetItemChecked(i, false);

            }
            acceso.usuarios_por_perfil(ref listBox2, comboBox1.Text);
            actualizar_menu();
            
            fin_espere();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            button4.Visible = true;
        }

        private void actualizar_menu()
        {
            for(int i=0;i<checkedListBox1.Items.Count;i++)
            {
                if (acceso.perfil_menu(checkedListBox1.Items[i].ToString().Trim(), comboBox1.Text))
                    checkedListBox1.SetItemChecked(i, true);
                Application.DoEvents();
            }

            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (acceso.perfil_menu(checkedListBox2.Items[i].ToString().Trim(), comboBox1.Text))
                    checkedListBox2.SetItemChecked(i, true);
                Application.DoEvents();
            }

            for (int i = 0; i < checkedListBox3.Items.Count; i++)
            {
                if (acceso.perfil_menu(checkedListBox3.Items[i].ToString().Trim(), comboBox1.Text))
                    checkedListBox3.SetItemChecked(i, true);
                Application.DoEvents();
            }

            for (int i = 0; i < checkedListBox4.Items.Count; i++)
            {
                if (acceso.perfil_menu(checkedListBox4.Items[i].ToString().Trim(), comboBox1.Text))
                    checkedListBox4.SetItemChecked(i, true);
                Application.DoEvents();
            }

            for (int i = 0; i < checkedListBox5.Items.Count; i++)
            {
                if (acceso.perfil_menu(checkedListBox5.Items[i].ToString().Trim(), comboBox1.Text))
                    checkedListBox5.SetItemChecked(i, true);
                Application.DoEvents();
            }

            for (int i = 0; i < checkedListBox6.Items.Count; i++)
            {
                if (acceso.perfil_menu(checkedListBox6.Items[i].ToString().Trim(), comboBox1.Text))
                    checkedListBox6.SetItemChecked(i, true);
                Application.DoEvents();
            }

            for (int i = 0; i < checkedListBox7.Items.Count; i++)
            {
                if (acceso.perfil_menu(checkedListBox7.Items[i].ToString().Trim(), comboBox1.Text))
                    checkedListBox7.SetItemChecked(i, true);
                Application.DoEvents();
            }
            primeravez = 1;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (primeravez == 1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    espere();
                    //se ha activado una nueva opción de menu para el perfil
                    acceso.agregar_menu_perfil(checkedListBox1.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
                if (e.NewValue == CheckState.Unchecked)
                {
                    espere();
                    //se ha desactivado una nueva opción de menu para el perfil
                    acceso.eliminar_menu_perfil(checkedListBox1.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
            }
            /*else
                MessageBox.Show("Debe seleccionar un perfil...");*/

        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (primeravez == 1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    espere();
                    //se ha activado una nueva opción de menu para el perfil
                    acceso.agregar_menu_perfil(checkedListBox2.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
                if (e.NewValue == CheckState.Unchecked)
                {
                    espere();
                    //se ha desactivado una nueva opción de menu para el perfil
                    acceso.eliminar_menu_perfil(checkedListBox2.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
            }
            /*else
                MessageBox.Show("Debe seleccionar un perfil...");*/

        }

        private void checkedListBox3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (primeravez == 1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    espere();
                    //se ha activado una nueva opción de menu para el perfil
                    acceso.agregar_menu_perfil(checkedListBox3.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
                if (e.NewValue == CheckState.Unchecked)
                {
                    espere();
                    //se ha desactivado una nueva opción de menu para el perfil
                    acceso.eliminar_menu_perfil(checkedListBox3.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
            }
            /*else
                MessageBox.Show("Debe seleccionar un perfil...");*/
        }

        private void checkedListBox4_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (primeravez == 1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    espere();
                    //se ha activado una nueva opción de menu para el perfil
                    acceso.agregar_menu_perfil(checkedListBox4.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
                if (e.NewValue == CheckState.Unchecked)
                {
                    espere();
                    //se ha desactivado una nueva opción de menu para el perfil
                    acceso.eliminar_menu_perfil(checkedListBox4.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
            }
            /*else
                MessageBox.Show("Debe seleccionar un perfil...");*/
        }

        private void checkedListBox5_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (primeravez == 1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    espere();
                    //se ha activado una nueva opción de menu para el perfil
                    acceso.agregar_menu_perfil(checkedListBox5.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
                if (e.NewValue == CheckState.Unchecked)
                {
                    espere();
                    //se ha desactivado una nueva opción de menu para el perfil
                    acceso.eliminar_menu_perfil(checkedListBox5.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
            }
            /*else
                MessageBox.Show("Debe seleccionar un perfil...");*/
        }

        private void checkedListBox6_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (primeravez == 1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    espere();
                    //se ha activado una nueva opción de menu para el perfil
                    acceso.agregar_menu_perfil(checkedListBox6.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
                if (e.NewValue == CheckState.Unchecked)
                {
                    espere();
                    //se ha desactivado una nueva opción de menu para el perfil
                    acceso.eliminar_menu_perfil(checkedListBox6.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
            }
            /*else
                MessageBox.Show("Debe seleccionar un perfil...");*/
        }

        private void checkedListBox7_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (primeravez == 1)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    espere();
                    //se ha activado una nueva opción de menu para el perfil
                    acceso.agregar_menu_perfil(checkedListBox7.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
                if (e.NewValue == CheckState.Unchecked)
                {
                    espere();
                    //se ha desactivado una nueva opción de menu para el perfil
                    acceso.eliminar_menu_perfil(checkedListBox7.Items[e.Index].ToString().Trim(), comboBox1.Text);
                    fin_espere();
                }
            }
            /*else
                MessageBox.Show("Debe seleccionar un perfil...");*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length == 0)
                MessageBox.Show("Debe seleccionar un perfil");
            else
            {
                if (listBox1.SelectedIndex == -1)
                    MessageBox.Show("Debe seleccionar el usuario a agregar al perfil");
                else
                {
                    if (acceso.usuario_tiene_perfil(listBox1.SelectedItem.ToString()))
                        MessageBox.Show("El usuario " + listBox1.SelectedItem.ToString() + " ya tiene perfil asociado");
                    else
                    {
                        espere();
                        acceso.agregar_perfil_a_usuario(listBox1.Items[listBox1.SelectedIndex].ToString(), comboBox1.Text);
                        listBox2.Items.Add(listBox1.Items[listBox1.SelectedIndex].ToString());
                        fin_espere();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length == 0)
                MessageBox.Show("Debe seleccionar un perfil");
            else
            {
                if (listBox2.SelectedIndex == -1)
                    MessageBox.Show("Debe seleccionar el usuario que tenga el perfil "+comboBox1.Text);
                else
                {
                    espere();
                    acceso.eliminar_perfil_a_usuario(listBox1.Items[listBox1.SelectedIndex].ToString(), comboBox1.Text);
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                    fin_espere();   
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            button4.Visible = false;
            acceso.agregar_perfil(textBox2.Text);
            comboBox1.Items.Clear();
            acceso.perfiles(ref comboBox1);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormUsuariosYPerfiles_Resize(object sender, EventArgs e)
        {
            tableLayoutPanel1.Width = this.Width - 50;

            tableLayoutPanel1.ColumnStyles[0].Width = this.Width / 8;
            tableLayoutPanel1.ColumnStyles[1].Width = this.Width / 8;
            tableLayoutPanel1.ColumnStyles[2].Width = this.Width / 8;
            tableLayoutPanel1.ColumnStyles[3].Width = this.Width / 8;
            tableLayoutPanel1.ColumnStyles[4].Width = this.Width / 8;
            tableLayoutPanel1.ColumnStyles[5].Width = this.Width / 8;
            tableLayoutPanel1.ColumnStyles[6].Width = this.Width / 8;

            tableLayoutPanel1.Top = this.Height - 250;
            tableLayoutPanel1.Height = 150 + (this.Height / 15);

            listBox1.Height = -90 + this.Height / 2;
            listBox2.Height = -90 + this.Height / 2;

            label10.Top = tableLayoutPanel1.Top - 20;

            checkedListBox1.Width = this.Width / 8;
            checkedListBox2.Width = this.Width / 8;
            checkedListBox3.Width = this.Width / 8;
            checkedListBox4.Width = this.Width / 8;
            checkedListBox5.Width = this.Width / 8;
            checkedListBox6.Width = this.Width / 8;
            checkedListBox7.Width = this.Width / 8;
        }

        private void checkedListBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox4_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {

        }

        private void checkedListBox5_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {

        }
    }
}
