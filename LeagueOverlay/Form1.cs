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
    }
}
