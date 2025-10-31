using Tizen;
using Tizen.System;
using System;
using Tizen.Common;

namespace DeviceSample
{
    public static class LedSample
    {
        private const string LogTag = "DeviceSample";
        
        public static void Run()
        {
            Log.Info(LogTag, "[LED] === LED Sample Started ===");
            
            try
            {
                // Get LED device information
                GetLedInfo();
                
                // LED brightness control sample
                LedBrightnessControlSample();
                
                // LED service effect sample
                LedServiceEffectSample();
                
                // Register LED event handlers
                RegisterLedEvents();
                
                Log.Info(LogTag, "[LED] LED sample completed successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[LED] Error in LED sample: {ex.Message}");
            }
            
            Log.Info(LogTag, "[LED] === LED Sample End ===");
        }
        
        /// <summary>
        /// Get and display current LED device information
        /// </summary>
        private static void GetLedInfo()
        {
            Log.Info(LogTag, "[LED] Getting LED device information...");
            
            try
            {
                // Get maximum brightness
                int maxBrightness = Led.MaxBrightness;
                Log.Info(LogTag, $"[LED] Maximum brightness: {maxBrightness}");
                
                // Get current brightness
                int currentBrightness = Led.Brightness;
                Log.Info(LogTag, $"[LED] Current brightness: {currentBrightness}");
                
                // Calculate brightness percentage
                int brightnessPercentage = maxBrightness > 0 ? (currentBrightness * 100) / maxBrightness : 0;
                Log.Info(LogTag, $"[LED] Brightness percentage: {brightnessPercentage}%");
                
                Log.Info(LogTag, "[LED] Required privilege: http://tizen.org/privilege/led");
                Log.Info(LogTag, "[LED] Required features: http://tizen.org/feature/led, http://tizen.org/feature/camera.back.flash");
                
                Log.Info(LogTag, "[LED] LED information retrieved successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[LED] Failed to get LED info: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Sample LED brightness control functionality
        /// </summary>
        private static void LedBrightnessControlSample()
        {
            Log.Info(LogTag, "[LED] Starting LED brightness control sample...");
            
            try
            {
                // Store original brightness
                int originalBrightness = Led.Brightness;
                int maxBrightness = Led.MaxBrightness;
                
                Log.Info(LogTag, $"[LED] Original brightness: {originalBrightness}/{maxBrightness}");
                
                // Set brightness to 25%
                int targetBrightness25 = maxBrightness / 4;
                Log.Info(LogTag, $"[LED] Setting brightness to 25%: {targetBrightness25}");
                Led.Brightness = targetBrightness25;
                System.Threading.Thread.Sleep(1000);
                
                // Set brightness to 50%
                int targetBrightness50 = maxBrightness / 2;
                Log.Info(LogTag, $"[LED] Setting brightness to 50%: {targetBrightness50}");
                Led.Brightness = targetBrightness50;
                System.Threading.Thread.Sleep(1000);
                
                // Set brightness to 75%
                int targetBrightness75 = (maxBrightness * 3) / 4;
                Log.Info(LogTag, $"[LED] Setting brightness to 75%: {targetBrightness75}");
                Led.Brightness = targetBrightness75;
                System.Threading.Thread.Sleep(1000);
                
                // Set brightness to 100%
                Log.Info(LogTag, $"[LED] Setting brightness to 100%: {maxBrightness}");
                Led.Brightness = maxBrightness;
                System.Threading.Thread.Sleep(1000);
                
                // Restore original brightness
                Log.Info(LogTag, $"[LED] Restoring original brightness: {originalBrightness}");
                Led.Brightness = originalBrightness;
                
                // Verify restoration
                int restoredBrightness = Led.Brightness;
                Log.Info(LogTag, $"[LED] Brightness after restoration: {restoredBrightness}");
                
                Log.Info(LogTag, "[LED] LED brightness control sample completed");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[LED] Failed in LED brightness control sample: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Sample LED service effect functionality
        /// </summary>
        private static void LedServiceEffectSample()
        {
            Log.Info(LogTag, "[LED] Starting LED service effect sample...");
            
            try
            {
                // Sample 1: White color blinking
                Log.Info(LogTag, "[LED] Sample 1: White color blinking (500ms on, 500ms off)");
                Color whiteColor = Color.FromRgba(255, 255, 255, 1);
                Led.Play(500, 500, whiteColor);
                System.Threading.Thread.Sleep(3000);
                Led.Stop();
                System.Threading.Thread.Sleep(1000);
                
                // Sample 2: Red color blinking
                Log.Info(LogTag, "[LED] Sample 2: Red color blinking (300ms on, 700ms off)");
                Color redColor = Color.FromRgba(255, 0, 0, 1);
                Led.Play(300, 700, redColor);
                System.Threading.Thread.Sleep(3000);
                Led.Stop();
                System.Threading.Thread.Sleep(1000);
                
                // Sample 3: Green color blinking
                Log.Info(LogTag, "[LED] Sample 3: Green color blinking (200ms on, 200ms off)");
                Color greenColor = Color.FromRgba(0, 255, 0, 1);
                Led.Play(200, 200, greenColor);
                System.Threading.Thread.Sleep(3000);
                Led.Stop();
                System.Threading.Thread.Sleep(1000);
                
                // Sample 4: Blue color blinking
                Log.Info(LogTag, "[LED] Sample 4: Blue color blinking (1000ms on, 1000ms off)");
                Color blueColor = Color.FromRgba(0, 0, 255, 1);
                Led.Play(1000, 1000, blueColor);
                System.Threading.Thread.Sleep(4000);
                Led.Stop();
                System.Threading.Thread.Sleep(1000);
                
                // Sample 5: Custom color (Yellow)
                Log.Info(LogTag, "[LED] Sample 5: Yellow color blinking (400ms on, 600ms off)");
                Color yellowColor = Color.FromRgba(255, 255, 0, 1);
                Led.Play(400, 600, yellowColor);
                System.Threading.Thread.Sleep(3000);
                Led.Stop();
                
                Log.Info(LogTag, "[LED] LED service effect sample completed");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[LED] Failed in LED service effect sample: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Register LED event handlers for monitoring changes
        /// </summary>
        private static void RegisterLedEvents()
        {
            Log.Info(LogTag, "[LED] Registering LED event handlers...");
            
            try
            {
                // Register brightness changed event
                Led.BrightnessChanged += OnLedBrightnessChanged;
                Log.Info(LogTag, "[LED] BrightnessChanged event registered successfully");
                
                Log.Info(LogTag, "[LED] All LED events registered successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[LED] Failed to register LED events: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Unregister LED event handlers
        /// </summary>
        public static void UnregisterLedEvents()
        {
            Log.Info(LogTag, "[LED] Unregistering LED event handlers...");
            
            try
            {
                Led.BrightnessChanged -= OnLedBrightnessChanged;
                Log.Info(LogTag, "[LED] All LED events unregistered successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[LED] Failed to unregister LED events: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Event handler for LED brightness change
        /// </summary>
        private static void OnLedBrightnessChanged(object sender, LedBrightnessChangedEventArgs args)
        {
            Log.Info(LogTag, $"[LED] BrightnessChanged - LED brightness changed to: {args.Brightness}");
            
            // Calculate brightness percentage for better context
            try
            {
                int maxBrightness = Led.MaxBrightness;
                int brightnessPercentage = maxBrightness > 0 ? (args.Brightness * 100) / maxBrightness : 0;
                Log.Info(LogTag, $"[LED] Brightness percentage: {brightnessPercentage}%");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[LED] Failed to get additional brightness context: {ex.Message}");
            }
        }
    }
}
