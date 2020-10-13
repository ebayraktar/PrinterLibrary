using System;

namespace PrinterLibrary.Exceptions
{
    /// <summary>
    /// Yazıcı hataları
    /// </summary>
    public class PrinterException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public PrinterException() : base()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public PrinterException(string message) : base(message)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inncerException"></param>
        public PrinterException(string message, Exception inncerException) : base(message, inncerException)
        {

        }
    }
}
