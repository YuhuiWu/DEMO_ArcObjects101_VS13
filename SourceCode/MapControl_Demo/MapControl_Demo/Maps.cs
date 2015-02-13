using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
namespace MapControl_Demo
{
    [Guid("34bede2d-6968-4786-be93-c6dfc9fc7a23")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("MapControl_Demo.Maps")]
    public class Maps:IMaps,IDisposable
    {
        #region class members
        private ArrayList m_array = null;
        #endregion
        #region constructor
        public Maps()
        {
            m_array = new ArrayList();
        }
        #endregion
        #region IDisposable
        public void Dispose()
        {
            if (m_array != null)
            {
                m_array.Clear();
                m_array = null;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Create a new Map, add it to the collection and return it to the caller
        /// </summary>
        /// <returns></returns>
        public IMap Create()
        {
            IMap newMap = new MapClass();
            m_array.Add(newMap);
            return newMap;
        }
        /// <summary>
        /// Add the given Map to the collection
        /// </summary>
        /// <param name="Map"></param>
        public void Add(IMap Map)
        {
            if (Map == null)
                throw new Exception("Maps::Add:\r\nNew Map is not initialized!");
            m_array.Add(Map);
        }
        /// <summary>
        /// Remove the instance of the given Map
        /// </summary>
        /// <param name="Map"></param>
        public void Remove(IMap Map)
        {
            m_array.Remove(Map);
        }
        /// <summary>
        /// Get the number of Maps in the collection
        /// </summary>
        public int Count
        {
            get
            {
                return m_array.Count;
            }
        }
        /// <summary>
        /// Return the Map at the given index
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public IMap get_Item(int Index)
        {
            if (Index > m_array.Count || Index < 0)
                throw new Exception("Maps::get_Item:\r\nIndex is out of range!");
            return m_array[Index] as IMap;
        }
        /// <summary>
        /// Remove the Map at the given index
        /// </summary>
        /// <param name="Index"></param>
        public void RemoveAt(int Index)
        {
            if (Index > m_array.Count || Index < 0)
                throw new Exception("Maps::RemoveAt:\r\nIndex is out of range!");
            m_array.RemoveAt(Index);
        }
        /// <summary>
        /// Reset the Maps array
        /// </summary>
        public void Reset()
        {
            m_array.Clear();
        }
        #endregion



    }
}
