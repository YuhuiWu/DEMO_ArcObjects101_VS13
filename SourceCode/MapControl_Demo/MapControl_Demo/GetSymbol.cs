using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapControl_Demo
{
    public partial class GetSymbol : Form
    {
        public ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;

        private Label label1;
        private ComboBox cbxStyles;
        private Button btnOtherStyles;

        private FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
        esriSymbologyStyleClass styleClass;
        public GetSymbol(esriSymbologyStyleClass symStyleClass)
        {
            InitializeComponent();
            styleClass = symStyleClass;
        }
        private void cbxStylesAddItems(string[] files)
        {
            if (files.GetLength(0) == 0) return;
            foreach (string file in files)
            {
                cbxStyles.Items.Add(file);
            }
        }
        string stylesPath = string.Empty;
        private void cbxStylesAddItems(string path)
        {
            string[] serverstyleFiles = System.IO.Directory.GetFiles(stylesPath,
                "*.serverstyle", SearchOption.AllDirectories);
            string[] styleFiles = System.IO.Directory.GetFiles(stylesPath,
                "*.style", SearchOption.AllDirectories);
            cbxStylesAddItems(serverstyleFiles);
            cbxStylesAddItems(styleFiles);
        }
        private void LoadStyles()
        {
            string sInstall = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
            string defaultStyle = System.IO.Path.Combine(sInstall, "Styles\\Survey.ServerStyle");
            if (System.IO.File.Exists(defaultStyle))
            {
                axSymbologyControl1.LoadStyleFile(defaultStyle);
                axSymbologyControl1.StyleClass = styleClass;
                //axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass).SelectItem(0);
                cbxStyles.Text = defaultStyle;
            }
            stylesPath = sInstall + "Styles\\";
            cbxStyles.Items.Clear();
            cbxStylesAddItems(stylesPath);
        }

        private void GetSymbol_Load(object sender, EventArgs e)
        {
            LoadStyles();
        }

        public IStyleGalleryItem m_styleGalleryItem;
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            m_styleGalleryItem = null;
            this.Hide();
        }
        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void axSymbologyControl1_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            m_styleGalleryItem = e.styleGalleryItem as IStyleGalleryItem;
        }
        private void PreviewImage()
        {
            ISymbologyStyleClass symbologyStyleClass =
                axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass);
            stdole.IPictureDisp picture = symbologyStyleClass.PreviewItem
                (m_styleGalleryItem, pictureBox1.Width, pictureBox1.Height);
            System.Drawing.Image image = System.Drawing.Image.
                FromHbitmap(new System.IntPtr(picture.Handle));
            pictureBox1.Image = image;
        }

        private void btnOtherStyles_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                stylesPath = folderBrowserDialog1.SelectedPath;
                cbxStylesAddItems(stylesPath);
            }
        }

        private void cbxStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxStyles.SelectedItem == null) return;
            axSymbologyControl1.Clear();
            stylesPath = cbxStyles.SelectedItem.ToString();
            string ext = System.IO.Path.GetExtension(stylesPath).ToLower();
            if (ext == ".serverstyle")
                axSymbologyControl1.LoadStyleFile(stylesPath);
            if (ext == ".style")
                axSymbologyControl1.LoadDesktopStyleFile(stylesPath);
            axSymbologyControl1.StyleClass = styleClass;
        }
        public IStyleGalleryItem GetItem(ESRI.ArcGIS.Controls.esriSymbologyStyleClass styleClass)
        {
            //Set the style class
            axSymbologyControl1.StyleClass = styleClass;
            axSymbologyControl1.Update();
            //Show the modal form
            this.ShowDialog();
            //Return the selected label style
            return m_styleGalleryItem;
        }
    }
}
