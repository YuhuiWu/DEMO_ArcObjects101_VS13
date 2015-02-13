using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
namespace MapControl_Demo
{
    public class ControlsSynchronizer
    {
        #region class members
        private IMapControl3 m_mapControl = null;
        private IPageLayoutControl2 m_pageLayoutControl = null;
        private ITool m_mapActiveTool = null;
        private ITool m_pageLayoutActiveTool = null;
        private bool m_IsMapCtrlactive = true;
        private ArrayList m_frameworkControls = null;
        #endregion

        #region constructor
       
        public ControlsSynchronizer()
        {
            m_frameworkControls = new ArrayList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="pageLayoutControl"></param>
        public ControlsSynchronizer(IMapControl3 mapControl, IPageLayoutControl2 pageLayoutControl)
            : this()
        {
            
            m_mapControl = mapControl;
            m_pageLayoutControl = pageLayoutControl;
        }
        #endregion

        #region properties
        /// <summary>
        /// 
        /// </summary>
        public IMapControl3 MapControl
        {
            get { return m_mapControl; }
            set { m_mapControl = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public IPageLayoutControl2 PageLayoutControl
        {
            get { return m_pageLayoutControl; }
            set { m_pageLayoutControl = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ActiveViewType
        {
            get
            {
                if (m_IsMapCtrlactive)
                    return "MapControl";
                else
                    return "PageLayoutControl";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public object ActiveControl
        {
            get
            {
                if (m_mapControl == null || m_pageLayoutControl == null)
                    throw new Exception("ControlsSynchronizer::ActiveControl:\r\n");
                if (m_IsMapCtrlactive)
                    return m_mapControl.Object;
                else
                    return m_pageLayoutControl.Object;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// when the active control changes, the class iterates through the array of the framework controls
        ///  and calles SetBuddyControl on each of the controls.
        /// </summary>
        /// <param name="buddy">the active control</param>
        private void SetBuddies(object buddy)
        {
            try
            {
                if (buddy == null)
                    throw new Exception("ControlsSynchronizer::SetBuddies:\r\nTarget Buddy Control is not initialized!");
                foreach (object obj in m_frameworkControls)
                {
                    if (obj is IToolbarControl)
                    {
                        ((IToolbarControl)obj).SetBuddyControl(buddy);
                    }
                    else if (obj is ITOCControl)
                    {
                        ((ITOCControl)obj).SetBuddyControl(buddy);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ControlsSynchronizer::SetBuddies:\r\n{0}", ex.Message));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ActivateMap()
        {
            try
            {
                if (m_pageLayoutControl == null || m_mapControl == null)
                    throw new Exception("ControlsSynchronizer::ActivateMap:\r\nEither MapControl or PageLayoutControl are not initialized!");
               
                if (m_pageLayoutControl.CurrentTool != null)
                    m_pageLayoutActiveTool = m_pageLayoutControl.CurrentTool;
              
                m_pageLayoutControl.ActiveView.Deactivate();
               
                m_mapControl.ActiveView.Activate(m_mapControl.hWnd);
                
                if (m_mapActiveTool != null)
                    m_mapControl.CurrentTool = m_mapActiveTool;
                m_IsMapCtrlactive = true;
              
                this.SetBuddies(m_mapControl.Object);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ControlsSynchronizer::ActivateMap:\r\n{0}", ex.Message));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ActivatePageLayout()
        {
            try
            {
                if (m_pageLayoutControl == null || m_mapControl == null)
                    throw new Exception("ControlsSynchronizer::ActivatePageLayout:\r\nEither MapControl or PageLayoutControl are not initialized!");
             
                if (m_mapControl.CurrentTool != null)
                    m_mapActiveTool = m_mapControl.CurrentTool;
            
                m_mapControl.ActiveView.Deactivate();
           
                m_pageLayoutControl.ActiveView.Activate(m_pageLayoutControl.hWnd);
                
                if (m_pageLayoutActiveTool != null)
                    m_pageLayoutControl.CurrentTool = m_pageLayoutActiveTool;
                m_IsMapCtrlactive = false;
               
                this.SetBuddies(m_pageLayoutControl.Object);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("ControlsSynchronizer::ActivatePageLayout:\r\n{0}", ex.Message));
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="newMap"></param>
        public void ReplaceMap(IMap newMap)
        {
            if (newMap == null)
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nNew map for replacement is not initialized!");
            if (m_pageLayoutControl == null || m_mapControl == null)
                throw new Exception("ControlsSynchronizer::ReplaceMap:\r\nEither MapControl or PageLayoutControl are not initialized!");
           
            IMaps maps = new Maps();
            maps.Add(newMap);
            bool bIsMapActive = m_IsMapCtrlactive;
           
            this.ActivatePageLayout();
            m_pageLayoutControl.PageLayout.ReplaceMaps(maps);
            m_mapControl.Map = newMap;
            
            m_pageLayoutActiveTool = null;
            m_mapActiveTool = null;
           
            if (bIsMapActive)
            {
                this.ActivateMap();
                m_mapControl.ActiveView.Refresh();
            }
            else
            {
                this.ActivatePageLayout();
                m_pageLayoutControl.ActiveView.Refresh();
            }
        }
        /// <summary>
        /// bind the MapControl and PageLayoutControl together by assigning a new joint focus map
        /// </summary>
        /// <param name="activateMapFirst">true if the MapControl supposed to be activated first</param>
        public void BindControls(bool activateMapFirst)
        {
            if (m_pageLayoutControl == null || m_mapControl == null)
                throw new Exception("ControlsSynchronizer::BindControls:\r\nEither MapControl or PageLayoutControl are not initialized!");
            IMap newMap = new MapClass();
            newMap.Name = "Map";
            IMaps maps = new Maps(); 
            maps.Add(newMap);
            m_pageLayoutControl.PageLayout.ReplaceMaps(maps); 
            m_mapControl.Map = newMap;  
            
            m_pageLayoutActiveTool = null;
            m_mapActiveTool = null;
            
            if (activateMapFirst)
                this.ActivateMap();
            else
                this.ActivatePageLayout();
        }
        /// <summary>
        /// bind the MapControl and PageLayoutControl together by assigning a new joint focus map
        /// </summary>
        /// <param name="mapControl"></param>
        /// <param name="pageLayoutControl"></param>
        /// <param name="activateMapFirst">true if the MapControl supposed to be activated first</param>
        public void BindControls(IMapControl3 mapControl, IPageLayoutControl2 pageLayoutControl, bool activateMapFirst)
        {
            if (mapControl == null || pageLayoutControl == null)
                throw new Exception("ControlsSynchronizer::BindControls:\r\nEither MapControl or PageLayoutControl are not initialized!");
            m_mapControl = MapControl;
            m_pageLayoutControl = pageLayoutControl;
            this.BindControls(activateMapFirst);
        }
        /// <summary>
        ///by passing the application's toolbars and TOC to the synchronization class, it saves you the
        ///management of the buddy control each time the active control changes. This method ads the framework
        ///control to an array; once the active control changes, the class iterates through the array and 
        ///calles SetBuddyControl on each of the stored framework control.
        /// </summary>
        /// <param name="control"></param>
        public void AddFrameworkControl(object control)
        {
            if (control == null)
                throw new Exception("ControlsSynchronizer::AddFrameworkControl:\r\nAdded control is not initialized!");
            m_frameworkControls.Add(control);
        }
        /// <summary>
        /// Remove a framework control from the managed list of controls
        /// </summary>
        /// <param name="control"></param>
        public void RemoveFrameworkControl(object control)
        {
            if (control == null)
                throw new Exception("ControlsSynchronizer::RemoveFrameworkControl:\r\nControl to be removed is not initialized!");
            m_frameworkControls.Remove(control);
        }
        /// <summary>
        /// Remove a framework control from the managed list of controls by specifying its index in the list
        /// </summary>
        /// <param name="index"></param>
        public void RemoveFrameworkControlAt(int index)
        {
            if (m_frameworkControls.Count < index)
                throw new Exception("ControlsSynchronizer::RemoveFrameworkControlAt:\r\nIndex is out of range!");
            m_frameworkControls.RemoveAt(index);
        }
        #endregion
    }
}
