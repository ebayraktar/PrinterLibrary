using PrinterLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterLibrary.Builders
{
    internal class CPCLTextBuilder : Builder
    {

        readonly List<byte> output;
        private int DefaultX { get; set; }

        private int Index { get; set; }
        private int Height { get; set; }
        private int Width { get; set; }
        private int SubTitleSize { get; set; }
        private int TitleSize { get; set; }
        private int TextSize { get; set; }
        private int CopyCount { get; set; }
        private int DefaultDensity { get; set; }
        public CPCLTextBuilder()
        {
            output = new List<byte>();
            Width = 560;
            Height = 0;
            DefaultX = 5;
            TitleSize = 12;
            SubTitleSize = 10;
            TextSize = 8;
            CopyCount = 1;
            Index = 10;
            DefaultDensity = 8;
        }

        private int CalculateSubTextHeight(string text, FontSize fontType)
        {
            int width;
            int height;
            switch (fontType)
            {
                case FontSize.Size1:
                    width = 0;
                    height = 0;
                    break;
                case FontSize.Size2:
                    width = 48;
                    height = 23;
                    break;
                case FontSize.Size3:
                    width = 0;
                    height = 0;
                    break;
                default:
                    width = 48;
                    height = 23;
                    break;
            }

            return (((text.Length / width) + (text.Length % width > 0 ? 1 : 0)) * height) + 5;
        }

        private string BlockBuilder(string block, int width)
        {
            if (string.IsNullOrEmpty(block))
                return block;

            string tempBlock = "";

            int count = block.Length / width;
            if (block.Length % width != 0)
                count++;

            int length = width;
            for (int i = 0; i < count; i++)
            {
                int startIndex = System.Math.Min(i * width, block.Length - 1);
                if (startIndex + length > block.Length)
                    length = block.Length - startIndex;

                tempBlock += block.Substring(startIndex, length) + "\n";
            }
            return tempBlock;
        }
        public override Builder Barcode(string text, BarcodeType barcodeType, HumanReadable humanReadable)
        {
            string barcode = barcodeType switch
            {
                BarcodeType.Code128 => "128",
                _ => "",
            };
            switch (humanReadable)
            {
                case HumanReadable.NotReadable:
                    output.AddRange(Languages.CPCL.JUSTIFICATION("CENTER"));
                    break;
                case HumanReadable.AlignsToLeft:
                    output.AddRange(Languages.CPCL.JUSTIFICATION("LEFT"));
                    break;
                case HumanReadable.AlignsToCenter:
                    output.AddRange(Languages.CPCL.JUSTIFICATION("CENTER"));
                    break;
                case HumanReadable.AlignsToRight:
                    output.AddRange(Languages.CPCL.JUSTIFICATION("RIGHT"));
                    break;
                default:
                    output.AddRange(Languages.CPCL.JUSTIFICATION("CENTER"));
                    break;
            }
            output.AddRange(Languages.CPCL.BARCODE(barcode, 1, 1, 100, 0, Index, text));
            Index += 100;
            if (humanReadable != HumanReadable.NotReadable)
            {
                Index += 15;
                output.AddRange(Languages.CPCL.TEXT(7, 0, 0, Index, text));
                Index += 15;
            }
            Index += 15;
            return this;
        }

        public override Builder Block(string block, bool center = false)
        {
            if (center)
            {
                output.AddRange(Languages.CPCL.JUSTIFICATION("CENTER"));
            }
            else
            {
                output.AddRange(Languages.CPCL.JUSTIFICATION("LEFT"));
            }
            FontSize fontType = FontSize.Size2;
            int textHeight = CalculateSubTextHeight(block, fontType);
            //fontType 2 => width = 48
            string tempText = BlockBuilder(block, 48);
            output.AddRange(Languages.CPCL.MULTILINE(20, 7, 0, 0, Index, tempText));
            Index += textHeight;
            return this;
        }

        public override byte[] Build()
        {
            Index += 50;
            string Command = Encoding.UTF8.GetString(output.ToArray());
            string _command = $"! 0 200 200 {Index} {CopyCount}\r\n{Command}\r\nPRINT\r\n";
            Command = _command;

            return Encoding.ASCII.GetBytes(Command);
        }

        public override Builder QRCode(string text, int size, bool center = true)
        {
            Index += 10;
            int cellWidth = size;
            //25 is default
            int qrSide = cellWidth * 25;
            if (center)
            {
                output.AddRange(Languages.CPCL.JUSTIFICATION("CENTER"));
            }
            else
            {
                output.AddRange(Languages.CPCL.JUSTIFICATION("LEFT"));
            }
            output.AddRange(Languages.CPCL.QRCODE(0, Index, cellWidth, text));
            Index += (qrSide + 15);
            return this;
        }

        public override Builder SubTitle(string text, bool center = false)
        {
            if (center)
            {
                output.AddRange(Languages.CPCL.JUSTIFICATION("CENTER"));
            }
            else
            {
                output.AddRange(Languages.CPCL.JUSTIFICATION("LEFT"));
            }
            output.AddRange(Languages.CPCL.TEXT(5, 2, 0, Index, text));
            Index += 50;
            return this;
        }

        public override Builder Text(string text, FontSize fontType, bool center = false)
        {
            int font;
            int size;
            int textSize;
            switch (fontType)
            {
                case FontSize.Size1:
                    font = 5;
                    size = 0;
                    textSize = 30;
                    break;
                case FontSize.Size2:
                    font = 5;
                    size = 1;
                    textSize = 50;
                    break;
                case FontSize.Size3:
                    font = 5;
                    size = 2;
                    textSize = 50;
                    break;
                default:
                    font = 5;
                    size = 0;
                    textSize = 30;
                    break;
            }

            if (center)
            {
                output.AddRange(Languages.CPCL.JUSTIFICATION("CENTER"));
            }
            else
            {
                output.AddRange(Languages.CPCL.JUSTIFICATION("LEFT"));
            }
            output.AddRange(Languages.CPCL.TEXT(font, size, 0, Index, text));
            Index += textSize;
            return this;
        }

        public override Builder Title(string text, bool center = false)
        {
            if (center)
            {
                output.AddRange(Languages.CPCL.JUSTIFICATION("CENTER"));
            }
            else
            {
                output.AddRange(Languages.CPCL.JUSTIFICATION("LEFT"));
            }
            output.AddRange(Languages.CPCL.TEXT(5, 3, 0, Index, text));
            Index += 70;
            return this;
        }
    }
}
