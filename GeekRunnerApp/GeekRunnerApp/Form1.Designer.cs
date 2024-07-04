namespace GeekRunnerApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pb_character = new System.Windows.Forms.PictureBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.tGame = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_character)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_character
            // 
            this.pb_character.Location = new System.Drawing.Point(50, 536); // Adjust position
            this.pb_character.Name = "pb_character";
            this.pb_character.Size = new System.Drawing.Size(50, 50); // Adjust size
            this.pb_character.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb_character.TabIndex = 2;
            this.pb_character.TabStop = false;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(28, 31);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(44, 16);
            this.lblLevel.TabIndex = 3;
            this.lblLevel.Text = "label1";
            // 
            // tGame
            //
            this.tGame.Interval = 20;
            this.tGame.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(700, 31);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(44, 16);
            this.lblScore.TabIndex = 4;
            this.lblScore.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.pb_character);
            this.MaximumSize = new System.Drawing.Size(1200, 800);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "Form1";
            this.Text = "Geek Runner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pb_character)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pb_character;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Timer tGame;
        private System.Windows.Forms.Label lblScore;
    }
}

