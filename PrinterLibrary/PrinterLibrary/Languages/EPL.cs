using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterLibrary.Languages
{
    /// <summary>
    /// 
    /// </summary>
    public static class EPL
    {
        /// <summary>
        /// This command clears the image buffer prior to building a new label image.
        /// </summary>
        /// <returns></returns>
        public static byte[] CLEAR_BUFFER()
        {
            return System.Text.Encoding.ASCII.GetBytes("\r\nN\r\n");
        }

        /// <summary>
        /// Renders an ASCII text string to the image print buffer
        /// <para></para>See EPL documentation for more information
        /// </summary>
        /// <param name="x">Horizontal start position (X) in dots.</param>
        /// <param name="y">Vertical start position (Y) in dots</param>
        /// <param name="rotation">Characters are organized vertically from left to right and then rotated to print.
        /// <para>Accepted Values:</para>
        /// <para>0 = normal (no rotation)</para>
        /// <para>1 = 90 degrees</para>
        /// <para>2 = 180 degrees</para>
        /// <para>3 = 270 degrees</para>
        /// </param>
        /// <param name="font">Values for 203 dpi:
        /// <para>1 = 8x12 dots</para>
        /// <para>2 = 10x16 dots</para>
        /// <para>3 = 12x20 dots</para>
        /// <para>4 = 14x24 dots</para>
        /// <para>5 = 32x48 dots</para>
        /// </param>
        /// <param name="horizontalMultiplier">Horizontal multilier expands the text horizontally.
        /// <para>Accepted Values:</para>
        /// <para>1-6,8</para>
        /// </param>
        /// <param name="verticalMultiplier">Vertical multiplier expands the text vertically.
        /// <para>Accepted Values:</para>
        /// <para>1-9</para>
        /// </param>
        /// <param name="reverse">Accepted Values:
        /// <para>N = Normal</para>
        /// <para>R = Reverse Image</para>
        /// </param>
        /// <param name="data">Fixed data field.
        /// <para>The backslash (\) character designates the following character is a literal and will encode into the data field.</para>
        /// </param>
        /// <returns></returns>
        /// <example>A50,0,0,1,1,1,N,"Example 1"\r\n</example>
        public static byte[] TEXT(int x, int y, int rotation, int font, int horizontalMultiplier, int verticalMultiplier, string reverse, string data)
        {
            string text = $"A{x},{y},{rotation},{font},{horizontalMultiplier},{verticalMultiplier},{reverse},\"{data}\"\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Use this command to print standard bar codes
        /// </summary>
        /// <param name="x">Horizontal start position (X) in dots.</param>
        /// <param name="y">Vertical start position (Y) in dots</param>
        /// <param name="rotation">Characters are organized vertically from left to right and then rotated to print.
        /// <para>Accepted Values:</para>
        /// <para>0 = normal (no rotation)</para>
        /// <para>1 = 90 degrees</para>
        /// <para>2 = 180 degrees</para>
        /// <para>3 = 270 degrees</para>
        /// </param>
        /// <param name="barcodeSelection">See Table 1, Bar Codes on page 51 on EPL documentation for more information</param>
        /// <param name="narrowBarWidth">Narrow bar width in dots 
        /// <para>See Table 1, Bar Codes on page 51 on EPL documentation for more information</para>
        /// </param>
        /// <param name="wideBarWidth">Wide bar width in dots
        /// <para>Accepted Values:</para>
        /// <para>2-30</para>
        /// <para>See Table 1, Bar Codes on page 51 on EPL documentation for more information</para>
        /// </param>
        /// <param name="height">Bar code height in dots</param>
        /// <param name="humanReadable">Accepted Values:
        /// <para>B = Yes</para>
        /// <para>N = No</para>
        /// </param>
        /// <param name="data">The data in this field must comply with the selected bar code's specified format. The backslash (\) character designates the following character is a literal and will encode into the data field.</param>
        /// <returns></returns>
        /// <example>B10,10,0,3,3,7,200,B,"998152-001"\r\n</example>
        public static byte[] BARCODE(int x, int y, int rotation, int barcodeSelection, int narrowBarWidth, int wideBarWidth, int height, string humanReadable, string data)
        {
            string text = $"B{x},{y},{rotation},{barcodeSelection},{narrowBarWidth},{wideBarWidth},{height},{humanReadable},\"{data}\"\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Use this command to generate QR Code bar code symbols with a single command.
        /// </summary>
        /// <param name="x">Horizontal start position (X) in dots.</param>
        /// <param name="y">Vertical start position (Y) in dots</param>
        /// <param name="qrcode">Must be "Q" for QR Code.</param>
        /// <param name="data">Data sent to the printer is converted to one of four formats depending the value set by parameter p7, Data Input Mode select.</param>
        /// <param name="codeModel">Accepted Values:
        /// <para>1 = Model 1</para>
        /// <para>2 = Model 2</para>
        /// <para>Default Value: Model 2</para>
        /// </param>
        /// <param name="scaleFactor">Accepted Values: 1-99
        /// <para>Default Value: 3</para>
        /// </param>
        /// <param name="errorCorrection">Accepted Values:
        /// <para>L = Lower error correction, most data</para>
        /// <para>M = Default</para>
        /// <para>Q = Optimized for error correction over data</para>
        /// <para>H = Highest error correction, least data</para>
        /// <para>Default Value = M</para>
        /// </param>
        /// <param name="dataInputMode">Accepted Values:
        /// <para>A = Automatic Data Select</para>
        /// <para>M = Initialized the manual data mode and the data type is set by the firs character in the fixed data field ("DATA")</para>
        /// <para>Default Value: A</para>
        /// </param>
        /// <returns></returns>
        public static byte[] QRCODE(int x, int y, string qrcode, string data, int codeModel = 2, int scaleFactor = 3, string errorCorrection = "M", string dataInputMode = "A")
        {
            string text = $"b{x},{y},{qrcode},{codeModel},{scaleFactor},{errorCorrection},{dataInputMode},\"{data}\"\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Use this command to select the print speed
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] SPEED(int value)
        {
            string text = $"S{value}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Use this command to draw black lines
        /// </summary>
        /// <param name="x">Horizontal start position (X) in dots</param>
        /// <param name="y">Vertical start position (Y) in dots</param>
        /// <param name="horizontalLength">Horizonral length in dots</param>
        /// <param name="verticalLength">Vertical length in dots</param>
        /// <returns></returns>
        /// <example>LO50,200,400,20\r\n</example>
        public static byte[] LINE(int x, int y, int horizontalLength, int verticalLength)
        {
            string text = $"LO{x},{y},{horizontalLength},{verticalLength}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Use this command to draw a box shape
        /// </summary>
        /// <param name="x">Horizontal start position (X) in dots</param>
        /// <param name="y">Vertical start position (Y) in dots</param>
        /// <param name="lineThickness">Line thickness in dots</param>
        /// <param name="horizontalEndPoint">Horizontal end point (X) in dots</param>
        /// <param name="verticalEndtPoint">Vertical end point (Y) in dots</param>
        /// <returns></returns>
        public static byte[] BOX(int x, int y, int lineThickness, int horizontalEndPoint, int verticalEndtPoint)
        {
            string text = $"LO{x},{y},{lineThickness},{horizontalEndPoint},{verticalEndtPoint}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Use this command to print the contents of the image buffer
        /// </summary>
        /// <param name="quantity">Accepted Values:
        /// <para>1 to 65535</para>
        /// </param>
        /// <param name="copy">Number of copies of each label (used in combination with counters to print multiple copies of the same lable).
        /// <para>Accepted values:</para>
        /// <para>1 to 65535</para>
        /// </param>
        /// <returns></returns>
        public static byte[] PRINT(int quantity, int copy = 1)
        {
            string text = $"P{quantity},{copy}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Use this command to set width of the printable area of the media
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] SET_WIDTH(int value)
        {
            string text = $"q{value}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Use this command to select the print orientation
        /// </summary>
        /// <param name="orientation">Accepted Values:
        /// <para>T = Printing from top of image buffer</para>
        /// <para>B = Printing from bottom of image buffer</para>
        /// <para>Default Value: T</para>
        /// </param>
        /// <returns></returns>
        public static byte[] DIRECTION(string orientation)
        {
            string text = $"Z{orientation}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
    }
}
