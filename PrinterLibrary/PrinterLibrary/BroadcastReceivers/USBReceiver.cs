using System;
using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using Android.Widget;

namespace PrinterLibrary.BroadcastReceivers
{
    /// <summary>
    /// 
    /// </summary>
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { UsbManager.ActionUsbDeviceAttached, UsbManager.ActionUsbAccessoryAttached, UsbManager.ActionUsbDeviceDetached, UsbManager.ActionUsbAccessoryDetached })]
    public class USBReceiver : BroadcastReceiver
    {
        /// <summary>
        /// 
        /// </summary>
        public const string ACTION_USB_PERMISSION =
    "com.Bayraktar.PrinterLibrary.USB_PERMISSION";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="intent"></param>
        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;
            if (ACTION_USB_PERMISSION.Equals(action))
            {
                UsbDevice device = (UsbDevice)intent.GetParcelableExtra(UsbManager.ExtraDevice);
                Toast.MakeText(context, "USB_PERMISSION: " + action, ToastLength.Short).Show();
                if (intent.GetBooleanExtra(UsbManager.ExtraPermissionGranted, false))
                {
                    Toast.MakeText(context, "İzin alındı", ToastLength.Short).Show();
                    if (device != null)
                    {
                        Toast.MakeText(context, "Cihaz: " + device.DeviceName, ToastLength.Short).Show();
                        //call method to set up device communication
                    }
                    else
                    {
                        Toast.MakeText(context, "Cihaz yok", ToastLength.Short).Show();

                    }
                }
                else
                {
                    Toast.MakeText(context, "İzin alınamadı", ToastLength.Short).Show();
                }

            }
        }
    }
}
