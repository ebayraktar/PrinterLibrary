using PrinterLibrary.Builders;
using PrinterLibrary.Enums;
using PrinterLibrary.Interfaces;
using PrinterLibrary.PrinterConnections;
using PrinterLibrary.Printers;
using System.Threading.Tasks;

namespace PrinterLibrary.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class PrinterService : IPrinter
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsConnected
        {
            get { return printer.IsConnected; }
        }

        private readonly Printer printer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="printerType"></param>
        public PrinterService(PrinterConnectionType printerType)
        {
            switch (printerType)
            {
                case PrinterConnectionType.Bluetooth:
                    printer = new BluetoothPrinter();
                    break;
                case PrinterConnectionType.Network:
                    printer = new NetworkPrinter();
                    break;
                case PrinterConnectionType.USB:
                    printer = new USBPrinter();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public Task<bool> Connect(PrinterConnection connection)
        {
            return printer.Connect(connection);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Disconnect()
        {
            printer.Disconnect();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Task<int> Print(byte[] command)
        {
            return printer.Print(command);
        }

        /// <summary>
        /// Yazıcı için text builder
        /// </summary>
        public class TextBuilder : Builder
        {
            private readonly Builder builder;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="printerType"></param>
            public TextBuilder(PrinterLanguageType printerType)
            {
                switch (printerType)
                {
                    case PrinterLanguageType.TSPL:
                        builder = new TSPLTextBuilder();
                        break;
                    case PrinterLanguageType.ESC:
                        builder = new ESCTextBuilder();
                        break;
                    case PrinterLanguageType.CPCL:
                        builder = new CPCLTextBuilder();
                        break;
                    case PrinterLanguageType.EPL:
                        builder = new EPLTextBuilder();
                        break;
                    default:
                        break;
                }
            }
            /// <summary>
            /// Builder sırasına Barkod ekler
            /// </summary>
            /// <param name="text"></param>
            /// <param name="barcodeType"></param>
            /// <param name="humanReadable"></param>
            public override Builder Barcode(string text, BarcodeType barcodeType, HumanReadable humanReadable)
            {
                return builder.Barcode(text, barcodeType, humanReadable);
            }

            /// <summary>
            /// Builder sırasına 1den fazla satır içeren text ekler
            /// </summary>
            /// <param name="block"></param>
            /// <param name="center"></param>
            /// <returns></returns>
            public override Builder Block(string block, bool center = false)
            {
                return builder.Block(block, center);
            }

            /// <summary>
            /// Builder sırasını yazdırılacak formata getirir
            /// </summary>
            /// <returns></returns>
            public override byte[] Build()
            {
                return builder.Build();
            }

            /// <summary>
            /// Builder sırasına QR kod ekler
            /// </summary>
            /// <param name="text"></param>
            /// <param name="size"></param>
            /// <param name="center"></param>
            /// <returns></returns>
            public override Builder QRCode(string text, int size, bool center = true)
            {
                return builder.QRCode(text, size, center);
            }

            /// <summary>
            /// Builder sırasına alt başlık ekler
            /// </summary>
            /// <param name="text"></param>
            /// <param name="center"></param>
            /// <returns></returns>
            public override Builder SubTitle(string text, bool center = false)
            {
                return builder.SubTitle(text, center);
            }

            /// <summary>
            /// Builder sırasına metin ekler
            /// </summary>
            /// <param name="text"></param>
            /// <param name="fontType"></param>
            /// <param name="center"></param>
            /// <returns></returns>
            public override Builder Text(string text, FontSize fontType, bool center = false)
            {
                return builder.Text(text, fontType, center);
            }

            /// <summary>
            /// Builder sırasına başlık ekler
            /// </summary>
            /// <param name="text"></param>
            /// <param name="center"></param>
            /// <returns></returns>
            public override Builder Title(string text, bool center = false)
            {
                return builder.Title(text, center);
            }
        }
    }
}
