using System;

using System.Windows.Forms;

namespace grupo1___github
{
    public partial class cola_circular : Form
    {
        // Definimos la cola circular
        private ColaCircular cola;
        private int maxFilas;

        public cola_circular()
        {
            InitializeComponent();
            cola = new ColaCircular(10); // Tamaño de la cola circular

            // Asignar eventos KeyPress a los TextBox para restringir entradas
            textBox1.KeyPress += SoloNumeros_KeyPress;
            textBox4.KeyPress += SoloNumeros_KeyPress;
            textBox7.KeyPress += SoloNumeros_KeyPress;
            textBox9.KeyPress += SoloNumeros_KeyPress;

            textBox2.KeyPress += SoloLetras_KeyPress;
            textBox3.KeyPress += SoloLetras_KeyPress;
            textBox10.KeyPress += SoloLetras_KeyPress;
            textBox5.KeyPress += SoloLetras_KeyPress;
            textBox6.KeyPress += SoloLetras_KeyPress;
            textBox8.KeyPress += SoloLetras_KeyPress;
        }

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Cancela el evento para caracteres no válidos
            }
        }

        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; // Cancela el evento para caracteres no válidos
            }
        }

        private void cola_circular_Load(object sender, EventArgs e)
        {
            // Configura el DataGridView como no editable
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            // Establecer la fecha mínima de dateTimePicker1 a la fecha actual
            dateTimePicker1.MinDate = DateTime.Today;

            // Deshabilitar dateTimePicker2 para que no se pueda seleccionar ninguna fecha
            dateTimePicker2.Enabled = false; // O puedes ocultarlo usando dateTimePicker2.Visible = false
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Verificar si la cola tiene elementos
            if (!cola.IsEmpty())
            {
                cola.Dequeue(); // Eliminar el primer elemento (frente de la cola)
                MostrarCola(); // Actualizar el DataGridView
            }
            else
            {
                MessageBox.Show("No hay elementos en la cola para eliminar.", "Cola vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MostrarCola()
        {
            dataGridView1.Rows.Clear(); // Limpiar el DataGridView

            // Mostrar todos los elementos en el DataGridView
            foreach (var item in cola.Elementos)
            {
                dataGridView1.Rows.Add(item); // Agregar filas
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Comprobar si algún TextBox está vacío
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox10.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text) ||
                string.IsNullOrWhiteSpace(textBox9.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de agregar.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detener la ejecución si hay campos vacíos
            }

            // Obtener el valor máximo de filas permitidas desde textBox9
            if (int.TryParse(textBox9.Text, out maxFilas))
            {
                if (cola.Count >= maxFilas)
                {
                    MessageBox.Show($"No se pueden agregar más de {maxFilas} elementos en la cola.", "Límite de filas alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Detener si se supera el límite
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un valor numérico válido en el campo de límite de filas.", "Error en el límite de filas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener los valores de los TextBox
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

            // Insertar un nuevo elemento en la cola
            if (cola.IsFull())
            {
                MessageBox.Show("No se pueden agregar más elementos. Se ha alcanzado el límite de la cola.", "Límite de cola alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                cola.Enqueue(new string[] { value1, value2, value3, value4, value5, value6, value7, value8, value9, value10, value11 });
                MostrarCola(); // Actualizar el DataGridView
            }
        }

        public class ColaCircular
        {
            private string[][] elementos;
            private int frente, atras, capacidad;

            public ColaCircular(int tamano)
            {
                capacidad = tamano;
                elementos = new string[capacidad][];
                frente = -1;
                atras = -1;
            }

            public bool IsFull()
            {
                return (atras + 1) % capacidad == frente;
            }

            public bool IsEmpty()
            {
                return frente == -1;
            }

            public void Enqueue(string[] elemento)
            {
                if (IsFull())
                    return; // No se puede agregar si está llena

                if (IsEmpty())
                    frente = 0; // Primer elemento añadido

                atras = (atras + 1) % capacidad;
                elementos[atras] = elemento;
            }

            public void Dequeue()
            {
                if (IsEmpty())
                    return; // No se puede eliminar si está vacía

                if (frente == atras) // La cola queda vacía
                {
                    frente = -1;
                    atras = -1;
                }
                else
                {
                    frente = (frente + 1) % capacidad;
                }
            }

            public string[][] Elementos
            {
                get
                {
                    // Devolver los elementos en el orden correcto para mostrar en el DataGridView
                    string[][] resultado = new string[Count][];
                    int j = 0;
                    for (int i = frente; j < Count; i = (i + 1) % capacidad)
                    {
                        resultado[j] = elementos[i];
                        j++;
                    }
                    return resultado;
                }
            }

            public int Count
            {
                get
                {
                    if (IsEmpty())
                        return 0;
                    if (frente <= atras)
                        return atras - frente + 1;
                    return capacidad - frente + atras + 1;
                }
            }
        }
    }
}
