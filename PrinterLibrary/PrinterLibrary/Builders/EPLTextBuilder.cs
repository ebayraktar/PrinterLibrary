using PrinterLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterLibrary.Builders
{
    internal class EPLTextBuilder : Builder
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
        public EPLTextBuilder()
        {
            output = new List<byte>();
            output.AddRange(Languages.EPL.CLEAR_BUFFER());
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
        //private int CalculateSubTextHeight(string text, FontSize fontType)
        //{
        //    int width;
        //    int height;
        //    switch (fontType)
        //    {
        //        case FontSize.Size1:
        //            width = 8;
        //            height = 12;
        //            break;
        //        case FontSize.Size2:
        //            width = 10;
        //            height = 16;
        //            break;
        //        case FontSize.Size3:
        //            width = 12;
        //            height = 20;
        //            break;
        //        default:
        //            width = 10;
        //            height = 16;
        //            break;
        //    }

        //    return (((text.Length / width) + (text.Length % width > 0 ? 1 : 0)) * height) + 5;
        //}
        private string[] BlockBuilder(string block, int width)
        {
            if (string.IsNullOrEmpty(block))
                return null;


            int count = block.Length / width;
            if (block.Length % width != 0)
                count++;

            string[] tempBlock = new string[count];

            int length = width;
            for (int i = 0; i < count; i++)
            {
                int startIndex = System.Math.Min(i * width, block.Length - 1);
                if (startIndex + length > block.Length)
                    length = block.Length - startIndex;

                tempBlock[i] = block.Substring(startIndex, length);
            }
            return tempBlock;
        }
        private int CalculateCenter(int length, int fontWidth)
        {
            if (length >= (Width / fontWidth))
                return 0;

            return Math.Abs((Width / 2) - (length * (fontWidth / 2)));
        }
        private int GetFontWidth(int fontSize)
        {
            return fontSize switch
            {
                1 => 10,
                2 => 12,
                3 => 14,
                4 => 16,
                5 => 34,
                _ => 10,
            };
        }
        public override Builder Barcode(string text, BarcodeType barcodeType, HumanReadable humanReadable)
        {
            int wideWidth;
            int narrowWidth;
            int barcode;
            switch (barcodeType)
            {
                case BarcodeType.Code128:
                    barcode = 1;
                    narrowWidth = 1;
                    wideWidth = 1;
                    break;
                default:
                    barcode = 1;
                    narrowWidth = 1;
                    wideWidth = 1;
                    break;
            }

            string hr;
            int x;
            switch (humanReadable)
            {
                case HumanReadable.NotReadable:
                    x = DefaultX;
                    hr = "N";
                    break;
                case HumanReadable.AlignsToLeft:
                    x = DefaultX;
                    hr = "B";
                    break;
                case HumanReadable.AlignsToCenter:
                    x = CalculateCenter(text.Length, GetFontWidth(3));
                    hr = "B";
                    break;
                case HumanReadable.AlignsToRight:
                    x = DefaultX;
                    hr = "B";
                    break;
                default:
                    x = DefaultX;
                    hr = "N";
                    break;
            }
            output.AddRange(Languages.EPL.BARCODE(x, Index, 0, barcode, narrowWidth, wideWidth, 100, hr, text));
            Index += 100;
            if (humanReadable != HumanReadable.NotReadable)
            {
                Index += 35;
            }
            return this;
        }

        public override Builder Block(string block, bool center = false)
        {
            //FontSize fontType = FontSize.Size1;
            //int textHeight = CalculateSubTextHeight(block, fontType);
            //fontType 2 => width = 43
            string[] tempText = BlockBuilder(block, Width / 10);
            foreach (var text in tempText)
            {
                output.AddRange(Languages.EPL.TEXT(center ? CalculateCenter(text.Length, GetFontWidth(1)) : DefaultX, Index, 0, 1, 1, 1, "N", text.Trim()));
                Index += 15;
            }
            return this;
        }

        public override byte[] Build()
        {
            output.AddRange(Languages.EPL.PRINT(CopyCount));
            return output.ToArray();
        }

        public override Builder QRCode(string text, int size, bool center = true)
        {
            Index += 10;
            int cellWidth = size;
            //25 is default
            int qrSide = cellWidth * 25;
            output.AddRange(Languages.EPL.QRCODE(DefaultX, Index, "Q", text, 2, size));
            Index += (qrSide + 15);
            return this;
        }

        public override Builder SubTitle(string text, bool center = false)
        {
            int fontSize = 3;
            output.AddRange(Languages.EPL.TEXT(center ? CalculateCenter(text.Length, GetFontWidth(fontSize)) : DefaultX, Index, 0, fontSize, 1, 1, "N", text));
            Index += 20;
            return this;
        }

        public override Builder Text(string text, FontSize fontType, bool center = false)
        {
            int fontSize;
            int textSize;
            switch (fontType)
            {
                case FontSize.Size1:
                    fontSize = 1;
                    textSize = 15;
                    break;
                case FontSize.Size2:
                    fontSize = 2;
                    textSize = 20;
                    break;
                case FontSize.Size3:
                    fontSize = 4;
                    textSize = 25;
                    break;
                default:
                    fontSize = 1;
                    textSize = 15;
                    break;
            }

            output.AddRange(Languages.EPL.TEXT(center ? CalculateCenter(text.Length, GetFontWidth(fontSize)) : DefaultX, Index, 0, fontSize, 1, 1, "N", text));
            Index += textSize;
            return this;
        }

        public override Builder Title(string text, bool center = false)
        {
            int fontSize = 4;
            output.AddRange(Languages.EPL.TEXT(center ? CalculateCenter(text.Length, GetFontWidth(fontSize)) : DefaultX, Index, 0, fontSize, 1, 1, "N", text));
            Index += 25;
            return this;
        }
    }
}
