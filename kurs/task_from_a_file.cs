using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Knapsack
{
    public partial class task_from_a_file : Form
    {
        public main_form mainForm = null;

        public task_from_a_file(main_form f)
        {
            InitializeComponent();
            mainForm = f;

            // Установка фильтров для открытия и сохранения файлов
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        public option_with_cost option_with_cost
        {
            get => default;
            set
            {
            }
        }

        // Обработчик загрузки формы
        private void task_from_a_file_Load(object sender, EventArgs e)
        {
            // Устанавливаем фокус на кнопку "Выберите файл"
            this.ActiveControl = button4;
        }

        // Закрытие формы
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Переход в главное меню
        private void главнаяСтраницаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newForm = new main_form();
            newForm.Show();
        }

        // Открытие формы с базой данных
        private void базаДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dbForm = new show_db();
            dbForm.Show();
        }

        // Нажатие на кнопку "Выберите файл"
        private async void button4_Click(object sender, EventArgs e)
        {
            ResetForm();

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            // Проверка, что файл имеет расширение .txt
            string filePath = openFileDialog1.FileName;
            if (System.IO.Path.GetExtension(filePath).ToLower() != ".txt")
            {
                ShowError("Файл должен быть в формате .txt!");
                return;
            }

            string fileText = await Task.Run(() => System.IO.File.ReadAllText(filePath));

            try
            {
                var taskData = ParseFileContent(fileText);

                richTextBox1.Text = $"Тип задачи: {taskData.TaskType}\n" +
                                    $"Вес рюкзака: {taskData.BackpackWeight}\n" +
                                    $"Предметы: {string.Join(", ", taskData.ItemNames)}\n" +
                                    $"Вес предметов: {string.Join(", ", taskData.ItemWeights)}\n" +
                                    $"Стоимость предметов: {string.Join(", ", taskData.ItemCosts ?? new int[0])}\n" +
                                    $"Количество предметов: {string.Join(", ", taskData.ItemQuantities ?? new int[0])}";

                await ProcessTask(taskData);
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка: {ex.Message}");
            }
        }

        // Парсинг содержимого файла
        private TaskData ParseFileContent(string fileText)
        {
            var regexTaskType = new Regex(@"(?<=Тип задачи:\s+).+");
            var regexBackpackWeight = new Regex(@"(?<=Вес рюкзака:\s+)\d+");
            var regexItems = new Regex(@"(?<=Предметы:\s+).+");
            var regexWeights = new Regex(@"(?<=Вес предметов:\s+).+");
            var regexCosts = new Regex(@"(?<=Стоимость предметов:\s+).+"); // Стоимость может отсутствовать
            var regexQuantities = new Regex(@"(?<=Количество предметов:\s+).+"); // Количество может отсутствовать

            var taskTypeMatch = regexTaskType.Match(fileText);
            var backpackWeightMatch = regexBackpackWeight.Match(fileText);
            var itemsMatch = regexItems.Match(fileText);
            var weightsMatch = regexWeights.Match(fileText);
            var costsMatch = regexCosts.Match(fileText); // Может не быть
            var quantitiesMatch = regexQuantities.Match(fileText); // Может не быть

            if (!taskTypeMatch.Success || !backpackWeightMatch.Success ||
                !itemsMatch.Success || !weightsMatch.Success)
            {
                throw new FormatException("Некорректный формат файла. Проверьте наличие всех параметров.");
            }

            var taskData = new TaskData
            {
                TaskType = taskTypeMatch.Value.Trim(),
                BackpackWeight = int.Parse(backpackWeightMatch.Value),
                ItemNames = itemsMatch.Value.Split(',').Select(x => x.Trim()).ToArray(),
                ItemWeights = weightsMatch.Value.Split(',').Select(int.Parse).ToArray(),
                ItemCosts = costsMatch.Success ? costsMatch.Value.Split(',').Select(int.Parse).ToArray() : null,
                ItemQuantities = quantitiesMatch.Success ? quantitiesMatch.Value.Split(',').Select(int.Parse).ToArray() : null
            };

            ValidateTaskData(taskData);

            return taskData;
        }

        // Валидация данных
        private void ValidateTaskData(TaskData taskData)
        {
            if (taskData.ItemNames.Length != taskData.ItemWeights.Length ||
                (taskData.ItemCosts != null && taskData.ItemNames.Length != taskData.ItemCosts.Length) ||
                (taskData.ItemQuantities != null && taskData.ItemNames.Length != taskData.ItemQuantities.Length))
            {
                throw new FormatException("Количество предметов не соответствует количеству других параметров.");
            }

            if (taskData.BackpackWeight <= 0 || taskData.BackpackWeight > 100000)
            {
                throw new ArgumentException("Вес рюкзака должен быть больше 0 и меньше или равен 100 000.");
            }
        }

        // Обработка задачи
        private async Task ProcessTask(TaskData taskData)
        {
            bool c2 = false, c3 = false, c4 = false;

            Item.Items = new Item[taskData.ItemNames.Length];
            for (int i = 0; i < taskData.ItemNames.Length; i++)
            {
                Item.Items[i] = new Item(
                    taskData.ItemNames[i],
                    taskData.ItemWeights[i],
                    taskData.ItemCosts?[i] ?? 0, // Если стоимости нет, задаем 0
                    taskData.ItemQuantities?[i] ?? 0); // Если количества нет, задаем 0
            }

            switch (taskData.TaskType.Trim())
            {
                case "без стоимости, каждый предмет имеется в единственном экземпляре":
                    c3 = true;
                    break;
                case "без стоимости, каждый предмет имеется в неограниченном количестве":
                    c2 = true;
                    break;
                case "без стоимости, каждый предмет имеется в ограниченном количестве":
                    c4 = true;
                    break;
                case "со стоимостью, каждый предмет имеется в единственном экземпляре":
                    c3 = true;
                    break;
                case "со стоимостью, каждый предмет имеется в неограниченном количестве":
                    c2 = true;
                    break;
                case "со стоимостью, каждый предмет имеется в ограниченном количестве":
                    c4 = true;
                    break;
                default:
                    throw new ArgumentException("Тип задачи некорректен.");
            }

            // Выполнение алгоритма
            if (taskData.TaskType.Contains("со стоимостью"))
            {
                textBox2.Text = await Task.Run(() =>
                    algorithm_with_cost.max_cost(Item.Items, taskData.BackpackWeight, c2, c3, c4).ToString());
                textBox3.Text = await Task.Run(() =>
                    algorithm_with_cost.arr_items[Item.Items.Length, taskData.BackpackWeight]);
            }
            else
            {
                textBox2.Text = await Task.Run(() =>
                    algorithm_without_cost.max_weight(Item.Items, taskData.BackpackWeight, c2, c3, c4).ToString());
                textBox3.Text = await Task.Run(() =>
                    algorithm_without_cost.arr_items[Item.Items.Length, taskData.BackpackWeight]);
            }

            visible(null, null);

            if (checkBoxSaveToDb.Checked)
            {
                mainForm.recording_the_solution(
                    taskData.TaskType,
                    taskData.BackpackWeight,
                    Item.Items.Length,
                    int.Parse(textBox2.Text),
                    textBox3.Text);
            }
        }

        // Сброс состояния формы
        private void ResetForm()
        {
            not_visible(null, null);
            richTextBox1.Clear();
        }

        // Отображение ошибки
        private void ShowError(string message)
        {
            not_visible(null, null);
            MessageBox.Show(message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Печать результатов
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            var myFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            e.Graphics.DrawString($"Дата: {DateTime.Now}", myFont, Brushes.Black, 40, 40);
            e.Graphics.DrawString("Исходные данные:", myFont, Brushes.Black, 40, 70);
            e.Graphics.DrawString(richTextBox1.Text, myFont, Brushes.Black, 40, 100);
            e.Graphics.DrawString("Результаты:", myFont, Brushes.Black, 40, 380);
            e.Graphics.DrawString($"Ответ: {textBox2.Text}", myFont, Brushes.Black, 40, 410);
            e.Graphics.DrawString($"Набор предметов: {textBox3.Text}", myFont, Brushes.Black, new RectangleF(40, 440, 770, 800));
        }

        //Кнопка для печати результатов
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Заполните все данные перед печатью!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            printPreviewDialog1.ShowDialog();
        }

        // Сохранение результатов в файл
        private void button6_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;

            string filename = saveFileDialog1.FileName;
            string content = $"Дата: {DateTime.Now}\n\n" +
                             $"Исходные данные:\n{richTextBox1.Text}\n\n" +
                             $"Результаты:\nОтвет: {textBox2.Text}\nНабор предметов: {textBox3.Text}";
            System.IO.File.WriteAllText(filename, content);
            MessageBox.Show("Файл сохранен");
        }

        // Отображение элементов формы
        private void visible(object sender, EventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            buttonPrint.Visible = true;
            button6.Visible = true;
        }

        // Скрытие элементов формы
        private void not_visible(object sender, EventArgs e)
        {
            label2.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            buttonPrint.Visible = false;
            button6.Visible = false;
        }
    }

    // Класс для хранения данных задачи
    public class TaskData
    {
        public string TaskType { get; set; }
        public int BackpackWeight { get; set; }
        public string[] ItemNames { get; set; }
        public int[] ItemWeights { get; set; }
        public int[] ItemCosts { get; set; }
        public int[] ItemQuantities { get; set; }
    }
}
