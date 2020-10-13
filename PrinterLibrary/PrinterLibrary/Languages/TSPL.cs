namespace PrinterLibrary.Languages
{
    /// <summary>
    /// TSPL Yazıcıların temel komutlarını içeren sınıf
    /// </summary>
    public static class TSPL
    {
        /// <summary>
        /// This command defines the label width and length.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static byte[] SIZE(int m)
        {
            string text = "SIZE " + m + "," + m + "\n";

            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command defines the label width and length.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static byte[] SIZE(int m, int n)
        {
            string text = "SIZE " + m + "," + n + "\n";

            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command defines the label width and length.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="measurement"></param>
        /// <returns></returns>
        public static byte[] SIZE(int m, Enums.Measurement measurement)
        {
            string _meas = measurement == Enums.Measurement.Default ? "" : measurement.ToString();
            string text = "SIZE " + m + " " + _meas + "," + m + " " + _meas + "\n";

            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command defines the label width and length.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="measurement"></param>
        /// <returns></returns>
        public static byte[] SIZE(int m, int n, Enums.Measurement measurement)
        {
            string _meas = measurement == Enums.Measurement.Default ? "" : measurement.ToString();
            string text = "SIZE " + m + " " + _meas + "," + n + " " + _meas;

            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Defines the gap distance between two labels.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static byte[] GAP(int m, int n)
        {
            string text = "GAP " + m + "," + n + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// Defines the gap distance between two labels.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <param name="measurement"></param>
        /// <returns></returns>
        public static byte[] GAP(int m, int n, Enums.Measurement measurement)
        {
            string _meas = measurement == Enums.Measurement.Default ? "" : measurement.ToString();
            string text = "GAP " + m + " " + _meas + "," + n + " " + _meas + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// This command defines the print speed.
        /// </summary>
        /// <param name="n">1-18</param>
        /// <returns></returns>
        public static byte[] SPEED(int n)
        {
            string text = "SPEED " + n + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command sets the printing darkness.
        /// </summary>
        /// <param name="n">0~15
        /// <para></para>0: specifies the lightest level
        /// <para></para>15: specifies the darkest level</param>
        /// <returns></returns>
        public static byte[] DENSITY(int n)
        {
            string text = "DENSITY " + n + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command defines the printout direction and mirror image. This will be stored in the printer memory.
        /// </summary>
        /// <param name="n">0 or 1</param>
        /// <param name="m">0: Print normal image <para></para>1: Print mirror image</param>
        /// <returns></returns>
        public static byte[] DIRECTION(int n, int m = 0)
        {
            string text = "DIRECTION " + n + "," + m + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// This command clears the image buffer. 
        /// </summary>
        /// <returns></returns>
        public static byte[] CLS()
        {
            string text = "CLS\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command feeds label with the specified length. The length is specified by dot.
        /// </summary>
        /// <param name="n">unit: dot
        /// <para></para>
        /// 1 ≤ n ≤ 9999</param>
        /// <returns></returns>
        public static byte[] FEED(int n)
        {
            string text = "FEED " + n + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// This command feeds the label in reverse. The length is specified by dot.
        /// </summary>
        /// <param name="n">unit: dot
        /// <para></para>1 ≤ n ≤ 9999</param>
        /// <returns></returns>
        public static byte[] BACKFEED(int n)
        {
            string text = "BACKFEED " + n + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// This command prints the label format currently stored in the image buffer
        /// </summary>
        /// <param name="m">Specifies how many sets of labels will be printed.
        /// <para></para>1 ≤ m ≤ 999999999</param>
        /// <param name="n">Specifies how many copies should be printed for each particular label set.
        /// <para></para>1 ≤ n ≤ 999999999</param>
        /// <returns></returns>
        public static byte[] PRINT(int m, int n)
        {
            string text = "PRINT " + m + "," + n + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command prints the label format currently stored in the image buffer
        /// </summary>
        /// <param name="m">Specifies how many sets of labels will be printed.
        /// <para></para>1 ≤ m ≤ 999999999</param>
        /// <returns></returns>
        public static byte[] PRINT(int m)
        {
            string text = "PRINT " + m + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command controls the sound frequency of the beeper. There are 10 levels of sounds. The timing
        /// control can be set by the "interval" parameter
        /// </summary>
        /// <param name="level">Sound level: 0~9</param>
        /// <param name="interval">Sound interval: 1~4095</param>
        /// <returns></returns>
        public static byte[] SOUND(int level, int interval)
        {
            string text = "SOUND " + level + "," + interval + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command activates the cutter to immediately cut the labels without back feeding the label. 
        /// </summary>
        /// <returns></returns>
        public static byte[] CUT()
        {
            string text = "CUT \n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// This command draws a bar on the label format.
        /// </summary>
        /// <param name="x">The upper left corner x-coordinate (in dots)</param>
        /// <param name="y">The upper left corner y-coordinate (in dots)</param>
        /// <param name="width">Bar width (in dots)</param>
        /// <param name="height">Bar height (in dots)</param>
        /// <returns></returns>
        /// <example>
        /// SIZE 50 mm,25 mm
        /// <para></para>
        /// GAP 3 mm,0
        /// <para></para>
        /// DIRECTION 1
        /// <para></para>
        /// CLS
        /// <para></para>
        /// BAR 80,80,300,100
        /// <para></para>
        /// PRINT 1,1
        /// </example>
        public static byte[] BAR(int x, int y, int width, int height)
        {
            string text = "BAR " + x + "," + y + "," + width + "," + height + "\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command prints 1D barcodes.
        /// </summary>
        /// <param name="x">Specify the x-coordinate bar code on the label</param>
        /// <param name="y">Specify the y-coordinate bar code on the label</param>
        /// <param name="codeType">128, 128M, EAN128, EAN128M, 25, 25C, 255, 251, 39, 39C, 39S, 93, EAN13, EAN13+2, EAN13+5, EAN8, EAN8+2</param>
        /// <param name="height">Bar code height (in dots)</param>
        /// <param name="humanReadable">0: not readable
        /// <para></para>
        /// 1: human readable aligns to left
        /// <para></para>
        /// 2: human readable aligns to center
        /// <para></para>
        /// 3: human readable aligns to right</param>
        /// <param name="rotation">
        /// 0 : No rotation
        /// <para></para>
        /// 90 : Rotate 90 degrees clockwise
        /// <para></para>
        /// 180 : Rotate 180 degrees clockwise
        /// <para></para>
        /// 270 : Rotate 270 degrees clockwise</param>
        /// <param name="narrow">Width of narrow element (in dots)</param>
        /// <param name="wide">Width of wide element (in dots)</param>
        /// <param name="alignment">Specify the alignment of barcode
        /// <para></para>
        /// 0 : default (Left)
        /// <para></para>
        /// 1 : Left
        /// <para></para>
        /// 2 : Center
        /// <para></para>
        /// 3 : Right</param>
        /// <param name="content">Content of barcode</param>
        /// <returns></returns>
        public static byte[] BARCODE(int x, int y, string codeType, int height, int humanReadable, int rotation, int narrow, int wide, int alignment, string content)
        {
            string text = $"BARCODE {x},{y},\"{codeType}\",{height},{humanReadable},{rotation},{narrow},{wide},{alignment},\"{content}\"\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command prints 1D barcodes.
        /// </summary>
        /// <param name="x">Specify the x-coordinate bar code on the label</param>
        /// <param name="y">Specify the y-coordinate bar code on the label</param>
        /// <param name="codeType">128, 128M, EAN128, EAN128M, 25, 25C, 255, 251, 39, 39C, 39S, 93, EAN13, EAN13+2, EAN13+5, EAN8, EAN8+2</param>
        /// <param name="height">Bar code height (in dots)</param>
        /// <param name="humanReadable">0: not readable
        /// <para></para>
        /// 1: human readable aligns to left
        /// <para></para>
        /// 2: human readable aligns to center
        /// <para></para>
        /// 3: human readable aligns to right</param>
        /// <param name="rotation">
        /// 0 : No rotation
        /// <para></para>
        /// 90 : Rotate 90 degrees clockwise
        /// <para></para>
        /// 180 : Rotate 180 degrees clockwise
        /// <para></para>
        /// 270 : Rotate 270 degrees clockwise</param>
        /// <param name="narrow">Width of narrow element (in dots)</param>
        /// <param name="wide">Width of wide element (in dots)</param>
        /// <param name="content">Content of barcode</param>
        /// <returns></returns>
        public static byte[] BARCODE(int x, int y, string codeType, int height, int humanReadable, int rotation, int narrow, int wide, string content)
        {
            string text = $"BARCODE {x},{y},\"{codeType}\",{height},{humanReadable},{rotation},{narrow},{wide},\"{content}\"\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command draws rectangles on the label.
        /// </summary>
        /// <param name="x">Specify x-coordinate of upper left corner (in dots)</param>
        /// <param name="y">Specify y-coordinate of upper left corner (in dots)</param>
        /// <param name="x_end">Specify x-coordinate of lower right corner (in dots)</param>
        /// <param name="y_end">Specify y-coordinate of lower right corner (in dots)</param>
        /// <param name="lineThickness">Line thickness (in dots)</param>
        /// <returns></returns>
        public static byte[] BOX(int x, int y, int x_end, int y_end, int lineThickness)
        {
            string text = $"BOX {x},{y},{x_end},{y_end},{lineThickness}\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command draws rectangles on the label.
        /// </summary>
        /// <param name="x">Specify x-coordinate of upper left corner (in dots)</param>
        /// <param name="y">Specify y-coordinate of upper left corner (in dots)</param>
        /// <param name="x_end">Specify x-coordinate of lower right corner (in dots)</param>
        /// <param name="y_end">Specify y-coordinate of lower right corner (in dots)</param>
        /// <param name="lineThickness">Line thickness (in dots)</param>
        /// <param name="radius">Optional. Specify the round corner. Default is 0.</param>
        /// <returns></returns>
        public static byte[] BOX(int x, int y, int x_end, int y_end, int lineThickness, int radius)
        {
            string text = $"BOX {x},{y},{x_end},{y_end},{lineThickness},{radius}\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// This command prints QR code.
        /// </summary>
        /// <param name="x">The upper left corner x-coordinate of the QR code</param>
        /// <param name="y">The upper left corner y-coordinate of the QR code</param>
        /// <param name="eccLevel">Error correction recovery level
        /// <para></para>
        /// L : 7%
        /// <para></para>
        /// M : 15%
        /// <para></para>
        /// Q : 25%
        /// <para></para>
        /// H : 30%</param>
        /// <param name="cellWidth">1~10</param>
        /// <param name="mode">Auto / manual encode
        /// <para></para>
        /// A : Auto
        /// <para></para>
        /// M : Manual</param>
        /// <param name="rotation">0 : 0 degree
        /// <para></para>90 : 90 degree
        /// <para></para>180 : 180 degree
        /// <para></para>270 : 270 degree</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte[] QRCODE(int x, int y, string eccLevel, int cellWidth, string mode, int rotation, string content)
        {
            string text = $"QRCODE {x},{y},{eccLevel},{cellWidth},{mode},{rotation},M2,\"{content}\"\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// This command prints text on label.
        /// </summary>
        /// <param name="x">The x-coordinate of the text</param>
        /// <param name="y">The y-coordinate of the text</param>
        /// <param name="font">Font name
        /// <para>0 Monotye CG Triumvirate Bold Condensed, font width and height is stretchable</para>
        /// <para>1 8 x 12 fixed pitch dot font</para>
        /// <para>2 12 x 20 fixed pitch dot font</para>
        /// <para>3 16 x 24 fixed pitch dot font</para>
        /// <para>4 24 x 32 fixed pitch dot font</para>
        /// <para>5 32 x 48 dot fixed pitch font</para>
        /// <para>6 14 x 19 dot fixed pitch font OCR - B</para>
        /// <para>7 21 x 27 dot fixed pitch font OCR - B</para>
        /// <para>8 14 x25 dot fixed pitch font OCR - A </para>
        /// </param>
        /// <param name="rotation">0 : 0 degree
        /// <para></para>90 : 90 degree
        /// <para></para>180 : 180 degree
        /// <para></para>270 : 270 degree</param>
        /// <param name="xMultiplication">Horizontal multiplication, up to 10x
        /// <para>Available factors: 1~10</para></param>
        /// <param name="yMultiplication">Vertical multiplication, up to 10x
        /// <para>Available factors: 1~10</para></param>
        /// <param name="content">Content of text string</param>
        /// <returns></returns>
        public static byte[] TEXT(int x, int y, string font, int rotation, int xMultiplication, int yMultiplication, string content)
        {
            string text = $"TEXT {x},{y},\"{font}\",{rotation},{xMultiplication},{yMultiplication},\"{content}\"\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// This command prints text on label.
        /// </summary>
        /// <param name="x">The x-coordinate of the text</param>
        /// <param name="y">The y-coordinate of the text</param>
        /// <param name="font">Font name
        /// <para>0 Monotye CG Triumvirate Bold Condensed, font width and height is stretchable</para>
        /// <para>1 8 x 12 fixed pitch dot font</para>
        /// <para>2 12 x 20 fixed pitch dot font</para>
        /// <para>3 16 x 24 fixed pitch dot font</para>
        /// <para>4 24 x 32 fixed pitch dot font</para>
        /// <para>5 32 x 48 dot fixed pitch font</para>
        /// <para>6 14 x 19 dot fixed pitch font OCR - B</para>
        /// <para>7 21 x 27 dot fixed pitch font OCR - B</para>
        /// <para>8 14 x25 dot fixed pitch font OCR - A </para>
        /// </param>
        /// <param name="rotation">0 : 0 degree
        /// <para></para>90 : 90 degree
        /// <para></para>180 : 180 degree
        /// <para></para>270 : 270 degree</param>
        /// <param name="xMultiplication">Horizontal multiplication, up to 10x
        /// <para>Available factors: 1~10</para></param>
        /// <param name="yMultiplication">Vertical multiplication, up to 10x
        /// <para>Available factors: 1~10</para></param>
        /// <param name="alignment">Optional. Specify the alignment of text. (V6.73 EZ)
        /// <para>0 : Default(Left)</para>
        /// <para>1 : Left         </para>
        /// <para>2 : Center       </para>
        /// <para>3 : Right        </para>
        /// </param>
        /// <param name="content">Content of text string</param>
        /// <returns></returns>
        public static byte[] TEXT(int x, int y, string font, int rotation, int xMultiplication, int yMultiplication, int alignment, string content)
        {
            string text = $"TEXT {x},{y},\"{font}\",{rotation},{xMultiplication},{yMultiplication},{alignment},\"{content}\"\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// This command prints paragraph on label.
        /// </summary>
        /// <param name="x">The x-coordinate of the text</param>
        /// <param name="y">The y-coordinate of the text</param>
        /// <param name="width">The width of block for the paragraph in dots</param>
        /// <param name="height">The height of block for the paragraph in dots</param>
        /// <param name="font">Font name
        /// <para>0 Monotye CG Triumvirate Bold Condensed, font width and height is stretchable</para>
        /// <para>1 8 x 12 fixed pitch dot font</para>
        /// <para>2 12 x 20 fixed pitch dot font</para>
        /// <para>3 16 x 24 fixed pitch dot font</para>
        /// <para>4 24 x 32 fixed pitch dot font</para>
        /// <para>5 32 x 48 dot fixed pitch font</para>
        /// <para>6 14 x 19 dot fixed pitch font OCR - B</para>
        /// <para>7 21 x 27 dot fixed pitch font OCR - B</para>
        /// <para>8 14 x25 dot fixed pitch font OCR - A </para>
        /// </param>
        /// <param name="rotation">0 : 0 degree
        /// <para></para>90 : 90 degree
        /// <para></para>180 : 180 degree
        /// <para></para>270 : 270 degree</param>
        /// <param name="xMultiplication">Horizontal multiplication, up to 10x
        /// <para>Available factors: 1~10</para></param>
        /// <param name="yMultiplication">Vertical multiplication, up to 10x
        /// <para>Available factors: 1~10</para></param>
        /// <param name="alignment">Optional. Specify the alignment of text. (V6.73 EZ)
        /// <para>0 : Default(Left)</para>
        /// <para>1 : Left         </para>
        /// <para>2 : Center       </para>
        /// <para>3 : Right        </para>
        /// </param>
        /// <param name="content">Data in block. The maximum data length is 4092 bytes.</param>
        /// <returns></returns>
        public static byte[] BLOCK(int x, int y, int width, int height, string font, int rotation, int xMultiplication, int yMultiplication, int alignment, string content)
        {
            string text = $"BLOCK {x},{y},{width},{height},\"{font}\",{rotation},{xMultiplication},{yMultiplication},0,{alignment},\"{content}\"\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
    }
}
