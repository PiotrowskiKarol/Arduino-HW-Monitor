using FormTestApp.HWM;
using LibreHardwareMonitor.Hardware;
using System.Collections.Generic;

namespace FormHWPApp.HWM
{
    public class HWMData : IHWMData
    {
        public static volatile Dictionary<string, Dictionary<string, List<ISensor>>> pc;
        public static volatile PCData PCData;

        public static int pcUpdatedCounter = 0;

        private readonly IHWMDataService HWMDataService;
        public HWMData(IHWMDataService dataService)
        {
            this.HWMDataService = dataService;
        }

        public void GetNewBatch()
        {
            Computer computer = HWMDataService.GetComputerData();

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
                    if (!pc[hardware.Name].ContainsKey(sensor.SensorType.ToString()))
                    {
                        pc[hardware.Name].Add(sensor.SensorType.ToString(), new List<ISensor>());
                    }
                    pc[hardware.Name][sensor.SensorType.ToString()].Add(sensor);
                }
            }

            PCData pcData = new PCData();
            pcData.pcname = "PECET";
           

            foreach (IHardware hardware in computer.Hardware)
            {
                PCComponent pcComponent = new PCComponent();
                pcComponent.name = hardware.Name;

                pcData.data.Add(pcComponent);

                foreach (ISensor sensor in hardware.Sensors)
                {
                    ComponentData componentData = new ComponentData();
                    componentData.value = sensor.Value.ToString();
                    componentData.type = sensor.SensorType.ToString();
                    componentData.name = sensor.Name;
                    pcComponent.components.Add(componentData);
                }
            }
            PCData = pcData;

            pcUpdatedCounter++;
        }

        public List<ISensor> GetLoads()
        {
            return pc["Intel Core i7-4790K"]["Load"];
        }

        public string GetTemp()
        {
            ISensor sensor = pc["Intel Core i7-4790K"]["Load"][0];
            return sensor.Value.ToString();
        }

        public int getUpdateCounter()
        {
           return pcUpdatedCounter;
        }

        public PCData GetData()
        {
            return PCData;
        }
    }
}
