namespace lvlRandomWalk
{
    partial class AttackForm
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
            components = new System.ComponentModel.Container();
            nValue = new NumericUpDown();
            cancBT = new Button();
            startBT = new Button();
            label1 = new Label();
            label2 = new Label();
            bindingSource1 = new BindingSource(components);
            picBox = new PictureBox();
            label3 = new Label();
            mSystems = new NumericUpDown();
            pValue = new NumericUpDown();
            sValue = new NumericUpDown();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)nValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mSystems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pValue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sValue).BeginInit();
            SuspendLayout();
            // 
            // nValue
            // 
            nValue.Location = new Point(163, 11);
            nValue.Margin = new Padding(4, 2, 4, 2);
            nValue.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nValue.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nValue.Name = "nValue";
            nValue.Size = new Size(119, 39);
            nValue.TabIndex = 1;
            nValue.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cancBT
            // 
            cancBT.Location = new Point(1926, 15);
            cancBT.Margin = new Padding(4, 2, 4, 2);
            cancBT.Name = "cancBT";
            cancBT.Size = new Size(123, 47);
            cancBT.TabIndex = 3;
            cancBT.Text = "Cancel";
            cancBT.UseVisualStyleBackColor = true;
            cancBT.Click += cancBT_Click;
            // 
            // startBT
            // 
            startBT.Location = new Point(1796, 13);
            startBT.Margin = new Padding(4, 2, 4, 2);
            startBT.Name = "startBT";
            startBT.Size = new Size(123, 47);
            startBT.TabIndex = 4;
            startBT.Text = "Start";
            startBT.UseVisualStyleBackColor = true;
            startBT.Click += startBT_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(119, 17);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(37, 32);
            label1.TabIndex = 6;
            label1.Text = "N:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(744, 17);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(120, 32);
            label2.TabIndex = 7;
            label2.Text = "Choose p:";
            // 
            // picBox
            // 
            picBox.Location = new Point(15, 70);
            picBox.Margin = new Padding(4, 2, 4, 2);
            picBox.Name = "picBox";
            picBox.Size = new Size(2043, 1067);
            picBox.TabIndex = 8;
            picBox.TabStop = false;
            picBox.MouseDown += mouseDown;
            picBox.MouseMove += mouseMove;
            picBox.MouseUp += mouseUp;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(355, 17);
            label3.Margin = new Padding(7, 0, 7, 0);
            label3.Name = "label3";
            label3.Size = new Size(132, 32);
            label3.TabIndex = 9;
            label3.Text = "M systems:";
            // 
            // mSystems
            // 
            mSystems.Location = new Point(494, 11);
            mSystems.Margin = new Padding(7, 6, 7, 6);
            mSystems.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            mSystems.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            mSystems.Name = "mSystems";
            mSystems.Size = new Size(126, 39);
            mSystems.TabIndex = 10;
            mSystems.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // pValue
            // 
            pValue.DecimalPlaces = 5;
            pValue.Increment = new decimal(new int[] { 25, 0, 0, 262144 });
            pValue.Location = new Point(884, 15);
            pValue.Margin = new Padding(4, 2, 4, 2);
            pValue.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            pValue.Name = "pValue";
            pValue.Size = new Size(141, 39);
            pValue.TabIndex = 2;
            // 
            // sValue
            // 
            sValue.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            sValue.Location = new Point(1329, 15);
            sValue.Minimum = new decimal(new int[] { 20, 0, 0, 0 });
            sValue.Name = "sValue";
            sValue.Size = new Size(148, 39);
            sValue.TabIndex = 11;
            sValue.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1191, 17);
            label4.Name = "label4";
            label4.Size = new Size(119, 32);
            label4.TabIndex = 12;
            label4.Text = "Choose S:";
            // 
            // AttackForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(2069, 1150);
            Controls.Add(label4);
            Controls.Add(sValue);
            Controls.Add(mSystems);
            Controls.Add(label3);
            Controls.Add(picBox);
            Controls.Add(pValue);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(startBT);
            Controls.Add(cancBT);
            Controls.Add(nValue);
            Margin = new Padding(4, 2, 4, 2);
            Name = "AttackForm";
            Text = "Form1";
            FormClosing += closingFunc;
            ResizeEnd += resizePicBox;
            ((System.ComponentModel.ISupportInitialize)nValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)mSystems).EndInit();
            ((System.ComponentModel.ISupportInitialize)pValue).EndInit();
            ((System.ComponentModel.ISupportInitialize)sValue).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public NumericUpDown nValue;
        public Button cancBT;
        public Button startBT;

        private Label label1;
        private Label label2;
        private BindingSource bindingSource1;
        public PictureBox picBox;
        private Label label3;
        private NumericUpDown mSystems;
        public NumericUpDown pValue;
        private NumericUpDown sValue;
        private Label label4;
    }
}