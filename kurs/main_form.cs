using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows.Forms;

namespace Knapsack
{
    public partial class main_form : Form
    {
        public main_form()
        {
            InitializeComponent();
        }

        // Свойство для получения количества предметов
        public int NumberOfItems => (int)numericUpDown1.Value;

        // Проверка состояния чекбоксов
        public bool IsCheckBox1Checked { get; set; } // Задача со стоимостью
        public bool IsCheckBox2Checked => checkBox2.Checked; // Неограниченное количество
        public bool IsCheckBox3Checked => checkBox3.Checked; // Ограниченное количество
        public bool IsCheckBox4Checked => checkBox4.Checked; // Единственный экземпляр
        public bool IncludeCost => checkBox1.Checked;        // Учитывать стоимость

        // Метод записи решения в базу данных
        public void recording_the_solution(string taskType, int backpackWeight, int numberOfItems, int answer, string items)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("KnapsackDBConnectionString");
            var optionsBuilder = new DbContextOptionsBuilder<dbo.ApplicationContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (var db = new dbo.ApplicationContext(options))
            {
                var problem = new dbo.backpack_problem
                {
                    Task_type = taskType,
                    Backpack_weight = backpackWeight,
                    Number_of_items = numberOfItems,
                    Answer = answer,
                    Items = items,
                    Date_time = DateTime.Now
                };

                db.backpack_problem.Add(problem);
                db.SaveChanges();
            }
        }

        // Обработка кнопки "Перейти к решению"
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем, что текстовое представление numericUpDown1 не пустое
            if (string.IsNullOrWhiteSpace(numericUpDown1.Text))
            {
                MessageBox.Show("Поле 'Количество предметов' не может быть пустым!",
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Прекращаем выполнение
            }

            // Проверка, выбран ли один из чекбоксов (2, 3 или 4)
            if (!checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
            {
                MessageBox.Show("Выберите один из вариантов (Неограниченное количество, Ограниченное количество или Единственный экземпляр)!",
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Останавливаем выполнение, если ни один чекбокс не выбран
            }

            if (checkBox1.Checked)
            {
                // Если выбрано учитывать стоимость
                var newForm = new option_with_cost(this);
                newForm.Show();
            }
            else
            {
                // Если выбрано не учитывать стоимость
                var newForm = new option_without_cost(this);
                newForm.Show();
            }
        }
        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Проверяем, является ли вводимый символ недопустимым
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отменяем ввод символа
                MessageBox.Show("Можно вводить только числа от 2 до 100!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // Проверяем, что значение в пределах от 2 до 100
            if (numericUpDown1.Value < numericUpDown1.Minimum || numericUpDown1.Value > numericUpDown1.Maximum)
            {
                MessageBox.Show($"Количество предметов должно быть от {numericUpDown1.Minimum} до {numericUpDown1.Maximum}!",
                    "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Сбрасываем значение в границы диапазона
                numericUpDown1.Value = numericUpDown1.Minimum;
            }
        }

        // Обработка переключений чекбоксов
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Checked = false;
                checkBox4.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        // Переход к форме работы с базой данных
        private void базаДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dbForm = new show_db();
            dbForm.Show();
        }

        // Загрузка данных из файла
        private void button2_Click(object sender, EventArgs e)
        {
            var fileForm = new task_from_a_file(this);
            fileForm.Show();
        }

        // Установка активного элемента при загрузке формы
        private void main_form_Load(object sender, EventArgs e)
        {
            this.ActiveControl = numericUpDown1;
        }

    }
}
