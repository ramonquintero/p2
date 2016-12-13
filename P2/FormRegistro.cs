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
    
    public partial class FormRegistro : Form
    {
        private int usuario;
        private string periodo;
        private string pais;
        ResoluciónDelExpediente f1;
        FormListaRegistro f2;
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        ComboBoxWrap combocausas = new ComboBoxWrap();
        ImagenProceso imagen;
        PictureBox img;
        //bool nuevoregistro = false;
        string[] resolucion = new string[12];

        public FormRegistro(int usr, string per, string pa)
        {
            InitializeComponent();
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
            usuario=usr;
            periodo = per;
            pais = pa;
            acceso.accesapais(Application.StartupPath,periodo, pais);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FormRegistro_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'venezuelaDataSet3.Solicitudes' Puede moverla o quitarla según sea necesario.
            this.solicitudesTableAdapter.Connection.ConnectionString = acceso.stringdeconexionpais;
            // TODO: esta línea de código carga datos en la tabla 'venezuelaDataSet2.Ingresos' Puede moverla o quitarla según sea necesario.
            this.ingresosTableAdapter.Connection.ConnectionString = acceso.stringdeconexionpais;
            // TODO: esta línea de código carga datos en la tabla 'venezuelaDataSet1.Familiares' Puede moverla o quitarla según sea necesario.
            this.familiaresTableAdapter.Connection.ConnectionString = acceso.stringdeconexionpais;
            
            textBox3.Text = periodo;

            acceso.paises(ref comboBox1);

            tabPage1.Controls.Add(combocausas);
            combocausas.Left = 120;
            combocausas.Top = 140;
            combocausas.Width = 472;
            combocausas.MaxDropDownItems = 10;
            combocausas.DropDownHeight = 100;

            acceso.causas(ref combocausas);

            acceso.ciudades_solicitudes(ref comboBox3);

            //acceso.estado_solicitudes(ref comboBox2);

            acceso.estado_solicitudes(ref comboBox4);

            acceso.parentesco(ref comboBox5);

            acceso.accesapais(Application.StartupPath,periodo, pais);

            acceso.usuarios(ref comboBox14);
            comboBox14.Text = "";

            acceso.medio(ref comboBox7);

            //actualizar_familiares("","");

        }

        private void actualizar_familiares(string periodo, string expediente)
        {
            this.familiaresTableAdapter.Fill(this.venezuelaDataSet1.Familiares);
            familiaresBindingSource.Filter = "Periodo='"+periodo+"' AND NumExp='"+expediente+"'";
            dataGridViewFamiliares.Refresh();
        }

        private void actualizar_ingresos(string periodo, string expediente)
        {
            this.ingresosTableAdapter.Fill(this.venezuelaDataSet2.Ingresos);
            ingresosBindingSource.Filter = "Periodo='" + periodo + "' AND NumExp='" + expediente + "'";
            
        }

        private void actualizar_historico(string apellidos, string nombres)
        {
            this.solicitudesTableAdapter.Fill(this.venezuelaDataSet3.Solicitudes);
            solicitudesBindingSource.Filter = "Apellidos like '%" + apellidos.Trim() + "%' AND Nombres like '%" + nombres.Trim() + "%'";
            //dataGridViewHistorico.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            f1 = new ResoluciónDelExpediente(this,pais);
            f1.MdiParent = this.MdiParent;
            f1.Show();
            f1.ActualizarDataInterna(resolucion);
            button1.Visible = false;
        }

        public void mostrar_resolucion()
        {
            button1.Visible=true;
        }

        public void mostrar_registros()
        {
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f2 = new FormListaRegistro(this,periodo, pais);
            f2.MdiParent = this.MdiParent;
            f2.Show();
            button2.Visible = false;
        }

        private void FormRegistro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (f1 != null)
                f1.Close();
            if (f2 != null)
                f2.Close();
        }

        public void Cargar_resolucion(DataGridViewRow Row)
        {
            resolucion[0] = Row.Cells[42].Value.ToString(); //marca
            resolucion[1] = Row.Cells[48].Value.ToString(); //fecmarca
            resolucion[2] = periodo;
            resolucion[3] = Row.Cells[1].Value.ToString();
            resolucion[4] = Row.Cells[52].Value.ToString(); //IdStatus
            resolucion[5] = Row.Cells[53].Value.ToString(); //FecStatus
            resolucion[6] = Row.Cells[58].Value.ToString(); //FecFallece
            resolucion[7] = Row.Cells[59].Value.ToString(); //BenefCh
            resolucion[8] = Row.Cells[57].Value.ToString(); //CambioED
            resolucion[9] = Row.Cells[56].Value.ToString(); //CambioDB
            resolucion[10] = pais;
            resolucion[11] = acceso.nombre_de_usuario(usuario);
        }

        public void mostrar_registros_cambio_registro(DataGridViewRow Row,Boolean todosLosCampos)
        {
            Cargar_resolucion(Row);
            try
            {
                dateTimePicker1.Value = DateTime.Parse(Row.Cells[2].Value.ToString());
            }
            catch (Exception e)
            {
            }
            try{
            textBox3.Text = Row.Cells[0].Value.ToString();
                }
            catch (Exception e)
            {
            }
            try{
                if (textBox9.Enabled==true)
            textBox9.Text = Row.Cells[1].Value.ToString();
                }
            catch (Exception e)
            {
            }
            try   {
            comboBox14.Text = Row.Cells[50].Value.ToString();
                }
            catch (Exception e)
            {
            }
            try{
            textBox1.Text = Row.Cells[3].Value.ToString();
            }
            catch (Exception e)
            {
            }
            try{
            textBox2.Text = Row.Cells[4].Value.ToString();
            }
            catch (Exception e)
            {
            }
            try{
            dateTimePicker2.Value = DateTime.Parse(Row.Cells[8].Value.ToString());
            }
            catch (Exception e)
            {
            }
            try{
            comboBox2.Text = Row.Cells[9].Value.ToString();
            comboBox1.Text = Row.Cells[61].Value.ToString();
            if (textBox6.Enabled==true)
            textBox6.Text = Row.Cells[10].Value.ToString();
            }
            catch (Exception e)
            {
            }
            try{
            dateTimePicker4.Value = DateTime.Parse(Row.Cells[11].Value.ToString());
            comboBox7.Text = Row.Cells[62].Value.ToString();
            }
            catch (Exception e)
            {
            }
            try{
            textBox4.Text = Row.Cells[5].Value.ToString();
            textBox5.Text = Row.Cells[6].Value.ToString();
            }
            catch (Exception e)
            {
            }
            try{
            dateTimePicker3.Value = DateTime.Parse(Row.Cells[7].Value.ToString());
            }
            catch (Exception e)
            {
            }
            try{
            textBox7.Text = Row.Cells[12].Value.ToString();
            textBox8.Text = Row.Cells[13].Value.ToString();
            comboBox3.Text = Row.Cells[14].Value.ToString();
            comboBox4.Text = Row.Cells[15].Value.ToString();
            string telefonos = Row.Cells[16].Value.ToString();
            telefonos = telefonos.Replace("-","");
            telefonos = telefonos.Replace(".", "");
            telefonos = telefonos.Replace(",", "");
            telefonos = telefonos.Replace("(", "");
            telefonos = telefonos.Replace(")", "");
            telefonos = telefonos.Replace("_", "");
            string[] tel = telefonos.Split('/');
            textBox23.Text = tel[0];
            textBox24.Text = tel[1];
            }
            catch (Exception ex) { }
            try{
            combocausas.Text = Row.Cells[36].Value.ToString();
            textBox17.Text = Row.Cells[37].Value.ToString();

            actualizar_familiares(textBox3.Text,textBox9.Text);

            actualizar_ingresos(textBox3.Text, textBox9.Text);
            }
            catch (Exception e)
            {
            }
            try
            {
                dateTimePicker6.Value = DateTime.Parse(Row.Cells[17].Value.ToString());
            }
            catch (Exception ex) 
            {
                dateTimePicker6.Value = DateTime.Now;
            }
            try{
            comboBox6.Text = Row.Cells[18].Value.ToString();
            textBox21.Text = Row.Cells[20].Value.ToString();
            comboBox8.Text = Row.Cells[19].Value.ToString();
            textBox22.Text = Row.Cells[25].Value.ToString();
            if (Row.Cells[21].Value.ToString().Equals("True"))
            {
                comboBox9.Text = "Trab. Cuenta ajena";
            }
            else
            if (Row.Cells[22].Value.ToString().Equals("True"))
            {
                comboBox9.Text = "Autónomo";
            }
            else
            if (Row.Cells[23].Value.ToString().Equals("True"))
            {
                comboBox9.Text = "No Activo";
            }
            else
            if (Row.Cells[24].Value.ToString().Equals("True"))
            {
                comboBox9.Text = "Pensionista";
            }
            else
                comboBox9.Text = "";
            comboBox10.Text = Row.Cells[26].Value.ToString();
            comboBox11.Text = Row.Cells[27].Value.ToString();
            if (Row.Cells[30].Value.ToString().Equals("True"))
            {
                comboBox12.Text = "Si";
            }
            else
            if (Row.Cells[30].Value.ToString().Equals("False"))
            {
                comboBox12.Text = "No";
            }
            else
                comboBox12.Text = "";
            }
            catch (Exception e)
            {
            }
            try
            {
                dateTimePicker7.Value = DateTime.Parse(Row.Cells[28].Value.ToString());
            }
            catch (Exception ex)
            {
                dateTimePicker7.Value = DateTime.Now;
            }
            try
            {
                dateTimePicker8.Value = DateTime.Parse(Row.Cells[29].Value.ToString());
            }
            catch (Exception ex)
            {
                dateTimePicker8.Value = DateTime.Now;
            }

            try{
            if (Row.Cells[31].Value.ToString().Equals("True"))
            {
                comboBox13.Text = "Situación de Precariedad";
            }
            else
                if (Row.Cells[32].Value.ToString().Equals("True"))
                {
                    comboBox13.Text = "Gastos de Asistencia Jurídica";
                }
                else
                    if (Row.Cells[33].Value.ToString().Equals("True"))
                    {
                        comboBox13.Text = "Gastos de Asistencia Sanitaria";
                    }
                    else
                        if (Row.Cells[34].Value.ToString().Equals("True"))
                        {
                            comboBox13.Text = "Causas Extraordinarias Derivadas de la Emigración o Retorno";
                        }
                        else
                            if (Row.Cells[35].Value.ToString().Equals("True"))
                            {
                                comboBox13.Text = "Familia Monoparental";
                            }
                            else
                                comboBox13.Text = "";
            
            }
            catch (Exception e)
            {
            }
            actualizar_historico(textBox1.Text, textBox2.Text);
        }

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {
            int number;
            if (!((Int32.TryParse(e.KeyChar.ToString(), out number)) || (e.KeyChar == 8)))
            {
                e.Handled = true;
            }
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            int number;
            if (!((Int32.TryParse(e.KeyChar.ToString(), out number)) || (e.KeyChar == 8)))
            {
                e.Handled = true;
            }
        }

        private void dataGridViewFamiliares_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                textBox11.Text = dataGridViewFamiliares.CurrentRow.Cells[2].Value.ToString();
                textBox12.Text = dataGridViewFamiliares.CurrentRow.Cells[3].Value.ToString();
                textBox13.Text = dataGridViewFamiliares.CurrentRow.Cells[4].Value.ToString();
                textBox14.Text = dataGridViewFamiliares.CurrentRow.Cells[5].Value.ToString();
                textBox15.Text = dataGridViewFamiliares.CurrentRow.Cells[7].Value.ToString();
                comboBox5.Text = dataGridViewFamiliares.CurrentRow.Cells[6].Value.ToString();
            }
            catch (Exception ex) {
                textBox11.Text = textBox12.Text = textBox13.Text = textBox14.Text = textBox15.Text =
                    comboBox5.Text = "";
            }
        }

        private void dataGridViewIngresos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
            //textBox16.Text = dataGridViewIngresos.CurrentRow.Cells[1].Value.ToString();
            textBox18.Text = dataGridViewIngresos.CurrentRow.Cells[2].Value.ToString();
            textBox19.Text = dataGridViewIngresos.CurrentRow.Cells[3].Value.ToString();
            try
            {
                dateTimePicker5.Value = DateTime.Parse(dataGridViewIngresos.CurrentRow.Cells[4].Value.ToString());
            }
            catch (Exception ex)
            {
                dateTimePicker5.Value = DateTime.Now;
            }
            textBox20.Text = dataGridViewIngresos.CurrentRow.Cells[5].Value.ToString();
            }
            catch (Exception ex) {
                textBox18.Text = textBox19.Text = textBox20.Text = "";
                dateTimePicker5.Value = DateTime.Now;
            }
        }

        private void buttonNuevoFamiliar_Click(object sender, EventArgs e)
        {
            button3.Visible = button4.Visible = button5.Visible = false;
            buttonEliminarFamiliar.Text = "Cancelar";
            buttonModificarFamiliar.Text = "Guardar";
            buttonNuevoFamiliar.Enabled = false;
            textBox11.Text = textBox12.Text = textBox13.Text = textBox14.Text = textBox15.Text =
                comboBox5.Text = "";
        }

        private void buttonModificarFamiliar_Click(object sender, EventArgs e)
        {
            if (buttonModificarFamiliar.Text.Equals("Guardar"))
            {
                //Nuevo Familiar
                acceso.guardar_familiar(periodo, textBox9.Text, textBox11.Text, textBox12.Text, textBox13.Text,
                    textBox14.Text, comboBox5.Text, textBox15.Text, acceso.nombre_de_usuario(usuario), DateTime.Now.ToShortDateString());

            }
            else
            {
                //Modificar Familiar
                acceso.modificar_familiar(periodo, textBox9.Text, textBox11.Text, textBox12.Text, textBox13.Text,
                    textBox14.Text, comboBox5.Text, textBox15.Text, acceso.nombre_de_usuario(usuario), DateTime.Now.ToShortDateString());
            }

            button3.Visible = button4.Visible = button5.Visible = true;
            buttonEliminarFamiliar.Text = "Eliminar";
            buttonModificarFamiliar.Text = "Modificar";
            buttonNuevoFamiliar.Enabled = true;
            actualizar_familiares(textBox3.Text, textBox9.Text);
        }

        private void buttonEliminarFamiliar_Click(object sender, EventArgs e)
        {
            if (buttonEliminarFamiliar.Text.Equals("Cancelar"))
            {
                //Abortar nuevo familiar
                
            }
            else
            {
                if (MessageBox.Show("El familiar que se muestra será eliminado. Está seguro?", "Eliminar familiar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Eliminar familiar
                    acceso.eliminar_familiar(periodo, textBox9.Text, textBox11.Text, textBox12.Text);
                    textBox11.Text = textBox12.Text = textBox13.Text = textBox14.Text = textBox15.Text =
                    comboBox5.Text = "";
                }
            }

            button3.Visible = button4.Visible = button5.Visible = true;
            buttonEliminarFamiliar.Text = "Eliminar";
            buttonModificarFamiliar.Text = "Modificar";
            buttonNuevoFamiliar.Enabled = true;
            actualizar_familiares(textBox3.Text, textBox9.Text);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = button6.Enabled = false;
            button5.Text = "Cancelar";
            acceso.nuevo_expediente(ref textBox9,periodo);
            textBox9.Enabled = false;
            acceso.nuevo_registro(ref textBox6, periodo);
            textBox6.Enabled = false;
            textBox3.Text = periodo;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if ((textBox9.Text.Length > 0) && (acceso.expediente_valido(textBox9.Text)))
            {
                button3.Enabled = button6.Enabled = false;
                button5.Text = "Cancelar";
                textBox9.Enabled = false;
                acceso.nuevo_registro(ref textBox6, periodo);
                textBox6.Enabled = false;
                //nuevoregistro = true;
                textBox3.Text = periodo;
            }
            else
            {
                MessageBox.Show("Debe indicar un expediente válido para agregar una nueva entrada");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text.Equals("Cancelar"))
            {
                button5.Text = "Eliminar";
                button3.Enabled = button6.Enabled = true;
                textBox9.Enabled = true;
                textBox6.Enabled = true; 
            }
            else
            {
                if (MessageBox.Show("El expediente que se muestra será eliminado completamente. Está seguro?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    imagen.espere();
                    acceso.eliminarRegistro(periodo, textBox9.Text, textBox6.Text);
                    actualizar_historico(textBox1.Text, textBox2.Text);
                    foreach (Control c in this.Controls)
                    {
                        try
                        {
                            if ((c is TextBox) || (c is ComboBox))
                                c.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    foreach (Control c in groupBox1.Controls)
                    {
                        try
                        {
                            if ((c is TextBox) || (c is ComboBox))
                                c.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    foreach (Control c in groupBox2.Controls)
                    {
                        try
                        {
                            if ((c is TextBox) || (c is ComboBox))
                                c.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    foreach (Control c in groupBox3.Controls)
                    {
                        try
                        {
                            if ((c is TextBox) || (c is ComboBox))
                                c.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    foreach (Control c in groupBox4.Controls)
                    {
                        try
                        {
                            if ((c is TextBox) || (c is ComboBox))
                                c.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    foreach (Control c in tabPage1.Controls)
                    {
                        try
                        {
                            if ((c is TextBox) || (c is ComboBox))
                                c.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    foreach (Control c in tabPage2.Controls)
                    {
                        try
                        {
                            if ((c is TextBox) || (c is ComboBox))
                                c.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    foreach (Control c in tabPage3.Controls)
                    {
                        try
                        {
                            if ((c is TextBox) || (c is ComboBox))
                                c.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    foreach (Control c in tabPage4.Controls)
                    {
                        try
                        {
                            if ((c is TextBox) || (c is ComboBox))
                                c.Text = "";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    try
                    {
                        dataGridViewFamiliares.Rows.Clear();
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        dataGridViewHistorico.Rows.Clear();
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        dataGridViewIngresos.Rows.Clear();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                imagen.fin_espere();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string telefonos = textBox23.Text+" / "+textBox24.Text;
            string cantidadfamiliares = dataGridViewFamiliares.Rows.Count.ToString();
            string hoy = DateTime.Now.ToShortDateString();
            try
            {
                imagen.espere();
                if (button5.Text.Equals("Cancelar"))
                {
                    //Se trata de un nuevo registro
                    button5.Text = "Eliminar";
                    button3.Enabled = button6.Enabled = true;
                    textBox9.Enabled = true;
                    textBox6.Enabled = true;
                    acceso.agregarRegistro(periodo, textBox9.Text, dateTimePicker1.Value.ToShortDateString(), textBox1.Text, textBox2.Text,
                        textBox4.Text, textBox5.Text, dateTimePicker3.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString(),
                        comboBox2.Text, textBox6.Text, dateTimePicker4.Value.ToShortDateString(), textBox7.Text, textBox8.Text, comboBox3.Text,
                        comboBox4.Text, telefonos, dateTimePicker6.Value.ToShortDateString(), comboBox6.Text, comboBox8.Text, textBox21.Text,
                        comboBox9.Text.Equals("Trab. Cuenta ajena"), comboBox9.Text.Equals("Autónomo"), comboBox9.Text.Equals("No Activo"),
                        comboBox9.Text.Equals("Pensionista"), textBox22.Text, comboBox10.Text, comboBox11.Text, dateTimePicker7.Value.ToShortDateString(),
                        dateTimePicker8.Value.ToShortDateString(), comboBox12.Text.Equals("Si"), comboBox13.Text.Equals("Situación de Precariedad"),
                        comboBox13.Text.Equals("Gastos de Asistencia Jurídica"), comboBox13.Text.Equals("Gastos de Asistencia Sanitaria"),
                        comboBox13.Text.Equals("Causas Extraordinarias Derivadas de la Emigración o Retorno"), comboBox13.Text.Equals("Familia Monoparental"),
                        combocausas.Text, textBox17.Text, cantidadfamiliares, hoy, hoy, "", acceso.nombre_de_usuario(usuario), hoy, comboBox7.Text, comboBox1.Text);
                    actualizar_historico(textBox1.Text, textBox2.Text);
                }
                else
                {
                    //Modificar un registro existente
                    acceso.modificarRegistro(periodo, textBox9.Text, dateTimePicker1.Value.ToShortDateString(), textBox1.Text, textBox2.Text,
                        textBox4.Text, textBox5.Text, dateTimePicker3.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString(),
                        comboBox2.Text, textBox6.Text, dateTimePicker4.Value.ToShortDateString(), textBox7.Text, textBox8.Text, comboBox3.Text,
                        comboBox4.Text, telefonos, dateTimePicker6.Value.ToShortDateString(), comboBox6.Text, comboBox8.Text, textBox21.Text,
                        comboBox9.Text.Equals("Trab. Cuenta ajena"), comboBox9.Text.Equals("Autónomo"), comboBox9.Text.Equals("No Activo"),
                        comboBox9.Text.Equals("Pensionista"), textBox22.Text, comboBox10.Text, comboBox11.Text, dateTimePicker7.Value.ToShortDateString(),
                        dateTimePicker8.Value.ToShortDateString(), comboBox12.Text.Equals("Si"), comboBox13.Text.Equals("Situación de Precariedad"),
                        comboBox13.Text.Equals("Gastos de Asistencia Jurídica"), comboBox13.Text.Equals("Gastos de Asistencia Sanitaria"),
                        comboBox13.Text.Equals("Causas Extraordinarias Derivadas de la Emigración o Retorno"), comboBox13.Text.Equals("Familia Monoparental"),
                        combocausas.Text, textBox17.Text, cantidadfamiliares, hoy, hoy, "", acceso.nombre_de_usuario(usuario), hoy, comboBox7.Text, comboBox1.Text);
                    actualizar_historico(textBox1.Text, textBox2.Text);

                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                imagen.fin_espere();
            }
        }

        private void buttonNuevoIngreso_Click(object sender, EventArgs e)
        {
            button3.Visible = button4.Visible = button5.Visible = false;
            buttonEliminarIngreso.Text = "Cancelar";
            buttonModificarIngreso.Text = "Guardar";
            buttonNuevoIngreso.Enabled = false;
            textBox18.Text = textBox19.Text = textBox20.Text = "";
            dateTimePicker5.Value = DateTime.Now;
        }

        private void buttonModificarIngreso_Click(object sender, EventArgs e)
        {
            if (buttonModificarIngreso.Text.Equals("Guardar"))
            {
                //Nuevo Ingreso
                acceso.guardar_ingreso(periodo, textBox9.Text, textBox18.Text, textBox19.Text,dateTimePicker5.Value.ToShortDateString(),
                    textBox20.Text, acceso.nombre_de_usuario(usuario), DateTime.Now.ToShortDateString());

            }
            else
            {
                //Modificar Ingreso
                acceso.modificar_ingreso(periodo, textBox9.Text, textBox18.Text, textBox19.Text, dateTimePicker5.Value.ToShortDateString(),
                    textBox20.Text, acceso.nombre_de_usuario(usuario), DateTime.Now.ToShortDateString());
            }

            button3.Visible = button4.Visible = button5.Visible = true;
            buttonEliminarIngreso.Text = "Eliminar";
            buttonModificarIngreso.Text = "Modificar";
            buttonNuevoIngreso.Enabled = true;
            actualizar_ingresos(textBox3.Text, textBox9.Text);

        }

        private void buttonEliminarIngreso_Click(object sender, EventArgs e)
        {
            if (buttonEliminarIngreso.Text.Equals("Cancelar"))
            {
                //Abortar nuevo Ingreso

            }
            else
            {
                if (MessageBox.Show("El ingreso que se muestra será eliminado. Está seguro?", "Eliminar ingreso", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Eliminar familiar
                    acceso.eliminar_ingreso(periodo, textBox9.Text, textBox18.Text, textBox19.Text);
                    textBox18.Text = textBox19.Text = textBox20.Text = "";
                    dateTimePicker5.Value = DateTime.Now;
                }
            }

            button3.Visible = button4.Visible = button5.Visible = true;
            buttonEliminarIngreso.Text = "Eliminar";
            buttonModificarIngreso.Text = "Modificar";
            buttonNuevoIngreso.Enabled = true;
            actualizar_ingresos(textBox3.Text, textBox9.Text);
        }
    }
}
