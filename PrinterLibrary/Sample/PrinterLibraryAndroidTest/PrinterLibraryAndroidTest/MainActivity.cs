using Android.App;
using Android.OS;

namespace PrinterLibraryAndroidTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            FindViewById(Resource.Id.btnBluetoothPrinter).Click += (s, e) =>
            {
                StartActivity(new Android.Content.Intent(this, typeof(Views.BluetoothPrinterActivity)));
            };
            FindViewById(Resource.Id.btnNetworkPrinter).Click += (s, e) =>
            {
                StartActivity(new Android.Content.Intent(this, typeof(Views.NetworkPrinterActivity)));
            };
            FindViewById(Resource.Id.btnUSBPrinter).Click += (s, e) =>
            {
                StartActivity(new Android.Content.Intent(this, typeof(Views.USBPrinterActivity)));
            };

        }
    }
}