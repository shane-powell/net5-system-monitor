using Microsoft.VisualStudio.TestTools.UnitTesting;
using SystemMonitorLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemMonitorLib.Tests
{
    [TestClass()]
    public class MonitoringFunctionsTests
    {
        [TestMethod()]
        public void GetCpuUsageTest()
        {
            var result = MonitoringFunctions.GetCpuUsage();

            Assert.IsTrue(result is >= 0 and <= 100);
        }

        [TestMethod()]
        public void GetPerformanceCounterTest()
        {
            var result = MonitoringFunctions.GetPerformanceCounter("Processor", "% Processor Time", "_Total");

            Assert.IsTrue(result is >= 0 and <= 100);
        }

        [TestMethod()]
        public void GetMemoryUsageTest()
        {
            var result = MonitoringFunctions.GetMemoryUsage();

            Assert.IsTrue(result is >= 0 and <= 100);
        }

        [TestMethod()]
        public void GetDiskUsageTest()
        {
            var result = MonitoringFunctions.GetDiskUsage();

            Assert.IsTrue(result is >= 0 and <= 100);
        }
    }
}