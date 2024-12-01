using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Windows.Forms;

namespace Knapsack
{
    static class Program
    {
        // Главная точка входа для Windows Forms-приложения
        [STAThread]
        static void Main()
        {
            // Настройка конфигурации (чтение appsettings.json)
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Путь к текущей директории
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);  // Загружаем конфигурацию

            var configuration = builder.Build();  // Чтение конфигурации из appsettings.json

            // Настройка строки подключения
            var connectionString = configuration.GetConnectionString("KnapsackDBConnectionString");

            // Инициализация Entity Framework DbContext с полученной строкой подключения
            var optionsBuilder = new DbContextOptionsBuilder<dbo.ApplicationContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // Создание экземпляра DbContext
            using (var db = new dbo.ApplicationContext(optionsBuilder.Options))
            {
                // Пример использования контекста данных (например, можно проверить подключение)
                try
                {
                    db.Database.CanConnect(); // Проверка соединения с базой данных
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Завершаем работу приложения, если ошибка подключения
                }
            }

            // Настройка окна формы
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запуск главной формы
            Application.Run(new main_form());
        }
    }
}
