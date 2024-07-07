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


        private int groundLevel; //added
        private int initialCharacterTop; //added

        private readonly string[] charactersImages = { "Images\\search_1.png", "Images\\wrench_1.png", "Images\\find_1.png" };  //added
        private readonly string[] backgroundImages = { "Images\\background_1.jpg", "Images\\background_1.jpg", "Images\\background_1.jpg" };  //added
        private readonly string[] obstacleImages = { "desktop_1.png", "Images\\left-click_1.png", "Images\\wireless-router_1.png" };  //added
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


            groundLevel = this.ClientSize.Height - pb_character.Height - 100; //added
            initialCharacterTop = groundLevel; //added
            pb_character.Top = groundLevel; //added
            pb_character.Left = 50;
            pb_character.Image = Image.FromFile(charactersImages[0]); //added
            this.BackgroundImage = Image.FromFile(backgroundImages[0]); //added
            AddObstacles(); //added

            tGame.Start();
        }

        private void AddObstacles()//added
        {
            for (int i = 0; i < 3; i++) // Adjust the number of obstacles as needed
            {
                PictureBox obstacle = new PictureBox
                {
                    Tag = "obstacle",
                    Image = Image.FromFile(obstacleImages[random.Next(obstacleImages.Length)]), // Select a random image for the obstacle
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Left = this.ClientSize.Width + random.Next(200, 800),
                    Top = groundLevel + 50 // Adjust height based on ground level and obstacle size
                };
                this.Controls.Add(obstacle);
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*pb_character.Top += jumpSpeed; //karakterot go pomestuvame spored jumpspeedot, 

            if (jumping && force < 0) //proveruvame dali karakterot treba da prekine da skoka
            {
                jumping = false;
            }
            if (jumping) // ako skoka namaluvame vrednost za da odi nagore, vo sprotivno dodeluvame pozitivna za da ide nadole
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }*/

            //proben kod za da ne odi podole od odredeno nivo
            if (pb_character.Top + pb_character.Height < groundLevel || jumping)
            {
                pb_character.Top += jumpSpeed;

                if (jumping)
                {
                    jumpSpeed = -10; 
                    force -= 1; 

                    if (force < 0)
                    {
                        jumping = false;
                    }
                }
                else
                {
                    jumpSpeed = 10;
                }
            }
            else
            {
                pb_character.Top = groundLevel;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "obstacle")
                {
                    x.Left -= gameSpeed;
                    if (x.Left < -100)
                    {
                        do //added do/while za da se proveri dali se poklopuva nekoja prepreka
                        {
                            x.Left = this.ClientSize.Width + random.Next(200, 800);
                        } while (IsOverlappingObstacle(x));
                        score++;
                        //levelup funkcija tuka
                        CheckForLevelUp();
                    }
                    if (pb_character.Bounds.IntersectsWith(x.Bounds))
                    {
                        //game over funkcija tuka
                        GameOver();
                    }

                }
            }
            lblScore.Text = "Score: " + score;

            if(score > highScore)
            {
                highScore = score;
            }

        }

        private bool IsOverlappingObstacle(Control newObstacle) //added
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
                if(level <= 3)
                {
                    gameSpeed = levelSpeeds[level - 1];
                    lblLevel.Text = "Level " + level;

                    //da se smeni karakter i pozadina
                    pb_character.Image = Image.FromFile(charactersImages[level-1]); //added
                    this.BackgroundImage = Image.FromFile(backgroundImages[level-1]); //added
                }
                else
                {
                    MessageBox.Show("Congratulations, you finished the game!", "Game Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //game over
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

            //da se smeni karakterot i pozadinata
            pb_character.Image = Image.FromFile(charactersImages[0]);  //added
            this.BackgroundImage = Image.FromFile(backgroundImages[0]);   //added

            foreach (Control x in this.Controls)
            {
                if(x is PictureBox && x.Tag == "obstacle") {
                    x.Left = this.ClientSize.Width + random.Next(500, 800);
                }
            }

            tGame.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space /*&& !jumping*/)
            {
                jumping = true;
                force = 12;
            }
        }
    }
}
