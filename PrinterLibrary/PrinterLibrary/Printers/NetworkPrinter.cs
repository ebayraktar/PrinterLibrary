using PrinterLibrary.PrinterConnections;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace PrinterLibrary.Printers
{
    internal class NetworkPrinter : Printer
    {
        Socket pSocket;
        public NetworkPrinter()
        {

        }

        public override Task<bool> Connect(PrinterConnection connection)
        {
            return Task.Run(() =>
            {
                if (connection is NetworkConnection nc)
                {
                    pSocket = new Socket(SocketType.Stream, ProtocolType.IP);

                    // Set a timeout for attemps to connect, here set to 1500 miliseconds
                    pSocket.SendTimeout = 1500;
                    // Connect to the specified ip address and port
                    if (!pSocket.Connected)
                        pSocket.Connect(nc.Host, nc.Port);
                    IsConnected = true;
                    return true;
                }
                else
                {
                    throw new Exceptions.PrinterException("Bağlantı tipi uyuşmuyor!", new NotImplementedException());
                }
            });
        }

        public override void Disconnect()
        {
            IsConnected = false;
            pSocket.Close();
        }

        public override Task<int> Print(byte[] command)
        {
            return Task.Run(() =>
            {
                if (!IsConnected)
                    throw new Exceptions.PrinterException("Önce bağlantı oluşturun.");

                try
                {
                    return pSocket.Send(command);
                }
                catch (Exception ex)
                {
                    throw new Exceptions.PrinterException("Yazdırılırken hata oluştu.", ex);
                }
            });
        }
    }
}
