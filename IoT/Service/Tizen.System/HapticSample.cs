using Tizen;
using Tizen.System;
using System;

namespace DeviceSample
{
    public static class HapticSample
    {
        private const string LogTag = "DeviceSample";
        
        public static void Run()
        {
            Log.Info(LogTag, "[Haptic] === Haptic Sample Started ===");
            
            try
            {
                // Vibration control sample
                VibrationControlSample();
                
                Log.Info(LogTag, "[Haptic] Haptic sample completed successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Haptic] Error in haptic sample: {ex.Message}");
            }
            
            Log.Info(LogTag, "[Haptic] === Haptic Sample End ===");
        }
        
        /// <summary>
        /// Sample vibration control functionality
        /// </summary>
        private static void VibrationControlSample()
        {
            Log.Info(LogTag, "[Haptic] Starting vibration control sample...");
            
            try
            {
                // Check if vibrators are available
                var vibrators = Vibrator.Vibrators;
                if (vibrators == null || vibrators.Count == 0)
                {
                    Log.Warn(LogTag, "[Haptic] No vibrators available for vibration control sample");
                    return;
                }
                
                // Get the first vibrator
                using (Vibrator vibrator = vibrators[0])
                {
                    Log.Info(LogTag, "[Haptic] Starting vibration sample with first vibrator");
                    
                    // Sample 1: Short vibration (500ms, 50% intensity)
                    Log.Info(LogTag, "[Haptic] Sample 1: Short vibration (500ms, 50% intensity)");
                    vibrator.Vibrate(500, 50);
                    System.Threading.Thread.Sleep(1000);
                    
                    // Sample 2: Medium vibration (1000ms, 70% intensity)
                    Log.Info(LogTag, "[Haptic] Sample 2: Medium vibration (1000, 70% intensity)");
                    vibrator.Vibrate(1000, 70);
                    System.Threading.Thread.Sleep(1500);
                    
                    // Sample 3: Strong vibration (1500ms, 90% intensity)
                    Log.Info(LogTag, "[Haptic] Sample 3: Strong vibration (1500ms, 90% intensity)");
                    vibrator.Vibrate(1500, 90);
                    System.Threading.Thread.Sleep(2000);
                    
                    // Stop any ongoing vibration
                    Log.Info(LogTag, "[Haptic] Stopping vibration");
                    vibrator.Stop();
					vibrator.Dispose();
                    
                    Log.Info(LogTag, "[Haptic] Vibration control sample completed");
                }
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Haptic] Failed in vibration control sample: {ex.Message}");
            }
        }
    }
}
