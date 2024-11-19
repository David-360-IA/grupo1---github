using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grupo1___github
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            textBox1.KeyPress += SoloNumeros_KeyPress;
            textBox4.KeyPress += SoloNumeros_KeyPress;
            textBox7.KeyPress += SoloNumeros_KeyPress;


            textBox2.KeyPress += SoloLetras_KeyPress;
            textBox3.KeyPress += SoloLetras_KeyPress;
        }
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
