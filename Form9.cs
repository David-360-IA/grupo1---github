using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace grupo1___github
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
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
        public class Nodo
        {
            public int Valor;
            public Nodo Izquierda;
            public Nodo Derecha;
            public int Altura;

            public Nodo(int valor)
            {
                Valor = valor;
                Izquierda = null;
                Derecha = null;
                Altura = 1; // Altura inicial de un nodo es 1
            }
        }

        public class ArbolAVL
        {
            private Nodo raiz;

            private int Altura(Nodo nodo)
            {
                return nodo == null ? 0 : nodo.Altura;
            }

            private int Balance(Nodo nodo)
            {
                return nodo == null ? 0 : Altura(nodo.Izquierda) - Altura(nodo.Derecha);
            }

            private Nodo RotacionDerecha(Nodo y)
            {
                Nodo x = y.Izquierda;
                Nodo T2 = x.Derecha;
                x.Derecha = y;
                y.Izquierda = T2;
                y.Altura = Math.Max(Altura(y.Izquierda), Altura(y.Derecha)) + 1;
                x.Altura = Math.Max(Altura(x.Izquierda), Altura(x.Derecha)) + 1;
                return x;
            }

            private Nodo RotacionIzquierda(Nodo x)
            {
                Nodo y = x.Derecha;
                Nodo T2 = y.Izquierda;
                y.Izquierda = x;
                x.Derecha = T2;
                x.Altura = Math.Max(Altura(x.Izquierda), Altura(x.Derecha)) + 1;
                y.Altura = Math.Max(Altura(y.Izquierda), Altura(y.Derecha)) + 1;
                return y;
            }

            public Nodo Insertar(Nodo nodo, int valor)
            {
                if (nodo == null)
                    return new Nodo(valor);

                if (valor < nodo.Valor)
                    nodo.Izquierda = Insertar(nodo.Izquierda, valor);
                else if (valor > nodo.Valor)
                    nodo.Derecha = Insertar(nodo.Derecha, valor);
                else
                    return nodo;

                nodo.Altura = Math.Max(Altura(nodo.Izquierda), Altura(nodo.Derecha)) + 1;

                int balance = Balance(nodo);

                if (balance > 1 && valor < nodo.Izquierda.Valor)
                    return RotacionDerecha(nodo);

                if (balance < -1 && valor > nodo.Derecha.Valor)
                    return RotacionIzquierda(nodo);

                if (balance > 1 && valor > nodo.Izquierda.Valor)
                {
                    nodo.Izquierda = RotacionIzquierda(nodo.Izquierda);
                    return RotacionDerecha(nodo);
                }

                if (balance < -1 && valor < nodo.Derecha.Valor)
                {
                    nodo.Derecha = RotacionDerecha(nodo.Derecha);
                    return RotacionIzquierda(nodo);
                }

                return nodo;
            }

            public void Agregar(int valor)
            {
                raiz = Insertar(raiz, valor);
            }

            public Nodo Raiz()
            {
                return raiz;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
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
            ArbolAVL arbol = new ArbolAVL();

           
            if (int.TryParse(textBox1.Text, out int valor1)) arbol.Agregar(valor1);
            if (int.TryParse(textBox4.Text, out int valor2)) arbol.Agregar(valor2);
            if (int.TryParse(textBox7.Text, out int valor3)) arbol.Agregar(valor3);


            if (int.TryParse(dateTimePicker1.Value.Year.ToString(), out int valDate))
                arbol.Agregar(valDate);


            panel1.Controls.Clear();

           
            DibujarArbol(arbol.Raiz(), panel1);
        }
        private void DibujarArbol(Nodo raiz, Panel panel)
        {
            Graphics g = panel.CreateGraphics();
            g.Clear(Color.White); 

            if (raiz != null)
            {
                // Dibujar el árbol comenzando desde la raíz
                DibujarNodo(raiz, panel.Width / 2, 30, g);
            }
        }

        // Método recursivo para dibujar los nodos del árbol
        private void DibujarNodo(Nodo nodo, int x, int y, Graphics g)
        {
            if (nodo == null)
                return;

            // Dibujar el nodo
            g.FillEllipse(Brushes.LightBlue, x - 20, y - 20, 40, 40);
            g.DrawEllipse(Pens.Black, x - 20, y - 20, 40, 40);
            g.DrawString(nodo.Valor.ToString(), this.Font, Brushes.Black, x - 10, y - 10);

            // Dibujar las líneas hacia los hijos
            if (nodo.Izquierda != null)
                g.DrawLine(Pens.Black, x, y, x - 50, y + 50);
            if (nodo.Derecha != null)
                g.DrawLine(Pens.Black, x, y, x + 50, y + 50);

            // Dibujar los hijos recursivamente
            if (nodo.Izquierda != null)
                DibujarNodo(nodo.Izquierda, x - 50, y + 50, g);
            if (nodo.Derecha != null)
                DibujarNodo(nodo.Derecha, x + 50, y + 50, g);
        }


        private void button3_Click(object sender, EventArgs e)
        {
          this.Close();   
        }

        private void Form9_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
