using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Домоуправление
{
    public partial class главная : Form
    {
        private readonly string connString = @"Data Source=dom.db;Version=3;";
        public главная()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            жильцы доб = new жильцы(); // Создание новой формы
            доб.Show(); // Показ новой формы
        }

        private void главная_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCourtshipData();
            LoadCourtshipData1();
            LoadCourtshipData2();
        }
        private void LoadCourtshipData1()
        {
            string query = "SELECT * FROM Квартиры";
            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable Квартиры = new DataTable();
                _ = adapter.Fill(Квартиры);

                // добавляем столбы для datagridview, для их отображения
                dataGridView1.DataSource = Квартиры;
                dataGridView1.Columns["номер_квартиры"].HeaderText = "номер квартиры";
                dataGridView1.Columns["номер_дома"].HeaderText = "номер дома";
                dataGridView1.Columns["число_жильцов"].HeaderText = "число жильцов";
                dataGridView1.Columns["площадь"].HeaderText = "площадь";

            }
        }

        private void LoadCourtshipData()
        {
            string query = "SELECT * FROM Жильцы";
            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable Жильцы = new DataTable();
                _ = adapter.Fill(Жильцы);

                // Set up DataGridView
                dataGridView1.DataSource = Жильцы;
                dataGridView1.Columns["номер_квартиры"].HeaderText = "номер квартиры";
                dataGridView1.Columns["имя_жильца"].HeaderText = "имя жильца";
                dataGridView1.Columns["возраст_жильца"].HeaderText = "возраст жильца";
                dataGridView1.Columns["контактная_информация"].HeaderText = "контактная информация";
            }
        }

        private void LoadCourtshipData2()
        {
            string query = "SELECT * FROM Оплаты";
            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable Оплаты = new DataTable();
                _ = adapter.Fill(Оплаты);

                // Set up DataGridView
                dataGridView1.DataSource = Оплаты;
                dataGridView1.Columns["номер_квартиры"].HeaderText = "номер квартиры";
                dataGridView1.Columns["вид_оплаты"].HeaderText = "вид оплаты";
                dataGridView1.Columns["цена_площади"].HeaderText = "цена площади";
                dataGridView1.Columns["цена_жильца"].HeaderText = "цена жильца";
                dataGridView1.Columns["сумма_оплаты"].HeaderText = "сумма оплаты";
                dataGridView1.Columns["месяц_год_оплаты"].HeaderText = "месяц год оплаты";
                dataGridView1.Columns["дата_оплаты"].HeaderText = "дата оплаты";
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                
               
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "SELECT SUM(сумма_оплаты) AS сумма_месячной_оплаты\r\nFROM Оплаты\r\nWHERE номер_квартиры IN (\r\n    SELECT номер_квартиры\r\n    FROM Квартиры\r\n    WHERE номер_дома = 5\r\n);";

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string query = "SELECT SUM(сумма_оплаты) AS задолженность\r\nFROM Оплаты\r\nWHERE тип_услуги = 'теплоснабжение' AND номер_квартиры = 512 AND номер_дома = 5;";

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string query = "SELECT SUM(число_жильцов) AS общее_число_жильцов\r\nFROM Квартиры\r\nWHERE номер_дома = 5;";

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "SELECT Квартиры.*\r\nFROM Квартиры\r\nLEFT JOIN Оплаты ON Квартиры.номер_квартиры = Оплаты.номер_квартиры\r\nWHERE Оплаты.сумма_оплаты IS NULL\r\n  OR Оплаты.месяц_год_оплаты < '2023-01-01';";

            using (SQLiteConnection connection = new SQLiteConnection(connString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            квартиры кв = new квартиры(); // Создание новой формы
            кв.Show(); // Показ новой формы
        }
    }
}
