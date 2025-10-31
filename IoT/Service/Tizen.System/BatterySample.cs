using Tizen;
using Tizen.System;
using System;

namespace DeviceSample
{
    public static class BatterySample
    {
        private const string LogTag = "DeviceSample";
        
        public static void Run()
        {
            Log.Info(LogTag, "[Battery] === Battery Sample Started ===");
            
            try
            {
                // Display current battery information
                GetBatteryInfo();
                
                // Register battery event handlers
                RegisterBatteryEvents();
                
                Log.Info(LogTag, "[Battery] Battery sample completed successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Battery] Error in battery sample: {ex.Message}");
            }
            
            Log.Info(LogTag, "[Battery] === Battery Sample End ===");
        }
        
        /// <summary>
        /// Get and display current battery information
        /// </summary>
        private static void GetBatteryInfo()
        {
            Log.Info(LogTag, "[Battery] Getting battery information...");
            
            try
            {
                // Get battery percentage
                int batteryPercent = Battery.Percent;
                Log.Info(LogTag, $"[Battery] Battery Percentage: {batteryPercent}%");
                
                // Get battery level status
                BatteryLevelStatus batteryLevel = Battery.Level;
                Log.Info(LogTag, $"[Battery] Battery Level: {batteryLevel}");
                
                // Get charging state
                bool isCharging = Battery.IsCharging;
                Log.Info(LogTag, $"[Battery] Is Charging: {isCharging}");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Battery] Failed to get battery info: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Register battery event handlers for monitoring changes
        /// </summary>
        private static void RegisterBatteryEvents()
        {
            Log.Info(LogTag, "[Battery] Registering battery event handlers...");
            
            try
            {
                // Register percent changed event
                Battery.PercentChanged += OnPercentChanged;
                Log.Info(LogTag, "[Battery] PercentChanged event registered");
                
                // Register level changed event
                Battery.LevelChanged += OnLevelChanged;
                Log.Info(LogTag, "[Battery] LevelChanged event registered");
                
                // Register charging state changed event
                Battery.ChargingStateChanged += OnChargingStateChanged;
                Log.Info(LogTag, "[Battery] ChargingStateChanged event registered");
                
                Log.Info(LogTag, "[Battery] All battery events registered successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Battery] Failed to register battery events: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Unregister battery event handlers
        /// </summary>
        public static void UnregisterBatteryEvents()
        {
            Log.Info(LogTag, "[Battery] Unregistering battery event handlers...");
            
            try
            {
                Battery.PercentChanged -= OnPercentChanged;
                Battery.LevelChanged -= OnLevelChanged;
                Battery.ChargingStateChanged -= OnChargingStateChanged;
                
                Log.Info(LogTag, "[Battery] All battery events unregistered successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Battery] Failed to unregister battery events: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Event handler for battery percentage change
        /// </summary>
        private static void OnPercentChanged(object sender, BatteryPercentChangedEventArgs args)
        {
            Log.Info(LogTag, $"[Battery] PercentChanged - Battery percentage: {args.Percent}%");
            
            // Log additional information based on percentage changes
            if (args.Percent <= 20)
            {
                Log.Warn(LogTag, "[Battery] WARNING: Battery is low (≤20%)!");
            }
            else if (args.Percent <= 10)
            {
                Log.Error(LogTag, "[Battery] CRITICAL: Battery is very low (≤10%)!");
            }
            else if (args.Percent >= 90)
            {
                Log.Info(LogTag, "[Battery] Battery is almost full (≥90%)");
            }
            
            // Log charging status context
            try
            {
                bool isCharging = Battery.IsCharging;
                string chargingStatus = isCharging ? "while charging" : "while discharging";
                Log.Info(LogTag, $"[Battery] Battery percentage changed to {args.Percent}% {chargingStatus}");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[Battery] Failed to get charging status: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Event handler for battery level change
        /// </summary>
        private static void OnLevelChanged(object sender, BatteryLevelChangedEventArgs args)
        {
            Log.Info(LogTag, $"[Battery] LevelChanged - Battery level: {args.Level}");
            
            // Log warning for critical battery levels
            switch (args.Level)
            {
                case BatteryLevelStatus.Empty:
                    Log.Warn(LogTag, "[Battery] WARNING: Battery is empty!");
                    break;
                case BatteryLevelStatus.Critical:
                    Log.Warn(LogTag, "[Battery] WARNING: Battery level is critical!");
                    break;
                case BatteryLevelStatus.Low:
                    Log.Warn(LogTag, "[Battery] WARNING: Battery level is low!");
                    break;
                case BatteryLevelStatus.Full:
                    Log.Info(LogTag, "[Battery] Battery is fully charged");
                    break;
            }
        }
        
        /// <summary>
        /// Event handler for charging state change
        /// </summary>
        private static void OnChargingStateChanged(object sender, BatteryChargingStateChangedEventArgs args)
        {
            string chargingStatus = args.IsCharging ? "Charging" : "Not Charging";
            Log.Info(LogTag, $"[Battery] ChargingStateChanged - Status: {chargingStatus}");
            
            if (args.IsCharging)
            {
                Log.Info(LogTag, "[Battery] Charger connected");
            }
            else
            {
                Log.Info(LogTag, "[Battery] Charger disconnected");
            }
        }
    }
}
