using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HidApiAdapter;

namespace DevicesViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowDevices();

            Console.WriteLine("press any key to exit ...");
            Console.ReadKey();
        }

        private static void ShowDevices()
        {
            HidDeviceManager deviceManager = HidDeviceManager.GetManager();
            
            // trying to find any device
            List<HidDevice> devices = deviceManager.SearchDevices(0, 0);

            if (devices.Any())
            {
                var infos = new List<string>();

                // Connect
                foreach(var device in devices)
                {
                    device.Connect();
                }

                // show devices
                foreach (var device in devices)
                {
                    infos.Add(device.ToInfo());
                    //infos.Add(device.ToString());
                    Console.WriteLine(device.ToInfo());
                }

                // Disconnect
                foreach (var device in devices)
                {
                    device.Disconnect();
                }
            }
            else
            {
                Console.WriteLine("no devices found");
            }
        }

    }

    static class HidDeviceExtension
    {
        public static string ToInfo(this HidDevice device)
        {
            return (
                $"VendorID {device.VendorId:X4} ProductID {device.ProductId:X4} Product {device.Product()}");
                //$"device: {device.Path()}\n" +
                //$"manufacturer: {device.Manufacturer()}\n" +
                //$"product: {device.Product()}\n" +
                //$"serial number: {device.SerialNumber()}\n");
        }

    }

}
