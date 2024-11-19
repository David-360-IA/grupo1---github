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
        private class nodo
        {
            public int id { get;set;}
            public string propietario { get; set; }
            public string producto { get; set; }
            public int cantidad { get; set; }
            public int costo { get; set; }
            public DateTime fechaentrega { get; set; }
            public nodo Siguiente { get; set; }

            public nodo(string dato)
            {
                id = id;
                propietario = propietario; 
                producto = producto;
                cantidad = cantidad;
                costo = costo;
                fechaentrega = fechaentrega;

                Siguiente = null;
            }
        }
        public lista_simple()
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
        
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2 == null)
            {
                MessageBox.Show("No hay facturas para eliminar", "Información",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validar que todos los campos tienen datos
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar los datos al DataGridView
            string[] row = {
                textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox7.Text
            };
            dataGridView1.Rows.Add(row);

            // Limpiar los TextBox después de guardar
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox7.Clear();

            MessageBox.Show("Datos guardados exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lista_simple_Load(object sender, EventArgs e)
        {

        }
    }
}
