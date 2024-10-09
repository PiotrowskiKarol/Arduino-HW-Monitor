using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HWM_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Monitor();
        }

        public static void Monitor()
        {
            

            //Dictionary<IHardware, Dictionary<strin, ISensor>> pc = new Dictionary<IHardware, Dictionary<string, ISensor>>();
            Dictionary<string, Dictionary<string, List<ISensor>>> pc2 = new Dictionary<string, Dictionary<string, List<ISensor>>>();

            Computer computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = false,
                IsControllerEnabled = false,
                IsNetworkEnabled = false,
                IsStorageEnabled = false
            };

            computer.Open();
            computer.Accept(new UpdateVisitor());

            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (IHardware hardware in computer.Hardware)
            {
                //Console.WriteLine("Hardware: {0}", hardware.Name);
                //pc2.Add(hardware.Name, new Dictionary<string, List<ISensor>>{} );

                foreach (IHardware subhardware in hardware.SubHardware)
                {
                    //Console.WriteLine("\tSubhardware: {0}", subhardware.Name);

                    foreach (ISensor sensor in subhardware.Sensors)
                    {
                        /*if(! pc2[hardware.Name].ContainsKey(sensor.SensorType.ToString()) )
                        {
                            pc2[hardware.Name].Add(sensor.SensorType.ToString(), new List<ISensor>());
                        }
                        pc2[hardware.Name][sensor.SensorType.ToString()].Add(sensor);*/

                        //Console.WriteLine("\t\tSensor: {0}, value: {1}, type {2}", sensor.Name, sensor.Value, sensor.SensorType);
                    }
                }

                foreach (ISensor sensor in hardware.Sensors)
                {
                    //Console.WriteLine("\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
                    /*if (!pc2[hardware.Name].ContainsKey(sensor.SensorType.ToString()))
                    {
                        pc2[hardware.Name].Add(sensor.SensorType.ToString(), new List<ISensor>());
                    }
                    pc2[hardware.Name][sensor.SensorType.ToString()].Add(sensor);*/
                }
            }

            //string selectedSensor = pc2["Intel Core i7-4790K"]["Load"][0].Value.ToString();

            computer.Close();

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }



    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }
}
