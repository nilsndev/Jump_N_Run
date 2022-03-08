using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Media;
using System.Windows.Forms;

namespace Jump_N_Run
{
    public partial class Form1 : Form
    {
        SoundPlayer sp = new SoundPlayer();
        bool goLeft, goRight;
        bool jumping = false;
        bool coinFlipped = false;
        bool falling = true;
        int playerMoveXSpeed = 5;
        int backgroundSpeed = 5;
        bool playerFalsorJumps;
        int fallSpeed = 7;
        int standardLeft = 0;
        int jumpSpeed = 50;
        Point spawnLocation = new Point(141, 772);


        public Form1()
        {
            InitializeComponent();

        }

        private void game_timer1_Tick(object sender, EventArgs e)
        {
            if (player_pictureBox1.Location.Y >= this.Height)
            {
                this.background_panel1.Controls.Add(falling_platform_pictureBox1);
                this.background_panel1.Controls.Add(falling_platform_pictureBox2);
                this.background_panel1.Controls.Add(falling_platform_pictureBox3);
                background_panel1.Left = standardLeft;
                player_pictureBox1.Location = spawnLocation;
            }
            //-----------Move X---------------------------
            if (goLeft)
            {

                player_pictureBox1.Left -= playerMoveXSpeed;

                if (background_panel1.Left < 0)
                    background_panel1.Left += backgroundSpeed;
            }

            if (goRight)
            {
                player_pictureBox1.Left += playerMoveXSpeed;

                if (background_panel1.Left + background_panel1.Width > this.Width)
                    background_panel1.Left -= backgroundSpeed;
            }

            //===============================================

            //----------Move Y-------------------------------
            if (jumping && !falling)
            {

                if (!falling)
                {
                    ;
                    falling = true;
                    for (int i = 0; i < 4; i++)
                    {
                        player_pictureBox1.Top -= jumpSpeed;
                    }

                }
                jumping = false;

            }
            if (falling)
            {
                player_pictureBox1.Top += fallSpeed;



            }
            //===============================================

            foreach (Control c in this.background_panel1.Controls)
            {
                if (c is PictureBox)
                {

                    if ((string)c.Tag == "platform"|| (string)c.Tag == "falling_platform")
                    {
                        if (player_pictureBox1.Bounds.IntersectsWith(c.Bounds) && !jumping)
                        {
                            if (player_pictureBox1.Top + 30 < c.Top)
                            {
                                playerFalsorJumps = false;
                                player_pictureBox1.Top = c.Top - player_pictureBox1.Height;
                                falling = false;

                                if((string)c.Tag == "falling_platform")
                                    
                                {
                                    this.background_panel1.Controls.Remove(c);
                                    
                               
                                }
                                if(c == bounds_pictureBox1)
                                {

                                    for (int i = 0; i < 4; i++)
                                    {
                                        player_pictureBox1.Top -= 190;
                                    }
                                        falling = true;
                                    
                                }
                                if (player_pictureBox1.Bounds.IntersectsWith(rohr_start_pictureBox1.Bounds))
                                {
                                    background_panel1.Left = -rohr_end_pictureBox1.Location.X + 100;
                                    player_pictureBox1.Location = new Point(rohr_end_pictureBox1.Location.X, rohr_end_pictureBox1.Location.Y - 50);
                                }
                                if (c == final_pictureBox6 || c == final_pictureBox2 || c == final__pictureBox1)
                                {

                                    newSpawn(c);
                                  //  spawnLocation = new Point(final_pictureBox6.Location.X, final_pictureBox6.Location.Y - player_pictureBox1.Height + 20);
                                  //  standardLeft = -(final_pictureBox6.Left - 200);
                                }
                            }
                        }

                        else if (!player_pictureBox1.Bounds.IntersectsWith(c.Bounds) && !jumping)
                        {
                            falling = true;
                        }

                    }
                    if ((string)c.Tag == "border")
                    {
                        if (player_pictureBox1.Bounds.IntersectsWith(c.Bounds))
                        {

                            if (player_pictureBox1.Top + 50 > c.Top)
                            {
                                player_pictureBox1.Location = spawnLocation;
                                background_panel1.Left = 0;

                            }
                            else if (player_pictureBox1.Top + 50 < c.Top && !jumping)
                            {
                                player_pictureBox1.Top = c.Top - player_pictureBox1.Height;
                                falling = false;
                                playerFalsorJumps = false;
                            }
                        }




                    }
                    if ((string)c.Tag == "zack" || (string)c.Tag == "lava")
                    {
                        if (player_pictureBox1.Bounds.IntersectsWith(c.Bounds))
                        {
                            background_panel1.Left = standardLeft;
                            player_pictureBox1.Location = spawnLocation;

                        }
                    }


                    if ((string)c.Tag == "ziel")
                    {
                        if (player_pictureBox1.Bounds.IntersectsWith(c.Bounds))
                        {
                            Application.Exit();
                        }
                    }

                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {

                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && !jumping && !playerFalsorJumps)
            {
                playerFalsorJumps = true;
                falling = false;
                jumping = true;


            }
        }

        private void platform_pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void background_panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                axWindowsMediaPlayer1.URL = @"C:\Users\Nils\source\repos\cs_programming\Projects_cs\Jump_N_Run_sn\Jump_N_Run\Sounds\mdk_music.wav";
            }
            catch { }
                this.Focus();

            background_panel1.Left = standardLeft;


        }

        private void exit_pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void coin_timer1_Tick(object sender, EventArgs e)
        {
            foreach (Control c in background_panel1.Controls)
            {
                if (c is PictureBox)
                {

                    if ((string)c.Tag == "coin")
                    {



                        if (player_pictureBox1.Bounds.IntersectsWith(c.Bounds))
                        {
                            try
                            {
                                sp.SoundLocation = @"C:\Users\Nils\source\repos\cs_programming\Projects_cs\Jump_N_Run_sn\Jump_N_Run\Sounds\Collect_Point_02.wav";

                                sp.Play();
                            }
                            catch
                            {

                            }
                            this.background_panel1.Controls.Remove(c);
                        }
                    }
                }
            }
        }

        private void coin_rotate_timer1_Tick(object sender, EventArgs e)
        {

            if (coinFlipped)
            {
                coin_pictureBox1.BackgroundImage = Jump_N_Run.Properties.Resources.coin;
                coin_pictureBox2.BackgroundImage = Jump_N_Run.Properties.Resources.coin;
                coin_pictureBox3.BackgroundImage = Jump_N_Run.Properties.Resources.coin;
                coin_pictureBox4.BackgroundImage = Jump_N_Run.Properties.Resources.coin;
                coin_pictureBox5.BackgroundImage = Jump_N_Run.Properties.Resources.coin;
                coin_pictureBox6.BackgroundImage = Jump_N_Run.Properties.Resources.coin;
                coin_pictureBox7.BackgroundImage = Jump_N_Run.Properties.Resources.coin;
                coinFlipped = false;
            }
            else
            {
                coin_pictureBox1.BackgroundImage = Jump_N_Run.Properties.Resources.coin_90;
                coin_pictureBox2.BackgroundImage = Jump_N_Run.Properties.Resources.coin_90;
                coin_pictureBox3.BackgroundImage = Jump_N_Run.Properties.Resources.coin_90;
                coin_pictureBox4.BackgroundImage = Jump_N_Run.Properties.Resources.coin_90;
                coin_pictureBox5.BackgroundImage = Jump_N_Run.Properties.Resources.coin_90;
                coin_pictureBox6.BackgroundImage = Jump_N_Run.Properties.Resources.coin_90;
                coin_pictureBox7.BackgroundImage = Jump_N_Run.Properties.Resources.coin_90;
                coinFlipped = true;
            }
        }

        private void minimize_pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }



        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        void newSpawn(Control c)
        {
            spawnLocation = new Point(c.Location.X, c.Top - player_pictureBox1.Height - 200);
            standardLeft = -(c.Left - 200);
        }

    }

}