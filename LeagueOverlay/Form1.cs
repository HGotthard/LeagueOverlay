using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeagueOverlay
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RiotConnect rt = new RiotConnect();
            rt.init(this);
        }

        public PictureBox[] pb
        {
            get { return new PictureBox[] { this.pictureBox1, this.pictureBox2,this.pictureBox3,
                this.pictureBox4, this.pictureBox5, this.pictureBox6, this.pictureBox7,
                this.pictureBox8, this.pictureBox9, this.pictureBox10, }; }
        }

        public Label[] lb
        {
            get
            {
                return new Label[] { this.label1, this.label2,this.label3,
                this.label4, this.label5, this.label6, this.label7,
                this.label8, this.label9, this.label10, };
            }
        }
    }
}
