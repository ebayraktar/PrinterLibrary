using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterLibrary.PrinterConnections
{
    /// <summary>
    /// 
    /// </summary>
    public class USBConnection : PrinterConnection
    {
        /// <summary>
        /// 
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Context Context { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="deviceName"></param>
        public USBConnection(Context context, string deviceName)
        {
            Context = context;
            DeviceName = deviceName;
        }
    }
}
