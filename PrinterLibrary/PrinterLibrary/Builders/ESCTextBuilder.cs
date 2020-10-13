using PrinterLibrary.Enums;
using PrinterLibrary.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterLibrary.Builders
{
    internal class ESCTextBuilder : Builder
    {
        readonly List<byte> output;

        public ESCTextBuilder()
        {
            output = new List<byte>();
            output.AddRange(ESC.INITIALIZE_PRINTER());
        }
        public override Builder Barcode(string text, BarcodeType barcodeType, HumanReadable humanReadable)
        {
            output.AddRange(ESC.BARCODE_HEIGHT(81));
            var barcode = barcodeType switch
            {
                BarcodeType.Code128 => 73,
                _ => 0,
            };

            switch (humanReadable)
            {
                case HumanReadable.NotReadable:
                    output.AddRange(ESC.JUSTIFICATION(1));
                    break;
                case HumanReadable.AlignsToLeft:
                    output.AddRange(ESC.JUSTIFICATION(0));
                    //output.AddRange(ESC.LINE_FEED());
                    //output.AddRange(Encoding.UTF8.GetBytes(text));
                    //output.AddRange(ESC.LINE_FEED());
                    break;
                case HumanReadable.AlignsToCenter:
                    output.AddRange(ESC.JUSTIFICATION(1));
                    //output.AddRange(ESC.LINE_FEED());
                    //output.AddRange(Encoding.UTF8.GetBytes(text));
                    //output.AddRange(ESC.LINE_FEED());
                    break;
                case HumanReadable.AlignsToRight:
                    output.AddRange(ESC.JUSTIFICATION(2));
                    break;
                default:
                    output.AddRange(ESC.JUSTIFICATION(1));
                    break;
            }
            output.AddRange(ESC.BARCODE_PRINT(barcode, text, 66));
            output.AddRange(ESC.LINE_FEED());
            output.AddRange(Encoding.UTF8.GetBytes(text));
            output.AddRange(ESC.LINE_FEED());
            output.AddRange(ESC.LINE_FEED());
            return this;
        }

        public override Builder Block(string block, bool center = false)
        {
            output.AddRange(ESC.CHARACTER_SIZE(0, 0));
            if (center)
            {
                output.AddRange(ESC.JUSTIFICATION(1));
            }
            else
            {
                output.AddRange(ESC.JUSTIFICATION(0));
            }
            output.AddRange(Encoding.UTF8.GetBytes(block));
            output.AddRange(ESC.LINE_FEED());
            return this;
        }

        public override byte[] Build()
        {
            output.AddRange(ESC.LINE_FEED());
            output.AddRange(ESC.CUT());
            return output.ToArray();
        }

        public override Builder QRCode(string text, int size, bool center = true)
        {
            if (center)
            {
                output.AddRange(ESC.JUSTIFICATION(1));
            }
            else
            {
                output.AddRange(ESC.JUSTIFICATION(0));
            }

            output.AddRange(ESC.QRCODE_MODEL(50));
            output.AddRange(ESC.QRCODE_SIZE(size));
            output.AddRange(ESC.QRCODE_CORRECTION_LEVEL(49));
            output.AddRange(ESC.QRCODE_STORE(text));
            output.AddRange(ESC.QRCODE_PRINT());
            output.AddRange(ESC.LINE_FEED());
            return this;
        }

        public override Builder SubTitle(string text, bool center = false)
        {

            output.AddRange(ESC.CHARACTER_SIZE(2, 2));
            if (center)
            {
                output.AddRange(ESC.JUSTIFICATION(1));
            }
            else
            {
                output.AddRange(ESC.JUSTIFICATION(0));
            }
            output.AddRange(Encoding.UTF8.GetBytes(text));
            output.AddRange(ESC.LINE_FEED());
            output.AddRange(ESC.CHARACTER_SIZE(0, 0));
            return this;
        }

        public override Builder Text(string text, FontSize fontType, bool center = false)
        {
            switch (fontType)
            {
                case FontSize.Size1:
                    output.AddRange(ESC.CHARACTER_SIZE(0, 0));
                    break;
                case FontSize.Size2:
                    output.AddRange(ESC.CHARACTER_SIZE(0, 1));
                    break;
                case FontSize.Size3:
                    output.AddRange(ESC.CHARACTER_SIZE(1, 1));
                    break;
                default:
                    output.AddRange(ESC.CHARACTER_SIZE(0, 0));
                    break;
            }
            if (center)
            {
                output.AddRange(ESC.JUSTIFICATION(1));
            }
            else
            {
                output.AddRange(ESC.JUSTIFICATION(0));
            }
            output.AddRange(Encoding.UTF8.GetBytes(text));
            output.AddRange(ESC.LINE_FEED());
            return this;
        }

        public override Builder Title(string text, bool center = false)
        {
            output.AddRange(ESC.CHARACTER_SIZE(3, 3));
            if (center)
            {
                output.AddRange(ESC.JUSTIFICATION(1));
            }
            else
            {
                output.AddRange(ESC.JUSTIFICATION(0));
            }
            output.AddRange(Encoding.UTF8.GetBytes(text));
            output.AddRange(ESC.LINE_FEED());
            output.AddRange(ESC.CHARACTER_SIZE(0, 0));
            return this;
        }
    }
}
