/* 
* FILE          : Screensaver.cs
* PROJECT       : wmpA5
* PROGRAMMER    : Ben Lorantfy
* FIRST VERSION : 2014-21-10
* DESCRIPTION   : Contains the Screensaver class
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace wmpA5 {
    /*
     *  NAME    : Line
     *  PURPOSE : Maintains reporistory of Line objects.
     *            Used to add lines, pause/resume, and quit screensaver.
     */
    class Screensaver {
        private int canvasWidth;                        // Canvas width
        private int canvasHeight;                       // Canvas height
        private ManualResetEvent pauseEvent;            // Used to signal pause
        private Graphics canvas;                        // Used to draw lines
        private List<Line> lines = new List<Line>();    // Contains Line objects
        private bool paused = false;                    // Keeps track of wether screensaver is paused or not

        public Screensaver(Graphics canvas, int canvasWidth, int canvasHeight) {
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            this.canvas = canvas;
            pauseEvent = new ManualResetEvent(false);
        }

        /* 
         * FUNCTION     : Add
         *
         * DESCRIPTION  : Returns a random angle between a min and max degrees and converts it to radians
         *
         * PARAMETERS   : int x : middle x value of line to create
         *                int y : middle y value of line to create
         *                int numTails : max number of tails for the line to have
         * 
         * RETURNS      : void
         */
        public void Add(int x, int y, int numTails){
            //
            // If not paused, add line
            //
            if (!paused) {
                lines.Add(new Line(canvas, new Point(x, y), numTails, canvasWidth, canvasHeight, pauseEvent));
            }
        }

        /* 
         * FUNCTION     : Pause
         *
         * DESCRIPTION  : Pauses the screen saver by signaling every line to pause
         *
         * PARAMETERS   : none
         * 
         * RETURNS      : void
         */
        public void Pause() {
            paused = true;
            pauseEvent.Reset();
            foreach (Line line in lines) {
                line.Pause();
            }
        }

        /* 
         * FUNCTION     : Resume
         *
         * DESCRIPTION  : Resumes the screen saver by signaling every line to resume
         *
         * PARAMETERS   : none
         * 
         * RETURNS      : void
         */
        public void Resume() {
            paused = false;
            foreach (Line line in lines) {
                line.Resume();
            }
            pauseEvent.Set();
        }

        /* 
         * FUNCTION     : Quit
         *
         * DESCRIPTION  : Gracefully stops all line threads
         *
         * PARAMETERS   : none
         * 
         * RETURNS      : void
         */
        public void Quit() {
            foreach (Line line in lines) {
                line.Quit();
            }
        }
    }
}
