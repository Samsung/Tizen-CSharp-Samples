using Tizen;
using Tizen.System;
using System;

namespace DeviceSample
{
    public static class PowerSample
    {
        private const string LogTag = "DeviceSample";
        
        public static void Run()
        {
            Log.Info(LogTag, "[Power] === Power Sample Started ===");
            
            try
            {
                // Power lock control sample
                PowerLockControlSample();
                
                Log.Info(LogTag, "[Power] Power sample completed successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Power] Error in power sample: {ex.Message}");
            }
            
            Log.Info(LogTag, "[Power] === Power Sample End ===");
        }
        
        /// <summary>
        /// Sample power lock control functionality
        /// </summary>
        private static void PowerLockControlSample()
        {
            Log.Info(LogTag, "[Power] Starting power lock control sample...");
            
            try
            {
                // Sample 1: CPU lock for 3 seconds
                Log.Info(LogTag, "[Power] Sample 1: Requesting CPU lock for 3000ms");
                Power.RequestLock(PowerLock.Cpu, 3000);
                Log.Info(LogTag, "[Power] CPU lock requested, waiting 2 seconds...");
                System.Threading.Thread.Sleep(2000);
                
                // Release CPU lock
                Log.Info(LogTag, "[Power] Releasing CPU lock");
                Power.ReleaseLock(PowerLock.Cpu);
                
                // Wait a moment between samples
                System.Threading.Thread.Sleep(1000);
                
                // Sample 2: Display Normal lock for 2 seconds
                Log.Info(LogTag, "[Power] Sample 2: Requesting Display Normal lock for 2000ms");
                Power.RequestLock(PowerLock.DisplayNormal, 2000);
                Log.Info(LogTag, "[Power] Display Normal lock requested, waiting 1 second...");
                System.Threading.Thread.Sleep(1000);
                
                // Release Display Normal lock
                Log.Info(LogTag, "[Power] Releasing Display Normal lock");
                Power.ReleaseLock(PowerLock.DisplayNormal);
                
                // Wait a moment between samples
                System.Threading.Thread.Sleep(1000);
                
                // Sample 3: Display Dim lock for 2 seconds
                Log.Info(LogTag, "[Power] Sample 3: Requesting Display Dim lock for 2000ms");
                Power.RequestLock(PowerLock.DisplayDim, 2000);
                Log.Info(LogTag, "[Power] Display Dim lock requested, waiting 1 second...");
                System.Threading.Thread.Sleep(1000);
                
                // Release Display Dim lock
                Log.Info(LogTag, "[Power] Releasing Display Dim lock");
                Power.ReleaseLock(PowerLock.DisplayDim);
                
                Log.Info(LogTag, "[Power] Power lock control sample completed");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Power] Failed in power lock control sample: {ex.Message}");
            }
        }
    }
}
