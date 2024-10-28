using FormHWPApp.Arduino;
using FormHWPApp.HWM;
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
            HWM.HWMService hwmService = new HWM.HWMService();
            hwmService.Open();

            Arduino arduino = new Arduino();
            
            PeriodicalTask periodical = new PeriodicalTask();
            periodical.setCoreAction(hwmService.RefreshData);

            periodical.StartTask();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(hwmService, arduino, periodical));

            
        }
    }
}
