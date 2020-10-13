using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using PrinterLibrary.BroadcastReceivers;
using PrinterLibrary.PrinterConnections;
using System;
using System.Threading.Tasks;

namespace PrinterLibrary.Printers
{
    internal class USBPrinter : Printer
    {
        UsbDevice usbDevice;
        UsbManager usbManager;
        UsbDeviceConnection connection;
        UsbInterface intf;
        PendingIntent permissionIntent;
        UsbEndpoint mEndpointBulkOut;

        Context context;
        readonly USBReceiver usbReceiver;
        public USBPrinter()
        {
            usbReceiver = new USBReceiver();
        }

        public override Task<bool> Connect(PrinterConnection connection)
        {
            return Task.Run(() =>
            {
                if (connection is USBConnection nc)
                {
                    context = nc.Context;
                    permissionIntent = PendingIntent.GetBroadcast(context, 0, new Intent(USBReceiver.ACTION_USB_PERMISSION), 0);
                    IntentFilter filter = new IntentFilter(USBReceiver.ACTION_USB_PERMISSION);
                    context.RegisterReceiver(usbReceiver, filter);

                    usbManager = (UsbManager)context.GetSystemService(Context.UsbService);

                    var deviceList = usbManager.DeviceList;

                    if (deviceList.Count == 0)
                    {
                        throw new Exceptions.PrinterException("Bağlı USB cihaz bulunamadı!", new NotImplementedException());
                    }

                    foreach (var key in deviceList.Keys)
                    {
                        if (deviceList.TryGetValue(key, out var tempUsbDevice))
                        {
                            if (tempUsbDevice.ProductName.Equals(nc.DeviceName))
                            {
                                usbDevice = tempUsbDevice;
                                break;
                            }
                        }
                    }
                    if (usbDevice == null)
                        throw new Exceptions.PrinterException("Cihazlar arasında " + nc.DeviceName + " USB cihaz bulunamadı!", new NotImplementedException());

                    return OpenConnection();

                }
                else
                {
                    throw new Exceptions.PrinterException("Bağlantı tipi uyuşmuyor!", new NotImplementedException());
                }
            });
        }

        private bool OpenConnection()
        {
            if (usbDevice != null)
            {
                if (usbManager.HasPermission(usbDevice))
                {
                    intf = usbDevice.GetInterface(0);
                    for (int i = 0; i < intf.EndpointCount; i++)
                    {
                        UsbEndpoint ep = intf.GetEndpoint(i);
                        if (ep.Type == UsbAddressing.XferBulk)
                        {
                            if (ep.Direction == UsbAddressing.Out)
                            {
                                mEndpointBulkOut = ep;
                                connection = usbManager.OpenDevice(usbDevice);

                                bool forceClaim = true;
                                connection.ClaimInterface(intf, forceClaim);
                                IsConnected = true;
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    usbManager.RequestPermission(usbDevice, permissionIntent);
                }
                return false;
            }
            else
                throw new Exceptions.PrinterException("Bağlantı hatası: USB cihaz bulunamadı!", new NotImplementedException());
        }

        public override void Disconnect()
        {
            IsConnected = false;
            connection?.ReleaseInterface(intf);
            context?.UnregisterReceiver(usbReceiver);
        }

        public override Task<int> Print(byte[] command)
        {
            return Task.Run(() =>
            {
                if (mEndpointBulkOut == null)
                    throw new Exceptions.PrinterException("Bağlantı noktası açılamadı!", new NotImplementedException());
                return connection.BulkTransfer(mEndpointBulkOut, command, command.Length, 0);
            });
        }
    }
}
