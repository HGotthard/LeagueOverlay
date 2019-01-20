using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueOverlay
{
    class TimerStarter
    {
        int time;
        Label label;
        Timer tm;
        public void startTimer(Label label, int time)
        {
            this.time = time;
            this.label = label;
            tm = new Timer();
            tm.Interval = 1000;
            tm.Tick += Tm_Tick1;
            tm.Start();
        }

        private void Tm_Tick1(object sender, EventArgs e)
        {
            label.Text = time.ToString();
            time--;
            if(time == 0)
            {
                tm.Stop();
                label.Text = "up";
            }
        }
    }

  
}
