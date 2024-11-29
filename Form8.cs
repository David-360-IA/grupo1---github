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
    public partial class Form8 : Form
    {
        public class Nodo
        {
            public int Valor;
            public Nodo Izquierdo;
            public Nodo Derecho;

            public Nodo(int valor)
            {
                Valor = valor;
                Izquierdo = null;
                Derecho = null;
            }
        }

        private ArbolBinario arbol;

        public Form8()
        {
            InitializeComponent();
            arbol = new ArbolBinario();
            textBox1.KeyPress += SoloNumeros_KeyPress;
            textBox4.KeyPress += SoloNumeros_KeyPress;
            textBox7.KeyPress += SoloNumeros_KeyPress;
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
        public class ArbolBinario
        {
            public Nodo Raiz;

            public ArbolBinario()
            {
                Raiz = null;
            }

            public void Insertar(int valor)
            {
                Raiz = InsertarRecursivo(Raiz, valor);
            }

            private Nodo InsertarRecursivo(Nodo raiz, int valor)
            {
                if (raiz == null)
                {
                    return new Nodo(valor);
                }

                if (valor < raiz.Valor)
                {
                    raiz.Izquierdo = InsertarRecursivo(raiz.Izquierdo, valor);
                }
                else if (valor > raiz.Valor)
                {
                    raiz.Derecho = InsertarRecursivo(raiz.Derecho, valor);
                }

                return raiz;
            }

       
            public void Dibujar(Graphics g, Nodo nodo, int x, int y, int offset)
            {
                if (nodo == null) return;

              
                g.FillEllipse(Brushes.LightBlue, x, y, 30, 30);
                g.DrawString(nodo.Valor.ToString(), new Font("Arial", 10), Brushes.Black, x + 10, y + 10);

                
                if (nodo.Izquierdo != null)
                {
                    g.DrawLine(Pens.Black, x + 15, y + 30, x - offset + 15, y + 60);
                    Dibujar(g, nodo.Izquierdo, x - offset, y + 60, offset / 2);
                }
                if (nodo.Derecho != null)
                {
                    g.DrawLine(Pens.Black, x + 15, y + 30, x + offset + 15, y + 60);
                    Dibujar(g, nodo.Derecho, x + offset, y + 60, offset / 2);
                }
            }
        }




        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            panel1.Invalidate();
            arbol = new ArbolBinario();

           
            InsertarValores();

        
            panel1.Invalidate();

        }
        private void InsertarValores()
        {
            if (int.TryParse(textBox1.Text, out int val1)) arbol.Insertar(val1);
            if (int.TryParse(textBox4.Text, out int val4)) arbol.Insertar(val4);
            if (int.TryParse(textBox7.Text, out int val7)) arbol.Insertar(val7);

           
            if (int.TryParse(dateTimePicker1.Value.Year.ToString(), out int valDate))
                arbol.Insertar(valDate);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            arbol.Dibujar(e.Graphics, arbol.Raiz, panel1.Width / 2, 20, 40);
        }
    }
}
