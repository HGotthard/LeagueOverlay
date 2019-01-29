using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueOverlay
{
    public partial class Form1 : Form
    {
        RiotConnect rt;
        private int timeALL = 150;
        private int timeCrab = 180;

        #region click through window code
        enum WindowStyles : uint
        {
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = 0x80000000,
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,

            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,

            WS_CAPTION = WS_BORDER | WS_DLGFRAME,
            WS_TILED = WS_OVERLAPPED,
            WS_ICONIC = WS_MINIMIZE,
            WS_SIZEBOX = WS_THICKFRAME,
            WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,

            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_CHILDWINDOW = WS_CHILD,

            //Extended Window Styles

            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_TRANSPARENT = 0x00000020,

            //#if(WINVER >= 0x0400)

            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,

            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,

            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,

            WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE),
            WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST),
            //#endif /* WINVER >= 0x0400 */

            //#if(WIN32WINNT >= 0x0500)

            WS_EX_LAYERED = 0x00080000,
            //#endif /* WIN32WINNT >= 0x0500 */

            //#if(WINVER >= 0x0500)

            WS_EX_NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
            WS_EX_LAYOUTRTL = 0x00400000, // Right to left mirroring
            //#endif /* WINVER >= 0x0500 */

            //#if(WIN32WINNT >= 0x0500)

            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_NOACTIVATE = 0x08000000
            //#endif /* WIN32WINNT >= 0x0500 */

        }
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint wMsg, UIntPtr wParam, IntPtr lParam); //used for maximizing the screen

        const int WM_SYSCOMMAND = 0x0112; //used for maximizing the screen.
        const int myWParam = 0xf120; //used for maximizing the screen.
        const int myLparam = 0x5073d; //used for maximizing the screen.


        int oldWindowLong;
        public enum GetWindowLongConst
        {
            GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12)
        }

        public enum LWA
        {
            ColorKey = 0x1,
            Alpha = 0x2,
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        /// <summary>
        /// Make the form (specified by its handle) a window that supports transparency.
        /// </summary>
        /// <param name="Handle">The window to make transparency supporting</param>
        public void SetFormTransparent(IntPtr Handle)
        {
            oldWindowLong = GetWindowLong(Handle, (int)GetWindowLongConst.GWL_EXSTYLE);
            SetWindowLong(Handle, (int)GetWindowLongConst.GWL_EXSTYLE, Convert.ToInt32(oldWindowLong | (uint)WindowStyles.WS_EX_LAYERED | (uint)WindowStyles.WS_EX_TRANSPARENT));
        }

        /// <summary>
        /// Make the form (specified by its handle) a normal type of window (doesn't support transparency).
        /// </summary>
        /// <param name="Handle">The Window to make normal</param>
        public void SetFormNormal(IntPtr Handle)
        {
            SetWindowLong(Handle, (int)GetWindowLongConst.GWL_EXSTYLE, Convert.ToInt32(oldWindowLong | (uint)WindowStyles.WS_EX_LAYERED));
        }

        /// <summary>
        /// Makes the form change White to Transparent and clickthrough-able
        /// Can be modified to make the form translucent (with different opacities) and change the Transparency Color.
        /// </summary>
        public void SetTheLayeredWindowAttribute()
        {
            uint transparentColor = 0xffffffff;

            SetLayeredWindowAttributes(this.Handle, transparentColor, 125, 0x2);

            this.TransparencyKey = Color.White;
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
            rt = new RiotConnect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        public PictureBox[] pb
        {
            get
            {
                return new PictureBox[] { this.pictureBox1, this.pictureBox2,this.pictureBox3,
                this.pictureBox4, this.pictureBox5, this.pictureBox6, this.pictureBox7,
                this.pictureBox8, this.pictureBox9, this.pictureBox10};
            }
        }

        public PictureBox[] campPb
        {
            get
            {
                return new PictureBox[] { this.pictureBox11, this.pictureBox12,this.pictureBox13,
                this.pictureBox14, this.pictureBox15, this.pictureBox16, this.pictureBox17,
                this.pictureBox18, this.pictureBox19, this.pictureBox20};
            }
        }

        public Label[] lb
        {
            get
            {
                return new Label[] { this.label1, this.label2,this.label3,
                this.label4, this.label5, this.label6, this.label7,
                this.label8, this.label9, this.label10};
            }
        }

        public Label[] championLabel
        {
            get
            {
                return new Label[] { this.label11, this.label12,this.label13,
                this.label14, this.label15};
            }
        }

        public Label[] campLb
        {
            get
            {
                return new Label[] { this.label16, this.label17,this.label18,
                this.label19, this.label20, this.label21, this.label22,
                this.label23, this.label24, this.label25};
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Enabled = false;
            TimerStarter ts = new TimerStarter();

            var cooldown = rt.GetCdLabels.Find(x => x.GetLabel.Equals(label1)).GetCd;
            ts.startTimer(pictureBox1, label1, cooldown);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Enabled = false;
            TimerStarter ts = new TimerStarter();

            var cooldown = rt.GetCdLabels.Find(x => x.GetLabel.Equals(label2)).GetCd;
            ts.startTimer(pictureBox2, label2, cooldown);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Enabled = false;
            TimerStarter ts = new TimerStarter();

            var cooldown = rt.GetCdLabels.Find(x => x.GetLabel.Equals(label3)).GetCd;
            ts.startTimer(pictureBox3, label3, cooldown);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.Enabled = false;
            TimerStarter ts = new TimerStarter();

            var cooldown = rt.GetCdLabels.Find(x => x.GetLabel.Equals(label4)).GetCd;
            ts.startTimer(pictureBox4, label4, cooldown);
        }

        internal void InitRiot()
        {
            Task taks = rt.InitAsync(this);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox5.Enabled = false;
            TimerStarter ts = new TimerStarter();
            var cooldown = rt.GetCdLabels.Find(x => x.GetLabel.Equals(label5)).GetCd;
            ts.startTimer(pictureBox5, label5, cooldown);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pictureBox6.Enabled = false;
            TimerStarter ts = new TimerStarter();
            var cooldown = rt.GetCdLabels.Find(x => x.GetLabel.Equals(label6)).GetCd;
            ts.startTimer(pictureBox6, label6, cooldown);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pictureBox7.Enabled = false;
            TimerStarter ts = new TimerStarter();

            var cooldown = rt.GetCdLabels.Find(x => x.GetLabel.Equals(label7)).GetCd;
            ts.startTimer(pictureBox7, label7, cooldown);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            pictureBox8.Enabled = false;
            TimerStarter ts = new TimerStarter();

            var cooldown = rt.GetCdLabels.Find(x => x.GetLabel.Equals(label8)).GetCd;
            ts.startTimer(pictureBox8, label8, cooldown);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            pictureBox9.Enabled = false;
            TimerStarter ts = new TimerStarter();

            var cooldown = rt.GetCdLabels.Find(x => x.GetLabel.Equals(label9)).GetCd;
            ts.startTimer(pictureBox9, label9, cooldown);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pictureBox10.Enabled = false;
            TimerStarter ts = new TimerStarter();

            var cooldown = rt.GetCdLabels.Find(x => x.GetLabel.Equals(label10)).GetCd;
            ts.startTimer(pictureBox10, label10, cooldown);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            pictureBox11.Enabled = false;
            TimerStarter ts = new TimerStarter();
            ts.startTimer(pictureBox11, label16, timeALL);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            pictureBox12.Enabled = false;
            TimerStarter ts = new TimerStarter();
            ts.startTimer(pictureBox12, label17, timeALL);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            pictureBox13.Enabled = false;
            TimerStarter ts = new TimerStarter();
            ts.startTimer(pictureBox13, label18, timeALL);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            pictureBox14.Enabled = false;
            TimerStarter ts = new TimerStarter();
            ts.startTimer(pictureBox14, label19, timeALL);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            pictureBox15.Enabled = false;
            TimerStarter ts = new TimerStarter();
            ts.startTimer(pictureBox15, label20, timeCrab);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            pictureBox16.Enabled = false;
            TimerStarter ts = new TimerStarter();
            ts.startTimer(pictureBox16, label21, timeCrab);
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            pictureBox17.Enabled = false;
            TimerStarter ts = new TimerStarter();
            ts.startTimer(pictureBox17, label22, timeALL);
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            pictureBox18.Enabled = false;
            TimerStarter ts = new TimerStarter();
            ts.startTimer(pictureBox18, label23, timeALL);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            pictureBox19.Enabled = false;
            TimerStarter ts = new TimerStarter();
            ts.startTimer(pictureBox19, label24, timeALL);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            pictureBox20.Enabled = false;
            TimerStarter ts = new TimerStarter();
            ts.startTimer(pictureBox20, label25, timeALL);
        }
    }
}
