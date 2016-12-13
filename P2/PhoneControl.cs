using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2
{
    public partial class PhoneControl : UserControl
    {
        public PhoneControl()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == 4)
            {
                textBox2.Focus();
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue > 47 && e.KeyValue < 58))
                e.SuppressKeyPress = true;
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue > 47 && e.KeyValue < 58))
                e.SuppressKeyPress = true;
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue > 47 && e.KeyValue < 58))
                e.SuppressKeyPress = true;
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.KeyValue > 47 && e.KeyValue < 58))
                e.SuppressKeyPress = true;
        }

        public string telefono
        {
            get
            {
                return ("("+textBox1.Text +")"+ textBox2.Text +"-"+ textBox3.Text +"-"+ textBox4.Text);
            }
        }

        public string codigo_area
        {
            set
            {
                textBox1.Text = value;
            }
        }

        public string numero
        {
            set
            {
                textBox2.Text = value.ToString().Substring(0,2);
                textBox3.Text = value.ToString().Substring(3, 4);
                textBox4.Text = value.ToString().Substring(5, 6);
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Int32 number;
            if (!((Int32.TryParse(e.KeyChar.ToString(), out number)) || (e.KeyChar == 8)))
            {
                e.Handled = true;
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Int32 number;
            if (!((Int32.TryParse(e.KeyChar.ToString(), out number)) || (e.KeyChar == 8)))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            Int32 number;
            if (!((Int32.TryParse(e.KeyChar.ToString(), out number)) || (e.KeyChar == 8)))
            {
                e.Handled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            Int32 number;
            if (!((Int32.TryParse(e.KeyChar.ToString(), out number))||(e.KeyChar==8)))
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == 3)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == 2)
            {
                textBox4.Focus();
            }
        }
    }
}
