using System;

using System.Windows.Forms;

namespace grupo1___github
{
    public partial class pila : Form
    {
        public pila()
        {
            InitializeComponent();

          
            textBox1.KeyPress += SoloNumeros_KeyPress;
            textBox4.KeyPress += SoloNumeros_KeyPress;
            textBox7.KeyPress += SoloNumeros_KeyPress;

         
            textBox2.KeyPress += SoloLetras_KeyPress;
            textBox3.KeyPress += SoloLetras_KeyPress;
            textBox10.KeyPress += SoloLetras_KeyPress;
            textBox5.KeyPress += SoloLetras_KeyPress;
            textBox8.KeyPress += SoloLetras_KeyPress;
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

        private void button1_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox10.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de agregar.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (int.TryParse(textBox9.Text, out int maxRows))
            {
                if (dataGridView1.Rows.Count >= maxRows)
                {
                    MessageBox.Show("No se pueden agregar más elementos. Se ha alcanzado el límite de la pila.", "Límite de pila alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string value1 = textBox1.Text;
                string value2 = textBox2.Text;
                string value3 = textBox3.Text;
                string value4 = textBox4.Text;
                string value5 = textBox10.Text;
                string value6 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                string value7 = textBox5.Text;
                string value8 = textBox6.Text;
                string value9 = textBox7.Text;
                string value10 = textBox8.Text;
                string value11 = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                dataGridView1.Rows.Insert(0, value1, value2, value3, value4, value5, value6, value7, value8, value9, value10, value11);
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número válido en la cantidad", "Entrada no válida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
            else
            {
                MessageBox.Show("No hay elementos en la pila para eliminar.", "Pila vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pila_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker2.Enabled = false; 

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
