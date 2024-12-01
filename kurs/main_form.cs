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

        // �������� ��� ��������� ���������� ���������
        public int NumberOfItems => (int)numericUpDown1.Value;

        // �������� ��������� ���������
        public bool IsCheckBox1Checked { get; set; } // ������ �� ����������
        public bool IsCheckBox2Checked => checkBox2.Checked; // �������������� ����������
        public bool IsCheckBox3Checked => checkBox3.Checked; // ������������ ����������
        public bool IsCheckBox4Checked => checkBox4.Checked; // ������������ ���������
        public bool IncludeCost => checkBox1.Checked;        // ��������� ���������

        // ����� ������ ������� � ���� ������
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

        // ��������� ������ "������� � �������"
        private void button1_Click(object sender, EventArgs e)
        {
            // ���������, ��� ��������� ������������� numericUpDown1 �� ������
            if (string.IsNullOrWhiteSpace(numericUpDown1.Text))
            {
                MessageBox.Show("���� '���������� ���������' �� ����� ���� ������!",
                    "������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // ���������� ����������
            }

            // ��������, ������ �� ���� �� ��������� (2, 3 ��� 4)
            if (!checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
            {
                MessageBox.Show("�������� ���� �� ��������� (�������������� ����������, ������������ ���������� ��� ������������ ���������)!",
                    "������!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // ������������� ����������, ���� �� ���� ������� �� ������
            }

            if (checkBox1.Checked)
            {
                // ���� ������� ��������� ���������
                var newForm = new option_with_cost(this);
                newForm.Show();
            }
            else
            {
                // ���� ������� �� ��������� ���������
                var newForm = new option_without_cost(this);
                newForm.Show();
            }
        }
        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ���������, �������� �� �������� ������ ������������
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // �������� ���� �������
                MessageBox.Show("����� ������� ������ ����� �� 2 �� 100!", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // ���������, ��� �������� � �������� �� 2 �� 100
            if (numericUpDown1.Value < numericUpDown1.Minimum || numericUpDown1.Value > numericUpDown1.Maximum)
            {
                MessageBox.Show($"���������� ��������� ������ ���� �� {numericUpDown1.Minimum} �� {numericUpDown1.Maximum}!",
                    "������!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // ���������� �������� � ������� ���������
                numericUpDown1.Value = numericUpDown1.Minimum;
            }
        }

        // ��������� ������������ ���������
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

        // ������� � ����� ������ � ����� ������
        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dbForm = new show_db();
            dbForm.Show();
        }

        // �������� ������ �� �����
        private void button2_Click(object sender, EventArgs e)
        {
            var fileForm = new task_from_a_file(this);
            fileForm.Show();
        }

        // ��������� ��������� �������� ��� �������� �����
        private void main_form_Load(object sender, EventArgs e)
        {
            this.ActiveControl = numericUpDown1;
        }

    }
}
