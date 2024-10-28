using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FormHWPApp.HWM
{
    public class PeriodicalTask
    {
        private volatile bool doUpdate = true;
        private int milisUpdateRate = 1000;

        private Action coreAction;
        private event Action afterAction;

        /*public PeriodicalTask(Action coreAction)
        {
            this.coreAction = coreAction;
        }*/
        public void setCoreAction(Action action)
        {
            this.coreAction = action;
        }

        public void StartTask()
        {
            doUpdate = true;
            Thread thread = new Thread(new ThreadStart(doJob));
            thread.Start();
        }

        public void StopTask()
        {
            doUpdate = false;
        }

        private void doJob()
        {
            while (doUpdate)
            {
                coreAction.Invoke();
                afterAction?.BeginInvoke(afterAction.EndInvoke, null); //async
                //NewData?.Invoke(); //sync
                Thread.Sleep(milisUpdateRate);
            }
        }

        public void addAction(Action action)
        {
            this.afterAction += action;
        }
    }
}
