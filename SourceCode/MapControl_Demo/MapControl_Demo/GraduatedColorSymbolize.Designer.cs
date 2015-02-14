namespace MapControl_Demo
{
    partial class GraduatedColorSymbolize
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxNormalization = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxFields = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.classifyCBX = new System.Windows.Forms.ComboBox();
            this.nudClassCount = new System.Windows.Forms.NumericUpDown();
            this.lblClassificationMethod = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelectColorRamp = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSymbolize = new System.Windows.Forms.Button();
            this.cbxLayers2Symbolize = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudClassCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbxNormalization);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbxFields);
            this.groupBox1.Location = new System.Drawing.Point(22, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 91);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Field";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Normalization";
            // 
            // cbxNormalization
            // 
            this.cbxNormalization.FormattingEnabled = true;
            this.cbxNormalization.Location = new System.Drawing.Point(94, 55);
            this.cbxNormalization.Name = "cbxNormalization";
            this.cbxNormalization.Size = new System.Drawing.Size(106, 21);
            this.cbxNormalization.TabIndex = 10;
            this.cbxNormalization.SelectedIndexChanged += new System.EventHandler(this.cbxNormalization_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Field:";
            // 
            // cbxFields
            // 
            this.cbxFields.FormattingEnabled = true;
            this.cbxFields.Location = new System.Drawing.Point(94, 21);
            this.cbxFields.Name = "cbxFields";
            this.cbxFields.Size = new System.Drawing.Size(106, 21);
            this.cbxFields.TabIndex = 10;
            this.cbxFields.SelectedIndexChanged += new System.EventHandler(this.cbxFields_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Layer:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.classifyCBX);
            this.groupBox2.Controls.Add(this.nudClassCount);
            this.groupBox2.Controls.Add(this.lblClassificationMethod);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(20, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 85);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Classify";
            // 
            // classifyCBX
            // 
            this.classifyCBX.FormattingEnabled = true;
            this.classifyCBX.Items.AddRange(new object[] {
            "Equal Interval",
            "Quantile",
            "Natural Breaks",
            "Geometrical Interval"});
            this.classifyCBX.Location = new System.Drawing.Point(141, 47);
            this.classifyCBX.Margin = new System.Windows.Forms.Padding(2);
            this.classifyCBX.Name = "classifyCBX";
            this.classifyCBX.Size = new System.Drawing.Size(116, 21);
            this.classifyCBX.TabIndex = 19;
            this.classifyCBX.SelectedIndexChanged += new System.EventHandler(this.classifyCBX_SelectedIndexChanged);
            // 
            // nudClassCount
            // 
            this.nudClassCount.Location = new System.Drawing.Point(79, 44);
            this.nudClassCount.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.nudClassCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudClassCount.Name = "nudClassCount";
            this.nudClassCount.Size = new System.Drawing.Size(43, 20);
            this.nudClassCount.TabIndex = 18;
            this.nudClassCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudClassCount.ValueChanged += new System.EventHandler(this.nudClassCount_ValueChanged);
            // 
            // lblClassificationMethod
            // 
            this.lblClassificationMethod.Location = new System.Drawing.Point(139, 20);
            this.lblClassificationMethod.Name = "lblClassificationMethod";
            this.lblClassificationMethod.Size = new System.Drawing.Size(66, 15);
            this.lblClassificationMethod.TabIndex = 5;
            this.lblClassificationMethod.Text = "Classify Method";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Class Count";
            // 
            // btnSelectColorRamp
            // 
            this.btnSelectColorRamp.BackColor = System.Drawing.Color.Bisque;
            this.btnSelectColorRamp.Location = new System.Drawing.Point(20, 148);
            this.btnSelectColorRamp.Name = "btnSelectColorRamp";
            this.btnSelectColorRamp.Size = new System.Drawing.Size(216, 25);
            this.btnSelectColorRamp.TabIndex = 30;
            this.btnSelectColorRamp.Text = "Select Color Ramp";
            this.btnSelectColorRamp.UseVisualStyleBackColor = false;
            this.btnSelectColorRamp.Click += new System.EventHandler(this.btnSelectColorRamp_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(255, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 119);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(297, 222);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 25);
            this.btnClose.TabIndex = 28;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSymbolize
            // 
            this.btnSymbolize.Location = new System.Drawing.Point(297, 176);
            this.btnSymbolize.Name = "btnSymbolize";
            this.btnSymbolize.Size = new System.Drawing.Size(66, 25);
            this.btnSymbolize.TabIndex = 27;
            this.btnSymbolize.Text = "Symbolize";
            this.btnSymbolize.UseVisualStyleBackColor = true;
            this.btnSymbolize.Click += new System.EventHandler(this.btnSymbolize_Click);
            // 
            // cbxLayers2Symbolize
            // 
            this.cbxLayers2Symbolize.FormattingEnabled = true;
            this.cbxLayers2Symbolize.Location = new System.Drawing.Point(127, 21);
            this.cbxLayers2Symbolize.Name = "cbxLayers2Symbolize";
            this.cbxLayers2Symbolize.Size = new System.Drawing.Size(111, 21);
            this.cbxLayers2Symbolize.TabIndex = 26;
            this.cbxLayers2Symbolize.SelectedIndexChanged += new System.EventHandler(this.cbxLayers2Symbolize_SelectedIndexChanged);
            // 
            // GraduatedColorSymbolize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 288);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSelectColorRamp);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSymbolize);
            this.Controls.Add(this.cbxLayers2Symbolize);
            this.Name = "GraduatedColorSymbolize";
            this.Text = "Grudual Color Symbolize";
            this.Load += new System.EventHandler(this.GraduatedColorSymbolize_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudClassCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxNormalization;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxFields;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox classifyCBX;
        private System.Windows.Forms.NumericUpDown nudClassCount;
        private System.Windows.Forms.Label lblClassificationMethod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSelectColorRamp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSymbolize;
        private System.Windows.Forms.ComboBox cbxLayers2Symbolize;

    }
}