using Tizen;
using Tizen.System;
using System;
using System.Collections.Generic;

namespace DeviceSample
{
    public static class IrSample
    {
        private const string LogTag = "DeviceSample";
        
        public static void Run()
        {
            Log.Info(LogTag, "[IR] === IR Sample Started ===");
            
            try
            {
                // Get IR device information
                GetIrInfo();
                
                // IR transmission sample
                IrTransmissionSample();
                
                // IR pattern samples
                IrPatternSamples();
                
                Log.Info(LogTag, "[IR] IR sample completed successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[IR] Error in IR sample: {ex.Message}");
            }
            
            Log.Info(LogTag, "[IR] === IR Sample End ===");
        }
        
        /// <summary>
        /// Get and display current IR device information
        /// </summary>
        private static void GetIrInfo()
        {
            Log.Info(LogTag, "[IR] Getting IR device information...");
            
            try
            {
                // Check IR availability
                bool isIrAvailable = IR.IsAvailable;
                Log.Info(LogTag, $"[IR] IR module available: {isIrAvailable}");
                
                if (isIrAvailable)
                {
                    Log.Info(LogTag, "[IR] IR transmitter is ready for command transmission");
                    Log.Info(LogTag, "[IR] Required privilege: http://tizen.org/privilege/use_ir");
                    Log.Info(LogTag, "[IR] Required feature: http://tizen.org/feature/consumer_ir");
                }
                else
                {
                    Log.Warn(LogTag, "[IR] IR module is not available on this device");
                    Log.Warn(LogTag, "[IR] IR transmission samples will be skipped");
                }
                
                Log.Info(LogTag, "[IR] IR information retrieved successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[IR] Failed to get IR info: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Sample IR transmission functionality
        /// </summary>
        private static void IrTransmissionSample()
        {
            Log.Info(LogTag, "[IR] Starting IR transmission sample...");
            
            try
            {
                // Check if IR is available
                if (!IR.IsAvailable)
                {
                    Log.Warn(LogTag, "[IR] IR not available, skipping transmission sample");
                    return;
                }
                
                // Create a simple IR pattern for demonstration
                var pattern = new List<int>();
                pattern.Add(100);  // ON duration
                pattern.Add(50);   // OFF duration
                pattern.Add(200);  // ON duration
                pattern.Add(100);  // OFF duration
                
                // Transmit IR command with carrier frequency
                int carrierFrequency = 38000; // Common IR frequency (38kHz)
                Log.Info(LogTag, $"[IR] Transmitting IR command with frequency: {carrierFrequency}Hz");
                Log.Info(LogTag, $"[IR] Pattern: [{string.Join(", ", pattern)}]");
                
                IR.Transmit(carrierFrequency, pattern);
                
                Log.Info(LogTag, "[IR] IR command transmitted successfully");
                Log.Info(LogTag, "[IR] IR transmission sample completed");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[IR] Failed in IR transmission sample: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Sample various IR patterns
        /// </summary>
        private static void IrPatternSamples()
        {
            Log.Info(LogTag, "[IR] Starting IR pattern samples...");
            
            try
            {
                // Check if IR is available
                if (!IR.IsAvailable)
                {
                    Log.Warn(LogTag, "[IR] IR not available, skipping pattern samples");
                    return;
                }
                
                // Sample 1: Short pulse pattern
                TransmitPattern("Short Pulse", 38000, new List<int> { 50, 50, 50, 50 });
                
                // Sample 2: Long pulse pattern
                TransmitPattern("Long Pulse", 38000, new List<int> { 200, 100, 200, 100 });
                
                // Sample 3: Complex pattern
                TransmitPattern("Complex Pattern", 38000, new List<int> { 100, 50, 200, 100, 50, 25, 100, 50 });
                
                // Sample 4: Different frequency pattern
                TransmitPattern("Different Frequency", 40000, new List<int> { 150, 75, 150, 75 });
                
                Log.Info(LogTag, "[IR] IR pattern samples completed");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[IR] Failed in IR pattern samples: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Helper method to transmit IR pattern with logging
        /// </summary>
        /// <param name="patternName">Name of the pattern for logging</param>
        /// <param name="carrierFrequency">Carrier frequency in Hz</param>
        /// <param name="pattern">IR pattern list</param>
        private static void TransmitPattern(string patternName, int carrierFrequency, List<int> pattern)
        {
            try
            {
                Log.Info(LogTag, $"[IR] Transmitting {patternName} pattern");
                Log.Info(LogTag, $"[IR] Frequency: {carrierFrequency}Hz, Pattern: [{string.Join(", ", pattern)}]");
                
                IR.Transmit(carrierFrequency, pattern);
                
                Log.Info(LogTag, $"[IR] {patternName} pattern transmitted successfully");
                
                // Wait between transmissions
                System.Threading.Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[IR] Failed to transmit {patternName} pattern: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Clean up IR resources
        /// </summary>
        public static void UnregisterIrEvents()
        {
            Log.Info(LogTag, "[IR] Cleaning up IR resources...");
            
            try
            {
                // IR API doesn't require explicit cleanup, but we log the completion
                Log.Info(LogTag, "[IR] All IR resources cleaned up successfully");
            }
            catch (Exception ex)
            {
                Log.Error(LogTag, $"[IR] Failed to clean up IR resources: {ex.Message}");
            }
        }
    }
}
