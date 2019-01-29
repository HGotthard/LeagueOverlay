using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

        public Form1()
        {
            InitializeComponent();
            rt = new RiotConnect();
            Task taks = rt.InitAsync(this);
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
