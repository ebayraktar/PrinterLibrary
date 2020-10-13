namespace PrinterLibrary.Enums
{
    /// <summary>
    /// Ölçü birimi
    /// </summary>
    public enum Measurement
    {
        /// <summary>
        /// Varsayılan (inch)
        /// </summary>
        Default = 0,
        /// <summary>
        /// inch
        /// </summary>
        inch = 0,
        /// <summary>
        /// mm 
        /// </summary>
        mm = 1,
        /// <summary>
        /// dot
        /// </summary>
        dot = 2
    }
    /// <summary>
    /// Hizalama
    /// </summary>
    public enum Alignment
    {
        /// <summary>
        /// Varsayılan (Sol)
        /// </summary>
        Default = 0,
        /// <summary>
        /// Solda
        /// </summary>
        Left = 1,
        /// <summary>
        /// Ortala
        /// </summary>
        Center = 2,
        /// <summary>
        /// Sağda
        /// </summary>
        Right = 3
    }

    /// <summary>
    /// Yazı boyutu
    /// </summary>
    public enum FontSize
    {
        /// <summary>
        /// Boyut 1
        /// </summary>
        Size1 = 1,
        /// <summary>
        /// Boyut 2
        /// </summary>
        Size2 = 2,
        /// <summary>
        /// Boyut 3
        /// </summary>
        Size3 = 3,
    }

    /// <summary>
    /// Döndürme
    /// </summary>
    public enum Rotation
    {
        /// <summary>
        /// Döndürme yok
        /// </summary>
        NoRotation = 0,
        /// <summary>
        /// 90 derece
        /// </summary>
        Degrees90 = 90,
        /// <summary>
        /// 180 derece
        /// </summary>
        Degrees180 = 180,
        /// <summary>
        /// 270 derece
        /// </summary>
        Degrees270 = 270,
    }

    /// <summary>
    /// Okunabilir Text
    /// </summary>
    public enum HumanReadable
    {
        /// <summary>
        /// Text eklemez
        /// </summary>
        NotReadable = 0,
        /// <summary>
        /// Text solda
        /// </summary>
        AlignsToLeft = 1,
        /// <summary>
        /// Text ortada
        /// </summary>
        AlignsToCenter = 2,
        /// <summary>
        /// Text sağda
        /// </summary>
        AlignsToRight = 3
    }

    /// <summary>
    /// QR Code hata oranları
    /// </summary>
    public enum ECCLevel
    {
        /// <summary>
        /// %7
        /// </summary>
        L = 7,
        /// <summary>
        /// %15
        /// </summary>
        M = 15,
        /// <summary>
        /// %25
        /// </summary>
        Q = 25,
        /// <summary>
        /// %30
        /// </summary>
        H = 30
    }

    /// <summary>
    /// Barkod tipleri
    /// </summary>
    public enum BarcodeType
    {
        /// <summary>
        /// Barcode128
        /// </summary>
        Code128 = 128
    }
}
