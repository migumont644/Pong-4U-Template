/*
 * Description:     A basic PONG simulator
 * Author:           
 * Date:            
 */

#region libraries

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;

#endregion

namespace Pong
{
    public partial class Form1 : Form
    {
        #region global values

        //graphics objects for drawing
        SolidBrush drawBrush = new SolidBrush(Color.White);
        Font drawFont = new Font("Courier New", 10);

        // Sounds for game
        SoundPlayer scoreSound = new SoundPlayer(Properties.Resources.score);
        SoundPlayer collisionSound = new SoundPlayer(Properties.Resources.collision);
        SoundPlayer paddleHit = new SoundPlayer(Properties.Resources.paddle_hit);
        SoundPlayer wallBounce = new SoundPlayer(Properties.Resources.wall_bounce);

        //determines whether a key is being pressed or not
        Boolean aKeyDown, zKeyDown, jKeyDown, mKeyDown;

        // check to see if a new game can be started
        Boolean newGameOk = true;

        //ball directions, speed, and rectangle
        Boolean ballMoveRight = true;
        Boolean ballMoveDown = true;
        const int BALL_SPEED = 4;
        int ballX, ballY, ballSize;

        //paddle speeds and rectangles
        const int PADDLE_SPEED = 4;
        int p1X, p1Y, p2X, p2Y, pHeight, pWidth;

        //player and game scores
        int player1Score = 0;
        int player2Score = 0;
        int gameWinScore = 2;  // number of points needed to win game

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        // -- YOU DO NOT NEED TO MAKE CHANGES TO THIS METHOD
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //check to see if a key is pressed and set is KeyDown value to true if it has
            switch (e.KeyCode)
            {
                case Keys.A:
                    aKeyDown = true;
                    break;
                case Keys.Z:
                    zKeyDown = true;
                    break;
                case Keys.J:
                    jKeyDown = true;
                    break;
                case Keys.M:
                    mKeyDown = true;
                    break;
                case Keys.Y:
                case Keys.Space:
                    if (newGameOk)
                    {
                        SetParameters();
                    }
                    break;
                case Keys.N:
                    if (newGameOk)
                    {
                        Close();
                    }
                    break;
            }
        }
        
        // -- YOU DO NOT NEED TO MAKE CHANGES TO THIS METHOD
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //check to see if a key has been released and set its KeyDown value to false if it has
            switch (e.KeyCode)
            {
                case Keys.A:
                    aKeyDown = false;
                    break;
                case Keys.Z:
                    zKeyDown = false;
                    break;
                case Keys.J:
                    jKeyDown = false;
                    break;
                case Keys.M:
                    mKeyDown = false;
                    break;
            }
        }

        /// <summary>
        /// sets the ball and paddle positions for game start
        /// </summary>
        private void SetParameters()
        {
            if (newGameOk)
            {
                player1Score = player2Score = 0;
                newGameOk = false;
                startLabel.Visible = false;
                gameUpdateLoop.Start();
            }

            //set starting position for paddles on new game and point scored 
            const int PADDLE_EDGE = 20;  // buffer distance between screen edge and paddle            

            pWidth = 10;    //width for paddles
            pHeight = 55;   //height for paddles

            //p1 starting position
            p1X = PADDLE_EDGE;
            p1Y = this.Height / 2 - pHeight / 2;

            //p2 starting position
            p2X = this.Width - PADDLE_EDGE - pWidth;
            p2Y = this.Height / 2 - pHeight / 2;

            // TODO set Width and Height of ball
            ballSize = 15;

            // TODO set starting X position for ball to middle of screen, (use this.Width and ball.Width)
            ballX = this.Width / 2;

            // TODO set starting Y position for ball to middle of screen, (use this.Height and ball.Height)
            ballY = this.Height / 2;
        }

        /// <summary>
        /// This method is the game engine loop that updates the position of all elements
        /// and checks for collisions.
        /// </summary>
        private void gameUpdateLoop_Tick(object sender, EventArgs e)
        {

            #region update ball position

            // TODO create code to move ball either left or right based on ballMoveRight and using BALL_SPEED
            if (ballMoveRight == true)
            {
                ballX += BALL_SPEED;
            }
            else
            {
                ballX -= BALL_SPEED;
            }

            // TODO create code move ball either down or up based on ballMoveDown and using BALL_SPEED
            if (ballMoveDown == true)
            {
                ballY += BALL_SPEED;
            }
            else
            {
                ballY -= BALL_SPEED;
            }

            #endregion

            #region update paddle positions

            // TODO create an if statement and code to move player 1 paddle down using p1.Y and PADDLE_SPEED

            // TODO create an if statement and code to move player 2 paddle up using p2.Y and PADDLE_SPEED

            // TODO create an if statement and code to move player 2 paddle down using p2.Y and PADDLE_SPEED
           
            if (aKeyDown == true && p1Y > 0)
            {
           // TODO create code to move player 1 paddle up using p1.Y and PADDLE_SPEED        
                p1Y -= PADDLE_SPEED;
            }

            if (zKeyDown == true && p1Y < 410)
            {
                p1Y += PADDLE_SPEED;
            }
            if (jKeyDown == true && p2Y > 0)
            {
                p2Y -= PADDLE_SPEED;
            }

            if (mKeyDown == true && p2Y < 410)
            {
                p2Y += PADDLE_SPEED;
            }
            #endregion

            #region ball collision with top and bottom lines

            if (ballY < 0) // if ball hits top line
            {
                // TODO use ballMoveDown boolean to change direction
                ballMoveDown = true;         
                // TODO play a collision sound
                collisionSound.Play();
            }
            else if (ballY > this.Height)
            {
                ballMoveDown = false;
                collisionSound.Play();
            }
            // TODO In an else if statement use ball.Y, this.Height, and ball.Width to check for collision with bottom line
            // If true use ballMoveDown down boolean to change direction

            #endregion

            #region ball collision with paddles

            // TODO create if statment that checks p1 collides with ball and if it does
            // --- play a "paddle hit" sound and
            // --- use ballMoveRight boolean to change direction

            Rectangle p1Rec = new Rectangle(p1X, p1Y, pWidth, pHeight);
            Rectangle p2Rec = new Rectangle(p2X, p2Y, pWidth, pHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballSize, ballSize);
            if (ballRec.IntersectsWith(p1Rec) || ballRec.IntersectsWith(p2Rec))
            {
                collisionSound.Play();
                ballMoveRight =! ballMoveRight;
            }


            // TODO create if statment that checks p2 collides with ball and if it does
            // --- play a "paddle hit" sound and
            // --- use ballMoveRight boolean to change direction
            /*  ENRICHMENT
             *  Instead of using two if statments as noted above see if you can create one
             *  if statement with multiple conditions to play a sound and change direction
             */

            #endregion

            #region ball collision with side walls (point scored)

            if (ballX < 0)  // ball hits left wall logic
            {
                // TODO
                // --- play score sound
                scoreSound.Play();
                // --- update player 2 score
                player2Score++;
                ballMoveRight = true;
                // TODO use if statement to check to see if player 2 has won the game. If true run 
                // GameOver method. Else change direction of ball and call SetParameters method.
            }
            else if (ballX > this.Width)
            {
                scoreSound.Play();
                player1Score++;
                ballMoveRight = false;
            }
            if (player1Score == gameWinScore)
            {
                GameOver("1");  
            }
            else if (player2Score == gameWinScore)
            {
                GameOver("2");
            }

            // TODO same as above but this time check for collision with the right wall

            #endregion
            
            //refresh the screen, which causes the Form1_Paint method to run
            this.Refresh();
        }
        
        /// <summary>
        /// Displays a message for the winner when the game is over and allows the user to either select
        /// to play again or end the program
        /// </summary>
        /// <param name="winner">The player name to be shown as the winner</param>
        private void GameOver(string winner)
        {
            newGameOk = true;

            // TODO create game over logic
            // --- stop the gameUpdateLoop
            // --- show a message on the startLabel to indicate a winner, (need to Refresh).
            // --- pause for two seconds 
            // --- use the startLabel to ask the user if they want to play again
            gameUpdateLoop.Stop();
            startLabel.Visible = true;
            startLabel.Text = $"Player {winner} is the winner!";
            startLabel.Refresh();
            Thread.Sleep(2000);
            startLabel.Text = $"Do you want to play again?";
            startLabel.Refresh();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // TODO draw paddles using FillRectangle
            e.Graphics.FillRectangle(whiteBrush, p1X, p1Y, pWidth, pHeight);
            e.Graphics.FillRectangle(whiteBrush, p2X, p2Y, pWidth, pHeight);
            // TODO draw ball using FillRectangle
            e.Graphics.FillRectangle(whiteBrush, ballX, ballY, ballSize, ballSize);
            // TODO draw scores to the screen using DrawString
        }

    }
}
