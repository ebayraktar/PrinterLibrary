using PrinterLibrary.Interfaces;
using PrinterLibrary.PrinterConnections;
using System.Threading.Tasks;

namespace PrinterLibrary.Printers
{
    internal abstract class Printer : IPrinter
    {
        public bool IsConnected { get; set; }
        public abstract Task<bool> Connect(PrinterConnection connection);

        public abstract void Disconnect();

        public abstract Task<int> Print(byte[] command);
    }
}
