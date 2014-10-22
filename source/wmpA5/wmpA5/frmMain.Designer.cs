namespace wmpA5 {
    partial class frmMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pnlCanvas = new System.Windows.Forms.Panel();
            this.tkbNumTails = new System.Windows.Forms.TrackBar();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnPauseOrResume = new System.Windows.Forms.Button();
            this.lblTailLength = new System.Windows.Forms.Label();
            this.lblPrompt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tkbNumTails)).BeginInit();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.BackColor = System.Drawing.Color.Black;
            this.pnlCanvas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCanvas.Location = new System.Drawing.Point(0, 0);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(684, 397);
            this.pnlCanvas.TabIndex = 0;
            this.pnlCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseDown);
            // 
            // tkbNumTails
            // 
            this.tkbNumTails.Location = new System.Drawing.Point(70, 24);
            this.tkbNumTails.Maximum = 20;
            this.tkbNumTails.Minimum = 1;
            this.tkbNumTails.Name = "tkbNumTails";
            this.tkbNumTails.Size = new System.Drawing.Size(167, 45);
            this.tkbNumTails.TabIndex = 1;
            this.tkbNumTails.Value = 10;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lblPrompt);
            this.pnlControls.Controls.Add(this.lblTailLength);
            this.pnlControls.Controls.Add(this.btnPauseOrResume);
            this.pnlControls.Controls.Add(this.tkbNumTails);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControls.Location = new System.Drawing.Point(0, 396);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(684, 66);
            this.pnlControls.TabIndex = 2;
            // 
            // btnPauseOrResume
            // 
            this.btnPauseOrResume.Location = new System.Drawing.Point(7, 7);
            this.btnPauseOrResume.Name = "btnPauseOrResume";
            this.btnPauseOrResume.Size = new System.Drawing.Size(57, 51);
            this.btnPauseOrResume.TabIndex = 2;
            this.btnPauseOrResume.Text = "Pause";
            this.btnPauseOrResume.UseVisualStyleBackColor = true;
            this.btnPauseOrResume.Click += new System.EventHandler(this.btnPauseOrResume_Click);
            // 
            // lblTailLength
            // 
            this.lblTailLength.AutoSize = true;
            this.lblTailLength.Location = new System.Drawing.Point(76, 8);
            this.lblTailLength.Name = "lblTailLength";
            this.lblTailLength.Size = new System.Drawing.Size(63, 13);
            this.lblTailLength.TabIndex = 3;
            this.lblTailLength.Text = "Tail Length:";
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Location = new System.Drawing.Point(255, 26);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(258, 13);
            this.lblPrompt.TabIndex = 4;
            this.lblPrompt.Text = "Click anywhere above to create lines for a cool effect";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlCanvas);
            this.Name = "frmMain";
            this.Text = "SETSticks";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.tkbNumTails)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCanvas;
        private System.Windows.Forms.TrackBar tkbNumTails;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnPauseOrResume;
        private System.Windows.Forms.Label lblTailLength;
        private System.Windows.Forms.Label lblPrompt;
    }
}

