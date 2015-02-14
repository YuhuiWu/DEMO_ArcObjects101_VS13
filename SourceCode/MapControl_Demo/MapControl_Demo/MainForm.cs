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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
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
        IToolbarMenu m_TocLayerMenu = new ToolbarMenuClass();
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
            IHookHelper hookHelper = new HookHelperClass();
            //hookHelper.Hook = axMapControl1.Object;
            hookHelper.Hook = m_controlsSynchronizer.MapControl.Object;
            poverView = new EagleEyes(hookHelper);
            OpenNewMapDocument openMapDoc = new OpenNewMapDocument(m_controlsSynchronizer);
            axToolbarControl1.AddItem(openMapDoc, -1, 0, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
            m_TocLayerMenu.AddItem(new OpenAttributeTableCmd(), 0, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_TocLayerMenu.SetHook(axMapControl1);

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

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            IBasicMap map = new MapClass();
            ILayer layer = new FeatureLayerClass();
            object other = new object();
            object index = new object();
            esriTOCControlItem item = new esriTOCControlItem();
            axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
            if (e.button == 2)
            {
                if (layer is IFeatureLayer)
                {
                    m_controlsSynchronizer.MapControl.CustomProperty = layer;
                    m_TocLayerMenu.PopupMenu(e.x, e.y, axTOCControl1.hWnd);
                }
            }
            if (e.button == 1)
            {
                if (layer == null)
                    return;
                IFeatureLayer featurelayer = layer as IFeatureLayer;
                if (featurelayer == null)
                    return;
                IGeoFeatureLayer geoFeatureLayer = (IGeoFeatureLayer)featurelayer;
                ILegendClass legendClass = new LegendClassClass();
                ISymbol symbol = null;
                if (other is ILegendGroup && (int)index != -1)
                {
                    legendClass = ((ILegendGroup)other).get_Class((int)index);
                    symbol = legendClass.Symbol;
                }
                if (symbol == null)
                    return;
                symbol = GetSymbolByControl(symbol);
                if (symbol == null)
                    return;
                legendClass.Symbol = symbol;
                this.Activate();
                //((IActiveView)m_MapDocument.get_Map(0)).ContentsChanged();

                axMapControl1.ActiveView.ContentsChanged();
                axMapControl1.Refresh(esriViewDrawPhase.esriViewGeography, null, null);
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
        }

        private void method1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = 
                new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "CAD (*.dwg)|*.dwg";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                if (filename != "")
                {
                    //axMapControl1.ClearLayers();
                    IWorkspaceFactory pCadWorkspaceFactory = 
                        new CadWorkspaceFactory();
                    IWorkspace pWorkspace = pCadWorkspaceFactory.OpenFromFile(
                        System.IO.Path.GetDirectoryName(filename), 0);
                    ICadDrawingWorkspace pCadDrawingWorkspace = 
                        (ICadDrawingWorkspace)pWorkspace;
                    ICadDrawingDataset pCadDataset = 
                        pCadDrawingWorkspace.OpenCadDrawingDataset(
                        System.IO.Path.GetFileName(filename));
                    ICadLayer pCadLayer = new CadLayerClass();
                    pCadLayer.CadDrawingDataset = pCadDataset;
                    axMapControl1.AddLayer(pCadLayer, 0);
                }
            }
        }

        private void method2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = 
                new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Filter = "CAD (*.dwg)|*.dwg";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                if (filename != "")
                {
                    //axMapControl1.ClearLayers();
                    IWorkspaceFactory pCadWorkspaceFactory = new CadWorkspaceFactory();
                    IFeatureWorkspace pWorkspace = pCadWorkspaceFactory.OpenFromFile(
                        System.IO.Path.GetDirectoryName(filename), 0) as IFeatureWorkspace;
                    IFeatureDataset pFeatureDataset = pWorkspace.OpenFeatureDataset(
                        System.IO.Path.GetFileName(filename));
                    IFeatureClassContainer pFeatureClassContainer = 
                        pFeatureDataset as IFeatureClassContainer;
                    IFeatureClass pFeatureClass;
                    IFeatureLayer pFeatureLayer;
                    for (int i = 0; i < pFeatureClassContainer.ClassCount; i++)
                    {
                        pFeatureClass = pFeatureClassContainer.get_Class(i);
                        if (pFeatureClass.FeatureType == 
                            esriFeatureType.esriFTCoverageAnnotation)
                            pFeatureLayer = new CadAnnotationLayerClass();
                        else
                            pFeatureLayer = new FeatureLayerClass();
                        pFeatureLayer.Name = pFeatureClass.AliasName;
                        pFeatureLayer.FeatureClass = pFeatureClass;
                        axMapControl1.AddLayer(pFeatureLayer, 0);
                    }
                }
            }
        }

        private void addMapGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IActiveView pActiveView = m_controlsSynchronizer.PageLayoutControl.PageLayout 
                as IActiveView;
            CreateGrid(pActiveView, axPageLayoutControl1.PageLayout);
        }
        public void CreateGrid(IActiveView activeView, IPageLayout pageLayout)
        {
            IMapGrid mapGrid = new GraticuleClass();
            mapGrid.Name = "Map Grid";
            IColor color = new RgbColorClass(); color.RGB = 0XBBBBBB;

            ICartographicLineSymbol cartographicLineSymbol = new CartographicLineSymbolClass();
            cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
            cartographicLineSymbol.Color = color; cartographicLineSymbol.Width = 2;
            mapGrid.LineSymbol = (ILineSymbol)cartographicLineSymbol;
            mapGrid.Border = null;
            mapGrid.TickLength = 15;

            cartographicLineSymbol = new CartographicLineSymbolClass();
            cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
            cartographicLineSymbol.Color = color; cartographicLineSymbol.Width = 1;
            mapGrid.TickLineSymbol = (ILineSymbol)cartographicLineSymbol;
            mapGrid.TickMarkSymbol = null; mapGrid.SubTickCount = 5; mapGrid.SubTickLength = 10;

            cartographicLineSymbol = new CartographicLineSymbolClass();
            cartographicLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
            cartographicLineSymbol.Color = color; cartographicLineSymbol.Width = 0.2;
            mapGrid.SubTickLineSymbol = (ILineSymbol)cartographicLineSymbol;

            IGridLabel gridLabel = mapGrid.LabelFormat;
            gridLabel.LabelOffset = 15;
            mapGrid.SetTickVisibility(true, true, true, true);
            mapGrid.SetSubTickVisibility(true, true, true, true);
            mapGrid.SetLabelVisibility(true, true, true, true);
            mapGrid.Visible = true;
            IMeasuredGrid measuredGrid = mapGrid as IMeasuredGrid;
            measuredGrid.FixedOrigin = true;
            measuredGrid.XIntervalSize = 10;
            measuredGrid.XOrigin = 5; //Shift grid 5
            measuredGrid.YIntervalSize = 10; //Parallel interval.
            measuredGrid.YOrigin = 5; //Shift grid 5

            IMap map = activeView.FocusMap;
            IGraphicsContainer graphicsContainer = pageLayout as IGraphicsContainer;
            IFrameElement frameElement = graphicsContainer.FindFrame(map);
            IMapFrame mapFrame = frameElement as IMapFrame;
            IMapGrids mapGrids = null;
            mapGrids = mapFrame as IMapGrids;
            mapGrids.AddMapGrid(mapGrid);

            activeView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
        }

        private void insertLegendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGraphicsContainer graphicsContainer =
                m_controlsSynchronizer.PageLayoutControl.GraphicsContainer;
            IMapFrame mapFrame = (IMapFrame)graphicsContainer.FindFrame
                (m_controlsSynchronizer.PageLayoutControl.ActiveView.FocusMap);
            if (mapFrame == null) return;
            UID uID = new UIDClass(); 
            uID.Value = "esriCarto.Legend";
            IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(uID, null);
            if (mapSurroundFrame == null) return;
            if (mapSurroundFrame.MapSurround == null) return;
            mapSurroundFrame.MapSurround.Name = "Legend";
            IEnvelope envelope = new Envelope() as IEnvelope;
            envelope.PutCoords(1, 1, 3.4, 2.4);
            IElement element = (IElement)mapSurroundFrame;
            element.Geometry = envelope;
            m_controlsSynchronizer.PageLayoutControl.AddElement(element,
                Type.Missing, Type.Missing, "Legend", 0);
            m_controlsSynchronizer.PageLayoutControl.ActiveView.
                PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void deleteLegendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGraphicsContainer graphicsContainer = m_controlsSynchronizer.
                PageLayoutControl.GraphicsContainer;
            IElement element = m_controlsSynchronizer.PageLayoutControl.
                FindElementByName("Legend", 1);

            if (element != null)
            {
                graphicsContainer.DeleteElement(element);
                m_controlsSynchronizer.PageLayoutControl.ActiveView.
                    PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            }
        }
        private ISymbol GetSymbolByControl(ISymbol symbolType)
        {
            ISymbol symbol = null;
            IStyleGalleryItem styleGalleryItem = null;
            esriSymbologyStyleClass styleClass = esriSymbologyStyleClass.
                esriStyleClassMarkerSymbols;
            if (symbolType is IMarkerSymbol)
            {
                styleClass = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
            }
            if (symbolType is ILineSymbol)
            {
                styleClass = esriSymbologyStyleClass.esriStyleClassLineSymbols;
            }
            if (symbolType is IFillSymbol)
            {
                styleClass = esriSymbologyStyleClass.esriStyleClassFillSymbols;
            }

            GetSymbol symbolForm = new GetSymbol(styleClass);
            symbolForm.ShowDialog();
            styleGalleryItem = symbolForm.m_styleGalleryItem;
            if (styleGalleryItem == null)
                return null;
            symbol = styleGalleryItem.Item as ISymbol;
            symbolForm.Dispose();
            this.Activate();
            return symbol;
        }

        private void addNorthArrowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand addNorthArrow = new AddNortharrow();
            addNorthArrow.OnCreate(axPageLayoutControl1.Object);
            axPageLayoutControl1.CurrentTool = addNorthArrow as ITool;
        }

        private void scaleBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand addScaleBar = new AddScaleBar();
            addScaleBar.OnCreate(axPageLayoutControl1.Object);
            axPageLayoutControl1.CurrentTool = addScaleBar as ITool;
        }

        private void exportAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand export = new ExportView();
            export.OnCreate(axPageLayoutControl1.Object);
            export.OnClick();
            axPageLayoutControl1.CurrentTool = export as ITool;
        }

        private void queryByAttributeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new AttributeQueryCmd();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void tableSortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new TableSortCmd();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void bufferAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new BufferAnalysisCmd();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void overlayAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new OverlayAnalysisCmd();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void gradualColorSymbolizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new GraduatedColorSymbolsCmd();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
        }
    }
}