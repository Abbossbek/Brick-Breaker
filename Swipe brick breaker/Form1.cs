using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Swipe_brick_breaker
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        private static int n = 24;
        private Random rn = new Random(); Timer t = new Timer();
        private int x, y, i = 0, k = 0, bw, bh, ball = 0, picw=15,pich=15, fastcount=0;
        private bool xb = true, yb = true;
        Button[] btn = new Button[n];
        public Form1()
        {
            InitializeComponent();
            bw = pictureBox1.Width / 10;
            bh = bw / 2;
            for (int j = 0; j < n; j++)
            {
                if (j % 8 == 0)
                    k++;
                btn[j] = new Button();
                btn[j].SetBounds(j % 8 * bw+bw, k * bh, bw, bh);
                btn[j].Text = "";
                btn[j].BackColor = Color.Blue;
                btn[j].Enabled = false;
                this.Controls.Add(btn[j]);
                btn[j].BringToFront();
            }
            Startpage.BringToFront();
            btnStart.BringToFront();
            btnQuit.BringToFront();
            pictureBox2.Location = new Point(r.Next(0, pictureBox1.Width - 30), r.Next(100, 300));

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (y < pictureBox1.Height)
            {
                for (int j = 0; j < n; j++)
                {
                    if (btn[j].Visible)
                    {
                        if (x == btn[j].Location.X + bw && y <= btn[j].Location.Y + bh && y + pich >= btn[j].Location.Y - pich)
                        {
                            xb = true;
                            btn[j].Visible = false;
                            ball++;
                        }
                        if (x + picw == btn[j].Location.X && y <= btn[j].Location.Y + bh && y + pich >= btn[j].Location.Y)
                        {
                            xb = false;
                            btn[j].Visible = false;
                            ball++;
                        }
                        if (y == btn[j].Location.Y + bh && x <= btn[j].Location.X + bw && x >= btn[j].Location.X - picw)
                        {
                            yb = true;
                            btn[j].Visible = false;
                            ball++;
                        }
                        if (y + pich == btn[j].Location.Y &&x<= btn[j].Location.X + bw && x >= btn[j].Location.X - picw)
                        {
                            yb = false;
                            btn[j].Visible = false;
                            ball++;
                        }
                        lblBall.Text = Convert.ToString(ball);
                    }
                    else
                    {
                        if (ball == n)
                        {
                            ball = 0;
                            while (i > 0)
                            {
                                t.Tick -= new EventHandler(timer1_Tick);
                                i--;
                            }
                            MessageBox.Show("YOU WIN");
                            Startpage.Visible = true;
                            Startpage.BringToFront();
                            btnStart.Visible = true;
                            btnStart.BringToFront();
                            btnQuit.Visible = true;
                            btnQuit.BringToFront();
                        }
                    }
                }
                pictureBox2.Location = new Point(x, y);
                if (x == 0)
                {
                    xb = true;
                }
                if (x == pictureBox1.Width - pictureBox2.Width)
                {
                    xb = false;
                }
                if (y == 0)
                {
                    yb = true;
                }
                if (y + pictureBox2.Height == button1.Location.Y && x + pictureBox2.Width / 2 <= button1.Location.X + button1.Width && x + pictureBox2.Width / 2 >= button1.Location.X)
                {
                    yb = false;
                    fastcount++;
                    if (fastcount == 2)
                    {
                        fastcount = 0;
                        i++;
                        t.Tick += new EventHandler(timer1_Tick);
                        t.Start();
                    }
                }
                if (y + pictureBox2.Height == button1.Location.Y && x + pictureBox2.Width / 2 <= button1.Location.X && x + pictureBox2.Width / 2 >= button1.Location.X - pictureBox2.Width/2)
                {
                    yb = false;
                    xb = false;
                    fastcount++;
                    if (fastcount == 2)
                    {
                        fastcount = 0;
                        i++;
                        t.Tick += new EventHandler(timer1_Tick);
                        t.Start();
                    }
                }
                if (y + pictureBox2.Height == button1.Location.Y && x + pictureBox2.Width / 2 <= button1.Location.X + button1.Width + pictureBox2.Width/2 && x + pictureBox2.Width / 2 >= button1.Location.X + button1.Width)
                {
                    yb = false;
                    xb = true;
                    fastcount++;
                    if (fastcount == 2)
                    {
                        fastcount = 0;
                        i++;
                        t.Tick += new EventHandler(timer1_Tick);
                        t.Start();
                    }
                }
                x = xb ? x + 1 : x - 1;
                y = yb ? y + 1 : y - 1;
            }
            else
            {
                y = 293;
                while (i > 0)
                {
                    t.Tick -= new EventHandler(timer1_Tick);
                    i--;
                }
                MessageBox.Show("GAME OVER");
                Startpage.Visible = true;
                Startpage.BringToFront();
                btnStart.BringToFront();
                btnStart.Visible = true;
                btnQuit.Visible = true;
                btnQuit.BringToFront();
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < pictureBox1.Width - button1.Width)
            {
                button1.Location = new Point(e.X, button1.Location.Y);
            }
            else
            {
                button1.Location = new Point(pictureBox1.Width - button1.Width, button1.Location.Y);
            }
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            Startpage.Visible = false;
            btnStart.Visible = false;
            btnQuit.Visible = false;
            t.Interval = 1;
            i++;
            t.Tick += new EventHandler(timer1_Tick);
            x = 500;
            y = 280;
            ball = 0;
            t.Start();

            for (int j = 0; j < 16; j++)
            {
                btn[j].Visible = true;
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
