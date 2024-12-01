using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Windows.Forms;

namespace Knapsack
{
    static class Program
    {
        // ������� ����� ����� ��� Windows Forms-����������
        [STAThread]
        static void Main()
        {
            // ��������� ������������ (������ appsettings.json)
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // ���� � ������� ����������
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);  // ��������� ������������

            var configuration = builder.Build();  // ������ ������������ �� appsettings.json

            // ��������� ������ �����������
            var connectionString = configuration.GetConnectionString("KnapsackDBConnectionString");

            // ������������� Entity Framework DbContext � ���������� ������� �����������
            var optionsBuilder = new DbContextOptionsBuilder<dbo.ApplicationContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // �������� ���������� DbContext
            using (var db = new dbo.ApplicationContext(optionsBuilder.Options))
            {
                // ������ ������������� ��������� ������ (��������, ����� ��������� �����������)
                try
                {
                    db.Database.CanConnect(); // �������� ���������� � ����� ������
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"������ ����������� � ���� ������: {ex.Message}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // ��������� ������ ����������, ���� ������ �����������
                }
            }

            // ��������� ���� �����
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ������ ������� �����
            Application.Run(new main_form());
        }
    }
}
