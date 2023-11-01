namespace HW4_First_Part
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtColumns = new TextBox();
            txtNumber = new TextBox();
            submitButton = new Button();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtColumns
            // 
            txtColumns.Location = new Point(915, 63);
            txtColumns.Name = "txtColumns";
            txtColumns.Size = new Size(175, 35);
            txtColumns.TabIndex = 0;
            txtColumns.Text = "4";
            txtColumns.UseWaitCursor = true;
            // 
            // txtNumber
            // 
            txtNumber.Location = new Point(1614, 61);
            txtNumber.Name = "txtNumber";
            txtNumber.Size = new Size(175, 35);
            txtNumber.TabIndex = 1;
            txtNumber.Text = "10";
            // 
            // submitButton
            // 
            submitButton.Location = new Point(1818, 59);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(215, 40);
            submitButton.TabIndex = 2;
            submitButton.Text = "Choose the intervals";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += SubmitButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(638, 135);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 72;
            dataGridView1.Size = new Size(1468, 1111);
            dataGridView1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(638, 64);
            label1.Name = "label1";
            label1.Size = new Size(271, 30);
            label1.TabIndex = 6;
            label1.Text = "Insert the number variables:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1117, 64);
            label2.Name = "label2";
            label2.Size = new Size(462, 30);
            label2.TabIndex = 7;
            label2.Text = "Insert the number of examples for each variable:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(726, 21);
            label3.Name = "label3";
            label3.Size = new Size(721, 30);
            label3.TabIndex = 8;
            label3.Text = "Joint distribution of any number of 2,3, ...k, continuous quantitative variables";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2769, 1354);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(submitButton);
            Controls.Add(txtNumber);
            Controls.Add(txtColumns);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtColumns;
        private TextBox txtNumber;
        private Button submitButton;
        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
