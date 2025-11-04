using System.Linq;
using System.Threading.Tasks;
using Tizen.Applications;
using Tizen.System.Usb;

namespace UsbHostSample
{
    class App : ServiceApplication
    {
        // This sample app is for controlling a specific device,
        // in this case "Dream Cheeky Missile Launcher". Look at
        // the general shape of the app, but remember that your
        // device will of course offer different functionality.
        const int MISSILE_LAUNCHER_VENDOR_ID  = 0x0A81;
        const int MISSILE_LAUNCHER_PRODUCT_ID = 0x0701;

	const int MISSILE_LAUNCHER_CMD_FIRE = 0x10;

        static bool IsDeviceMissileLauncher(UsbDevice device)
        {
            return device.DeviceInformation.VendorId  == MISSILE_LAUNCHER_VENDOR_ID
                && device.DeviceInformation.ProductId == MISSILE_LAUNCHER_PRODUCT_ID;
        }

        static void FireZeMissiles(UsbDevice device)
        {
            device.Open();

            device.Configurations[0].SetAsActive();

            var iface = device.ActiveConfiguration.Interfaces[1];
            iface.Claim(true);

            UsbControlEndpoint endpoint = iface.Endpoints.Select(x => x.Value as UsbControlEndpoint).FirstOrDefault();
            byte[] buffer = new byte[8];
            buffer[0] = MISSILE_LAUNCHER_CMD_FIRE;

            // magic numbers expected by this particular device.
            endpoint.Transfer(0x21, 0x09, 0x200, 0, buffer, 8, 0);

            iface.Release();

            device.Close();
            device.Dispose();
        }

        void HotplugHandler(object sender, HotPluggedEventArgs args)
        {
            if (args.EventType != HotplugEventType.Attach)
            {
                Tizen.Log.Info("USB_HOST_SAMPLE", $"Hotplug: some device was detached, ignoring");
                return;
            }

            if (!IsDeviceMissileLauncher(args.Device))
            {
                Tizen.Log.Info("USB_HOST_SAMPLE", $"Hotplug: attached device wasn't missile launcher, ignoring");
                return;
            }

            Tizen.Log.Info("USB_HOST_SAMPLE", $"Hotplug: missile launcher detected. Removing hotplug handler and initiating launch sequence");
            ((UsbManager)sender).DeviceHotPlugged -= HotplugHandler;
            FireZeMissiles(args.Device);
            ExitWithoutRestarting();
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Tizen.Log.Info("USB_HOST_SAMPLE", "Started usb-host API sample");

            try
            {
                var usb = new UsbManager();


                Tizen.Log.Info("USB_HOST_SAMPLE", $"Iterating over devices, looking for VID {MISSILE_LAUNCHER_VENDOR_ID}, PID {MISSILE_LAUNCHER_PRODUCT_ID}");

                var device = usb.AvailableDevices.FirstOrDefault(IsDeviceMissileLauncher);
                if (device == null)
                {
                    Tizen.Log.Warn("USB_HOST_SAMPLE", $"Could not find missile launcher - waiting until it gets plugged!");
                    usb.DeviceHotPlugged += HotplugHandler;
                    return;
                }

                Tizen.Log.Info("USB_HOST_SAMPLE", "Missile launcher already present - initiating launch sequence");
                FireZeMissiles(device);
                ExitWithoutRestarting();
            }
            catch (System.Exception e)
            {
                Tizen.Log.Error("USB_HOST_SAMPLE", $"Caught exception {e}");
            }
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Run(args);
        }
    }
}
