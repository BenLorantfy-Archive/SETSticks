/* 
* FILE          : Line.cs
* PROJECT       : wmpA5
* PROGRAMMER    : Ben Lorantfy
* FIRST VERSION : 2014-21-10
* DESCRIPTION   : Contains the line class
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace wmpA5 {
    /*
     *  NAME    : Line
     *  PURPOSE : Line objects are used to independently draw each line, by spawning 
     *            a thread to animate the line so that it moves, bonuces, spins, and resizes
     */
    class Line {
        Graphics canvas;                                        // Used to draw lines on
        int width;                                              // Canvas width
        int height;                                             // Canvas height
        Pen eraser = new Pen(new SolidBrush(Color.Black));      // Used to erase lines
        Pen pen = new Pen(new SolidBrush(Color.Blue));          // Used to draw lines
        
        Point p1;                                               // Point 1 of moving line
        Point p2;                                               // Point 2 of moving line
        const int startOffset = 50;                             // Used to calculate starting points for line

        Queue<Point[]> tails = new Queue<Point[]>();            // Stores each drawn line
        int numTails;                                           // Keeps track of number of tails
                
        Random random = new Random();                           // Used to generate random numbers
        Thread thread;                                          // Used to bounce and draw lines
        bool run = true;                                        // Flag that indicates if line thread should run
        bool pause = false;                                     // Flag to indicate that thread shoudl pause
        ManualResetEvent pauseEvent;                            // Used to pause thread

        public Line(Graphics canvas, Point middle, int numTails, int width, int height, ManualResetEvent pauseEvent) {
            this.canvas = canvas;
            this.p1 = new Point(middle.X + startOffset, middle.Y + startOffset);
            this.p2 = new Point(middle.X - startOffset, middle.Y - startOffset);
            this.width = width;
            this.height = height;
            this.numTails = numTails;
            this.pauseEvent = pauseEvent;
            thread = new Thread(AnimateLine);
            thread.Start();
        }

        /* 
         * FUNCTION     : RandomAngle
         *
         * DESCRIPTION  : Generates a random angle between a min and max degrees and converts it to radians
         *
         * PARAMETERS   : int min : minimum possible angle
         *                int max : maximum possible angle
         * 
         * RETURNS      : double : the random angle in radians
         */
        private double RandomAngle(int min, int max) {
            return random.Next(min, max + 1) * (Math.PI / 180);
        }
        private double RandomAngle(int min1, int max1, int min2, int max2) {
            double angle = 0;
            bool range1 = Convert.ToBoolean(random.Next(0, 2));
            if (range1) {
                angle = random.Next(min1, max1 + 1) * (Math.PI / 180);
            } else {
                angle = random.Next(min2, max2 + 1) * (Math.PI / 180);
            }
            return angle;
        }

        /* 
         * FUNCTION     : CalculateDelta
         *
         * DESCRIPTION  : Calculates change in x and y given an angle and a distance along the angle 
         *
         * PARAMETERS   : double angle      : angle to calculate change in x and y from
         *                int increment     : change along angle
         * 
         * RETURNS      : Point             : the change in x and y as a point
         */
        private Point CalculateDelta(double angle, int increment) {
            return new Point((int)(increment * Math.Sin(angle)),(int)(increment * Math.Cos(angle)));
        }

        /* 
         * FUNCTION     : ColorFromHSV
         *
         * DESCRIPTION  : Converts hue/sat/val to red/green/blue
         *                See http://stackoverflow.com/questions/1335426/is-there-a-built-in-c-net-system-api-for-hsv-to-rgb
         *
         * PARAMETERS   : double angle      : angle to calculate change in x and y from
         *                int increment     : change along angle
         * 
         * RETURNS      : Point             : the change in x and y as a point
         */
        public static Color ColorFromHSV(double hue, double saturation, double value) {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);
            Color color;

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            switch (hi) {
                case 0:
                    color = Color.FromArgb(255, v, t, p);
                    break;                   
                case 1:
                    color = Color.FromArgb(255, q, v, p);
                    break;

                case 2:
                    color = Color.FromArgb(255, p, v, t);
                    break;

                case 3:
                    color = Color.FromArgb(255, p, q, v);
                    break;

                case 4:
                    color = Color.FromArgb(255, t, p, v);
                    break;

                default:
                    color = Color.FromArgb(255, v, p, q);
                    break;
            }

            return color;
        }

        /* 
         * FUNCTION     : AnimateLine
         *
         * DESCRIPTION  : Animates the start and end points of the line in different directions
         *                If a point hits wall, it bounces off in a random direction
         *                This produces the effect of the line randomly resizing and spinning
         *
         * PARAMETERS   : none
         * 
         * RETURNS      : void
         */
        public void AnimateLine(){
            int increment = 10;
            double p1Angle = RandomAngle(0, 360);
            double p2Angle = RandomAngle(0, 360);
            Point p1Delta = CalculateDelta(p1Angle, increment);
            Point p2Delta = CalculateDelta(p2Angle, increment);
            int hue = random.Next(0, 360);

            while (run) {
                //
                // Pause if thread should pause
                //
                if (pause) {
                    pauseEvent.WaitOne();
                }

                //
                // Erase last tail
                //
                if (tails.Count > numTails) {
                    Point[] tail = tails.Dequeue();
                    lock (canvas) {
                        canvas.DrawLine(eraser, tail[0], tail[1]);  
                    }                            
                }

                //
                // Bounce Point 1 X
                //
                if (p1.X < 0) {
                    p1.X = 0;
                    p1Angle = RandomAngle(20, 160);
                    p1Delta = CalculateDelta(p1Angle, increment);
                } else if (p1.X > width) {
                    p1.X = width;
                    p1Angle = RandomAngle(200, 340);
                    p1Delta = CalculateDelta(p1Angle, increment);
                }

                //
                // Bounce Point 1 Y
                //
                if (p1.Y < 0) {
                    p1.Y = 0;
                    p1Angle = RandomAngle(290, 360, 0, 70);
                    p1Delta = CalculateDelta(p1Angle, increment);
                } else if (p1.Y > height) {
                    p1.Y = height;
                    p1Angle = RandomAngle(110, 290);
                    p1Delta = CalculateDelta(p1Angle, increment);
                }

                //
                // Bounce Point 1 X
                //
                if (p2.X < 0) {
                    p2.X = 0;
                    p2Angle = RandomAngle(20, 160);
                    p2Delta = CalculateDelta(p2Angle, increment);
                } else if (p2.X > width) {
                    p2.X = width;
                    p2Angle = RandomAngle(200, 340);
                    p2Delta = CalculateDelta(p2Angle, increment);
                }

                //
                // Bounce Point 1 Y
                //
                if (p2.Y < 0) {
                    p2.Y = 0;
                    p2Angle = RandomAngle(290, 360, 0, 70);
                    p2Delta = CalculateDelta(p2Angle, increment);
                } else if (p2.Y > height) {
                    p2.Y = height;
                    p2Angle = RandomAngle(110, 290);
                    p2Delta = CalculateDelta(p2Angle, increment);
                }

                //
                // Move new line
                //
                p1.X += p1Delta.X;
                p1.Y += p1Delta.Y;
                p2.X += p2Delta.X;
                p2.Y += p2Delta.Y;

                //
                // Change color
                //
                hue = (hue == 360) ? 1 : hue + 1;
                pen.Color = ColorFromHSV(hue, 1, 1);

                //
                // Draw each tail
                //
                lock (canvas) {
                    foreach (Point[] tail in tails) {
                        canvas.DrawLine(pen, tail[0], tail[1]);
                    }
                }
                
                //
                // Add stick to tail
                //
                tails.Enqueue(new Point[]{new Point(p1.X,p1.Y), new Point(p2.X,p2.Y)});

                //
                // Wait for a small amount of time so you can see line moving
                //
                Thread.Sleep(50);
            }
        }

        /* 
         * FUNCTION     : Pause
         *
         * DESCRIPTION  : Sets the pause flag
         *
         * PARAMETERS   : none
         * 
         * RETURNS      : void
         */
        public void Pause() {
            pause = true;
        }

        /* 
         * FUNCTION     : Resume
         *
         * DESCRIPTION  : Clears the pause flag
         *
         * PARAMETERS   : none
         * 
         * RETURNS      : void
         */
        public void Resume() {
            pause = false;
        }

        /* 
         * FUNCTION     : Quit
         *
         * DESCRIPTION  : Sets the run flag to false and waits for the animate thread to finish
         *
         * PARAMETERS   : none
         * 
         * RETURNS      : void
         */
        public void Quit() {
            run = false;
            thread.Join();
        }
    }
}
