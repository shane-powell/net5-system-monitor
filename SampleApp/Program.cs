using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using SystemMonitorLib;

namespace SampleApp
{
    [SupportedOSPlatform("windows")]
    class Program
    {
        private static SerialPort port = null;

        static void Main(string[] args)
        {
            SerialSample();
        }

        static void ConsoleSample()
        {
            while (true)
            {
                Console.WriteLine($"CPU  {MonitoringFunctions.GetCpuUsage()}");
            }
        }

        static void SerialSample()
        {
           port = new SerialPort("COM9",
                9600, Parity.None, 8, StopBits.One);

            port.DataReceived += Port_DataReceived;
            port.Open();

            while (true)
            {
                try
                {
                    int cpuUsage = 0;
                    int memoryUsage = 0;
                    int diskUsage = 0;

                    var cpuTask = Task.Run(() => { cpuUsage = MonitoringFunctions.GetCpuUsage(); });
                    var memTask = Task.Run(() => { memoryUsage = MonitoringFunctions.GetMemoryUsage(); });
                    var diskTask = Task.Run(() => { diskUsage = MonitoringFunctions.GetDiskUsage(); });

                    Task.WaitAll(new Task[] { cpuTask, memTask, diskTask });

                    port.Write($"{cpuUsage} {memoryUsage} {diskUsage}\r");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }
        }

        private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Console.WriteLine(port.ReadExisting());
        }
    }
}
