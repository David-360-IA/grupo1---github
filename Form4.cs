﻿using System;
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
    public partial class Form4 : Form
    {
        public Form4()
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
            dataGridView1.Columns[0].Name = "TextBox1";
            dataGridView1.Columns[1].Name = "TextBox2";
            dataGridView1.Columns[2].Name = "TextBox3";
            dataGridView1.Columns[3].Name = "TextBox7";
            dataGridView1.Columns[4].Name = "TextBox4";
            dataGridView1.Columns[5].Name = "Fecha";
        }
        private void MostrarDatos()
        {
            string[] row = new string[]
            {
                textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                textBox7.Text,
                textBox4.Text,
                dateTimePicker2.Value.ToShortDateString()
            };

            dataGridView1.Rows.Add(row);
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
                string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Por favor, llena todos los campos antes de guardar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == textBox1.Text)
                {
                    MessageBox.Show("Ya existe un registro con el mismo valor en TextBox1.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            MostrarDatos();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox7.Clear();
            textBox4.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para borrar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 2)
            {
                MessageBox.Show("Debe haber al menos 2 datos en el DataGridView para ordenar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
