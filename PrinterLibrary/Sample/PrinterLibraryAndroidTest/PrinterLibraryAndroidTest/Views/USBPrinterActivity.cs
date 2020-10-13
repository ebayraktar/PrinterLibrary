using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using PrinterLibrary.Services;

namespace PrinterLibraryAndroidTest.Views
{
    [Activity(Label = "USBPrinterActivity")]
    public class USBPrinterActivity : Activity
    {
        TextView tvConnectionDescription, tvComment;
        EditText etCommand;

        PrinterService printerService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_usb_printer);
            // Create your application here

            tvConnectionDescription = FindViewById<TextView>(Resource.Id.tvConnectionDescription);
            tvComment = FindViewById<TextView>(Resource.Id.tvComment);
            etCommand = FindViewById<EditText>(Resource.Id.etCommand);

            printerService = new PrinterService(PrinterLibrary.Enums.PrinterConnectionType.USB);

            FindViewById<Button>(Resource.Id.btnQR).Click += BtnQR_Click;
            FindViewById<Button>(Resource.Id.btnBarcode).Click += BtnBarcode_Click;
            FindViewById<Button>(Resource.Id.btnText).Click += BtnText_Click;
            FindViewById<Button>(Resource.Id.btnConnect).Click += Btn_Connect;
            FindViewById<Button>(Resource.Id.btnDisconnect).Click += Btn_Disconnect;

        }

        private void Btn_Disconnect(object sender, EventArgs e)
        {
            if (printerService.IsConnected)
            {
                printerService.Disconnect();
                tvConnectionDescription.Text = "Bağlantı kesildi!";
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Toast.MakeText(this, requestCode, ToastLength.Short).Show();
        }

        private async void Btn_Connect(object sender, EventArgs e)
        {
            try
            {
                string deviceName = "T4e2";
                if (await printerService.Connect(new PrinterLibrary.PrinterConnections.USBConnection(this, deviceName)))
                {
                    tvConnectionDescription.Text = deviceName + " ile bağlantı sağlandı";
                }
                else
                {
                    tvConnectionDescription.Text = "Bağlantı sağlanamadı!";
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                tvConnectionDescription.Text = "Hata oluştu: " + ex.Message;
            }
        }

        private async void Print(byte[] command)
        {
            tvComment.Text = System.Text.Encoding.UTF8.GetString(command);
            if (printerService.IsConnected)
            {
                try
                {
                    int count = await printerService.Print(command);
                    Console.WriteLine("Count: " + count);
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Önce bağlantı oluşturun", ToastLength.Long).Show();
            }
        }

        private void BtnText_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etCommand.Text))
                return;
            var builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.EPL)
                .Text("Test starts", PrinterLibrary.Enums.FontSize.Size1, true)
                .Text("Font Size 3", PrinterLibrary.Enums.FontSize.Size3, true)
                .Text("Font Size 3", PrinterLibrary.Enums.FontSize.Size3, false)
                .Text("Font Size 2", PrinterLibrary.Enums.FontSize.Size2, true)
                .Text("Font Size 2", PrinterLibrary.Enums.FontSize.Size2, false)
                .Text("Font Size 1", PrinterLibrary.Enums.FontSize.Size1, true)
                .Text("Font Size 1", PrinterLibrary.Enums.FontSize.Size1, false)
                .Block("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris iaculis quam augue, vel dictum nunc lacinia in. Phasellus venenatis semper convallis. Cras interdum turpis bibendum orci ultrices dictum. Mauris maximus consectetur mauris, nec eleifend ex vestibulum at. Pellentesque orci nisl, luctus sed risus id, hendrerit posuere purus. Donec", true)
                .Text("Test ends", PrinterLibrary.Enums.FontSize.Size1, true);

            Print(builder.Build());
        }

        private void BtnBarcode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etCommand.Text))
                return;
            var builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.EPL)
                .Text("Barcode test starts", PrinterLibrary.Enums.FontSize.Size1, true)
                .Barcode(etCommand.Text, PrinterLibrary.Enums.BarcodeType.Code128, PrinterLibrary.Enums.HumanReadable.AlignsToCenter)
                .Barcode(etCommand.Text, PrinterLibrary.Enums.BarcodeType.Code128, PrinterLibrary.Enums.HumanReadable.NotReadable)
                .Text("Test ends", PrinterLibrary.Enums.FontSize.Size1, true);

            Print(builder.Build());
        }

        private void BtnQR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etCommand.Text))
                return;
            var builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.EPL)
                .Text("QR test starts", PrinterLibrary.Enums.FontSize.Size1, true)
                .QRCode(etCommand.Text, 3, true)
                .Text("QR test ends", PrinterLibrary.Enums.FontSize.Size1, true);

            Print(builder.Build());
        }
    }
}