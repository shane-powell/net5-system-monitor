﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Versioning;
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
    }
}
