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
    public partial class FormListaDeFichas : Form
    {
        AccesoADatos acceso = new AccesoADatos(Application.StartupPath);
        string periodo = "";
        string pais = "";
        FormFicha padre;
        DataTable dt;
        public FormListaDeFichas(FormFicha f, string per, string pa)
        {
            InitializeComponent();
            periodo = per;
            pais = pa;
            padre = f;

        }

        private void FormListaDeFichas_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'venezuelaDataSet4.InfSocial' Puede moverla o quitarla según sea necesario.
            //this.infSocialTableAdapter.Fill(this.venezuelaDataSet4.InfSocial);
            acceso.accesapais(Application.StartupPath,periodo, pais);
                        
            acceso.grid_fichas(ref dt, ref dataGridView1, periodo);
            

        }
    }
}
