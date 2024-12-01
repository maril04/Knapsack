namespace Knapsack
{
    partial class option_without_cost
    {
        private System.ComponentModel.IContainer components = null;

        // Очистка ресурсов
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(option_without_cost));
            базаДанныхToolStripMenuItem = new MenuStrip();
            главнаяСтраницаToolStripMenuItem = new ToolStripMenuItem();
            базаДанныхToolStripMenuItem1 = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            label1 = new Label();
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            button3 = new Button();
            button6 = new Button();
            label2 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            checkBoxSaveToDb = new CheckBox();
            printPreviewDialog1 = new PrintPreviewDialog();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            buttonPrint = new Button();
            базаДанныхToolStripMenuItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // базаДанныхToolStripMenuItem
            // 
            базаДанныхToolStripMenuItem.BackColor = SystemColors.AppWorkspace;
            базаДанныхToolStripMenuItem.Items.AddRange(new ToolStripItem[] { главнаяСтраницаToolStripMenuItem, базаДанныхToolStripMenuItem1 });
            базаДанныхToolStripMenuItem.Location = new Point(0, 0);
            базаДанныхToolStripMenuItem.Name = "базаДанныхToolStripMenuItem";
            базаДанныхToolStripMenuItem.Size = new Size(626, 24);
            базаДанныхToolStripMenuItem.TabIndex = 0;
            базаДанныхToolStripMenuItem.Text = "База данных";
            // 
            // главнаяСтраницаToolStripMenuItem
            // 
            главнаяСтраницаToolStripMenuItem.Name = "главнаяСтраницаToolStripMenuItem";
            главнаяСтраницаToolStripMenuItem.Size = new Size(117, 20);
            главнаяСтраницаToolStripMenuItem.Text = "Главная страница";
            главнаяСтраницаToolStripMenuItem.Click += главнаяСтраницаToolStripMenuItem_Click;
            // 
            // базаДанныхToolStripMenuItem1
            // 
            базаДанныхToolStripMenuItem1.Name = "базаДанныхToolStripMenuItem1";
            базаДанныхToolStripMenuItem1.Size = new Size(87, 20);
            базаДанныхToolStripMenuItem1.Text = "База данных";
            базаДанныхToolStripMenuItem1.Click += базаДанныхToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 40);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 1;
            label1.Text = "Вес рюкзака (кг):";
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(118, 38);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.None;
            dataGridView1.BackgroundColor = SystemColors.ControlLight;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(15, 85);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(585, 151);
            dataGridView1.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(12, 259);
            button1.Name = "button1";
            button1.Size = new Size(120, 23);
            button1.TabIndex = 4;
            button1.Text = "Собрать рюкзак";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.Location = new Point(15, 377);
            button3.Name = "button3";
            button3.Size = new Size(120, 23);
            button3.TabIndex = 5;
            button3.Text = "Закрыть";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button6
            // 
            button6.Location = new Point(293, 374);
            button6.Name = "button6";
            button6.Size = new Size(130, 23);
            button6.TabIndex = 7;
            button6.Text = "Сохранить в файл";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(154, 263);
            label2.Name = "label2";
            label2.Size = new Size(118, 15);
            label2.TabIndex = 8;
            label2.Text = "Максимальный вес:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(154, 292);
            label3.Name = "label3";
            label3.Size = new Size(108, 15);
            label3.TabIndex = 10;
            label3.Text = "Набор предметов:";
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.ControlLight;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Location = new Point(278, 263);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 16);
            textBox2.TabIndex = 9;
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.ControlLight;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Location = new Point(268, 292);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.Size = new Size(332, 80);
            textBox3.TabIndex = 11;
            // 
            // checkBoxSaveToDb
            // 
            checkBoxSaveToDb.AutoSize = true;
            checkBoxSaveToDb.Location = new Point(240, 40);
            checkBoxSaveToDb.Name = "checkBoxSaveToDb";
            checkBoxSaveToDb.Size = new Size(165, 19);
            checkBoxSaveToDb.TabIndex = 12;
            checkBoxSaveToDb.Text = "Сохранить в базу данных";
            checkBoxSaveToDb.UseVisualStyleBackColor = true;
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
            // buttonPrint
            // 
            buttonPrint.BackColor = SystemColors.ButtonHighlight;
            buttonPrint.Location = new Point(448, 374);
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new Size(130, 23);
            buttonPrint.TabIndex = 13;
            buttonPrint.Text = "Печать результата";
            buttonPrint.UseVisualStyleBackColor = false;
            buttonPrint.Click += buttonPrint_Click;
            // 
            // option_without_cost
            // 
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(626, 411);
            Controls.Add(checkBoxSaveToDb);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(button6);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(buttonPrint);
            Controls.Add(dataGridView1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(базаДанныхToolStripMenuItem);
            MainMenuStrip = базаДанныхToolStripMenuItem;
            Name = "option_without_cost";
            Text = "Задача без стоимости";
            Load += simple_option_Load;
            базаДанныхToolStripMenuItem.ResumeLayout(false);
            базаДанныхToolStripMenuItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.MenuStrip базаДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem главнаяСтраницаToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.CheckBox checkBoxSaveToDb;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button buttonPrint;
        private ToolStripMenuItem базаДанныхToolStripMenuItem1;
    }
}
