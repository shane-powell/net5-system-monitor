using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;

namespace SystemMonitorLib
{
    [SupportedOSPlatform("windows")]
    public static class MonitoringFunctions
    {
        [SupportedOSPlatform("windows")]
        public static int GetCpuUsage()
        {
            return GetPerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        [SupportedOSPlatform("windows")]
        public static int GetMemoryUsage()
        {
            return GetPerformanceCounter("Memory", "% Committed Bytes In Use", null);
        }

        [SupportedOSPlatform("windows")]
        public static int GetDiskUsage()
        {
            return GetPerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        [SupportedOSPlatform("windows")]
        public static int GetGpuUsage()
        {
            return GetPerformanceCounter("GPU Engine", "Utilization Percentage", "_Total");
        }

        [SupportedOSPlatform("windows")]
        public static int GetNvidiaGpuUsage()
        {
            return GetPerformanceCounter("GPU Engine", "Utilization Percentage", "_Total");
        }

        [SupportedOSPlatform("windows")]
        public static int GetNetworkUsage()
        {
            return GetPerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        [SupportedOSPlatform("windows")]
        public static int GetPerformanceCounter(string category, string counterName, string instanceName)
        {
            var performanceCounter = new PerformanceCounter(category, counterName, instanceName, Environment.MachineName);
            _ = performanceCounter.NextValue();
            Thread.Sleep(1000);
            return (int)performanceCounter.NextValue();
        }

        public static string BuildCountersString(bool writeToFile)
        {
            var sb = new StringBuilder();

            var cats = PerformanceCounterCategory.GetCategories();

            foreach (var performanceCounterCategory in cats)
            {
                sb.AppendLine();
                sb.AppendLine(performanceCounterCategory.CategoryName);

                if (performanceCounterCategory.CategoryName != "Thread")
                {
                    var instances = performanceCounterCategory.GetInstanceNames();

                    if (instances.Any())
                    {

                        foreach (var instance in instances)
                        {
                            sb.AppendLine($"    {instance}");
                            if (performanceCounterCategory.InstanceExists(instance))
                            {
                                var counters = performanceCounterCategory.GetCounters(instance);

                                foreach (var performanceCounter in counters)
                                {
                                    sb.AppendLine($"        {performanceCounter.CounterName}");
                                }
                            }

                        }

                    }
                    else
                    {
                        var counters = performanceCounterCategory.GetCounters();

                        foreach (var performanceCounter in counters)
                        {
                            sb.AppendLine($"    {performanceCounter.CounterHelp}");
                        }
                    }

                }

            }

            if (writeToFile)
            {
                try
                {
                    File.WriteAllText("counters.txt", sb.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return sb.ToString();
        }
    }
}
