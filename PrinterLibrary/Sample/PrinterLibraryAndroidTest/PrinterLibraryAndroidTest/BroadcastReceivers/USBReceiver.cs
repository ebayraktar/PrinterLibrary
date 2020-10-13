using System;
using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using Android.Widget;

namespace PrinterLibraryAndroidTest.BroadcastReceivers
{
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { UsbManager.ActionUsbDeviceAttached, UsbManager.ActionUsbAccessoryAttached, UsbManager.ActionUsbDeviceDetached, UsbManager.ActionUsbAccessoryDetached })]
    //[MetaData(UsbManager.ActionUsbDeviceAttached, Resource = "@xml/device_filter")]
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

    //class BroadcastReceiverUsbState : BroadcastReceiver
    //{
    //    public BroadcastReceiverUsbState() { }

    //    public override void OnReceive(Context context, Intent intent)
    //    {
    //        Android.Util.Log.Debug(GetType().ToString(), $"got state broadcast!");
    //        if (context is MainActivity)
    //        {
    //            MainActivity mainActivity = (MainActivity)context;
    //            mainActivity.usbIsDetached = true;
    //            Toast.MakeText(context, $"We got state broadcast: {intent.ToString()}", ToastLength.Long).Show();
    //        }
    //        else
    //        {
    //            Toast.MakeText(context, $"{context.GetType()} got state broadcast: {intent.ToString()}", ToastLength.Long).Show();
    //        }
    //    }
    //}

}