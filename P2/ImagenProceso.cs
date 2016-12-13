using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace P2
{
    class ImagenProceso
    {
        PictureBox img;
        Form actual;
        public ImagenProceso(PictureBox p, Form f)
        {
            img = p;
            actual = f;
        }

        public void espere()
        {

            img.Left = 0;
            img.Top = 0;
            img.Width = actual.Width;
            img.Height = actual.Height;
            img.Image = Image.FromFile(Application.StartupPath + "\\loading01.gif");
            img.SizeMode = PictureBoxSizeMode.StretchImage;
            img.Refresh();
            Application.DoEvents();
        }

        public void fin_espere()
        {
            img.Left = 2000;
            img.Top = 2000;

        }
    }
}
