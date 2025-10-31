using Tizen;
using Tizen.System;
using System;

namespace DeviceSample
{
    public static class DisplaySample
    {
        private const string LogTag = "DeviceSample";
        
        public static void Run()
        {
            Log.Info(LogTag, "[Display] === Display Sample Started ===");
            
            try
            {
                // Display basic information
                GetDisplayInfo();
                
                // Brightness control sample
                BrightnessControlSample();
                
                // Display state sample
                DisplayStateSample();
                
                // Register display event handlers
                RegisterDisplayEvents();
                
                Log.Info(LogTag, "[Display] Display sample completed successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Display] Error in display sample: {ex.Message}");
            }
            
            Log.Info(LogTag, "[Display] === Display Sample End ===");
        }
        
        /// <summary>
        /// Get and display current display information
        /// </summary>
        private static void GetDisplayInfo()
        {
            Log.Info(LogTag, "[Display] Getting display information...");
            
            try
            {
                // Get number of displays
                int displayCount = Display.NumberOfDisplays;
                Log.Info(LogTag, $"[Display] Number of displays: {displayCount}");
                
                // Get information for each display
                var displays = Display.Displays;
				int displayIndex = 0;
				foreach (Display display in displays)
				{
					Log.Info(LogTag, $"[Display] === Display {displayIndex} Information ===");

					// Get display brightness
					int brightness = display.Brightness;
					Log.Info(LogTag, $"[Display] Current brightness: {brightness}");

					// Get maximum brightness
					int maxBrightness = display.MaxBrightness;
					Log.Info(LogTag, $"[Display] Maximum brightness: {maxBrightness}");

					// Calculate brightness percentage
					int brightnessPercentage = maxBrightness > 0 ? (brightness * 100) / maxBrightness : 0;
					Log.Info(LogTag, $"[Display] Brightness percentage: {brightnessPercentage}%");

					// Get current display state
					DisplayState currentState = Display.State;
					Log.Info(LogTag, $"[Display] Current display state: {currentState}");

					displayIndex++;
				}
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Display] Failed to get display info: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Sample brightness control functionality
        /// </summary>
        private static void BrightnessControlSample()
        {
            Log.Info(LogTag, "[Display] Starting brightness control sample...");
            
            try
            {
                // Check if displays are available
                var displays = Display.Displays;
                if (displays == null || displays.Count == 0)
                {
                    Log.Warn(LogTag, "[Display] No displays available for brightness control sample");
                    return;
                }
                
                // Perform brightness control for each display
                int displayIndex = 0;
                foreach (Display display in displays)
                {
                    Log.Info(LogTag, $"[Display] === Brightness Control for Display {displayIndex} ===");
                    
                    // Store original brightness
                    int originalBrightness = display.Brightness;
                    int maxBrightness = display.MaxBrightness;
                    
                    Log.Info(LogTag, $"[Display] Display {displayIndex} - Original brightness: {originalBrightness}/{maxBrightness}");
                    
                    // Calculate target brightness (50% of max)
                    int targetBrightness = maxBrightness / 2;
                    Log.Info(LogTag, $"[Display] Display {displayIndex} - Setting brightness to 50%: {targetBrightness}");
                    
                    // Set brightness to 50%
                    display.Brightness = targetBrightness;
                    
                    // Verify the change
                    int currentBrightness = display.Brightness;
                    Log.Info(LogTag, $"[Display] Display {displayIndex} - Brightness after setting: {currentBrightness}");
                    
                    // Wait a moment to sample the change
                    System.Threading.Thread.Sleep(1000);
                    
                    // Restore original brightness
                    Log.Info(LogTag, $"[Display] Display {displayIndex} - Restoring original brightness: {originalBrightness}");
                    display.Brightness = originalBrightness;
                    
                    // Verify restoration
                    int restoredBrightness = display.Brightness;
                    Log.Info(LogTag, $"[Display] Display {displayIndex} - Brightness after restoration: {restoredBrightness}");
                    
                    displayIndex++;
                }
                
                Log.Info(LogTag, "[Display] Brightness control sample completed for all displays");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Display] Failed in brightness control sample: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Sample display state functionality
        /// </summary>
        private static void DisplayStateSample()
        {
            Log.Info(LogTag, "[Display] Starting display state sample...");
            
            try
            {
                // Get current display state
                DisplayState currentState = Display.State;
                Log.Info(LogTag, $"[Display] Current display state: {currentState}");
                Log.Info(LogTag, "[Display] Display state sample completed");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Display] Failed in display state sample: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Register display event handlers for monitoring changes
        /// </summary>
        private static void RegisterDisplayEvents()
        {
            Log.Info(LogTag, "[Display] Registering display event handlers...");
            
            try
            {
                // Register state changed event
                Display.StateChanged += OnDisplayStateChanged;
                Log.Info(LogTag, "[Display] StateChanged event registered successfully");
                
                Log.Info(LogTag, "[Display] All display events registered successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Display] Failed to register display events: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Unregister display event handlers
        /// </summary>
        public static void UnregisterDisplayEvents()
        {
            Log.Info(LogTag, "[Display] Unregistering display event handlers...");
            
            try
            {
                Display.StateChanged -= OnDisplayStateChanged;
                Log.Info(LogTag, "[Display] All display events unregistered successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Display] Failed to unregister display events: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Event handler for display state change
        /// </summary>
        private static void OnDisplayStateChanged(object sender, DisplayStateChangedEventArgs args)
        {
            Log.Info(LogTag, $"[Display] StateChanged - Display state changed to: {args.State}");
            
            // Log additional context based on the new state
            switch (args.State)
            {
                case DisplayState.Normal:
                    Log.Info(LogTag, "[Display] Display is now in normal state");
                    break;
                case DisplayState.Dim:
                    Log.Info(LogTag, "[Display] Display is now dimmed");
                    break;
                case DisplayState.Off:
                    Log.Info(LogTag, "[Display] Display is now off");
                    break;
            }
        }
    }
}
