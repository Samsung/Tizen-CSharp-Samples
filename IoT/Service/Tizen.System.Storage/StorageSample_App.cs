using System;
using System.Linq;
using Tizen.Applications;
using Tizen.System;
using Tizen;

namespace StorageSample
{
    class App : ServiceApplication
    {
        private void SampleAllStorageInfoTest()
        {
            Log.Info("STORAGE_SAMPLE", "====1. Get all storage information====");
            Log.Info("STORAGE_SAMPLE", "");

            try
            {
                var storages = StorageManager.Storages;
                Log.Info("STORAGE_SAMPLE", $"Found {storages.Count()} storage devices");

                foreach (var storage in storages)
                {
                    Log.Info("STORAGE_SAMPLE", "");
                    Log.Info("STORAGE_SAMPLE", $"ID: {storage.Id}");
                    Log.Info("STORAGE_SAMPLE", $"StorageType: {(storage.StorageType is StorageArea.Internal ? "Internal" : storage.StorageType is StorageArea.External ? "External" : "ExtendedInternal")}");
                    Log.Info("STORAGE_SAMPLE", $"DeviceType: {storage.DeviceType}");
                    Log.Info("STORAGE_SAMPLE", $"TotalSpace: {storage.TotalSpace} bytes");
                    Log.Info("STORAGE_SAMPLE", $"AvailableSpace: {storage.AvailableSpace} bytes");
                    Log.Info("STORAGE_SAMPLE", $"RootDir: {storage.RootDirectory}");
//                    Log.Info("STORAGE_SAMPLE", $"Flags: {storage.Flags}");
//                    Log.Info("STORAGE_SAMPLE", $"Fstype: {storage.Fstype}");
//                    Log.Info("STORAGE_SAMPLE", $"Fsuuid: {storage.Fsuuid}");
//                    Log.Info("STORAGE_SAMPLE", $"Primary: {(storage.Primary is true ? "YES" : "NO")}");
                    Log.Info("STORAGE_SAMPLE", $"State: {(storage.State is StorageState.Mounted ? "Mounted" : storage.State is StorageState.MountedReadOnly ? "MountedReadOnly" : storage.State is StorageState.Removed ? "Removed" : "Unmountable")}");
                }
            }
            catch (Exception ex)
            {
                Log.Error("STORAGE_SAMPLE", $"Error: {ex.Message}");
            }

            Log.Info("STORAGE_SAMPLE", "");
        }

        private void SampleAllAbsolutePathTest()
        {
            Log.Info("STORAGE_SAMPLE", "====2. Get all absolute path====");
            Log.Info("STORAGE_SAMPLE", "");

            try
            {
                Storage internalStorage = StorageManager.Storages.Where(s => s.StorageType == StorageArea.Internal).FirstOrDefault();

                foreach (DirectoryType directoryType in Enum.GetValues(typeof(DirectoryType)))
                {
                    var result = internalStorage.GetAbsolutePath(directoryType);
                    string directoryTypeName = Enum.GetName(typeof(DirectoryType), directoryType);
                    Log.Info("STORAGE_SAMPLE", $"DirecotryType: {directoryTypeName}, AbsolutePath: {result}");
                }
            }
            catch (Exception ex)
            {
                Log.Error("STORAGE_SAMPLE", $"Error: {ex.Message}");
            }

            Log.Info("STORAGE_SAMPLE", "");
        }

        private void SampleStorageStateChangedTest()
        {
            Log.Info("STORAGE_SAMPLE", "====3. Add a callback for storage state changed====");
            Log.Info("STORAGE_SAMPLE", "");

            try
            {
                Storage externalStorage = StorageManager.Storages.Where(s => s.StorageType == StorageArea.External).FirstOrDefault();
                externalStorage.StorageStateChanged += (s, e) =>
                {
                    var storage = s as Storage;
                    Log.Info("STORAGE_SAMPLE", string.Format("State Changed to {0}", storage.State));
                };
            }
            catch (Exception ex)
            {
                Log.Error("STORAGE_SAMPLE", $"Error: {ex.Message}");
            }

            Log.Info("STORAGE_SAMPLE", "");
        }

        private void SampleTest()
        {
            SampleAllStorageInfoTest();
            SampleAllAbsolutePathTest();
            SampleStorageStateChangedTest();
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Log.Info("STORAGE_SAMPLE", "App: Created");

            SampleTest();
        }

        protected override void OnAppControlReceived(AppControlReceivedEventArgs e)
        {
            base.OnAppControlReceived(e);
        }

        protected override void OnDeviceOrientationChanged(DeviceOrientationEventArgs e)
        {
            base.OnDeviceOrientationChanged(e);
        }

        protected override void OnLocaleChanged(LocaleChangedEventArgs e)
        {
            base.OnLocaleChanged(e);
        }

        protected override void OnLowBattery(LowBatteryEventArgs e)
        {
            base.OnLowBattery(e);
        }

        protected override void OnLowMemory(LowMemoryEventArgs e)
        {
            base.OnLowMemory(e);
        }

        protected override void OnRegionFormatChanged(RegionFormatChangedEventArgs e)
        {
            base.OnRegionFormatChanged(e);
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
            Log.Info("STORAGE_SAMPLE", "App: Terminated");
        }

        static void Main(string[] args)
        {
            App app = new App();
            app.Run(args);
        }
    }
}