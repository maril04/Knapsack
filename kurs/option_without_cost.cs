using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Knapsack
{
    public partial class option_without_cost : Form
    {
        // Ссылка на главную форму
        public main_form mainForm = null;

        public option_without_cost(main_form f)
        {
            InitializeComponent();
            mainForm = f;

            // Установка фильтров для диалогов открытия и сохранения файлов
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }
      
        // Обработчик загрузки формы
        private void simple_option_Load(object sender, EventArgs e)
        {
            // Устанавливаем фокус на текстовое поле
            this.ActiveControl = textBox1;

            // Избегаем повторного выполнения кода
            if (dataGridView1.RowCount > 1)
                return;

            // Отключаем возможность добавления строк вручную
            dataGridView1.AllowUserToAddRows = false;

            // Получаем количество предметов из главной формы
            int numberOfItems = Convert.ToInt32(mainForm.NumberOfItems);

            // Создаем таблицу для ввода данных
            dataGridView1.ColumnCount = numberOfItems + 1;


            // Настраиваем строки таблицы в зависимости от типа задачи
            if (mainForm.IsCheckBox2Checked || mainForm.IsCheckBox3Checked)
            {
                dataGridView1.RowCount = 2; // Строки: № предмета, Вес
                for (int i = 0; i < 2; i++)
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }
            else if (mainForm.IsCheckBox4Checked)
            {
                dataGridView1.RowCount = 3; // Строки: № предмета, Вес, Количество
                dataGridView1.Rows[2].Cells[0].Value = "Количество предметов";
                for (int i = 0; i < 3; i++)
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }

            // Устанавливаем заголовки строк и столбцов
            dataGridView1.Rows[0].Cells[0].Value = "№ предмета";
            dataGridView1.Rows[1].Cells[0].Value = "Вес предмета";

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
            not_visible(sender, e);

            // Проверяем корректность введенных данных
            if (!ValidateInputs(out int maxCapacity)) return;

            int numberOfItems = dataGridView1.ColumnCount - 1;
            Item.Items = new Item[numberOfItems];

            // Заполняем массив предметов
            for (int i = 0; i < numberOfItems; i++)
            {
                int weight = Convert.ToInt32(dataGridView1.Rows[1].Cells[i + 1].Value);
                int quantity = mainForm.IsCheckBox4Checked ? Convert.ToInt32(dataGridView1.Rows[2].Cells[i + 1].Value) : 0;
                Item.Items[i] = new Item(dataGridView1.Rows[0].Cells[i + 1].Value.ToString(), weight, 0, quantity);
            }

            // Проверка корректности весов предметов
            if (!ValidateItemWeights(maxCapacity)) return;

            // Выполнение алгоритма
            int result = await Task.Run(() => algorithm_without_cost.max_weight(Item.Items, maxCapacity, mainForm.IsCheckBox2Checked, mainForm.IsCheckBox3Checked, mainForm.IsCheckBox4Checked));
            string items = algorithm_without_cost.arr_items[numberOfItems, maxCapacity];

            // Вывод результатов
            textBox2.Text = result.ToString();
            textBox3.Text = items;
            visible(sender, e);

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

        // Метод для определения типа задачи
        private string GetTaskType()
        {
            if (mainForm.IsCheckBox1Checked) // Если задача со стоимостью определена в главной форме
            {
                return "Задача с учетом стоимости";
            }
            else if (mainForm.IsCheckBox3Checked)
            {
                return "Задача без стоимости, предметы в единственном экземпляре";
            }
            else if (mainForm.IsCheckBox2Checked)
            {
                return "Задача без стоимости, предметы в неограниченном количестве";
            }
            else if (mainForm.IsCheckBox4Checked)
            {
                return "Задача без стоимости, предметы в ограниченном количестве";
            }
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
                if (item.Weight <= maxCapacity) hasValidItem = true;
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

        

        //Кнопка для печати результатов
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
         string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Заполните все данные перед печатью!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        // Печать результатов
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font myFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            string date = "Дата: " + DateTime.Now;
            string task_type = "";
            if (mainForm.checkBox2.Checked)
                task_type = "Тип задачи: без стоимости, каждый предмет имеется в единственном экземпляре";
            else if (mainForm.checkBox3.Checked)
                task_type = "Тип задачи: без стоимости, каждый предмет имеется в неограниченном количестве";
            else if (mainForm.checkBox4.Checked)
                task_type = "Тип задачи: без стоимости, каждый предмет имеется в ограниченном количестве";

            string Backpack_weight = "Вес рюкзака: " + textBox1.Text;
            string Number_of_items = "Количество предметов: " + Convert.ToString(mainForm.numericUpDown1.Value);
            string Answer = "Ответ: " + textBox2.Text;
            string Items = "Набор предметов: " + textBox3.Text;

            e.Graphics.DrawString(date, myFont, Brushes.Black, 40, 40);
            e.Graphics.DrawString("Исходные данные:", myFont, Brushes.Black, 40, 100);
            e.Graphics.DrawString(task_type, myFont, Brushes.Black, 40, 130);
            e.Graphics.DrawString(Backpack_weight, myFont, Brushes.Black, 40, 160);
            e.Graphics.DrawString(Number_of_items, myFont, Brushes.Black, 40, 190);
            e.Graphics.DrawString("Результаты:", myFont, Brushes.Black, 40, 250);
            e.Graphics.DrawString(Answer, myFont, Brushes.Black, 40, 280);

            // Печать набора предметов
            e.Graphics.DrawString(Items, myFont, Brushes.Black,
                new RectangleF(40, 310, e.MarginBounds.Width - 40, e.MarginBounds.Height - 310));
        }


        // Сохранение резултатов в файл
        private void button6_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) return;

            string filename = saveFileDialog1.FileName;
            string taskType = mainForm.IsCheckBox3Checked
                ? "Тип задачи: предметы в единственном экземпляре"
                : mainForm.IsCheckBox2Checked
                ? "Тип задачи: предметы в неограниченном количестве"
                : "Тип задачи: предметы в ограниченном количестве";

            string resultText = $"Дата: {DateTime.Now}\n\nИсходные данные:\nТип задачи: {taskType}\nВес рюкзака: {textBox1.Text}\nКоличество предметов: {mainForm.NumberOfItems}\n\nРезультаты:\nОтвет: {textBox2.Text}\nНабор предметов: {textBox3.Text}";

            System.IO.File.WriteAllText(filename, resultText);
            MessageBox.Show("Файл сохранен");
        }

        private void not_visible(object sender, EventArgs e)
        {
            button6.Visible = false;
            label3.Visible = false;
            label2.Visible = false;
            textBox3.Visible = false;
            textBox2.Visible = false;
        }

        private void visible(object sender, EventArgs e)
        {
            label2.Visible = true;
            button6.Visible = true;
            label3.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
        }

        private void ShowError(string message, Control control)
        {
            not_visible(null, null);
            MessageBox.Show(message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.ActiveControl = control;
        }


    }
}
