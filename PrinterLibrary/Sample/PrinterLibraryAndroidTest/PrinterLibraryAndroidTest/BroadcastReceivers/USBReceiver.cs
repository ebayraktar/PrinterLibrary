using System;
using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using Android.Widget;

namespace PrinterLibraryAndroidTest.BroadcastReceivers
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { UsbManager.ActionUsbDeviceAttached, UsbManager.ActionUsbAccessoryAttached, UsbManager.ActionUsbDeviceDetached, UsbManager.ActionUsbAccessoryDetached })]
    public class USBReceiver : BroadcastReceiver
    {
        public const string ACTION_USB_PERMISSION =
    "com.android.example.USB_PERMISSION";
        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;
            Toast.MakeText(context, "USBReceiver.OnReceive: " + action, ToastLength.Long).Show();
            if (ACTION_USB_PERMISSION.Equals(action))
            {
                UsbDevice device = (UsbDevice)intent.GetParcelableExtra(UsbManager.ExtraDevice);

                if (intent.GetBooleanExtra(UsbManager.ExtraPermissionGranted, false))
                {
                    if (device != null)
                    {
                        //call method to set up device communication
                    }
                }
                else
                {
                    Console.WriteLine("permission denied for device " + device);
                }

            }
        }
    }
}