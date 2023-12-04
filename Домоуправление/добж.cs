using System;
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
    public partial class добж : Form
    {
        private readonly string connString = @"Data Source=dom.db;Version=3;";
        public добж()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string номер_квартиры = textBox1.Text.Trim();
            string имя_жильца = textBox2.Text.Trim();
            string возраст_жильца = textBox3.Text.Trim();
            string контактная_информация = textBox4.Text.Trim();


            // Проверяем, что поля логина и пароля не пустые
            if (номер_квартиры == "" || имя_жильца == "" || возраст_жильца == "" || контактная_информация == "")
            {
                _ = MessageBox.Show("Заполните все поля");
                return;
            }

            // Добавляем сообщение с подтверждением операции
            DialogResult confirmationResult = MessageBox.Show("Вы точно хотите сохранить жильца?", "Подтверждение", MessageBoxButtons.YesNo);
            if (confirmationResult == DialogResult.No)
            {
                return;
            }

            // Создаем новое подключение к базе данных SQLite
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                conn.Open();
                string insertQuery = $"INSERT INTO Жильцы (номер_квартиры,имя_жильца,возраст_жильца,контактная_информация) VALUES ('{номер_квартиры}', '{имя_жильца}', '{возраст_жильца}', '{контактная_информация}');";
                using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                {
                    // Выполняем запрос на добавление нового пользователя в базу данных
                    _ = insertCmd.ExecuteNonQuery();
                }
            }

            // Выводим окно с подтверждением операции
            DialogResult result = MessageBox.Show("новый жилец успешно занесено в базу данных. Хотите выполнить еще одну операцию?", "Подтверждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Очистить поля ввода для выполнения новой операции
                textBox2.Text = "";
                textBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";

            }
        }

        private void добж_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
