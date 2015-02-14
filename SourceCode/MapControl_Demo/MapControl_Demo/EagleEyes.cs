using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapControl_Demo
{
    public partial class EagleEyes : Form
    {
        IMapControl3 m_mapControl;
        IMap m_map;
        public EagleEyes(IHookHelper hook)
        {
            InitializeComponent();
            m_mapControl = (IMapControl3)hook.Hook;
            m_map = m_mapControl.Map;
        }

        private void axMapControl1_OnMouseMove(object sender, 
            IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button != 1)
                return;
            IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
            pPoint.PutCoords(e.mapX, e.mapY);
            m_mapControl.CenterAt(pPoint);
            m_mapControl.ActiveView.PartialRefresh(
                esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void EagleEyes_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= m_map.LayerCount - 1; i++)
            {
                axMapControl1.Map.AddLayer(m_map.get_Layer(i));
            }
            axMapControl1.ActiveView.Extent = axMapControl1.ActiveView.FullExtent;
        }

        private void axMapControl1_OnMouseDown(object sender, 
            IMapControlEvents2_OnMouseDownEvent e)
        {
            //Left Click to Move the rectangle
            if (e.button == 1)
            {
                IPoint point = new ESRI.ArcGIS.Geometry.Point();
                point.X = e.mapX;
                point.Y = e.mapY;
                axMapControl1.CenterAt(point);
                m_mapControl.CenterAt(point);

            }
            //Right Click to Draw the rectangle
            else if (e.button == 2)
            {
                IEnvelope pEnvelop = axMapControl1.TrackRectangle();
                m_mapControl.Extent = pEnvelop;
                m_mapControl.ActiveView.PartialRefresh(
                    esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

    }
}
