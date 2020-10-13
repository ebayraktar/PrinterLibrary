using PrinterLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterLibrary.Builders
{
    internal class TSPLTextBuilder : Builder
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
        public TSPLTextBuilder()
        {
            output = new List<byte>();
            Width = 575;
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
                    width = 56;
                    height = 17;
                    break;
                case FontSize.Size2:
                    width = 39;
                    height = 25;
                    break;
                case FontSize.Size3:
                    width = 20;
                    height = 30;
                    break;
                default:
                    width = 56;
                    height = 17;
                    break;
            }

            return (((text.Length / width) + (text.Length % width > 0 ? 1 : 0)) * height) + 5;
        }

        public override Builder Barcode(string text, BarcodeType barcodeType, HumanReadable humanReadable)
        {
            string barcode = barcodeType switch
            {
                BarcodeType.Code128 => "128",
                _ => "",
            };

            output.AddRange(Languages.TSPL.BARCODE(Width / 2, Index, barcode, 100, (int)humanReadable, 0, 3, 3, 2, text));
            if (humanReadable == HumanReadable.NotReadable)
                Index += 115;
            else
                Index += 130;
            return this;
        }

        public override Builder Block(string block, bool center = false)
        {
            FontSize fontType = FontSize.Size2;
            int textHeight = CalculateSubTextHeight(block, fontType);
            output.AddRange(Languages.TSPL.BLOCK(DefaultX, Index, Width, textHeight, "2", 0, 1, 1, (center ? 2 : 0), block));
            Index += textHeight;
            return this;
        }

        public override byte[] Build()
        {

            Index += 10;
            string density = "";
            string Command = Encoding.UTF8.GetString(output.ToArray());
            if (Command.Contains("QRCODE"))
            {
                density = "DENSITY 15";
            }
            string _command = $@"
            SIZE {Width}mm, {(Height != 0 ? Height + "mm" : Index + "dot")}
            GAP 0,0
            DIRECTION 1
            {density}
            CLS
            {Command}
            PRINT {CopyCount}
            SOUND 2,200
            ";
            Command = _command;

            return Encoding.ASCII.GetBytes(Command);
        }

        public override Builder QRCode(string text, int size, bool center = true)
        {
            Index += 10;
            int cellWidth = size;
            //25 is default
            int qrSide = cellWidth * 25;
            int x = DefaultX;
            if (center)
                //center x
                x = System.Math.Abs(Width - qrSide) / 2;

            output.AddRange(Languages.TSPL.QRCODE(x, Index, "H", cellWidth, "A", 0, text));
            Index += (qrSide + 15);
            return this;

        }

        public override Builder SubTitle(string text, bool center = false)
        {
            output.AddRange(Languages.TSPL.TEXT((center ? Width / 2 : DefaultX), Index, "3", 0, 1, 1, center ? 2 : 0, text));
            Index += 45;
            return this;
        }

        public override Builder Text(string text, FontSize fontType, bool center = false)
        {
            string font = fontType switch
            {
                FontSize.Size1 => "1",
                FontSize.Size2 => "2",
                FontSize.Size3 => "3",
                _ => "1",
            };
            output.AddRange(Languages.TSPL.TEXT((center ? Width / 2 : DefaultX), Index, font, 0, 1, 1, center ? 2 : 0, text));
            Index += 45;
            return this;
        }

        public override Builder Title(string text, bool center = false)
        {
            output.AddRange(Languages.TSPL.TEXT((center ? Width / 2 : DefaultX), Index, "5", 0, 1, 1, center ? 2 : 0, text));
            Index += 70;
            return this;
        }
    }
}
