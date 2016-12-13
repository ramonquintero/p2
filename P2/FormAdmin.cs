using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2
{
    public partial class FormAdmin : Form
    {
        int usuario;
        AccesoADatos acceso;
        string dbpais="";
        string periodo="";
        ImagenProceso imagen;
        PictureBox img;

        public FormAdmin(int usr)
        {
            InitializeComponent();
            usuario = usr;
            acceso = new AccesoADatos(Application.StartupPath);
            img = pictureBox1;
            imagen = new ImagenProceso(img, this);
            imagen.fin_espere();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            acceso.modificar_menu(ref  menuStrip1, usuario);
        }

        private void usuariosYPerfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUsuariosYPerfiles f = new FormUsuariosYPerfiles(usuario);
            f.MdiParent = this;
            f.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void seleccionarPaísYPeríodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPaisPeriodo f = new FormPaisPeriodo(usuario, ref dbpais, ref periodo);
            f.MdiParent = this;
            f.Show();
        }

        private void abrirNuevoPeríodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog(this);
            this.Close();
        }

        private void asignarUsuarioAlSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPeriodo f = new FormPeriodo(usuario);
            f.MdiParent = this;
            f.Show();
        }

        private void entradaDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
                FormRegistro f = new FormRegistro(usuario,periodo,dbpais);
                f.MdiParent = this;
                f.Show();
                /*ResoluciónDelExpediente f1 = new ResoluciónDelExpediente();
                f1.MdiParent = this;
                f1.Show();*/
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");
        }

        public void afectarPaisPeriodo(string pais, string per)
        {
            dbpais = pais;
            periodo = per;
        }

        private void informeSocioEconómicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
                FormFicha f = new FormFicha(usuario, periodo, dbpais);
                f.MdiParent = this;
                f.Show();
                /*ResoluciónDelExpediente f1 = new ResoluciónDelExpediente();
                f1.MdiParent = this;
                f1.Show();*/
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");
        }

        private void salidaDeDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
            FormStatus f = new FormStatus(usuario, periodo, dbpais);
            f.MdiParent = this;
            f.Show();
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");
        }

        private void emitirHojasDeInstrucciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
            FormPrintSol f = new FormPrintSol(usuario, periodo, dbpais);
            f.MdiParent = this;
            f.Show();
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");
        
        }

        private void resolucionesFavorablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
                imagen.espere();
                AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
                acceso.accesapais(Application.StartupPath, periodo, dbpais);
                acceso.Resoluciones_favorables(periodo,dbpais, saveFileDialog1);
                imagen.fin_espere();
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");
        }

        private void resolucionesDenegatoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
                imagen.espere();
                AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
                acceso.accesapais(Application.StartupPath, periodo, dbpais);
                acceso.Resoluciones_denegatorias(periodo, dbpais, saveFileDialog1);
                imagen.fin_espere();
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");
        }

        private void justificantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
                imagen.espere();
                AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
                acceso.accesapais(Application.StartupPath, periodo, dbpais);
                acceso.Justificante(periodo, dbpais, saveFileDialog1);
                imagen.fin_espere();
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");
        }

        private void paisesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPaises f = new FormPaises();
            f.MdiParent = this;
            f.Show();
        }

        private void estadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEstado f = new FormEstado();
            f.MdiParent = this;
            f.Show();
        }

        private void enviadoAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManStatus f = new FormManStatus();
            f.MdiParent = this;
            f.Show();
        }

        private void ciudadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCiudad f = new FormCiudad();
            f.MdiParent = this;
            f.Show();
        }

        private void emitirResolucionesYJustificantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMedio f = new FormMedio(usuario, periodo, dbpais);
            f.MdiParent = this;
            f.Show();
        }

        private void registroDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
                FormPago f = new FormPago(usuario, periodo, dbpais);
                f.MdiParent = this;
                f.Show();
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");
        }

        private void busquedaYConsultaDeDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
                FormMarca f = new FormMarca(usuario, periodo, dbpais);
                f.MdiParent = this;
                f.Show();
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");
        }

        private void hIstorialDeRegistroPorFechaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
                FormResol f = new FormResol(usuario, periodo, dbpais);
                f.MdiParent = this;
                f.Show();
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");

        }

        private void cuadroResumenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if ((dbpais.Length > 0) && (periodo.Length > 0))
            {*/
                //FormEstadis f = new FormEstadis(usuario, periodo, dbpais);
                FormEstadis f = new FormEstadis();
                f.MdiParent = this;
                f.Show();
            /*}
            else
                MessageBox.Show("Debe seleccionar un período y un país");*/
        }

        private void recursosDeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reqisitosYPlazosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void resumenDeSolicitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((dbpais.Length > 0) && (periodo.Length > 0))
            {
            FormListadoSolicitudes f = new FormListadoSolicitudes(usuario, periodo, dbpais);
            /*FormEstadis f = new FormEstadis();*/
            f.MdiParent = this;
            f.Show();
            }
            else
                MessageBox.Show("Debe seleccionar un período y un país");
        }
    }
}
