using PrinterLibrary.Interfaces;

namespace PrinterLibrary.Configurations
{
    /// <summary>
    /// To be Added
    /// </summary>
    public abstract class Configuration
    {
        /// <summary>
        /// To be Added
        /// </summary>
        public abstract void ClearBuffer();

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract void SetBackFeed(int length);

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract void SetDensity(int darkness);

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract void SetDirection(bool xMirror = true, bool yMirror = false);

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract void SetFeed(int length);

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract Configuration SetGap(int m, int n);

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract Configuration SetPrintCopy(int count);

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract Configuration SetSize(int width, int height);

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract void SetSpeed(int printSpeed);

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract Configuration SetSubTitleSize(int size);

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract Configuration SetTextSize(int size);

        /// <summary>
        /// To be Added
        /// </summary>
        public abstract Configuration SetTitleSize(int size);
    }
}
