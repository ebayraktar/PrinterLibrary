using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using Android.OS;
using Android.Widget;
using PrinterLibrary.Services;
using PrinterLibraryAndroidTest.BroadcastReceivers;

namespace PrinterLibraryAndroidTest.Views
{
    [Activity(Label = "NetworkPrinterActivity")]
    public class NetworkPrinterActivity : Activity
    {
        TextView tvConnectionDescription;
        EditText etHost, etPort, etCommand;
        PrinterService printerService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_network_printer);
            // Create your application here
            tvConnectionDescription = FindViewById<TextView>(Resource.Id.tvConnectionDescription);

            etHost = FindViewById<EditText>(Resource.Id.etHost);
            etPort = FindViewById<EditText>(Resource.Id.etPort);
            etCommand = FindViewById<EditText>(Resource.Id.etCommand);

            printerService = new PrinterService(PrinterLibrary.Enums.PrinterConnectionType.Network);

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
        private async void Btn_Connect(object sender, EventArgs e)
        {
            try
            {
                if (await printerService.Connect(new PrinterLibrary.PrinterConnections.NetworkConnection(etHost.Text, int.Parse(etPort.Text))))
                {
                    tvConnectionDescription.Text = etHost.Text + ":" + etPort.Text + " ile bağlantı sağlandı";
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
            var builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.ESC)
                .Text("Test starts", PrinterLibrary.Enums.FontSize.Size1, true)
                .Title("Title", true)
                .SubTitle("Subtitle", true)
                .Text("Font Size 3", PrinterLibrary.Enums.FontSize.Size3, true)
                .Text("Font Size 2", PrinterLibrary.Enums.FontSize.Size2, true)
                .Text("Font Size 1", PrinterLibrary.Enums.FontSize.Size1, true)
                .Block("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris iaculis quam augue, vel dictum nunc lacinia in. Phasellus venenatis semper convallis. Cras interdum turpis bibendum orci ultrices dictum. Mauris maximus consectetur mauris, nec eleifend ex vestibulum at. Pellentesque orci nisl, luctus sed risus id, hendrerit posuere purus. Donec", true)
                .Text(etCommand.Text, PrinterLibrary.Enums.FontSize.Size3, true)
                .Barcode(etCommand.Text, PrinterLibrary.Enums.BarcodeType.Code128, PrinterLibrary.Enums.HumanReadable.AlignsToCenter)
                .QRCode(etCommand.Text, 6, true)
                .Text("Test ends", PrinterLibrary.Enums.FontSize.Size1, true)
                .Build();
            string text = "Satır 1\r\nSatır2\r\nSatır3\nSadece Yeni Satır\tTab";
            Print(Encoding.UTF8.GetBytes(text));
        }

        private void BtnBarcode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etCommand.Text))
                return;
            var builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.ESC)
                   .Barcode(etCommand.Text, PrinterLibrary.Enums.BarcodeType.Code128, PrinterLibrary.Enums.HumanReadable.AlignsToCenter);

            Print(builder.Build());
        }

        private void BtnQR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etCommand.Text))
                return;
            var builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.ESC)
                .QRCode(etCommand.Text, 6, true);

            Print(builder.Build());
        }
    }
}