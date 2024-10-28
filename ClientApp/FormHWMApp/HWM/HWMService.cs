using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FormTestApp.HWM
{
    public class HWMService
    {
        public static volatile Dictionary<string, Dictionary<string, List<ISensor>>> pc;

        public static int pcUpdatedCounter = 0;
        public static Computer computer;
        
        public string GetTemp()
        {
            ISensor sensor = pc["Intel Core i7-4790K"]["Load"][0];
            return sensor.Value.ToString();
        }

        public List<ISensor> GetLoads()
        {
            return pc["Intel Core i7-4790K"]["Load"];
        }

        public void Open()
        {
            Computer pc = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsControllerEnabled = true,
                IsNetworkEnabled = true,
                IsStorageEnabled = true,
                IsPsuEnabled = false,
                IsBatteryEnabled = false,
            };

            pc.Open();
            computer = pc;
        }

        public void Close()
        {
            computer.Close();
        }

        public void StartThread()
        {
            Open();
            //doUpdate = true;
            //Thread thread = new Thread(new ThreadStart(RefreshData));
            //thread.Start();
        }

        public void StopThread()
        {
            //Close();
            //doUpdate = false;
            pcUpdatedCounter = 0;
        }

        public void RefreshData()
        {
            
                pcUpdatedCounter++;
                computer.Accept(new UpdateVisitor());
                pc = new Dictionary<string, Dictionary<string, List<ISensor>>>();

                foreach (IHardware hardware in computer.Hardware)
                {
                    pc.Add(hardware.Name, new Dictionary<string, List<ISensor>> { });
                    foreach (IHardware subhardware in hardware.SubHardware)
                    {
                        foreach (ISensor sensor in subhardware.Sensors)
                        {
                            if (!pc[hardware.Name].ContainsKey(sensor.SensorType.ToString()))
                            {
                                pc[hardware.Name].Add(sensor.SensorType.ToString(), new List<ISensor>());
                            }
                            pc[hardware.Name][sensor.SensorType.ToString()].Add(sensor);
                        }
                    }

                    foreach (ISensor sensor in hardware.Sensors)
                    {
                        //Console.WriteLine("\tSensor: {0}, value: {1}", sensor.Name, sensor.Value);
                        if (!pc[hardware.Name].ContainsKey(sensor.SensorType.ToString()))
                        {
                            pc[hardware.Name].Add(sensor.SensorType.ToString(), new List<ISensor>());
                        }
                        pc[hardware.Name][sensor.SensorType.ToString()].Add(sensor);
                    }
                } 
               
            
            
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
