using PrinterLibrary.Enums;
using PrinterLibrary.Interfaces;

namespace PrinterLibrary.Builders
{
    /// <summary>
    /// Yazıcı için text builder
    /// </summary>
    public abstract class Builder : IBuilder
    {
        /// <summary>
        /// Builder sırasına Barkod ekler
        /// </summary>
        /// <param name="text"></param>
        /// <param name="barcodeType"></param>
        /// <param name="humanReadable"></param>
        /// <returns></returns>
        public abstract Builder Barcode(string text, BarcodeType barcodeType, HumanReadable humanReadable);
        /// <summary>
        /// Builder sırasına 1den fazla satır içeren text ekler
        /// </summary>
        /// <param name="block"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public abstract Builder Block(string block, bool center = false);
        /// <summary>
        /// Builder sırasına QR kod ekler
        /// </summary>
        /// <param name="text"></param>
        /// <param name="size"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public abstract Builder QRCode(string text, int size, bool center = true);
        /// <summary>
        /// Builder sırasına alt başlık ekler
        /// </summary>
        /// <param name="text"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public abstract Builder SubTitle(string text, bool center = false);
        /// <summary>
        /// Builder sırasına metin ekler
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontType"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public abstract Builder Text(string text, FontSize fontType, bool center = false);
        /// <summary>
        /// Builder sırasına başlık ekler
        /// </summary>
        /// <param name="text"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public abstract Builder Title(string text, bool center = false);
        /// <summary>
        /// Builder sırasını yazdırılacak formata getirir
        /// </summary>
        /// <returns></returns>
        public abstract byte[] Build();
    }
}
