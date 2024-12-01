using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knapsack
{
    public partial class option_with_cost : Form
    {
        // Ссылка на главную форму
        public main_form mainForm = null;

        public option_with_cost(main_form f)
        {
            InitializeComponent();
            mainForm = f;

            // Установка фильтров для открытия и сохранения файлов
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        // Обработчик загрузки формы
        private void option_with_cost_Load(object sender, EventArgs e)
        {
            // Устанавливаем фокус на текстовое поле
            this.ActiveControl = textBox1;

            // Избегаем повторного выполнения кода
            if (dataGridView1.RowCount > 1)
                return;

            // Отключаем добавление строк вручную
            dataGridView1.AllowUserToAddRows = false;

            // Получаем количество предметов из главной формы
            int numberOfItems = Convert.ToInt32(mainForm.NumberOfItems);

            // Создаем таблицу для ввода данных
            dataGridView1.ColumnCount = numberOfItems + 1;

            // Настраиваем строки таблицы в зависимости от типа задачи
            if (mainForm.IsCheckBox2Checked || mainForm.IsCheckBox3Checked)
            {
                dataGridView1.RowCount = 3; // Строки: № предмета, Вес, Стоимость
                for (int i = 0; i < 3; i++)
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }
            else if (mainForm.IsCheckBox4Checked)
            {
                dataGridView1.RowCount = 4; // Строки: № предмета, Вес, Стоимость, Количество
                dataGridView1.Rows[3].Cells[0].Value = "Количество предметов";
                for (int i = 0; i < 4; i++)
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }

            // Устанавливаем заголовки строк и столбцов
            dataGridView1.Rows[0].Cells[0].Value = "№ предмета";
            dataGridView1.Rows[1].Cells[0].Value = "Вес предмета";
            dataGridView1.Rows[2].Cells[0].Value = "Стоимость предмета";

            for (int i = 1; i <= numberOfItems; i++)
                dataGridView1.Rows[0].Cells[i].Value = $"Предмет № {i}";
        }

        // Переход в главное меню
        private void главнаяСтраницаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mainForm = new main_form();
            mainForm.Show();
        }
        
        // Открытие формы с базой данных
        private void базаДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dbForm = new show_db();
            dbForm.Show();
        }

        // Обработчик кнопки "Получить решение"
        private async void button1_Click(object sender, EventArgs e)
        {
            // Очистка результатов
            textBox2.Text = "";
            textBox3.Text = "";
            HideUIElements();

            // Проверяем корректность введенных данных
            if (!ValidateInputs(out int maxCapacity))
                return;

            int numberOfItems = dataGridView1.ColumnCount - 1;
            Item.Items = new Item[numberOfItems];

            // Заполняем массив предметов
            for (int i = 0; i < numberOfItems; i++)
            {
                int weight = Convert.ToInt32(dataGridView1.Rows[1].Cells[i + 1].Value);
                int price = Convert.ToInt32(dataGridView1.Rows[2].Cells[i + 1].Value);
                int quantity = mainForm.IsCheckBox4Checked ? Convert.ToInt32(dataGridView1.Rows[3].Cells[i + 1].Value) : 0;

                Item.Items[i] = new Item(dataGridView1.Rows[0].Cells[i + 1].Value.ToString(), weight, price, quantity);
            }

            // Проверяем корректность весов предметов
            if (!ValidateItemWeights(maxCapacity))
                return;

            // Выполнение алгоритма
            int result = await Task.Run(() => algorithm_with_cost.max_cost(Item.Items, maxCapacity, mainForm.IsCheckBox2Checked, mainForm.IsCheckBox3Checked, mainForm.IsCheckBox4Checked));
            string items = algorithm_with_cost.arr_items[Item.Items.Length, maxCapacity];

            // Вывод результатов
            textBox2.Text = result.ToString();
            textBox3.Text = items;
            ShowUIElements();

            // Сохранение результатов в базу данных
            if (checkBoxSaveToDb.Checked) // Используем отдельный чекбокс для базы данных
            {
                string taskType = GetTaskType();
                await SaveResultsToDatabase(taskType, maxCapacity, numberOfItems, result, items);
            }
        }

        // Метод сохранения в базу данных
        private async Task SaveResultsToDatabase(string taskType, int maxCapacity, int numberOfItems, int result, string items)
        {
            
                // Асинхронно сохраняем результаты в базу данных
                await Task.Run(() =>
                {
                    mainForm.recording_the_solution(
                        taskType,
                        maxCapacity,
                        numberOfItems,
                        result,
                        items
                    );
                });
            }
        

        // Метод для получения типа задачи
        private string GetTaskType()
        {
            if (mainForm.IsCheckBox3Checked)
                return "Задача со стоимостью, каждый предмет в единственном экземпляре";
            if (mainForm.IsCheckBox2Checked)
                return "Задача со стоимостью, каждый предмет в неограниченном количестве";
            if (mainForm.IsCheckBox4Checked)
                return "Задача со стоимостью, каждый предмет в ограниченном количестве";
            return "Неизвестный тип задачи";
        }

        // Валидация входных данных
        private bool ValidateInputs(out int maxCapacity)
        {
            maxCapacity = 0;

            // Проверка веса рюкзака
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                ShowError("Введите вес рюкзака!", textBox1);
                return false;
            }

            if (!int.TryParse(textBox1.Text, out maxCapacity) || maxCapacity <= 0 || maxCapacity > 100000)
            {
                ShowError("Вес рюкзака должен быть целым числом от 1 до 100 000!", textBox1);
                return false;
            }

            // Проверяем заполненность таблицы
            for (int i = 1; i < dataGridView1.RowCount; i++)
            {
                for (int j = 1; j < dataGridView1.ColumnCount; j++)
                {
                    var cellValue = dataGridView1.Rows[i].Cells[j].Value?.ToString();

                    if (string.IsNullOrWhiteSpace(cellValue))
                    {
                        ShowError("Заполните все ячейки таблицы!", dataGridView1);
                        return false;
                    }

                    if (!int.TryParse(cellValue, out var parsedValue) || parsedValue <= 0 || parsedValue > 100000)
                    {
                        ShowError("Введите корректные положительные числа (не более 100 000) в таблице!", dataGridView1);
                        return false;
                    }
                }
            }

            return true;
        }

        // Проверка корректности весов предметов
        private bool ValidateItemWeights(int maxCapacity)
        {
            int sum = 0;
            bool hasValidItem = false;

            foreach (var item in Item.Items)
            {
                sum += item.Weight * (mainForm.IsCheckBox4Checked ? item.Quantity : 1);
                if (item.Weight <= maxCapacity)
                    hasValidItem = true;
            }

            if (maxCapacity < sum && hasValidItem)
            {
                return true;
            }

            if (!hasValidItem)
            {
                ShowError("Нет подходящих предметов для заданного веса рюкзака!", dataGridView1);
                return false;
            }

            return true;
        }

        // Закрытие формы
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Печать результатов
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font myFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            string date = $"Дата: {DateTime.Now}";
            string taskType = GetTaskType();

            string backpackWeight = $"Вес рюкзака: {textBox1.Text}";
            string numberOfItems = $"Количество предметов: {mainForm.NumberOfItems}";
            string maxCost = $"Максимальная стоимость: {textBox2.Text}"; 
            string items = $"Набор предметов: {textBox3.Text}";

            // Печатаем информацию
            e.Graphics.DrawString(date, myFont, Brushes.Black, 40, 40);
            e.Graphics.DrawString("Исходные данные:", myFont, Brushes.Black, 40, 100);
            e.Graphics.DrawString(taskType, myFont, Brushes.Black, 40, 130);
            e.Graphics.DrawString(backpackWeight, myFont, Brushes.Black, 40, 160);
            e.Graphics.DrawString(numberOfItems, myFont, Brushes.Black, 40, 190);
            e.Graphics.DrawString("Результаты:", myFont, Brushes.Black, 40, 250);
            e.Graphics.DrawString(maxCost, myFont, Brushes.Black, 40, 280); 
            e.Graphics.DrawString(items, myFont, Brushes.Black, new RectangleF(40, 310, 770, 800));
        }

        //Кнопка для печати результатов
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
        string.IsNullOrWhiteSpace(textBox3.Text))
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

            string taskType = GetTaskType();
            string resultText = $"Дата: {DateTime.Now}\n\n" +
                                $"Исходные данные:\n" +
                                $"Тип задачи: {taskType}\n" +
                                $"Вес рюкзака: {textBox1.Text}\n" +
                                $"Количество предметов: {mainForm.NumberOfItems}\n\n" +
                                $"Результаты:\n" +
                                $"Ответ: {textBox2.Text}\n" +
                                $"Максимальная стоимость: {textBox2.Text}\n" +
                                $"Набор предметов: {textBox3.Text}";

            System.IO.File.WriteAllText(filename, resultText);
            MessageBox.Show("Файл сохранен");
        }
        // Скрытие UI элементов
        private void HideUIElements()
        {
            button6.Visible = false;
            label3.Visible = false;
            label2.Visible = false;
            textBox3.Visible = false;
            textBox2.Visible = false;
        }

        // Отображение UI элементов
        private void ShowUIElements()
        {
            button6.Visible = true;
            label3.Visible = true;
            label2.Visible = true;
            textBox3.Visible = true;
            textBox2.Visible = true;
        }

        // Метод для отображения ошибок
        private void ShowError(string message, Control control)
        {
            HideUIElements();
            MessageBox.Show(message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.ActiveControl = control;
        }

    }
}
