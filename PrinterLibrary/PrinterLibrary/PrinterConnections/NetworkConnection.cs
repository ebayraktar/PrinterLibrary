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
    public class NetworkConnection : PrinterConnection
    {
        /// <summary>
        /// 
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public NetworkConnection(string host, int port)
        {
            Host = host;
            Port = port;
        }
    }
}
