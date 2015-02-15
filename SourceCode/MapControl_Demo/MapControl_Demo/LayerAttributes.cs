using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
namespace MapControl_Demo
{
    public partial class LayerAttributes : Form
    {
        #region
        IMap m_map = null;
        IActiveView m_activeView = null;
        IFeatureLayer currentLayer = null;
        string strOBJECTID = null;
        const string m_dataSetName = "m_layerDataSet";
        DataSet m_LayerDataSet = new DataSet(m_dataSetName);
        IActiveViewEvents_Event activeViewEvent = null;
        IActiveViewEvents_SelectionChangedEventHandler mapSelectionChanged;
        #endregion
        
        public LayerAttributes(IMap map, IFeatureLayer featureLayer)
        {
            InitializeComponent();
            m_map = map;
            m_activeView = map as IActiveView;
            currentLayer = featureLayer;
        }
        private void FindOIDField()
        {
            if (currentLayer == null)
                return;
            IFields fields = currentLayer.FeatureClass.Fields;
            IField field;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                field = fields.get_Field(i);
                if (field.Type == esriFieldType.esriFieldTypeOID)
                {
                    strOBJECTID = field.Name;
                    break;
                }
            }
        }
        private int ConstructDataSet(IFeatureLayer pFeatureLayer)
        {
            ILayerFields pFeatureLayerFields = pFeatureLayer as ILayerFields;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            int rows = 0;
            if (m_LayerDataSet.Tables[pFeatureLayer.Name] == null)
            {
                DataTable pTable = new DataTable(pFeatureLayer.Name);
                DataColumn pTableColumn;
                for (int i = 0; i < pFeatureLayerFields.FieldCount; i++)
                {
                    pTableColumn = new DataColumn(pFeatureLayerFields.get_Field(i).AliasName);
                    pTable.Columns.Add(pTableColumn);
                    pTableColumn = null;
                }
                IFeatureCursor features = pFeatureLayer.Search(null, false);
                IFeature feature = features.NextFeature();
                while (feature != null)
                {
                    DataRow pTableRow = pTable.NewRow();
                    for (int i = 0; i < pFeatureLayerFields.FieldCount; i++)
                    {
                        if (pFeatureLayerFields.FindField(pFeatureClass.ShapeFieldName) == i)
                        {
                            pTableRow[i] = pFeatureClass.ShapeType;
                        }
                        else
                        {
                            pTableRow[i] = feature.get_Value(i);
                        }
                    }
                    pTable.Rows.Add(pTableRow);
                    feature = features.NextFeature();
                }
                rows = pTable.Rows.Count;
                m_LayerDataSet.Tables.Add(pTable);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(features);
            }
            return rows;
        }

        private void LayerAttributes_Load(object sender, EventArgs e)
        {
            this.Text = currentLayer.Name + "  Feature DataSet";
            FindOIDField();
            if (strOBJECTID == null) return;
            int rowCount = ConstructDataSet(currentLayer);
            dataGridView1.DataSource = m_LayerDataSet;
            dataGridView1.DataMember = currentLayer.Name;
            toolStripStatusLabel1.Text = "Selected Items: 0";
            toolStripStatusLabel2.Text = "Total Items: " +
                currentLayer.FeatureClass.FeatureCount(null).ToString();
            OnFeatureLayerSelectionChanged();
            SetupEvents();
        }
        private void SelectFeatures(List<string> oidList)
        {
            IFeatureClass featureClass = currentLayer.FeatureClass;
            string strID = string.Empty;
            string[] IDs = oidList.ToArray();
            for (int i = 0; i < IDs.Length; i++)
            {
                strID = IDs[i];
                IFeature selectedFeature = featureClass.GetFeature(Convert.ToInt32(strID));
                m_map.SelectFeature(currentLayer, selectedFeature);
            }
            m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, m_activeView.Extent);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            m_map.ClearSelection();
            m_activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, 
                null, m_activeView.Extent);
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
            if (selectedRows == null) return;
            string strOID = string.Empty;
            List<string> OIDList = new List<string>();
            for (int i = 0; i < selectedRows.Count; i++)
            {
                DataGridViewRow row = selectedRows[i];
                strOID = row.Cells[strOBJECTID].Value.ToString();
                OIDList.Add(strOID);
            }
            SelectFeatures(OIDList);
            toolStripStatusLabel1.Text = "Selected Items：" + OIDList.Count.ToString();
            toolStripStatusLabel2.Text = "Total Items：" + 
                currentLayer.FeatureClass.FeatureCount(null).ToString();
        }
        private ISelectionSet GetSelectedFeature()
        {
            if (currentLayer == null) return null;

            IFeatureSelection featureSelection = currentLayer as IFeatureSelection;
            ISelectionSet selectionSet = featureSelection.SelectionSet;

            return selectionSet;
        }
        private void UpdateDataGridView(ISelectionSet selectedFeatures)
        {
            IEnumIDs enumIDs = selectedFeatures.IDs;
            int iD = enumIDs.Next();
            while (iD != -1) //-1 is reutned after the last valid ID has been reached        
            {
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == iD.ToString())
                        dataGridView1.Rows[i].Selected = true;
                }

                iD = enumIDs.Next();
            }
        }
        public void OnFeatureLayerSelectionChanged()
        {
            ISelectionSet selectedFeatures = GetSelectedFeature();
            UpdateDataGridView(selectedFeatures);
        }
        private void SetupEvents()
        {
            activeViewEvent = m_activeView as IActiveViewEvents_Event;
            mapSelectionChanged = new 
                IActiveViewEvents_SelectionChangedEventHandler(OnFeatureLayerSelectionChanged);
            activeViewEvent.SelectionChanged += mapSelectionChanged;
        }
    }
}
