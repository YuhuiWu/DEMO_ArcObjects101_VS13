namespace MapControl_Demo
{
    partial class GetSymbol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetSymbol));
            this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxStyles = new System.Windows.Forms.ComboBox();
            this.btnOtherStyles = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // axSymbologyControl1
            // 
            this.axSymbologyControl1.Location = new System.Drawing.Point(8, 47);
            this.axSymbologyControl1.Name = "axSymbologyControl1";
            this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
            this.axSymbologyControl1.Size = new System.Drawing.Size(489, 307);
            this.axSymbologyControl1.TabIndex = 0;
            this.axSymbologyControl1.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.axSymbologyControl1_OnItemSelected);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(521, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 163);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(553, 332);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(96, 25);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "OK";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(553, 304);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(96, 23);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select Symbology Style";
            // 
            // cbxStyles
            // 
            this.cbxStyles.FormattingEnabled = true;
            this.cbxStyles.Location = new System.Drawing.Point(131, 8);
            this.cbxStyles.Name = "cbxStyles";
            this.cbxStyles.Size = new System.Drawing.Size(447, 21);
            this.cbxStyles.TabIndex = 8;
            this.cbxStyles.SelectedIndexChanged += new System.EventHandler(this.cbxStyles_SelectedIndexChanged);
            // 
            // btnOtherStyles
            // 
            this.btnOtherStyles.Location = new System.Drawing.Point(594, 8);
            this.btnOtherStyles.Name = "btnOtherStyles";
            this.btnOtherStyles.Size = new System.Drawing.Size(55, 24);
            this.btnOtherStyles.TabIndex = 7;
            this.btnOtherStyles.Text = "Other Styles";
            this.btnOtherStyles.Click += new System.EventHandler(this.btnOtherStyles_Click);
            // 
            // GetSymbol
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(791, 397);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxStyles);
            this.Controls.Add(this.btnOtherStyles);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.axSymbologyControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GetSymbol";
            this.Text = "Select Symbol";
            this.Load += new System.EventHandler(this.GetSymbol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
    }
}