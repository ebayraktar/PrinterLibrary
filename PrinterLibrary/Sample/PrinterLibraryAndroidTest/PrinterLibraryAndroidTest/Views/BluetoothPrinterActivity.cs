using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.OS;
using Android.Widget;
using BluetoothLibrary.Models;
using PrinterLibrary.Services;

namespace PrinterLibraryAndroidTest.Views
{
    [Activity(Label = "BluetoothPrinterActivity")]
    public class BluetoothPrinterActivity : BluetoothLibrary.Activities.BluetoothRequestActivity
    {
        TextView tvBluetoothName;
        EditText etCommand;
        Spinner spnrPrinterType;
        PrinterService printerService;
        int printerType;
        bool isDiscovered;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_bluetooth_printer);
            // Create your application here
            tvBluetoothName = FindViewById<TextView>(Resource.Id.tvBluetoothName);

            etCommand = FindViewById<EditText>(Resource.Id.etCommand);
            spnrPrinterType = FindViewById<Spinner>(Resource.Id.spnrPrinterType);

            //printerService.Connect(new PrinterLibrary.PrinterConnections.BluetoothConnection("RF-BHS"));
            FindViewById<Button>(Resource.Id.btnConnect).Click += BtnConnect_Click;
            FindViewById<Button>(Resource.Id.btnDisconnect).Click += BtnDisconnect_Click;
            FindViewById<Button>(Resource.Id.btnQR).Click += BtnQR_Click;
            FindViewById<Button>(Resource.Id.btnBarcode).Click += BtnBarcode_Click;
            FindViewById<Button>(Resource.Id.btnText).Click += BtnText_Click;
            spnrPrinterType.ItemSelected += SpnrPrinterType_ItemSelected;
            Initialize();
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            if (printerService.IsConnected)
            {
                tvBluetoothName.Text = "Bağlantı kesildi";
                printerService.Disconnect();
            }
        }

        private void SpnrPrinterType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            printerType = e.Position;
        }

        private void Initialize()
        {
            isDiscovered = false;
            printerType = 0;

            var adapter = ArrayAdapter<string>.CreateFromResource(this, Resource.Array.printerTypes, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spnrPrinterType.Adapter = adapter;

            printerService = new PrinterService(PrinterLibrary.Enums.PrinterConnectionType.Bluetooth);

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
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (isDiscovered)
            {
                ShowDiscoveredDevices("KEŞFEDİLMİŞ CİHAZLARA BAĞLAN", async (callback) =>
                {
                    if (callback is BluetoothCallback bluetoothCallback)
                    {
                        if (bluetoothCallback.IsSuccess)
                        {
                            var device = bluetoothCallback.Device;
                            if (await printerService.Connect(new PrinterLibrary.PrinterConnections.BluetoothConnection(device.Name)))
                            {
                                Toast.MakeText(this, device.Name + " eşlendi", ToastLength.Short).Show();
                                tvBluetoothName.Text = device.Name + " eşlendi";
                            }
                            else
                            {
                                Toast.MakeText(this, device.Name + " eşleşemedi!", ToastLength.Short).Show();
                                tvBluetoothName.Text = device.Name + " eşleşemedi!";
                            }
                        }
                        else
                        {
                            Toast.MakeText(this, bluetoothCallback.ErrorMessage, ToastLength.Short).Show();
                        }
                    }
                });
            }
            else
            {
                ConnectDevice();
            }
        }
        protected override void OnPause()
        {
            if (printerService.IsConnected)
            {
                printerService.Disconnect();
            }
            base.OnPause();
        }
        private void BtnText_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etCommand.Text))
                return;

            PrinterService.TextBuilder builder;
            if (printerType == 0)
                builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.TSPL);
            else
                builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.CPCL);
            builder.Text("Test starts", PrinterLibrary.Enums.FontSize.Size1, true)
            .Title("Title", true)
            .SubTitle("Subtitle", true)
            .Text("Font Size 3", PrinterLibrary.Enums.FontSize.Size3, true)
            .Text("Font Size 2", PrinterLibrary.Enums.FontSize.Size2, true)
            .Text("Font Size 1", PrinterLibrary.Enums.FontSize.Size1, true)
            .Block("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris iaculis quam augue, vel dictum nunc lacinia in. Phasellus venenatis semper convallis. Cras interdum turpis bibendum orci ultrices dictum. Mauris maximus consectetur mauris, nec eleifend ex vestibulum at. Pellentesque orci nisl, luctus sed risus id, hendrerit posuere purus. Donec", true)
            .Text(etCommand.Text, PrinterLibrary.Enums.FontSize.Size3, true)
            .Barcode(etCommand.Text, PrinterLibrary.Enums.BarcodeType.Code128, PrinterLibrary.Enums.HumanReadable.AlignsToCenter)
            .QRCode(etCommand.Text, 6, true)
            .Text("Test ends", PrinterLibrary.Enums.FontSize.Size1, true);

            Print(builder.Build());
        }
        private void BtnBarcode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etCommand.Text))
                return;

            PrinterService.TextBuilder builder;
            if (printerType == 0)
                builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.TSPL);
            else
                builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.CPCL);
            builder.Barcode(etCommand.Text, PrinterLibrary.Enums.BarcodeType.Code128, PrinterLibrary.Enums.HumanReadable.AlignsToCenter);

            Print(builder.Build());
        }

        private void BtnQR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(etCommand.Text))
                return;

            PrinterService.TextBuilder builder;
            if (printerType == 0)
                builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.TSPL);
            else
                builder = new PrinterService.TextBuilder(PrinterLibrary.Enums.PrinterLanguageType.CPCL);
            builder.QRCode(etCommand.Text, 6, true);

            Print(builder.Build());
        }



        public override void OnBluetoothNotFound()
        {
            Toast.MakeText(this, "Bluetooth bulunamadı", ToastLength.Short).Show();
            tvBluetoothName.Text = "Bluetooth bulunamadı";
        }

        public override void OnError(string message)
        {
            tvBluetoothName.Text = "HATA: " + message;
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }

        public override async void OnPaired(string deviceName)
        {
            isDiscovered = true;
            if (await printerService.Connect(new PrinterLibrary.PrinterConnections.BluetoothConnection(deviceName)))
            {
                Toast.MakeText(this, deviceName + " eşlendi", ToastLength.Short).Show();
                tvBluetoothName.Text = deviceName + " eşlendi";
            }
            else
            {
                Toast.MakeText(this, deviceName + " eşleşemedi!", ToastLength.Short).Show();
                tvBluetoothName.Text = deviceName + " eşleşemedi!";
            }
        }

        public override void OnPermissionsDenied(string[] permissions)
        {
            tvBluetoothName.Text = "İzinler reddedildi";
            Toast.MakeText(this, "İzinler reddedildi", ToastLength.Short).Show();
        }

        public override void OnBluetoothEnableRequestIsDenied()
        {
            tvBluetoothName.Text = "Bluetooth açma isteği reddedildi";
            Toast.MakeText(this, "Bluetooth açma isteği reddedildi", ToastLength.Short).Show();
        }
    }
}