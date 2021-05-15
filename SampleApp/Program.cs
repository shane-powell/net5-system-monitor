using System;
using SystemMonitorLib;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"CPU  {MonitoringFunctions.GetCpuUsage()}");
            }
            
        }
    }
}
