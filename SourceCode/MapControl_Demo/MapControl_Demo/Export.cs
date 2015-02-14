using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.Display;
using Microsoft.VisualBasic;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace MapControl_Demo
{
    public partial class Export : Form
    {
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Button btnPrint;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private Label label1;
        private ComboBox cbxOutputType;
        private Button btnOutputFile;
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        private TextBox txtResolution;
        private Label label2;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        public Export(IPageLayout pagelayout)
        {
            InitializeComponent();
            axPageLayoutControl1.PageLayout = pagelayout;
        }
        string exportFileName = "C:\\Temp\\Test.bmp";
        string fileExtension = "BMP";

        private void btnOutputFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                exportFileName = saveFileDialog1.FileName + "." + fileExtension;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            Stream myStream;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "template files (*.mxt)|*.mxt|mxd files (*.mxd)|*.mxd";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    string fileName = openFileDialog1.FileName;
                    if (axPageLayoutControl1.CheckMxFile(fileName))
                    {
                        axPageLayoutControl1.LoadMxFile(fileName, "");
                    }
                    myStream.Close();
                }
            }
        }
        string strOutputType = "ExportBMP";
        IExport export = new ExportBMP() as IExport;

        private void cbxOutputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxOutputType.SelectedItem == null) return;
            strOutputType = cbxOutputType.SelectedItem.ToString();
            try
            {
                switch (strOutputType)
                {
                    case "ExportBMP":
                        export = new ExportBMP() as IExport;
                        fileExtension = "BMP";
                        break;
                    case "ExportGIF":  //ArcPressPrinter need ArcGIS Desktop License
                        export = new ExportGIF() as IExport;
                        fileExtension = "GIF";
                        break;
                    case "ExportJPEG":
                        export = new ExportJPEG() as IExport;
                        fileExtension = "JPG";
                        break;
                    case "ExportPNG":
                        export = new ExportPNG() as IExport;
                        fileExtension = "PNG";
                        break;
                    case "ExportTIFF":
                        export = new ExportTIFF() as IExport;
                        fileExtension = "TIFF";
                        break;
                    case "ExportAI":
                        export = new ExportAI() as IExport;
                        fileExtension = "AI";
                        break;
                    case "ExportEMF":
                        export = new ExportEMF() as IExport;
                        fileExtension = "EMF";
                        break;
                    case "ExportPDF":
                        export = new ExportPDF() as IExport;
                        fileExtension = "PDF";
                        break;
                    case "ExportPS":
                        export = new ExportPS() as IExport;
                        fileExtension = "PS";
                        break;
                    case "ExportSVG":
                        export = new ExportSVG() as IExport;
                        fileExtension = "SVG";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        IOutputRasterSettings rasterSettings;
        double iOutputResolution = 300;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (export is IOutputRasterSettings)
            {
                rasterSettings = export as IOutputRasterSettings;
                rasterSettings.ResampleRatio = 1;
            }
            if (Information.IsNumeric(txtResolution.Text))
                iOutputResolution = Convert.ToInt32(txtResolution.Text);
            else
                iOutputResolution = 300;
            IActiveView pActiveView = axPageLayoutControl1.ActiveView;
            double iScreenResolution = pActiveView.ScreenDisplay.
                DisplayTransformation.Resolution;
            tagRECT exportRECT;
            exportRECT.left = 0; exportRECT.top = 0;
            exportRECT.right = Convert.ToInt32(Math.Ceiling(pActiveView.ExportFrame.
                right * (iOutputResolution / iScreenResolution)));
            exportRECT.bottom = Convert.ToInt32(Math.Round(pActiveView.ExportFrame.
                bottom * (iOutputResolution / iScreenResolution)));
            IEnvelope pPixelBoundsEnv = new Envelope() as IEnvelope;
            pPixelBoundsEnv.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right
                , exportRECT.bottom);
            export.Resolution = iOutputResolution;
            export.PixelBounds = pPixelBoundsEnv;
            export.ExportFileName = exportFileName;
            int hDC = export.StartExporting();
            pActiveView.Output(hDC, (int)export.Resolution, ref exportRECT,
                null, null);
            export.FinishExporting();
            export.Cleanup();
            MessageBox.Show("Export Success");
        }
    }
}
