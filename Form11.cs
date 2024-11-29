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
    public partial class Form11 : Form
    {
        private List<Vertice> vertices = new List<Vertice>();
        private List<Arista> aristas = new List<Arista>();
        private int idCounter = 1; 
        public Form11()
        {
            InitializeComponent();
        }
        private class Vertice
        {
            public int Id { get; set; }
            public string Producto { get; set; }
            public Point Posicion { get; set; }
        }

        // Clase para la arista
        private class Arista
        {
            public int VerticeA { get; set; }
            public int VerticeB { get; set; }
            public int Recorrido { get; set; }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string nombreProducto = textBox2.Text;

            if (string.IsNullOrWhiteSpace(nombreProducto))
            {
                MessageBox.Show("Ingrese el producto.");
                return;
            }

            // Generar una posición aleatoria dentro del panel
            Random random = new Random();
            var nuevoVertice = new Vertice
            {
                Id = idCounter,
                Producto = nombreProducto,
                Posicion = new Point(random.Next(50, panel1.Width - 50), random.Next(50, panel1.Height - 50))
            };

            vertices.Add(nuevoVertice);
            idCounter++;

            textBox1.Text = idCounter.ToString();

            // Redibujar el panel
            panel1.Invalidate();

            // Limpiar el campo de texto
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox3.Text, out int verticeA) && int.TryParse(textBox4.Text, out int verticeB) && int.TryParse(textBox5.Text, out int recorrido))
            {
                var VerticeA = vertices.FirstOrDefault(v => v.Id == verticeA);
                var VerticeB = vertices.FirstOrDefault(v => v.Id == verticeB);

                if (VerticeA != null && VerticeB != null)
                {
                    var nuevaArista = new Arista
                    {
                        VerticeA = verticeA,
                        VerticeB = verticeB,
                        Recorrido = recorrido
                    };

                    aristas.Add(nuevaArista);
                    panel1.Invalidate();
                }
                else
                {
                    MessageBox.Show("Uno o ambos vértices no existen.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese valores válidos para los vértices y la distancia.");
            }        
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Complete los campos del recorrido para eliminarlo.");
                return;
            }

            
            var verticeA = vertices.FirstOrDefault(v => v.Id == int.Parse(textBox3.Text));
            var verticeB = vertices.FirstOrDefault(v => v.Id == int.Parse(textBox4.Text));

            if (verticeA == null || verticeB == null)
            {
                MessageBox.Show("Uno o ambos vértices no existen.");
                return;
            }

          
            var arista = aristas.FirstOrDefault(a =>
                (a.VerticeA == verticeA.Id && a.VerticeB == verticeB.Id) ||
                (a.VerticeA == verticeB.Id && a.VerticeB == verticeA.Id));

            if (arista == null)
            {
                MessageBox.Show("No existe una conexión entre los vértices especificados.");
                return;
            }

            aristas.Remove(arista);

            
            panel1.Invalidate();         
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            MessageBox.Show("Distancia eliminada correctamente.");
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n = vertices.Count;

            if (n == 0)
            {
                MessageBox.Show("No hay vértices en el grafo.");
                return;
            }

            
            int[,] dist = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        dist[i, j] = 0; 
                    else
                        dist[i, j] = int.MaxValue / 2; 
                }
            } 

            foreach (var arista in aristas)
            {
                var origen = vertices.FindIndex(v => v.Id == arista.VerticeA);
                var destino = vertices.FindIndex(v => v.Id == arista.VerticeB);

                if (origen != -1 && destino != -1)
                {
                    dist[origen, destino] = arista.Recorrido;
                    dist[destino, origen] = arista.Recorrido; 
                }
            }
       
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }
            
            var resultado = "Matriz de recorrido:\n";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    resultado += (dist[i, j] == int.MaxValue / 2 ? "∞" : dist[i, j].ToString()) + "\t";
                }
                resultado += "\n";
            }

            MessageBox.Show(resultado, "Resultados Finales");
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;

            foreach (var arista in aristas)
            {
                var verticeA = vertices.Find(v => v.Id == arista.VerticeA);
                var verticeB = vertices.Find(v => v.Id == arista.VerticeB);

                if (verticeA != null && verticeB != null)
                {
                    graphics.DrawLine(Pens.Black, verticeA.Posicion, verticeB.Posicion);
                    var midPoint = new Point(
                        (verticeA.Posicion.X + verticeB.Posicion.X) / 2,
                        (verticeA.Posicion.Y + verticeB.Posicion.Y) / 2
                    );
                    graphics.DrawString(arista.Recorrido.ToString(), DefaultFont, Brushes.Red, midPoint);
                }
            }
            
            foreach (var vertice in vertices)
            {              
                graphics.FillEllipse(Brushes.LightBlue, vertice.Posicion.X - 15, vertice.Posicion.Y - 15, 30, 30);
                graphics.DrawEllipse(Pens.Black, vertice.Posicion.X - 15, vertice.Posicion.Y - 15, 30, 30);               
                graphics.DrawString(vertice.Id.ToString(), DefaultFont, Brushes.Black, vertice.Posicion.X - 5, vertice.Posicion.Y - 5);
                graphics.DrawString(vertice.Producto, DefaultFont, Brushes.Black, vertice.Posicion.X - 20, vertice.Posicion.Y + 20);
            }
        }

    }
}

