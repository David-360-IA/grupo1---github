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
    public partial class lista_simple : Form
    {
        private class Nodo
        {
            public int Id { get; set; }
            public string Propietario { get; set; }
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal Costo { get; set; }
            public DateTime FechaEntrega { get; set; }
            public Nodo Siguiente { get; set; }

            public Nodo(int id, string propietario, string producto, int cantidad, decimal costo, DateTime fechaentrega)
            {
                this.Id = id;
                this.Propietario = propietario;
                this.Producto = producto;
                this.Cantidad = cantidad;
                this.Costo = costo;
                this.FechaEntrega = fechaentrega;
                this.Siguiente = null;
            }

            public override string ToString()
            {
                return $"Id: {Id}, Propietario: {Propietario}, Producto: {Producto}, Cantidad: {Cantidad}, Costo: {Costo:C}, Fecha de Entrega: {FechaEntrega:dd/MM/yyyy}";
            }
        }

        private Nodo primero;

        public lista_simple()
        {
            InitializeComponent();
            InitializeDataGridView();
            textBox1.KeyPress += SoloNumeros_KeyPress;
            textBox4.KeyPress += SoloNumeros_KeyPress;
            textBox7.KeyPress += SoloNumeros_KeyPress;


            textBox2.KeyPress += SoloLetras_KeyPress;
            textBox3.KeyPress += SoloLetras_KeyPress;
        }

        private void InitializeDataGridView()
        {           
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Propietario";
            dataGridView1.Columns[2].Name = "Producto";
            dataGridView1.Columns[3].Name = "Cantidad";
            dataGridView1.Columns[4].Name = "Costo";
            dataGridView1.Columns[5].Name = "Fecha de Entrega";
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
      
        private void AgregarNodo(int id, string propietario, string producto, int cantidad, decimal costo, DateTime fechaEntrega)
        {
            Nodo nuevo = new Nodo(id, propietario, producto, cantidad, costo, fechaEntrega);
            if (primero == null)
            {
                primero = nuevo;
            }
            else
            {
                Nodo actual = primero;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }

            MostrarLista(); 
        }
       
        private void MostrarLista()
        {
            dataGridView1.Rows.Clear();
            Nodo actual = primero;

            while (actual != null)
            {
                dataGridView1.Rows.Add(
                    actual.Id,
                    actual.Propietario,
                    actual.Producto,
                    actual.Cantidad,
                    actual.Costo.ToString("C"),
                    actual.FechaEntrega.ToShortDateString()
                );
                actual = actual.Siguiente;
            }
        } 
        private Nodo BuscarNodo(int id)
        {
            Nodo actual = primero;

            while (actual != null)
            {
                if (actual.Id == id)
                    return actual;

                actual = actual.Siguiente;
            }

            return null;
        }
   
        private void EliminarNodo(int id)
        {
            if (primero == null)
            {
                MessageBox.Show("La lista está vacía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (primero.Id == id)
            {
                primero = primero.Siguiente;
                MostrarLista();
                return;
            }

            Nodo actual = primero;
            while (actual.Siguiente != null && actual.Siguiente.Id != id)
            {
                actual = actual.Siguiente;
            }

            if (actual.Siguiente != null)
            {
                actual.Siguiente = actual.Siguiente.Siguiente;
                MostrarLista();
            }
            else
            {
                MessageBox.Show("ID no encontrado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int id) &&
                int.TryParse(textBox7.Text, out int cantidad) &&
                decimal.TryParse(textBox4.Text, out decimal costo))
            {
                AgregarNodo(id, textBox2.Text, textBox3.Text, cantidad, costo, dateTimePicker2.Value);

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox7.Clear();

                MessageBox.Show("El Nodo a sido agregado exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Verifique los campos numéricos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int id))
            {
                EliminarNodo(id);
            }
            else
            {
                MessageBox.Show("Ingrese un ID válido para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void lista_simple_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
