using System.Threading.Tasks;

namespace PrinterLibrary.Interfaces
{
    internal interface IPrinter
    {
        Task<int> Print(byte[] command);
        Task<bool> Connect(PrinterConnections.PrinterConnection connection);
        void Disconnect();
    }
}
