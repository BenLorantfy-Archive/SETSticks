/* 
* FILE          : frmMain.cs
* PROJECT       : wmpA5
* PROGRAMMER    : Ben Lorantfy
* FIRST VERSION : 2014-21-10
* DESCRIPTION   : Main form and UI
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wmpA5 {
    public partial class frmMain : Form {
        Screensaver screensaver;

        public frmMain() {
            InitializeComponent();

            //
            // Initilizes screen saver
            //
            screensaver = new Screensaver(pnlCanvas.CreateGraphics(), pnlCanvas.Width, pnlCanvas.Height);
        }

        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e) {
            //
            // Adds line at mouse coordinates with user specified tail length
            //
            screensaver.Add(e.X, e.Y, tkbNumTails.Value);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            //
            // Gracefully line ends threads
            //
            screensaver.Quit();
        }

        private void btnPauseOrResume_Click(object sender, EventArgs e) {
            //
            // Toggles pause/resume on screen saver
            //
            if (btnPauseOrResume.Text == "Resume") {
                btnPauseOrResume.Text = "Pause";
                screensaver.Resume();
            } else {
                btnPauseOrResume.Text = "Resume";
                screensaver.Pause();
            }
        }
    }
}
