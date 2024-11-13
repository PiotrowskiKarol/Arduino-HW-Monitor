using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormHWPApp.Arduino
{
    public class ArduinoConfig
    {
        private String SerialPort = "COM9";
        private int boundwith = 9600;

        public String GetSerialPort()
        {
            return SerialPort;
        }

        public int GetBoundwith()
        {
            return boundwith;
        }
    }
}
