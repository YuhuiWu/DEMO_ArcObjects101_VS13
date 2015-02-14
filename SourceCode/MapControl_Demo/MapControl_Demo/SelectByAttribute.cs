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
using ESRI.ArcGIS.Geodatabase;

namespace MapControl_Demo
{
    public partial class SelectByAttribute : Form
    {
        IHookHelper m_hookhelper;
        IMap m_map;
        IActiveView m_activeview;
        public SelectByAttribute(IHookHelper hookhelper)
        {
            InitializeComponent();
            m_hookhelper = hookhelper;
            m_map = hookhelper.FocusMap;
            m_activeview = m_hookhelper.ActiveView;
        }
        private IEnumLayer GetLayers()
        {
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}"; //IFeatureLayer
            IEnumLayer layers = m_map.get_Layers(uid, true);
            return layers;
        }

        private void SelectByAttribute_Load(object sender, EventArgs e)
        {
            IEnumLayer layers = GetLayers();
            layers.Reset();
            ILayer layer = layers.Next();
            while (layer != null)
            {
                comboBoxLayers.Items.Add(layer.Name.ToString());
                layer = layers.Next();
            }
            SetupEvents();
        }

        private void comboBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxFields.Items.Clear();
            listBoxValues.Items.Clear();
            string strSelectedLayerName = comboBoxLayers.Text;
            IFeatureLayer pFeatureLayer;
            try
            {
                for (int i = 0; i < m_map.LayerCount; i++)
                {
                    if (m_map.get_Layer(i).Name == strSelectedLayerName)
                    {
                        if (m_map.get_Layer(i) is IFeatureLayer)
                        {
                            pFeatureLayer = (IFeatureLayer)m_map.get_Layer(i);
                            for (int j = 0; j < pFeatureLayer.FeatureClass.Fields.FieldCount; j++)
                            {
                                listBoxFields.Items.Add(pFeatureLayer.FeatureClass.Fields.get_Field(j).Name);
                            }
                            labelDescription2.Text = strSelectedLayerName;
                        }
                        else
                        {
                            MessageBox.Show("This Layer could be queried! Please choose another");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private esriSelectionResultEnum selectmethod = esriSelectionResultEnum.esriSelectionResultNew;

        private void comboBoxMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxMethod.SelectedIndex)
            {
                case 0: selectmethod = esriSelectionResultEnum.esriSelectionResultNew; break;
                case 1: selectmethod = esriSelectionResultEnum.esriSelectionResultAdd; break;
                case 2: selectmethod = esriSelectionResultEnum.esriSelectionResultSubtract; break;
                case 3: selectmethod = esriSelectionResultEnum.esriSelectionResultAnd; break;
            }
        }
        private void clauseElementClicked(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = ((Button)sender).Text;
        }

        private void buttonBrace_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = "(  )";
            textBoxWhereClause.SelectionStart = textBoxWhereClause.Text.Length - 2;
        }

        private void listBoxFields_DoubleClick(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = listBoxFields.SelectedItem.ToString() + " ";
        }
        private int GetUniqueValuesCount(IFeatureClass featureClass, string strField)
        {
            ICursor cursor = featureClass.Search(null, false) as ICursor;
            IDataStatistics dataStatistics = new DataStatistics();
            dataStatistics.Field = strField;
            dataStatistics.Cursor = cursor;
            System.Collections.IEnumerator enumerator = dataStatistics.UniqueValues;
            return dataStatistics.UniqueValueCount;
        }
        private System.Collections.IEnumerator GetUniqueValues(
           IFeatureClass featureClass, string strField)
        {
            ICursor cursor = (ICursor)featureClass.Search(null, false);
            IDataStatistics dataStatistics = new DataStatistics();
            dataStatistics.Field = strField;
            dataStatistics.Cursor = cursor;
            System.Collections.IEnumerator enumerator = dataStatistics.UniqueValues;
            return enumerator;
        }
        private ILayer GetLayerByName(string strLayerName)
        {
            ILayer pLayer = null;
            for (int i = 0; i < m_map.LayerCount; i++)
            {
                pLayer = m_map.get_Layer(i);
                if (strLayerName == pLayer.Name) { break; }
            }
            return pLayer;
        }

        private void buttonGetValue_Click(object sender, EventArgs e)
        {
            if (listBoxFields.Text == "")
            {
                MessageBox.Show("Please choose a field"); return;
            }
            string strSelectedFieldName = listBoxFields.Text;
            listBoxValues.Items.Clear();
            valueCounts.Text = "";
            if (strSelectedFieldName == null) return;
            IFeatureClass pFeatureClass = ((IFeatureLayer)GetLayerByName
                (comboBoxLayers.Text)).FeatureClass;
            if (pFeatureClass == null) return;
            int fieldIndex = pFeatureClass.Fields.FindField(strSelectedFieldName);
            IField field = pFeatureClass.Fields.get_Field(fieldIndex);
            try
            {
                System.Collections.IEnumerator uniqueValues = GetUniqueValues
                    (pFeatureClass, strSelectedFieldName);
                if (uniqueValues == null) return;
                if ((field.Type == esriFieldType.esriFieldTypeDouble) ||
                    (field.Type == esriFieldType.esriFieldTypeInteger) ||
                    (field.Type == esriFieldType.esriFieldTypeSingle) ||
                    (field.Type == esriFieldType.esriFieldTypeSmallInteger))
                {
                    System.Collections.Generic.List<double> valuesList =
                        new System.Collections.Generic.List<double>();
                    while (uniqueValues.MoveNext())
                    {
                        valuesList.Add(double.Parse(uniqueValues.Current.ToString()));
                    }
                    valuesList.Sort();
                    foreach (object uniqueValue in valuesList)
                    {
                        listBoxValues.Items.Add(uniqueValue.ToString());
                    }
                }
                else
                {
                    System.Collections.Generic.List<object> valuesList =
                        new System.Collections.Generic.List<object>();
                    while (uniqueValues.MoveNext())
                    {
                        valuesList.Add(uniqueValues.Current);
                    }
                    valuesList.Sort();
                    foreach (object uniqueValue in valuesList)
                    {
                        listBoxValues.Items.Add(uniqueValue.ToString());
                    }
                }
                valueCounts.Text = GetUniqueValuesCount(pFeatureClass,
                    strSelectedFieldName).ToString() + " values";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxValues_DoubleClick(object sender, EventArgs e)
        {
            textBoxWhereClause.SelectedText = " " + listBoxValues.SelectedItem.ToString();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxWhereClause.Clear();
        }
        private IFeatureSelection pFeatureSelection = null;
        private int ExecuteAttributeSelect()
        {
            try
            {
                IQueryFilter pQueryFilter = new QueryFilter() as IQueryFilter;
                IFeatureLayer pFeatureLayer = null;
                pQueryFilter.WhereClause = textBoxWhereClause.Text;
                ILayer targetLayer = GetLayerByName(comboBoxLayers.Text);
                pFeatureLayer = (IFeatureLayer)targetLayer;
                pFeatureSelection = (IFeatureSelection)pFeatureLayer;
                pFeatureSelection.SelectFeatures(pQueryFilter, selectmethod, false);
                if (pFeatureSelection.SelectionSet.Count == 0)
                {
                    MessageBox.Show("Could not find");
                    return 0;
                }
                m_activeview.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                return pFeatureSelection.SelectionSet.Count;
            }
            catch
            {
                MessageBox.Show("Error may exist in query clause, please input again");
                return -1;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBoxWhereClause.Text == string.Empty)
            {
                MessageBox.Show("Please Build SQL query clause");
                return;
            }
            int result = ExecuteAttributeSelect();
            if (result == -1)
            {
                labelResult.Text = "Error!";
                return;
            }
            labelResult.Text = string.Format("Find {0} values", result);
        }

        private void SelectByAttribute_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pFeatureSelection != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureSelection);
            } 
        }
        IActiveViewEvents_Event activeViewEvent = null;
        IActiveViewEvents_SelectionChangedEventHandler mapSelectionChanged;
        private void SetupEvents()
        {
            activeViewEvent = m_activeview as IActiveViewEvents_Event;
            mapSelectionChanged = new
                IActiveViewEvents_SelectionChangedEventHandler(OnMapSelectionChanged);
            activeViewEvent.SelectionChanged += mapSelectionChanged;
        }

        private void OnMapSelectionChanged()
        {
            m_activeview.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection,
                null, m_activeview.Extent);
        }
    }
}
