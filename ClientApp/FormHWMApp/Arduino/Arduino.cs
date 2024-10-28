using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormHWPApp.Arduino
{
    class Arduino : ISerialCommunication
    {
        private SerialPort port;
        private String serialPort = "COM9";
        private int boundwith = 9600;

        public void sendData(string text)
        {
            port.Write(text);
        }

        public void Init()
        {
            port = new SerialPort(serialPort, boundwith);
            port.Open();
            Console.WriteLine("Inicjalizacja");
        }

        public void Close()
        {
            port.Close();
            port = null;
        }
        public void readData()
        {
            Console.WriteLine(port.ReadExisting());
        }

        public void startCommunication()
        {
            Init();
        }

        public void stopCommunication()
        {
            Close();
        }

        public bool isConnected()
        {
            return port != null;
        }
    }
}
