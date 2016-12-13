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
    public partial class FormResol : Form
    {
        private int usuario;
        private string periodo;
        private string pais;
        ImagenProceso imagen;
        PictureBox img;
        DataTable dt;
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        public FormResol(int usr, string per, string pa)
        {
            InitializeComponent();
            //img = pictureBox1;
            //imagen = new ImagenProceso(img, this);
            //imagen.fin_espere();
            usuario = usr;
            periodo = per;
            pais = pa;
            acceso.accesapais(Application.StartupPath, periodo, pais);
        }

        private void FormResol_Load(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
