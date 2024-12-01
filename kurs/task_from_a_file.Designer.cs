namespace Knapsack
{
    partial class task_from_a_file
    {
        private System.ComponentModel.IContainer components = null;

        // Освобождение ресурсов
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(task_from_a_file));
            menuStrip1 = new MenuStrip();
            главнаяСтраницаToolStripMenuItem = new ToolStripMenuItem();
            базаДанныхToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            button4 = new Button();
            label2 = new Label();
            buttonPrint = new Button();
            textBox2 = new TextBox();
            label3 = new Label();
            textBox3 = new TextBox();
            button6 = new Button();
            button3 = new Button();
            printPreviewDialog1 = new PrintPreviewDialog();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            checkBoxSaveToDb = new CheckBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.AppWorkspace;
            menuStrip1.Items.AddRange(new ToolStripItem[] { главнаяСтраницаToolStripMenuItem, базаДанныхToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(603, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // главнаяСтраницаToolStripMenuItem
            // 
            главнаяСтраницаToolStripMenuItem.Name = "главнаяСтраницаToolStripMenuItem";
            главнаяСтраницаToolStripMenuItem.Size = new Size(117, 20);
            главнаяСтраницаToolStripMenuItem.Text = "Главная страница";
            главнаяСтраницаToolStripMenuItem.Click += главнаяСтраницаToolStripMenuItem_Click;
            // 
            // базаДанныхToolStripMenuItem
            // 
            базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            базаДанныхToolStripMenuItem.Size = new Size(87, 20);
            базаДанныхToolStripMenuItem.Text = "База данных";
            базаДанныхToolStripMenuItem.Click += базаДанныхToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 40);
            label1.Name = "label1";
            label1.Size = new Size(135, 15);
            label1.TabIndex = 1;
            label1.Text = "Содержимое из файла:";
            // 
            // richTextBox1
            // 
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Location = new Point(15, 60);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(561, 150);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // button4
            // 
            button4.Location = new Point(15, 220);
            button4.Name = "button4";
            button4.Size = new Size(150, 23);
            button4.TabIndex = 3;
            button4.Text = "Выберите файл";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 260);
            label2.Name = "label2";
            label2.Size = new Size(154, 15);
            label2.TabIndex = 4;
            label2.Text = "Максимальная стоимость:";
            label2.Visible = false;
            // 
            // buttonPrint
            // 
            buttonPrint.BackColor = SystemColors.ControlLightLight;
            buttonPrint.Location = new Point(137, 372);
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new Size(150, 23);
            buttonPrint.TabIndex = 11;
            buttonPrint.Text = "Печать результата";
            buttonPrint.UseVisualStyleBackColor = false;
            buttonPrint.Click += buttonPrint_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.ControlLight;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Location = new Point(172, 260);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(100, 16);
            textBox2.TabIndex = 5;
            textBox2.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 286);
            label3.Name = "label3";
            label3.Size = new Size(108, 15);
            label3.TabIndex = 6;
            label3.Text = "Набор предметов:";
            label3.Visible = false;
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.ControlLight;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Location = new Point(129, 286);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.Size = new Size(344, 80);
            textBox3.TabIndex = 7;
            textBox3.Visible = false;
            // 
            // button6
            // 
            button6.Location = new Point(303, 372);
            button6.Name = "button6";
            button6.Size = new Size(150, 23);
            button6.TabIndex = 9;
            button6.Text = "Сохранить в файл";
            button6.UseVisualStyleBackColor = true;
            button6.Visible = false;
            button6.Click += button6_Click;
            // 
            // button3
            // 
            button3.Location = new Point(12, 427);
            button3.Name = "button3";
            button3.Size = new Size(150, 23);
            button3.TabIndex = 10;
            button3.Text = "Закрыть";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // printPreviewDialog1
            // 
            printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog1.ClientSize = new Size(400, 300);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Enabled = true;
            printPreviewDialog1.Icon = (Icon)resources.GetObject("printPreviewDialog1.Icon");
            printPreviewDialog1.Name = "printPreviewDialog1";
            printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // checkBoxSaveToDb
            // 
            checkBoxSaveToDb.AutoSize = true;
            checkBoxSaveToDb.Location = new Point(303, 224);
            checkBoxSaveToDb.Name = "checkBoxSaveToDb";
            checkBoxSaveToDb.Size = new Size(172, 19);
            checkBoxSaveToDb.TabIndex = 12;
            checkBoxSaveToDb.Text = "Сохранить в базу даннных";
            checkBoxSaveToDb.UseVisualStyleBackColor = true;
            // 
            // task_from_a_file
            // 
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(603, 462);
            Controls.Add(checkBoxSaveToDb);
            Controls.Add(button3);
            Controls.Add(button6);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(button4);
            Controls.Add(buttonPrint);
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "task_from_a_file";
            Text = "Задача из файла";
            Load += task_from_a_file_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem главнаяСтраницаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem базаДанныхToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button buttonPrint;
        private CheckBox checkBoxSaveToDb;
    }
}
