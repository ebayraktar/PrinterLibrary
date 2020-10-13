using System.Collections.Generic;
using System.Text;

namespace PrinterLibrary.Languages
{
    /// <summary>
    /// 
    /// </summary>
    public static class ESC
    {
        /// <summary>
        /// Moves the print position to the next tab position.
        /// </summary>
        /// <returns></returns>
        public static byte[] HORIZONTAL_TAB()
        {
            return new byte[] { 9 };
        }

        /// <summary>
        /// Prints the data in the print buffer and feeds one line based on the current line spacing.
        /// </summary>
        /// <returns></returns>
        public static byte[] LINE_FEED()
        {
            return new byte[] { 10 };
        }

        /// <summary>
        /// When automatic line feed is enabled, this command functions the same as LF; when automatic line feed is disabled, this command is ignored.
        /// </summary>
        /// <returns></returns>
        public static byte[] CARRIAGE_RETURN()
        {
            return new byte[] { 13 };
        }

        /// <summary>
        /// In page mode, delete all the print data in the current printable area.
        /// </summary>
        /// <returns></returns>
        public static byte[] CANCEL()
        {
            return new byte[] { 24 };
        }

        /// <summary>
        /// Clears the data in the print buffer and resets the printer mode to the mode that was in effect when the power was turned on.
        /// </summary>
        /// <returns></returns>
        public static byte[] INITIALIZE_PRINTER()
        {
            return new byte[] { 27, 64 };
        }

        /// <summary>
        /// Prints the data in the print buffer and feeds the paper [n x vertical or horizontal motion unit].
        /// </summary>
        /// <param name="n">0 ≤ n ≤ 255</param>
        /// <returns></returns>
        public static byte[] PRINT_AND_FEED(int n)
        {
            return new byte[] { 27, 74, (byte)n };
        }

        /// <summary>
        /// Selects character fonts
        /// </summary>
        /// <param name="n">0, 48 Character font A (12 X 24 ) Selected
        /// <para>1, 49 Character font B(9 X 24 ) Selected</para></param>
        /// <returns></returns>
        public static byte[] SELECT_FONT(int n)
        {
            return new byte[] { 27, 77, (byte)n };
        }

        /// <summary>
        /// Select the print direction and starting position in page mode.
        /// </summary>
        /// <param name="n">0 ≤n ≤3, 48 ≤n ≤51
        /// <para>0, 48 Left to right Upper left(A in the figure)</para>
        /// <para>1, 49 Bottom to top Lower left(B in the figure)</para>
        /// <para>2, 50 Right to left Lower right(C in the figure)</para>
        /// <para>3, 51 Top to bottom Upper right(D in the figure)</para>
        /// </param>
        /// <returns></returns>
        public static byte[] DIRECTION(int n)
        {
            return new byte[] { 27, 84, (byte)n };
        }

        /// <summary>
        /// Aligns all the data in one line to the specified position
        /// </summary>
        /// <param name="n">0 ≤ n ≤ 2, 48 ≤ n ≤ 50
        /// <para>0，48 Left justification</para>
        /// <para>1，49 Centering</para>
        /// <para>2，50 Right justification</para>
        /// </param>
        /// <returns></returns>
        public static byte[] JUSTIFICATION(int n)
        {
            return new byte[] { 27, 97, (byte)n };
        }

        /// <summary>
        /// Select character size
        /// </summary>
        /// <param name="width">1 ≤ vertical number of times ≤ 8</param>
        /// <param name="height">1 ≤ horizontal number of times ≤ 8</param>
        /// <returns></returns>
        public static byte[] CHARACTER_SIZE(int width, int height)
        {
            int t = (width * 16) + height;
            return new byte[] { 29, 33, (byte)t };
        }

        /// <summary>
        /// Partial cut(one point center uncut)
        /// </summary>
        /// <returns></returns>
        public static byte[] CUT()
        {
            return new byte[] { 29, 86, 66, 0 };
        }

        /// <summary>
        /// Select the height of the bar code. 
        /// </summary>
        /// <param name="height">n specifies the number of dots in the vertical direction.
        /// <para>1 ≤  n ≤ 255</para>
        /// <para>Default n = 162</para> </param>
        /// <returns></returns>
        public static byte[] BARCODE_HEIGHT(int height)
        {
            return new byte[] { 29, 104, (byte)height };
        }

        /// <summary>
        /// Selects a bar code system and prints the bar code
        /// </summary>
        /// <param name="barcodeSystem">UPC – A: 65, UPC – E: 66, EAN13: 67, EAN8: 68, CODE39: 69, ITF: 70, CODABAR: 71, CODE93: 72, CODE128: 73, </param>
        /// <param name="text">Text to be printed</param>
        /// <param name="spesificCharacter">
        /// When CODE93 (<paramref name="barcodeSystem"/>=72) is used :
        /// <para>NUL: 0, SOH: 1, STX: 2, ETX: 3, EOT: 4, ENQ: 5, ACK: 6, BEL: 7, BS: 8, HT: 9, LF: 10, VT: 11, FF: 12, CR: 13, SO: 14, SI: 15, DLE: 16, DC1: 17, DC2: 18, DC3: 19, DC4: 20, NAK: 21, SYN: 22, ETB: 23, CAN: 24, EM: 25, SUB: 26, ESC: 27, FS: 28, GS: 29, RS: 30, US: 31, DEL: 127</para>
        /// <para>When CODE 128 (barcodeSystem = 73) is used:</para>
        /// <para>SHIFT: 83, CODE A: 65, CODE B: 66, CODE C: 67, FNC1: 49, FNC2: 50, FNC3: 51, FNC4: 52, “{“: 123</para>
        /// </param>
        /// <returns></returns>
        public static byte[] BARCODE_PRINT(int barcodeSystem, string text, int spesificCharacter = 0)
        {
            List<byte> output = new List<byte>()
            { 29, 107, (byte)barcodeSystem };
            if (barcodeSystem == 73)
                output.AddRange(new byte[] { (byte)(text.Length + 2), 123, (byte)spesificCharacter });
            else if (barcodeSystem == 72)
                output.AddRange(new byte[] { (byte)(text.Length + 1), (byte)spesificCharacter });
            output.AddRange(Encoding.ASCII.GetBytes(text));
            return output.ToArray();
        }

        /// <summary>
        /// Set the horizontal size of the bar code.
        /// </summary>
        /// <param name="width">2 ≤ n ≤ 6
        /// <para>Default: 3</para>
        /// </param>
        /// <returns></returns>
        public static byte[] BARCODE_WIDTH(int width)
        {
            return new byte[] { 29, 119, (byte)width };
        }

        /// <summary>
        /// Selects the model for QR Code.
        /// </summary>
        /// <param name="model">49,50</param>
        /// <returns></returns>
        public static byte[] QRCODE_MODEL(int model)
        {
            return new byte[] { 29, 40, 107, 4, 0, 49, 65, (byte)model, 0 };
        }
        /// <summary>
        /// Sets the size of the module for QR Code to n dots.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[] QRCODE_SIZE(int size)
        {
            return new byte[] { 29, 40, 107, 3, 0, 49, 67, (byte)size };
        }
        /// <summary>
        /// Selects the error correction level for QR code.
        /// </summary>
        /// <param name="level">48 ≤ n ≤ 51
        /// <para>48 Selects Error correction level L 7</para>
        /// <para>49 Selects Error correction level M 15</para>
        /// <para>50 Selects Error correction level Q 25</para>
        /// <para>51 Selects Error correction level H 30</para>
        /// </param>
        /// <returns></returns>
        public static byte[] QRCODE_CORRECTION_LEVEL(int level)
        {
            return new byte[] { 29, 40, 107, 3, 0, 49, 69, (byte)level };
        }
        /// <summary>
        /// Store the QR Code symbol data in the symbol storage area.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] QRCODE_STORE(string text)
        {
            int store_len = text.Length + 3;
            byte store_pL = (byte)(store_len % 256);
            byte store_pH = (byte)(store_len / 256);
            List<byte> output = new List<byte>()
                { 29, 40, 107, store_pL, store_pH, 49, 80, 48 };
            output.AddRange(Encoding.UTF8.GetBytes(text));
            return output.ToArray();
        }
        /// <summary>
        /// Encodes and prints the QR Code symbol data in the symbol storage area using the process of Store the data.
        /// </summary>
        /// <returns></returns>
        public static byte[] QRCODE_PRINT()
        {
            return new byte[] { 29, 40, 107, 3, 0, 49, 81, 48 };
        }
    }
}
