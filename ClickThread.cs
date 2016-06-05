using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fastclick
{
    class ClickThread
    {
        private Thread clickThread;
        private volatile bool isWorking = false;
        private volatile int millisecondsInterval= 33;

        public int MillisecondsInterval
        {
            get { return millisecondsInterval; }
            set { millisecondsInterval = value; }
        }

        public bool IsWorking
        {
            get { return isWorking; }
        }

        public void runOrStop()
        {
            if (clickThread != null && clickThread.IsAlive)
            {
                isWorking = false;
            }
            else
            {
                isWorking = true;
                clickThread = new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    while (isWorking)
                    {
                        Clicker.LeftMouseClick();
                        Thread.Sleep(millisecondsInterval);
                    }
                });
                clickThread.Start();
            }
        }
    }
}
