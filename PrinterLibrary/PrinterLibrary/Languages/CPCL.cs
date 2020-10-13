using PrinterLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterLibrary.Languages
{
    /// <summary>
    /// This manual details the various commands in the CPCL language which enable the programmer to utilize the built in text, graphics, bar code printing and communications capabilities of Zebra mobile printers.
    /// <para>The following notation conventions are used throughout this manual:</para>
    /// <para>{ } Required item</para>
    /// <para>[ ] Optional item</para>
    /// <para>( ) Abbreviated command</para>
    /// <para>A space character is used to delimit each field in a command line. Many commands are accompanied by examples of the command in use.After the word “Input” in each example, the set of commands are displayed followed by a sample printout (“Output”)resulting from the printer processing those commands.</para>
    /// </summary>
    public static class CPCL
    {
        /// <summary>
        /// A label file always begins with the “!” character followed by an “x” offset parameter, “x” and “y” axis resolutions, a label length and finally a quantity of labels to print.The line containing these parameters is referred to as the Command Start Line.
        /// <para>A label file always begins with the Command Start Line and ends with the “PRINT” command.The commands that build specific labels are placed between these two commands. A space character is used to delimit each field in a command line.</para>
        /// </summary>
        /// <param name="offset">The horizontal offset for the entire label. This value causes all fields to be offset horizontally by the specified number of UNITS.</param>
        /// <param name="m">Horizontal resolution (in dots-per-inch).</param>
        /// <param name="n">Vertical resolution (in dots-per-inch).</param>
        /// <param name="height">The maximum height of the label.
        /// <para>
        /// The maximum label height is calculated by measuring from the bottom of the first black bar (or label gap) to the top of the next black bar(or label gap). Then 1/16” [1.5mm] is subtracted from this distance to obtain the maximum height. (In dots: subtract 12 dots on 203 d.p.i printers; 18 dots on 306 d.p.i.printers)
        /// </para>
        /// </param>
        /// <param name="quantity">Quantity of labels to be printed. Maximum = 1024.</param>
        /// <returns></returns>
        public static byte[] START(int offset, int m, int n, int height, int quantity)
        {
            string text = $"! {offset} {m} {n} {height} {quantity}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// The PRINT command terminates and prints the file. This must always be the last command (except when in Line Print Mode). Upon execution of the PRINT command, the printer will exit from a control session.Be sure to terminate this and all commands with both carriage-return and line-feed characters
        /// </summary>
        /// <returns></returns>
        public static byte[] PRINT()
        {
            string text = $"PRINT\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// The END command properly terminates a command and executes it without printing.
        /// </summary>
        /// <returns></returns>
        public static byte[] END()
        {
            string text = $"END\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// The ABORT command terminates a current control session without printing
        /// </summary>
        /// <returns></returns>
        public static byte[] ABORT()
        {
            string text = $"ABORT\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// The FORM command instructs the printer to feed to top of form after printing.
        /// </summary>
        /// <returns></returns>
        public static byte[] FORM()
        {
            string text = $"FORM\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// The TEXT command is used to place text on a label. This command and its variants control the specific font number and size used, the location of the text on the label, and the orientation of this text.Standard resident fonts can be rotated in 90˚ increments as shown in the example. 
        /// </summary>
        /// <param name="font"> Name/number of the font. </param>
        /// <param name="size"> Size identifier for the font.</param>
        /// <param name="x"> Horizontal starting position.</param>
        /// <param name="y"> Vertical starting position.</param>
        /// <param name="data"> The text to be printed.</param>
        /// <returns></returns>
        public static byte[] TEXT(int font, int size, int x, int y, string data)
        {
            string text = $"TEXT {font} {size} {x} {y} {data}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// MULTILINE (ML) allows you to print multiple lines of text using the same font and line-height.
        /// </summary>
        /// <param name="height">Unit-height for each line of text.</param>
        /// <param name="font"> Name/number of the font. </param>
        /// <param name="size"> Size identifier for the font.</param>
        /// <param name="x"> Horizontal starting position.</param>
        /// <param name="y"> Vertical starting position.</param>
        /// <param name="data"> The text to be printed.</param>
        /// <returns></returns>
        public static byte[] MULTILINE(int height, int font, int size, int x, int y, string data)
        {
            string text = $"MULTILINE {height}\r\nTEXT {font} {size} {x} {y} \r\n{data}\r\nENDMULTILINE\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// ROTATE commands are used to rotate all subsequent scalable text fields at a specified angle. Rotation direction is counter-clockwise about the center point of the text.This rotation remains in effect until another ROTATE command is issued.Default angle is zero degrees.
        /// </summary>
        /// <param name="angle">Degree of rotation (ccw). </param>
        /// <returns></returns>
        public static byte[] ROTATE(int angle)
        {
            string text = $"ROTATE {angle}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// The BARCODE command prints bar codes in both vertical and horizontal orientations at specified widths and heights.
        /// </summary>
        /// <param name="type">Choose from the following table:
        /// <para>Symbology: Use:</para>
        /// <para>UPC-A: UPCA, UPCA2, UPCA5 | UPC-E: UPCE, UPCE2, UPCE5 | EAN/JAN-13: EAN13, EAN132, EAN135 | EAN/JAN-8: EAN8, EAN82, EAN 85 | Code 39: 39, 39C, F39, F39C | Code 93/Ext. 93: 93 | Interleaved 2 of 5: I2OF5 | Interleaved 2 of 5 with checksum: I2OF5C |  German Post Code: I2OF5G | Code 128 (Auto): 128 | UCC EAN 128: UCCEAN128 | Codabar: CODABAR, CODABAR16 | MSI/Plessy: MSI, MSI10, MSI1010, MSI1110 | Postnet: POSTNET | FIM: FIM</para>
        /// </param>
        /// <param name="width">Unit-width of the narrow bar.</param>
        /// <param name="ratio"> Ratio of the wide bar to the narrow bar.
        /// 0 = 1.5:1 | 20 = 2.0:1 | 26 = 2.6:1
        /// 1 = 2.0:1 | 21 = 2.1:1 | 27 = 2.7:1
        /// 2 = 2.5:1 | 22 = 2.2:1 | 28 = 2.8:1
        /// 3 = 3.0:1 | 23 = 2.3:1 | 29 = 2.9:1
        /// 4 = 3.5:1 | 24 = 2.4:1 | 30 = 3.0:1
        /// 25 = 2.5:1</param>
        /// <param name="height"> Unit-height of the bar code.</param>
        /// <param name="x">Horizontal starting position</param>
        /// <param name="y">Vertical starting position</param>
        /// <param name="data">Bar code data.</param>
        /// <returns></returns>
        public static byte[] BARCODE(string type, int width, int ratio, int height, int x, int y, string data)
        {
            string text = $"BARCODE {type} {width} {ratio} {height} {x} {y} {data}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// The BARCODE-TEXT command is used to label bar codes with the same data used to create the bar code.The command eliminates the need to annotate the bar code using separate text commands.The text will be centered below the bar code.
        /// </summary>
        /// <param name="fontNumber">The font number to use when annotating the bar code.</param>
        /// <param name="fontSize">The font size to use when annotating the bar code.</param>
        /// <param name="offset">Unit distance to offset text away from the bar code</param>
        /// <param name="barcodes">Bar code data to be printed</param>
        /// <returns></returns>
        public static byte[] BARCODE_TEXT(int fontNumber, int fontSize, int offset, params byte[] barcodes)
        {
            string text = $"BARCODE-TEXT {fontNumber} {fontSize} {offset} \r\n{barcodes}\r\nBARCODE-TEXT OFF\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// Prints QR Code
        /// </summary>
        /// <param name="x">Horizontal starting position.</param>
        /// <param name="y">Vertical starting position.</param>
        /// <param name="u">Unit-width/Unit-height of the module.
        /// <para>Range is 1 to 32. Default is 6.</para></param>
        /// <param name="data">Describes information required for generating a QR code</param>
        /// <returns></returns>
        public static byte[] QRCODE(int x, int y, int u, string data)
        {
            string text = $"BARCODE QR {x} {y} M 2 U {u}\r\nMA, {data}\r\nENDQR\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// The BOX command provides the user with the ability to produce rectangular shapes of specified line thickness.
        /// </summary>
        /// <param name="x0">X-coordinate of the top left corner.</param>
        /// <param name="y0">Y-coordinate of the top left corner</param>
        /// <param name="x1">X-coordinate of the bottom right corner.</param>
        /// <param name="y1">Y-coordinate of the bottom right corner.</param>
        /// <param name="width">Unit-width (or thickness) of the lines forming the box.</param>
        /// <returns></returns>
        public static byte[] BOX(int x0, int y0, int x1, int y1, int width)
        {
            string text = $"BOX {x0} {y0} {x1} {y1} {width}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Lines of any length, thickness, and angular orientation can be drawn using the LINE command
        /// </summary>
        /// <param name="x0">X-coordinate of the top left corner.</param>
        /// <param name="y0">Y-coordinate of the top left corner</param>
        /// <param name="x1">X-coordinate of the bottom right corner.</param>
        /// <param name="y1">Y-coordinate of the bottom right corner.</param>
        /// <param name="width">Unit-width (or thickness) of the lines forming the box.</param>
        /// <returns></returns>
        public static byte[] LINE(int x0, int y0, int x1, int y1, int width)
        {
            string text = $"LINE {x0} {y0} {x1} {y1} {width}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
        /// <summary>
        /// The CONTRAST command is used to specify the print darkness for the entire label. The lightest printout is at contrast level 0. The darkest contrast level is 3. The printer defaults to contrast level 0 on power up. Contrast level must be specified for each label file.
        /// </summary>
        /// <param name="level"> Contrast level.
        /// <para>0 = Default</para>
        /// <para>1 = Medium</para>
        /// <para>2 = Dark</para>
        /// <para>3 = Very Dark</para></param>
        /// <returns></returns>
        public static byte[] CONTRAST(int level)
        {
            string text = $"CONTRAST {level}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// Alignment of fields can be controlled by using the justification commands. By default, the printer will left justify all fields.A justification command remains in effect for all subsequent fields until another justification command is specified.
        /// </summary>
        /// <param name="level">Choose from the following:
        /// <para>CENTER: Center justifies all subsequent fields.</para>
        /// <para>LEFT: Left justifies all subsequent fields.</para>
        /// <para>RIGHT: Right justifies all subsequent fields</para>
        /// </param>
        /// <returns></returns>
        public static byte[] JUSTIFICATION(string level)
        {
            string text = $"{level}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }

        /// <summary>
        /// This command is used to set the highest motor speed level. Each printer model is programmed with a minimum and maximum attainable speed.The SPEED command selects a speed level within a range of 0 to 5, with 0 the slowest speed.
        /// The maximum speed programmed into each printer model is attainable only under ideal conditions.The battery or power-supply voltage, stock thickness, print darkness, applicator usage, peeler usage, and label length are among the factors that could limit the maximum attainable print speed.
        /// </summary>
        /// <param name="level"> A number between 0 and 5, 0 being the slowest speed.</param>
        /// <returns></returns>
        public static byte[] SPEED(int level)
        {
            string text = $"SPEED {level}\r\n";
            return System.Text.Encoding.ASCII.GetBytes(text);
        }
    }
}
