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
    public class HWMService : IHWMDataService
    {
        
        public static Computer computer;

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
        }

        public Computer GetComputerData()
        {
            computer.Accept(new UpdateVisitor());
            return computer;
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
