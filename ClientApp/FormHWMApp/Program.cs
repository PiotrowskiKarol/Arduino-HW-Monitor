using FormHWPApp.Arduino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormTestApp
{
    internal static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Arduino arduino = new Arduino();

            HWM.HWMService hwmService = new HWM.HWMService();
            hwmService.Open();
            hwmService.StartThread();

            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(hwmService, arduino));
        }
    }
}
