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
    public class BluetoothConnection : PrinterConnection
    {
        /// <summary>
        /// 
        /// </summary>
        public string BluetoothDeviceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bluetoothDeviceName"></param>
        public BluetoothConnection(string bluetoothDeviceName)
        {
            BluetoothDeviceName = bluetoothDeviceName;
        }
    }
}
