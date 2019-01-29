using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueOverlay
{

    class League_State_Checker
    {

        #region Getting League Window Size / Location
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT Rect);
        #endregion

        #region Global Variables 
        Form1 overlayForm = new Form1();
        Thread stateCheckerThread;
        Process leagueProcess;
        RECT lRect;
        #endregion

        /// <summary>
        /// Constructs the league state checker thread to run in the background
        /// </summary>
        public League_State_Checker()
        {

        }

        /// <summary>
        /// Starts the league state checker thread
        /// </summary>
        public void start()
        {
            overlayForm.Show();
            //overlayForm.Visible = false;
            stateCheckerThread = new Thread(new ThreadStart(stateChecker));
            stateCheckerThread.Start();
        }

        /// <summary>
        /// Stops the league state checker thread
        /// </summary>
        public void stop()
        {
            overlayForm.Hide();
            stateCheckerThread.Abort();

        }

        /// <summary>
        /// Checks all open processes for the league of legends window
        /// </summary>
        /// <returns> True if window is open, false if not</returns>
        public bool leagueWindowOpen()
        {
            Process[] processlist = Process.GetProcesses();
            Process lProcess = null;
            foreach (Process process in processlist)
            {
                if ((process.MainWindowTitle).ToString().Equals("League of Legends"))
                {
                    GetWindowRect(process.MainWindowHandle, ref lRect);
                    lProcess = process;
                    leagueProcess = process;
                }
            }
            return lProcess != null;
        }

        //Handles the picturebox manipulation in form2 (using standard overlay)
        public void displayInGameOverlay()
        {
            overlayForm.Invoke((MethodInvoker)delegate
            {
                overlayForm.InitRiot();
                overlayForm.Show();
                overlayForm.WindowState = FormWindowState.Maximized;
                overlayForm.Location = new Point(lRect.left, lRect.top);
                overlayForm.Height = lRect.bottom;
                overlayForm.Width = lRect.right;


                var startPoint = overlayForm.Height / 2 - overlayForm.pb[0].Height / 2 * 5 - 100;
                var champLabel = 13;

                for (int i = 0; i < overlayForm.pb.Length; i += 2)
                {
                    overlayForm.championLabel[i / 2].Location = new Point(0, (i / 2) * overlayForm.pb[i].Height + startPoint + (i / 2) * champLabel);

                    overlayForm.pb[i].Location = new Point(0, (i / 2) * overlayForm.pb[i].Height + startPoint + ((i / 2) + 1) * champLabel);
                    overlayForm.pb[i + 1].Location = new Point(overlayForm.pb[i].Width, (i / 2) * overlayForm.pb[i].Height + startPoint + ((i / 2) + 1) * champLabel);
                    overlayForm.lb[i].Location = new Point(0, (i / 2) * overlayForm.pb[i].Height + startPoint + ((i / 2) + 1) * champLabel);
                    overlayForm.lb[i + 1].Location = new Point(overlayForm.pb[i].Width, (i / 2) * overlayForm.pb[i].Height + startPoint + ((i / 2) + 1) * champLabel);


                    overlayForm.campPb[i].Location = new Point(overlayForm.Width - 2 * overlayForm.campPb[i].Width, (i / 2) * overlayForm.pb[i].Height + startPoint);
                    overlayForm.campPb[i + 1].Location = new Point(overlayForm.Width - 1 * overlayForm.campPb[i].Width, (i / 2) * overlayForm.pb[i].Height + startPoint);
                    overlayForm.campLb[i].Location = new Point(overlayForm.Width - 2 * overlayForm.campPb[i].Width, (i / 2) * overlayForm.pb[i].Height + startPoint);
                    overlayForm.campLb[i + 1].Location = new Point(overlayForm.Width - 1 * overlayForm.campPb[i].Width, (i / 2) * overlayForm.pb[i].Height + startPoint);
                }
                overlayForm.Visible = true;
            });
        }

        /// <summary>
        /// This is what will continuously happen while the state checker thread
        /// is active. ths logic main branch.
        /// </summary>
        private void stateChecker()
        {
            bool overlayComplete = false;
            while (stateCheckerThread.IsAlive)
            {
                if (leagueWindowOpen())
                {
                    if (!overlayComplete)
                    {
                        displayInGameOverlay();
                        overlayComplete = true;
                    }
                }
                else
                {
                    overlayForm.Invoke((MethodInvoker)delegate
                    {
                        overlayForm.Visible = false;
                        overlayForm.WindowState = FormWindowState.Minimized;
                        overlayComplete = false;
                    });
                }
                Thread.Sleep(1000);
            }

        }


    }
}