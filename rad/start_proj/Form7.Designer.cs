namespace start_proj
{
    partial class Form7
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            button1 = new Button();
            checkedListBox1 = new CheckedListBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            dateTimePicker1 = new DateTimePicker();
            dateTimePicker2 = new DateTimePicker();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(0, 52);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(800, 219);
            dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(317, 362);
            button1.Name = "button1";
            button1.Size = new Size(142, 55);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(29, 308);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(160, 112);
            checkedListBox1.TabIndex = 2;
            checkedListBox1.Visible = false;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(317, 308);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(142, 23);
            comboBox1.TabIndex = 3;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 21F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(284, 11);
            label1.Name = "label1";
            label1.Size = new Size(221, 38);
            label1.TabIndex = 4;
            label1.Text = "Отчет оборотов";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(354, 290);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 5;
            label2.Text = "поставщик";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(578, 308);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 6;
            dateTimePicker1.ValueChanged += dtp1_Changed;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(578, 362);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(200, 23);
            dateTimePicker2.TabIndex = 7;
            dateTimePicker2.ValueChanged += dtp1_Changed;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(628, 290);
            label3.Name = "label3";
            label3.Size = new Size(92, 15);
            label3.TabIndex = 8;
            label3.Text = "выбор периода";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(534, 314);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 9;
            label4.Text = "от";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(534, 368);
            label5.Name = "label5";
            label5.Size = new Size(20, 15);
            label5.TabIndex = 10;
            label5.Text = "до";
            // 
            // Form7
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(dateTimePicker2);
            Controls.Add(dateTimePicker1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(checkedListBox1);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "Form7";
            Text = "Form7";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private CheckedListBox checkedListBox1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}