using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace MapControl_Demo
{
    public sealed partial class MainForm : Form
    {
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        IMapDocument m_MapDocument = new MapDocumentClass();
        private ControlsSynchronizer m_controlsSynchronizer = null;
        private IPageLayoutControl3 m_pageLayoutControl = null;//Layout View
        private EagleEyes poverView;
        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            //get the MapControl
            m_mapControl = (IMapControl3)axMapControl1.Object;
            //disable the Save menu (since there is no document yet)
            menuSaveDoc.Enabled = false;
            m_pageLayoutControl = (IPageLayoutControl3)axPageLayoutControl1.Object;
            //Initialize controls synchronization calss
            m_controlsSynchronizer = new ControlsSynchronizer(m_mapControl, m_pageLayoutControl);
            //Bind MapControl and PageLayoutControl(Point to the same Map), Set MapControl as the active Control
            m_controlsSynchronizer.BindControls(true);
            //In order to switch MapControl and PageLayoutControl, we need to add Framework Control
            m_controlsSynchronizer.AddFrameworkControl(axToolbarControl1.Object);
            m_controlsSynchronizer.AddFrameworkControl(this.axTOCControl1.Object);
    
            OpenNewMapDocument openMapDoc = new OpenNewMapDocument(m_controlsSynchronizer);
            axToolbarControl1.AddItem(openMapDoc, -1, 0, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            DialogResult res = MessageBox.Show("Save the current map?", "Message",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                //Save As Command
                ICommand command = new ControlsSaveAsDocCommandClass();
                if (m_mapControl != null)
                    command.OnCreate(m_controlsSynchronizer.MapControl.Object);
                else
                    command.OnCreate(m_controlsSynchronizer.PageLayoutControl.Object);
                command.OnClick();
            }
            //create a new map instance
            IMap map = new MapClass();
            map.Name = "Unnamed Map";
            m_controlsSynchronizer.MapControl.DocumentFilename = string.Empty;

            m_controlsSynchronizer.ReplaceMap(map);
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            if (this.axMapControl1.LayerCount > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to save the current map?", "Warning",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes)
                    this.menuSaveDoc_Click(null, null);

            }
            OpenNewMapDocument openMapDoc = new OpenNewMapDocument(m_controlsSynchronizer);
            openMapDoc.OnCreate(m_controlsSynchronizer.MapControl.Object);
            openMapDoc.OnClick();
            menuSaveDoc.Enabled = true;
        }
        private void SaveDocument()
        {
            if (m_MapDocument.get_IsReadOnly(m_MapDocument.DocumentFilename) == true)
            {
                MessageBox.Show("ReadOnly Map, fail to save");
            }
            try
            {
                m_MapDocument.Save(m_MapDocument.UsesRelativePaths, true);
                MessageBox.Show("Save map document successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail to save map document" + ex.ToString());
            }
        }
        private void menuSaveDoc_Click(object sender, EventArgs e)
        {
            if (null != m_pageLayoutControl.DocumentFilename && 
                m_mapControl.CheckMxFile(m_pageLayoutControl.DocumentFilename))
            {
                IMapDocument mapDoc = new MapDocumentClass();
                mapDoc.Open(m_pageLayoutControl.DocumentFilename, string.Empty);
                mapDoc.ReplaceContents((IMxdContents)m_pageLayoutControl.PageLayout);
                mapDoc.Save(mapDoc.UsesRelativePaths, false);
                mapDoc.Close();
            }
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            if (m_controlsSynchronizer.ActiveControl is IMapControl3)
            {
                if (MessageBox.Show("Save as map document will lose the setting of layout\r\nContinue?",
                    "Message", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion

        //listen to MapReplaced evant in order to update the statusbar and the Save menu
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                menuSaveDoc.Enabled = false;
                statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                menuSaveDoc.Enabled = true;
                statusBarXY.Text = System.IO.Path.GetFileName(m_mapDocumentName);
            }
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            statusBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                axToolbarControl1.SetBuddyControl(axMapControl1);
                m_controlsSynchronizer.ActivateMap();
            }
            else
            {
                axToolbarControl1.SetBuddyControl(axPageLayoutControl1);
                m_controlsSynchronizer.ActivatePageLayout();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IHookHelper hookHelper = new HookHelperClass();
            //hookHelper.Hook = axMapControl1.Object;
            hookHelper.Hook = m_controlsSynchronizer.MapControl.Object;
            poverView = new EagleEyes(hookHelper);
            poverView.Show();
        }

        private void axMapControl1_OnExtentUpdated(object sender, 
            IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            IEnvelope pEnv = e.newEnvelope as IEnvelope;
            IGraphicsContainer pGraphicsContainer = poverView.axMapControl1.Map
                as IGraphicsContainer;
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            pGraphicsContainer.DeleteAllElements();
            IRectangleElement pRectangleEle = new RectangleElementClass();
            IElement pEle = pRectangleEle as IElement;
            pEle.Geometry = pEnv;
            IRgbColor pColor = new RgbColorClass();
            pColor.RGB = 255;
            pColor.Transparency = 255;
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 1;
            pOutline.Color = pColor;
            pColor = new RgbColorClass();
            pColor.RGB = 255;
            pColor.Transparency = 0;
            IFillSymbol pFillSymbol;
            pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            IFillShapeElement pFillShapeEle = pEle as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;
            pEle = pFillShapeEle as IElement;
            pGraphicsContainer.AddElement(pEle, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

       
    }
}