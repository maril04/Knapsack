using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Knapsack
{
    public partial class show_db : Form
    {
        private string _connectionString;

        public show_db()
        {
            InitializeComponent();
            LoadConnectionString();
        }

        private void LoadConnectionString()
        {
            // Загрузка конфигурации из appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            // Получение строки подключения из конфигурации
            _connectionString = configuration.GetConnectionString("KnapsackDBConnectionString");
        }

        private void show_db_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    MessageBox.Show("Строка подключения не найдена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Создание DbContext
                var optionsBuilder = new DbContextOptionsBuilder<dbo.ApplicationContext>();
                optionsBuilder.UseSqlServer(_connectionString);

                using (var db = new dbo.ApplicationContext(optionsBuilder.Options))
                {
                    // Загрузка данных из таблицы backpack_solving
                    var data = db.backpack_problem.ToList();

                    // Привязка данных к DataGridView
                    if (data.Any())
                    {
                        dataGridView1.DataSource = data;
                    }
                    else
                    {
                        MessageBox.Show("Данные отсутствуют в таблице.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных из базы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
