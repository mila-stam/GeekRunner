using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeekRunnerApp
{
    public partial class Form1 : Form
    {
        private int score;
        private int highScore;
        private int gameSpeed;
        private bool jumping;
        private int jumpSpeed;
        private int force;
        private Random random;

        private int level;
        private readonly int[] levelSpeeds = { 10, 15, 20 };
        private readonly int[] levelScores = { 10, 20, 30 };


        private int groundLevel; 
        private int initialCharacterTop; 

        private readonly string[] charactersImages = { "Images\\search_1.png", "Images\\wrench_1.png", "Images\\find_1.png" };  
        private readonly string[] backgroundImages = { "Images\\background_1.jpg", "Images\\background_1.jpg", "Images\\background_1.jpg" };  
        private readonly string[] obstacleImages = { "Images\\desktop_1.png", "Images\\left-click_1.png", "Images\\wireless-router_1.png" };  
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            DoubleBuffered = true;
        }

        private void InitializeGame()
        {
            score = 0;
            highScore = 0;
            gameSpeed = levelSpeeds[0];
            jumping = false;
            level = 1;
            jumpSpeed = 0;
            force = 0;
            random = new Random();
            lblLevel.Text = "Level 1";
            lblScore.Text = "Score: 0";


            groundLevel = this.ClientSize.Height - pb_character.Height - 100; 
            initialCharacterTop = groundLevel; 
            pb_character.Top = groundLevel; 
            pb_character.Left = 50;
            pb_character.Image = Image.FromFile(charactersImages[0]); 
            this.BackgroundImage = Image.FromFile(backgroundImages[0]); 
            AddObstacles(); 

            tGame.Start();
        }

        private void AddObstacles()
        {
            int lastObstaclePosition = this.ClientSize.Width; 
            for (int i = 0; i < 3; i++) 
            {
                PictureBox obstacle = new PictureBox
                {
                    Tag = "obstacle",
                    Image = Image.FromFile(obstacleImages[random.Next(obstacleImages.Length)]), 
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Left = lastObstaclePosition + random.Next(700, 1000), 
                    Top = groundLevel + 50 
                };
                lastObstaclePosition = obstacle.Left;
                this.Controls.Add(obstacle);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pb_character.Top + pb_character.Height < groundLevel || jumping)
            {
                pb_character.Top += jumpSpeed;

                if (jumping)
                {
                    jumpSpeed = -12;
                    force -= 1;

                    if (force < 0)
                    {
                        jumping = false;
                    }
                }
                else
                {
                    jumpSpeed = 12;
                }
            }
            else
            {
                pb_character.Top = groundLevel;
                jumping = false;

            }
            int lastObstaclePosition = this.ClientSize.Width;
            foreach (Control x1 in this.Controls) 
            {
                if (x1 is PictureBox && x1.Tag == "obstacle")
                {
                    lastObstaclePosition = x1.Left;
                }
            }
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "obstacle")
                    {
                        
                        x.Left -= gameSpeed;
                        if (x.Left < -100)
                        {
                            x.Left = lastObstaclePosition + random.Next(700, 1000); //added
                            //do //added do/while za da se proveri dali se poklopuva nekoja prepreka
                            //{
                            //    x.Left = this.ClientSize.Width + random.Next(500, 1000);
                            //} while (IsOverlappingObstacle(x));
                            score++;
                            
                            CheckForLevelUp();
                        }
                        if (pb_character.Bounds.IntersectsWith(x.Bounds))
                        {
                            
                            GameOver();
                        }
                        lastObstaclePosition = x.Left;

                    }
                }
                lblScore.Text = "Score: " + score;

                if (score > highScore)
                {
                    highScore = score;
                }

            }

            private bool IsOverlappingObstacle(Control newObstacle) 
            {
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "obstacle" && x != newObstacle)
                    {
                        if (newObstacle.Bounds.IntersectsWith(x.Bounds))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            private void CheckForLevelUp()
            {
                if (level <= 3 && score >= levelScores[level - 1])
                {
                    MessageBox.Show("Congratulations, you passed the level!", "Level Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    level++;
                    if (level <= 3)
                    {
                        gameSpeed = levelSpeeds[level - 1];
                        lblLevel.Text = "Level " + level;

                        
                        pb_character.Image = Image.FromFile(charactersImages[level - 1]); 
                        this.BackgroundImage = Image.FromFile(backgroundImages[level - 1]);
                    }
                    else
                    {
                        MessageBox.Show("Congratulations, you finished the game!", "Game Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        GameOver();
                    }
                }
            }

            private void GameOver()
            {
                tGame.Stop();
                jumping = false;
                MessageBox.Show("Your score: " + score + "\nHigh Score: " + highScore);
                score = 0;
                level = 1;
                gameSpeed = levelSpeeds[0];
                lblLevel.Text = "Level 1";
                lblScore.Text = "Score: 0";

                
                pb_character.Image = Image.FromFile(charactersImages[0]);  
                this.BackgroundImage = Image.FromFile(backgroundImages[0]);
                int lastObstaclePosition = this.ClientSize.Width;
            foreach (Control x1 in this.Controls)
            {
                if (x1 is PictureBox && x1.Tag == "obstacle")
                {
                    lastObstaclePosition = x1.Left;
                }
            }
            foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "obstacle") {
                    x.Left = lastObstaclePosition + random.Next(700, 1000);
                    lastObstaclePosition = x.Left;
                }

                }
                
                
                tGame.Start();
            }

            private void Form1_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Space && pb_character.Top == groundLevel && jumping == false)
                {
                    jumping = true;
                    force = 15;
                }
            }
        }
    }

