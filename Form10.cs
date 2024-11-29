using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static grupo1___github.Form10;
using static grupo1___github.Form10.Grafo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace grupo1___github
{
    public partial class Form10 : Form
    {
        private Grafo grafo;  // Guardar el grafo a nivel de clas
        public Form10()
        {
            InitializeComponent();
            grafo = new Grafo(); // Inicializar el grafo
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
        public class Dijkstra
        {
            private Grafo grafo;

            public Dijkstra(Grafo grafo)
            {
                this.grafo = grafo;
            }

            public Dictionary<Node, int> FindShortestPath(Node start)
            {
                var distances = new Dictionary<Node, int>();
                var previousNodes = new Dictionary<Node, Node>();
                var nodes = new List<Node>(grafo.Nodes);

                foreach (var node in nodes)
                {
                    distances[node] = int.MaxValue;
                    previousNodes[node] = null;
                }
                distances[start] = 0;

                while (nodes.Count > 0)
                {
                    nodes = nodes.OrderBy(n => distances[n]).ToList();
                    var currentNode = nodes.First();
                    nodes.Remove(currentNode);

                    foreach (var neighbor in grafo.GetNeighbors(currentNode))
                    {
                        int tentativeDistance = distances[currentNode] + grafo.Edges.First(e => e.From == currentNode && e.To == neighbor).Weight;
                        if (tentativeDistance < distances[neighbor])
                        {
                            distances[neighbor] = tentativeDistance;
                            previousNodes[neighbor] = currentNode;
                        }
                    }
                }

                return distances;
            }
        }
        public class Grafo
        {
            public List<Node> Nodes { get; set; }
            public List<Edge> Edges { get; set; }

            public Grafo()
            {
                Nodes = new List<Node>();
                Edges = new List<Edge>();
            }

            public void AddNode(Node node)
            {
                Nodes.Add(node);
            }

            public void AddEdge(Node from, Node to, int weight)
            {
                Edges.Add(new Edge(from, to, weight));
            }

            public List<Node> GetNeighbors(Node node)
            {
                return Edges.Where(e => e.From == node).Select(e => e.To).ToList();
            }

            public class Node
            {
                public string Name { get; set; }
                public Point Position { get; set; }

                public Node(string name, Point position)
                {
                    Name = name;
                    Position = position;
                }
            }

            public class Edge
            {
                public Node From { get; set; }
                public Node To { get; set; }
                public int Weight { get; set; }

                public Edge(Node from, Node to, int weight)
                {
                    From = from;
                    To = to;
                    Weight = weight;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            grafo.Nodes.Clear();
            grafo.Edges.Clear();

            // Leer datos de los controles
            string node1Name = textBox1.Text;
            string node2Name = textBox4.Text;
            string node3Name = textBox7.Text;
            string node4Name = dateTimePicker1.Value.ToString("yyyy-MM-dd"); // Convertir la fecha a string

            // Crear nodos usando los datos leídos
            Node node1 = new Node(node1Name, new Point(50, 50));
            Node node2 = new Node(node2Name, new Point(200, 50));
            Node node3 = new Node(node3Name, new Point(200, 200));
            Node node4 = new Node(node4Name, new Point(50, 200));

            grafo.AddNode(node1);
            grafo.AddNode(node2);
            grafo.AddNode(node3);
            grafo.AddNode(node4);

            // Crear aristas (con pesos fijos por ahora, puedes cambiar los valores o añadir más lógica para personalizar)
            grafo.AddEdge(node1, node2, 1);
            grafo.AddEdge(node2, node3, 2);
            grafo.AddEdge(node3, node4, 3);
            grafo.AddEdge(node4, node1, 4);

            // Solicitar que el panel se vuelva a dibujar
            panel1.Invalidate();
        }

      

    private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Dibujar los nodos
            foreach (var node in grafo.Nodes)
            {
                g.FillEllipse(Brushes.Blue, node.Position.X - 15, node.Position.Y - 15, 30, 30);
                g.DrawString(node.Name, this.Font, Brushes.White, node.Position.X - 10, node.Position.Y - 10);
            }

            // Dibujar las aristas
            foreach (var edge in grafo.Edges)
            {
                g.DrawLine(Pens.Black, edge.From.Position, edge.To.Position);
                g.DrawString(edge.Weight.ToString(), this.Font, Brushes.Black,
                    (edge.From.Position.X + edge.To.Position.X) / 2,
                    (edge.From.Position.Y + edge.To.Position.Y) / 2);
            }
        }
    }
}

