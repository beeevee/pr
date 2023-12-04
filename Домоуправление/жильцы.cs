﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Домоуправление
{
    public partial class жильцы : Form
    {
        private readonly string connString = @"Data Source=dom.db;Version=3;";
        public жильцы()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Жильцы";
            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable Animall = new DataTable();
                _ = adapter.Fill(Animall);

                // добавляем столбы для datagridview, для их отображения
                dataGridView1.DataSource = Animall;
                dataGridView1.Columns["номер_квартиры"].HeaderText = "номер квартиры";
                dataGridView1.Columns["имя_жильца"].HeaderText = "имя жильца";
                dataGridView1.Columns["возраст_жильца"].HeaderText = "возраст жильца";
                dataGridView1.Columns["контактная_информация"].HeaderText = "контактная информация";


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранные записи?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        int id = Convert.ToInt32(row.Cells["номер_квартиры"].Value);

                        // Выполняем удаление записи из базы данных
                        using (SQLiteConnection connection = new SQLiteConnection(connString))
                        {
                            connection.Open();
                            SQLiteCommand command = new SQLiteCommand("DELETE FROM Жильцы WHERE номер_квартиры = @номер_квартиры", connection);
                            command.Parameters.AddWithValue("@номер_квартиры", id);
                            command.ExecuteNonQuery();
                        }

                        // Удаляем выбранную строку из DataGridView
                        dataGridView1.Rows.Remove(row);
                    }

                    MessageBox.Show("Выбранные записи успешно удалены.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выделите записи, которые хотите удалить.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            добж добж = new добж();

            // Открываем новую форму
            добж.Show();
        }

        private void жильцы_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Жильцы";
            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable Animall = new DataTable();
                _ = adapter.Fill(Animall);

                // добавляем столбы для datagridview, для их отображения
                dataGridView1.DataSource = Animall;
                dataGridView1.Columns["номер_квартиры"].HeaderText = "номер квартиры";
                dataGridView1.Columns["имя_жильца"].HeaderText = "имя жильца";
                dataGridView1.Columns["возраст_жильца"].HeaderText = "возраст жильца";
                dataGridView1.Columns["контактная_информация"].HeaderText = "контактная информация";


            }
        }
    }
}
