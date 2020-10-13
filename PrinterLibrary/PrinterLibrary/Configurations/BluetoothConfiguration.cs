using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterLibrary.Configurations
{
    internal class BluetoothConfiguration : Configuration
    {
        public int GapM { get; set; }
        public int GapN { get; set; }
        public int CopyCount { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TitleSize { get; set; }
        public int SubTitleSize { get; set; }
        public int TextSize { get; set; }
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }

        public BluetoothConfiguration()
        {

        }

        public override void ClearBuffer()
        {
            throw new NotImplementedException();
        }

        public override void SetBackFeed(int length)
        {
            throw new NotImplementedException();
        }

        public override void SetDensity(int darkness)
        {
            throw new NotImplementedException();
        }

        public override void SetDirection(bool xMirror = true, bool yMirror = false)
        {
            DirectionX = xMirror ? 1 : 0;
            DirectionY = yMirror ? 1 : 0;
        }

        public override void SetFeed(int length)
        {
            throw new NotImplementedException();
        }

        public override Configuration SetGap(int m, int n)
        {
            GapM = m;
            GapN = n;
            return this;
        }

        public override Configuration SetPrintCopy(int count)
        {
            CopyCount = count;
            return this;
        }

        public override Configuration SetSize(int width, int height)
        {
            Width = width;
            Height = height;
            return this;
        }

        public override void SetSpeed(int printSpeed)
        {
            throw new NotImplementedException();
        }

        public override Configuration SetSubTitleSize(int size)
        {
            SubTitleSize = size;
            return this;
        }

        public override Configuration SetTextSize(int size)
        {
            TextSize = size;
            return this;
        }

        public override Configuration SetTitleSize(int size)
        {
            TitleSize = size;
            return this;
        }
    }
}
