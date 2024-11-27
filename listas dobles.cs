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
    public partial class listas_dobles : Form
    {
        public listas_dobles()
        {
            InitializeComponent();
            InitializeDataGridView();
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
        private class nodoDoble
        {
            public int Id { get; set; }
            public string Propietario { get; set; }
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal Costo { get; set; }
            public DateTime FechaEntrega { get; set; }
            public nodoDoble Siguiente { get; set; }
            public nodoDoble Anterior { get; set; }

            public nodoDoble(int id, string propietario, string producto, int cantidad, decimal costo, DateTime fechaentrega)
            {
                Id = id;
                Propietario = propietario;
                Producto = producto;
                Cantidad = cantidad;
                Costo = costo;
                FechaEntrega = fechaentrega;
                Siguiente = null;
                Anterior = null;
            }

            public override string ToString()
            {
                return $"Id: {Id}, Propietario: {Propietario}, Producto: {Producto}, Cantidad: {Cantidad}, Costo: {Costo:C}, Fecha Entrega: {FechaEntrega:dd/MM/yyyy}";
            }
        }

        private nodoDoble primero;
        private nodoDoble ultimo;



        private void InitializeDataGridView()
        {
            // Configuración de columnas en el DataGridView
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "Propietario";
            dataGridView1.Columns[2].Name = "Producto";
            dataGridView1.Columns[3].Name = "Cantidad";
            dataGridView1.Columns[4].Name = "Costo";
            dataGridView1.Columns[5].Name = "Fecha Entrega";
        }

        private void AgregarNodo(int id, string propietario, string producto, int cantidad, decimal costo, DateTime fechaEntrega)
        {
            nodoDoble nuevo = new nodoDoble(id, propietario, producto, cantidad, costo, fechaEntrega);

            if (primero == null)
            {
                primero = ultimo = nuevo;
            }
            else
            {
                ultimo.Siguiente = nuevo;
                nuevo.Anterior = ultimo;
                ultimo = nuevo;
            }

            ActualizarDataGridView();
        }

        private void EliminarNodo(int id)
        {
            nodoDoble actual = primero;
            while (actual != null)
            {
                if (actual.Id == id)
                {
                    if (actual.Anterior != null) actual.Anterior.Siguiente = actual.Siguiente;
                    if (actual.Siguiente != null) actual.Siguiente.Anterior = actual.Anterior;
                    if (actual == primero) primero = actual.Siguiente;
                    if (actual == ultimo) ultimo = actual.Anterior;
                    break;
                }
                actual = actual.Siguiente;
            }
            ActualizarDataGridView();
        }

        private void ActualizarDataGridView()
        {
            dataGridView1.Rows.Clear();
            nodoDoble actual = primero;
            while (actual != null)
            {
                dataGridView1.Rows.Add(
                    actual.Id,
                    actual.Propietario,
                    actual.Producto,
                    actual.Cantidad,
                    actual.Costo,
                    actual.FechaEntrega.ToShortDateString()
                );
                actual = actual.Siguiente;
            }
        }

        private void listas_dobles_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
               string.IsNullOrWhiteSpace(textBox2.Text) ||
               string.IsNullOrWhiteSpace(textBox3.Text) ||
               string.IsNullOrWhiteSpace(textBox4.Text) ||
               string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AgregarNodo(
                int.Parse(textBox1.Text),
                textBox2.Text,
                textBox3.Text,
                int.Parse(textBox4.Text),
                decimal.Parse(textBox7.Text),
                dateTimePicker2.Value
            );

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox7.Clear();
            MessageBox.Show("Datos guardados exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                EliminarNodo(id);
            }
            else
            {
                MessageBox.Show("Selecciona una fila para borrar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
