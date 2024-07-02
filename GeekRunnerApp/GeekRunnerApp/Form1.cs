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
        private bool jumping = false;
        private int jumpSpeed;
        private int force;
        private Random random;

        private int level;
        private readonly int[] levelSpeeds = { 10, 15, 20 };
        private readonly int[] levelScores = { 10, 20, 30 };

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
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

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pb_character.Top += jumpSpeed; //karakterot go pomestuvame spored jumpspeedot, 

            if(jumping && force < 0) //proveruvame dali karakterot treba da prekine da skoka
            {
                jumping = false; 
            }
            if(jumping) // ako skoka namaluvame vrednost za da odi nagore, vo sprotivno dodeluvame pozitivna za da ide nadole
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }
            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag =="obstacle")
                {
                    x.Left -= gameSpeed;
                    if(x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + random.Next(200, 800);
                        score++;
                        //levelup funkcija tuka
                    }
                    if(pb_character.Bounds.IntersectsWith(x.Bounds))
                    {
                        //game over funkcija tuka
                    }

                }
            }
            lblScore.Text = "Score: " + score;

        }
    }
}
