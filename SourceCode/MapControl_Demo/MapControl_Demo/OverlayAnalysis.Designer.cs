namespace MapControl_Demo
{
    partial class OverlayAnalysis
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.cboOveralyerType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSelectInputLayer = new System.Windows.Forms.ComboBox();
            this.lblInputLevel = new System.Windows.Forms.Label();
            this.numUpDownInputLevel = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cboOverlayLayer = new System.Windows.Forms.ComboBox();
            this.lblOverlayLevel = new System.Windows.Forms.Label();
            this.numUpDownOverlayLevel = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTolerance = new System.Windows.Forms.TextBox();
            this.lblAttributeType = new System.Windows.Forms.Label();
            this.cboAttributeType = new System.Windows.Forms.ComboBox();
            this.lblFeatureType = new System.Windows.Forms.Label();
            this.cboFeatureType = new System.Windows.Forms.ComboBox();
            this.grpInformation = new System.Windows.Forms.GroupBox();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOverlay = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownInputLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownOverlayLevel)).BeginInit();
            this.grpInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.cboOveralyerType);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cboSelectInputLayer);
            this.flowLayoutPanel1.Controls.Add(this.lblInputLevel);
            this.flowLayoutPanel1.Controls.Add(this.numUpDownInputLevel);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.cboOverlayLayer);
            this.flowLayoutPanel1.Controls.Add(this.lblOverlayLevel);
            this.flowLayoutPanel1.Controls.Add(this.numUpDownOverlayLevel);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.txtTolerance);
            this.flowLayoutPanel1.Controls.Add(this.lblAttributeType);
            this.flowLayoutPanel1.Controls.Add(this.cboAttributeType);
            this.flowLayoutPanel1.Controls.Add(this.lblFeatureType);
            this.flowLayoutPanel1.Controls.Add(this.cboFeatureType);
            this.flowLayoutPanel1.Controls.Add(this.grpInformation);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 23);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(297, 429);
            this.flowLayoutPanel1.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Overlay Type:    ";
            // 
            // cboOveralyerType
            // 
            this.cboOveralyerType.FormattingEnabled = true;
            this.cboOveralyerType.Items.AddRange(new object[] {
            "Intersect",
            "Union",
            "Different"});
            this.cboOveralyerType.Location = new System.Drawing.Point(94, 3);
            this.cboOveralyerType.Name = "cboOveralyerType";
            this.cboOveralyerType.Size = new System.Drawing.Size(167, 21);
            this.cboOveralyerType.TabIndex = 31;
            this.cboOveralyerType.SelectedIndexChanged += new System.EventHandler(this.cboOveralyerType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Input Layer:       ";
            // 
            // cboSelectInputLayer
            // 
            this.cboSelectInputLayer.FormattingEnabled = true;
            this.cboSelectInputLayer.Location = new System.Drawing.Point(93, 30);
            this.cboSelectInputLayer.Name = "cboSelectInputLayer";
            this.cboSelectInputLayer.Size = new System.Drawing.Size(168, 21);
            this.cboSelectInputLayer.TabIndex = 10;
            this.cboSelectInputLayer.SelectedIndexChanged += new System.EventHandler(this.cboSelectInputLayer_SelectedIndexChanged);
            // 
            // lblInputLevel
            // 
            this.lblInputLevel.AutoSize = true;
            this.lblInputLevel.Location = new System.Drawing.Point(3, 54);
            this.lblInputLevel.Name = "lblInputLevel";
            this.lblInputLevel.Size = new System.Drawing.Size(66, 13);
            this.lblInputLevel.TabIndex = 9;
            this.lblInputLevel.Text = "Input Level: ";
            // 
            // numUpDownInputLevel
            // 
            this.numUpDownInputLevel.Location = new System.Drawing.Point(75, 57);
            this.numUpDownInputLevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDownInputLevel.Name = "numUpDownInputLevel";
            this.numUpDownInputLevel.Size = new System.Drawing.Size(186, 20);
            this.numUpDownInputLevel.TabIndex = 33;
            this.numUpDownInputLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownInputLevel.ValueChanged += new System.EventHandler(this.numUpDownInputLevel_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Overlay Layer:";
            // 
            // cboOverlayLayer
            // 
            this.cboOverlayLayer.FormattingEnabled = true;
            this.cboOverlayLayer.Location = new System.Drawing.Point(84, 83);
            this.cboOverlayLayer.Name = "cboOverlayLayer";
            this.cboOverlayLayer.Size = new System.Drawing.Size(177, 21);
            this.cboOverlayLayer.TabIndex = 10;
            this.cboOverlayLayer.SelectedIndexChanged += new System.EventHandler(this.cboOverlayLayer_SelectedIndexChanged);
            // 
            // lblOverlayLevel
            // 
            this.lblOverlayLevel.AutoSize = true;
            this.lblOverlayLevel.Location = new System.Drawing.Point(3, 107);
            this.lblOverlayLevel.Name = "lblOverlayLevel";
            this.lblOverlayLevel.Size = new System.Drawing.Size(78, 13);
            this.lblOverlayLevel.TabIndex = 9;
            this.lblOverlayLevel.Text = "Overlay Level: ";
            // 
            // numUpDownOverlayLevel
            // 
            this.numUpDownOverlayLevel.Location = new System.Drawing.Point(87, 110);
            this.numUpDownOverlayLevel.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDownOverlayLevel.Name = "numUpDownOverlayLevel";
            this.numUpDownOverlayLevel.Size = new System.Drawing.Size(174, 20);
            this.numUpDownOverlayLevel.TabIndex = 33;
            this.numUpDownOverlayLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownOverlayLevel.ValueChanged += new System.EventHandler(this.numUpDownOverlayLevel_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tolerance:     ";
            // 
            // txtTolerance
            // 
            this.txtTolerance.Location = new System.Drawing.Point(82, 136);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.Size = new System.Drawing.Size(179, 20);
            this.txtTolerance.TabIndex = 32;
            this.txtTolerance.Leave += new System.EventHandler(this.txtTolerance_Leave);
            // 
            // lblAttributeType
            // 
            this.lblAttributeType.AutoSize = true;
            this.lblAttributeType.Location = new System.Drawing.Point(3, 159);
            this.lblAttributeType.Name = "lblAttributeType";
            this.lblAttributeType.Size = new System.Drawing.Size(76, 13);
            this.lblAttributeType.TabIndex = 19;
            this.lblAttributeType.Text = "Attribute Type:";
            // 
            // cboAttributeType
            // 
            this.cboAttributeType.FormattingEnabled = true;
            this.cboAttributeType.Items.AddRange(new object[] {
            "All Fields",
            "Not Inlcude FID",
            "Only Include FID"});
            this.cboAttributeType.Location = new System.Drawing.Point(85, 162);
            this.cboAttributeType.Name = "cboAttributeType";
            this.cboAttributeType.Size = new System.Drawing.Size(176, 21);
            this.cboAttributeType.TabIndex = 10;
            this.cboAttributeType.SelectedIndexChanged += new System.EventHandler(this.cboAttributeType_SelectedIndexChanged);
            // 
            // lblFeatureType
            // 
            this.lblFeatureType.AutoSize = true;
            this.lblFeatureType.Location = new System.Drawing.Point(3, 186);
            this.lblFeatureType.Name = "lblFeatureType";
            this.lblFeatureType.Size = new System.Drawing.Size(70, 13);
            this.lblFeatureType.TabIndex = 19;
            this.lblFeatureType.Text = "Feature Type";
            // 
            // cboFeatureType
            // 
            this.cboFeatureType.FormattingEnabled = true;
            this.cboFeatureType.Items.AddRange(new object[] {
            "According by Input",
            "Line",
            "Point"});
            this.cboFeatureType.Location = new System.Drawing.Point(79, 189);
            this.cboFeatureType.Name = "cboFeatureType";
            this.cboFeatureType.Size = new System.Drawing.Size(182, 21);
            this.cboFeatureType.TabIndex = 10;
            this.cboFeatureType.SelectedIndexChanged += new System.EventHandler(this.cboFeatureType_SelectedIndexChanged);
            // 
            // grpInformation
            // 
            this.grpInformation.Controls.Add(this.txtMessages);
            this.grpInformation.Location = new System.Drawing.Point(3, 216);
            this.grpInformation.Name = "grpInformation";
            this.grpInformation.Size = new System.Drawing.Size(268, 182);
            this.grpInformation.TabIndex = 25;
            this.grpInformation.TabStop = false;
            this.grpInformation.Text = "Message";
            // 
            // txtMessages
            // 
            this.txtMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessages.Location = new System.Drawing.Point(3, 16);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(262, 163);
            this.txtMessages.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 458);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 25);
            this.button1.TabIndex = 39;
            this.button1.Text = "Output Path";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(249, 458);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 25);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOverlay
            // 
            this.btnOverlay.Location = new System.Drawing.Point(126, 458);
            this.btnOverlay.Name = "btnOverlay";
            this.btnOverlay.Size = new System.Drawing.Size(94, 25);
            this.btnOverlay.TabIndex = 37;
            this.btnOverlay.Text = "Overlay Analysis";
            this.btnOverlay.UseVisualStyleBackColor = true;
            this.btnOverlay.Click += new System.EventHandler(this.btnOverlay_Click);
            // 
            // OverlayAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(324, 494);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOverlay);
            this.Name = "OverlayAnalysis";
            this.Text = "Overlay Analysis";
            this.Load += new System.EventHandler(this.OverlayAnalysis_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownInputLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownOverlayLevel)).EndInit();
            this.grpInformation.ResumeLayout(false);
            this.grpInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboOveralyerType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSelectInputLayer;
        private System.Windows.Forms.Label lblInputLevel;
        private System.Windows.Forms.NumericUpDown numUpDownInputLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboOverlayLayer;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblOverlayLevel;
        private System.Windows.Forms.NumericUpDown numUpDownOverlayLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAttributeType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtTolerance;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox cboAttributeType;
        private System.Windows.Forms.Label lblFeatureType;
        private System.Windows.Forms.ComboBox cboFeatureType;
        private System.Windows.Forms.GroupBox grpInformation;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnOverlay;

    }
}