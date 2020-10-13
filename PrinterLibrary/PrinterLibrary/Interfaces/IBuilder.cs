using PrinterLibrary.Enums;

namespace PrinterLibrary.Interfaces
{
    internal interface IBuilder
    {
        Builders.Builder Title(string text, bool center = false);
        Builders.Builder SubTitle(string text, bool center = false);
        Builders.Builder Text(string text, FontSize fontType, bool center = false);
        Builders.Builder Block(string block, bool center = false);
        Builders.Builder QRCode(string text, int size, bool center = true);
        Builders.Builder Barcode(string text, BarcodeType barcodeType, HumanReadable humanReadable);

        byte[] Build();
    }
}
