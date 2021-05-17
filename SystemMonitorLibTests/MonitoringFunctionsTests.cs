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
    }
}