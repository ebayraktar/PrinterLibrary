using Android.Bluetooth;
using Java.Util;
using PrinterLibrary.PrinterConnections;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterLibrary.Printers
{
    internal class BluetoothPrinter : Printer
    {
        private readonly BluetoothAdapter bluetoothAdapter;
        BluetoothSocket bluetoothSocket;
        public BluetoothPrinter()
        {
            bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        }
        public override Task<bool> Connect(PrinterConnection connection)
        {
            return Task.Run(() =>
            {
                if (connection is BluetoothConnection bc)
                {
                    BluetoothDevice device = (from bd in bluetoothAdapter?.BondedDevices
                                              where bd?.Name == bc.BluetoothDeviceName
                                              select bd).FirstOrDefault();

                    if (device == null)
                        throw new Exceptions.PrinterException("Yazıcı bulunamadı", new NullReferenceException());

                    bluetoothSocket = device.CreateRfcommSocketToServiceRecord(
                        //UUID.FromString(Guid.NewGuid().ToString())
                        UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")
                        );
                    if (!bluetoothSocket.IsConnected)
                    {
                        bluetoothSocket.Connect();
                    }
                    IsConnected = true;
                    return true;
                }
                else
                {
                    throw new Exceptions.PrinterException("Bağlantı tipi uyuşmuyor!", new NotImplementedException());
                }
            });
        }
        public override void Disconnect()
        {
            IsConnected = false;
            bluetoothSocket.Close();
        }

        public override Task<int> Print(byte[] command)
        {
            return Task.Run(async () =>
            {
                if (!IsConnected)
                    throw new Exceptions.PrinterException("Önce bağlantı oluşturun.");

                try
                {
                    bluetoothSocket.OutputStream.Flush();
                    await bluetoothSocket.OutputStream.WriteAsync(command, 0, command.Length);

                    return 1;
                }
                catch (Exception exp)
                {
                    throw new Exceptions.PrinterException("Yazdırılırken hata oluştu.", exp);
                }
            });
        }
    }
}
