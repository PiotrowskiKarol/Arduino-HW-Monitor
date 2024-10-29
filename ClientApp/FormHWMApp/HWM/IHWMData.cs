using LibreHardwareMonitor.Hardware;
using System.Collections.Generic;

namespace FormHWPApp.HWM
{
    public interface IHWMData
    {
        int getUpdateCounter();

        List<ISensor> GetLoads();
    }
}