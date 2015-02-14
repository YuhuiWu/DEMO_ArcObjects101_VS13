using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using Microsoft.VisualBasic;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
namespace MapControl_Demo
{
    public partial class BufferAnalysis : Form
    {
        IHookHelper m_hookHelper = null;
        IActiveView m_activeView = null;
        IMap m_map = null;
        public BufferAnalysis(IHookHelper hookHelper)
        {
            InitializeComponent();
            if (hookHelper == null) return;
            m_hookHelper = hookHelper;
            m_activeView = m_hookHelper.ActiveView;
            m_map = m_hookHelper.FocusMap;
        }
        private IEnumLayer GetLayers()
        {
            UID uid = new UIDClass();
            // IFeatureLayer
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
            if (m_map.LayerCount != 0)
            {
                IEnumLayer layers = m_map.get_Layers(uid, true);
                return layers;
            }
            return null;
        }
        private void initialize()
        {
            if (GetLayers() == null) return;
            IEnumLayer layers = GetLayers();
            layers.Reset();
            ILayer layer = layers.Next();
            while (layer != null)
            {
                if (layer is IFeatureLayer)
                {
                    chklstOverlayLayers.Items.Add(layer.Name, true);
                    cboBufferLayer.Items.Add(layer.Name);
                }
                layer = layers.Next();
            }
            cboBufferLayer.SelectedIndex = 0;
            cboBufferField.Enabled = false;
            txtBufferDistance.Enabled = true;
            cboSideType.Enabled = false;
            cboEndType.Enabled = false;
            chklstFields.Visible = false;
        }

        private void BufferAnalysis_Load(object sender, EventArgs e)
        {
            initialize();
        }
        private IFeatureLayer GetFeatureLayer(string layerName)
        {
            if (GetLayers() == null) return null;
            IEnumLayer layers = GetLayers();
            layers.Reset();
            ILayer layer = null;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name == layerName)
                    return layer as IFeatureLayer;
            }
            return null;
        }
        string strBufferLayer;

        private void cboBufferLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBufferLayer.SelectedItem != null)
            {
                strBufferLayer = cboBufferLayer.SelectedItem.ToString();
                IFeatureLayer featureLayer = GetFeatureLayer(strBufferLayer);
                if (featureLayer == null) return;
                if (featureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    cboSideType.Enabled = true;
                    cboEndType.Enabled = true;
                }
                else
                {
                    cboSideType.Enabled = false;
                    cboEndType.Enabled = false;
                }
            }
        }
        private void CboBufferFieldAdditems(IFeatureLayer featureLayer)
        {
            IFields fields = featureLayer.FeatureClass.Fields;
            IField field = null;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                field = fields.get_Field(i);
                if (field.Type == esriFieldType.esriFieldTypeDouble
                    || field.Type == esriFieldType.esriFieldTypeInteger
                    || field.Type == esriFieldType.esriFieldTypeSingle
                    || field.Type == esriFieldType.esriFieldTypeSmallInteger)
                    cboBufferField.Items.Add(field.Name);
            }
        }

        private void rdoBufferDistance_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBufferDistance.Checked)
                txtBufferDistance.Enabled = true;
            else
                txtBufferDistance.Enabled = false;
        }
        IFeatureLayer featureBufferLayer;

        private void rdoBufferField_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBufferField.Checked)
            {
                cboBufferField.Enabled = true;
                if (strBufferLayer != "")
                {
                    if (GetFeatureLayer(strBufferLayer) == null) return;
                    featureBufferLayer = GetFeatureLayer(strBufferLayer);
                    CboBufferFieldAdditems(featureBufferLayer);
                }
            }
            else
                cboBufferField.Enabled = false;
        }
        double bufferDistance = 10; 
        object bufferDistanceField;

        private void txtBufferDistance_Leave(object sender, EventArgs e)
        {
            if (rdoBufferDistance.Checked)
            {
                if (Information.IsNumeric(txtBufferDistance.Text))
                {
                    bufferDistance = Convert.ToDouble(txtBufferDistance.Text);
                    bufferDistanceField = bufferDistance;
                }
            }
        }
        string strBufferField;

        private void cboBufferField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoBufferField.Checked)
            {
                if (cboBufferField.SelectedItem != null)
                {
                    strBufferField = cboBufferField.SelectedItem.ToString();
                    bufferDistanceField = strBufferField;
                }
            }
        }
        string strSideType;

        private void cboSideType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSideType;
            if (cboSideType.SelectedItem != null)
            {
                selectedSideType = cboSideType.SelectedItem.ToString();
                switch (selectedSideType)
                {
                    case "Both Side":
                        strSideType = "FULL";
                        break;
                    case "Left Side":
                        strSideType = "LEFT";
                        break;
                    case "Right Side":
                        strSideType = "RIGHT";
                        break;
                    default:
                        break;
                }
            }
        }
        string strEndType;

        private void cboEndType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEndType;
            if (cboEndType.SelectedItem != null)
            {
                selectedEndType = cboEndType.SelectedItem.ToString();
                switch (selectedEndType)
                {
                    case "Round":
                        strEndType = "ROUND";
                        break;
                    case "Flat":
                        strEndType = "FLAT";
                        break;
                    default:
                        break;
                }
            }
        }
        private void ChklstFieldsAddItems(IFeatureLayer featureLayer)
        {
            if (featureLayer == null)
            {
                featureLayer = GetFeatureLayer(strBufferLayer);
            }
            if (featureLayer == null) return;
            IFields fields = featureLayer.FeatureClass.Fields;
            IField field = null;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                field = fields.get_Field(i);
                chklstFields.Items.Add(field.Name);
            }
            chklstFields.Refresh();
        }
        string strDissolveType;

        private void cboDissolveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectDissolveTyppe;
            if (cboDissolveType.SelectedItem != null)
            {
                selectDissolveTyppe = cboDissolveType.SelectedItem.ToString();
                switch (selectDissolveTyppe)
                {
                    case "Not Dissolve":
                        strDissolveType = "NONE";
                        chklstFields.Enabled = false;
                        break;
                    case "Dissolve All Buffers":
                        strDissolveType = "ALL";
                        chklstFields.Enabled = false;
                        break;
                    case "Dissolve by Fields":
                        strDissolveType = "LIST";
                        chklstFields.Visible = true;
                        chklstFields.Enabled = true;
                        ChklstFieldsAddItems(featureBufferLayer);
                        break;
                    default:
                        break;
                }
            }
        }
        string strDissolveFields;

        private void chklstFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (object itemChecked in chklstFields.CheckedItems)
            {
                strDissolveFields += itemChecked + ";";
            }
        }
        string strOutputPath = System.IO.Path.GetTempPath();

        private void btnOutpuPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                strOutputPath = folderBrowserDialog1.SelectedPath;
            }
        }
        private bool IsDouble(string s)
        {
            try
            {
                Double.Parse(s);
            }
            catch
            {
                return false;
            }
            return true;
        }
        double tolerance = 0.1;

        private void txtTolerance_Leave(object sender, EventArgs e)
        {
            if (IsDouble(txtTolerance.Text))
                tolerance = Convert.ToDouble(txtTolerance.Text);
        }
        int overlayLevel = 1;
        private void numUpDownOverlayLevel_ValueChanged(object sender, EventArgs e)
        {
            overlayLevel = (int)numUpDownOverlayLevel.Value;
        }
        int inputLevel = 1;
        private void numUpDownInputLevel_ValueChanged(object sender, EventArgs e)
        {
            inputLevel = (int)numUpDownInputLevel.Value;
        }

        string strJoinAttributeType = "ALL";
        private void cboAttributeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string attributeType = cboAttributeType.SelectedItem.ToString();
            switch (attributeType)
            {
                case "All Attributes":
                    strJoinAttributeType = "ALL";
                    break;
                case "Not Include FID":
                    strJoinAttributeType = "NO_FID";
                    break;
                case "Include only FID":
                    strJoinAttributeType = "ONLY_FID";
                    break;
                default:
                    break;
            }
        }
        string strOutputFeatureType = "INPUT";
        private void cboFeatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string featureType = cboFeatureType.SelectedItem.ToString();
            switch (featureType)
            {
                case "By Input features":
                    strOutputFeatureType = "INPUT";
                    break;
                case "Line":
                    strOutputFeatureType = "LINE";
                    break;
                case "Point":
                    strOutputFeatureType = "POINT";
                    break;
                default:
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ScrollToBottom(TextBox txtBox)
        {
            txtBox.SelectionStart = txtBox.Text.Length;
            txtBox.SelectionLength = 0;
            txtBox.ScrollToCaret();
        }
        private string ReturnMessages(Geoprocessor gp)
        {
            StringBuilder sb = new StringBuilder();
            if (gp.MessageCount > 0)
            {
                for (int Count = 0; Count <= gp.MessageCount - 1; Count++)
                {
                    System.Diagnostics.Trace.WriteLine(gp.GetMessage(Count));
                    sb.AppendFormat("{0}\n", gp.GetMessage(Count));
                }
            }
            return sb.ToString();
        }
        string bufferedFeatureClassName;  
        private IGeoProcessorResult CreateBuffer(Geoprocessor gp)
        {
            txtMessages.Text += "Creating Buffer: " + "\r\n";
            txtMessages.Update();
            ESRI.ArcGIS.AnalysisTools.Buffer buffer = new ESRI.ArcGIS.AnalysisTools.Buffer();
            IFeatureLayer bufferLayer = GetFeatureLayer(strBufferLayer);
            buffer.in_features = bufferLayer;
            bufferedFeatureClassName = strBufferLayer + "_" + "Buffer";
            string outputFullPath = System.IO.Path.Combine(strOutputPath, bufferedFeatureClassName);
            buffer.out_feature_class = outputFullPath;
            buffer.buffer_distance_or_field = bufferDistanceField;
            buffer.line_side = strSideType;
            buffer.line_end_type = strEndType;
            buffer.dissolve_option = strDissolveType;
            buffer.dissolve_field = strDissolveFields;
            IGeoProcessorResult results = (IGeoProcessorResult)gp.Execute(buffer, null);
            buffer = null;
            txtMessages.Text += ReturnMessages(gp);
            txtMessages.Text += "Buffer Created! " + "\r\n";
            ScrollToBottom(txtMessages);
            txtMessages.Update();
            return results;
        }
        private IGeoProcessorResult BufferOverlayAnalysisOneLayer
            (string layerName, Geoprocessor gp)
        {
            txtMessages.Text += "Input Layer: " + layerName + "\r\n";
            txtMessages.Text += "Overlay Layer:" + bufferedFeatureClassName + "\r\n";
            txtMessages.Text += "May cost long time, please wait ... " + "\r\n";
            IGpValueTableObject vtobject = new GpValueTableObject()
                as IGpValueTableObject;
            vtobject.SetColumns(1);
            object row1 = "";
            row1 = GetFeatureLayer(layerName);
            vtobject.AddRow(ref row1);
            object row2 = "";
            string outputFullOverlay = System.IO.Path.Combine(strOutputPath,
                bufferedFeatureClassName);
            row2 = outputFullOverlay + ".shp";
            vtobject.AddRow(ref row2);
            IVariantArray pVarArray = new VarArrayClass();
            pVarArray.Add(vtobject);
            string outputFullPath = System.IO.Path.Combine(
                strOutputPath, layerName + "_" + "BufferOverlay.shp");
            pVarArray.Add(outputFullPath);
            pVarArray.Add(strJoinAttributeType);
            pVarArray.Add(tolerance);
            pVarArray.Add(strOutputFeatureType);
            IGeoProcessorResult results = gp.Execute("Intersect_analysis",
                pVarArray, null) as IGeoProcessorResult;
            txtMessages.Text += layerName + "Layer and" + bufferedFeatureClassName
                + "Layer has been overlaid!" + "\r\n";
            return results;
        }
        private void BufferOverlayAnalysis(Geoprocessor gp)
        {
            foreach (object itemChecked in chklstOverlayLayers.CheckedItems)
            {
                BufferOverlayAnalysisOneLayer(itemChecked.ToString(), gp);
                ScrollToBottom(txtMessages);
            }
        }

        private void btnBufferAnalysis_Click(object sender, EventArgs e)
        {
            txtMessages.Text += "Begin Buffer Analysis,Please wait... " + "\r\n";
            txtMessages.Text += DateTime.Now.ToString() + "\r\n";
            txtMessages.Update();
            Geoprocessor gp = new Geoprocessor();
            gp.OverwriteOutput = true;
            gp.AddOutputsToMap = true;
            IGeoProcessorResult results = CreateBuffer(gp);
            if ((results != null) && (results.Status == esriJobStatus.esriJobSucceeded))
            {
                BufferOverlayAnalysis(gp);
                txtMessages.Text += "Buffer Analysis Finished!" + "\r\n";
                txtMessages.Text += DateAndTime.Now.ToString() + "\r\n";
                ScrollToBottom(txtMessages);
                txtMessages.Update();
            }
            gp = null;
        }
    }
}
