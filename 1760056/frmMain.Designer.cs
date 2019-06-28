namespace _1760056
{
    partial class frmMain
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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.tròChơiMớiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toànMànHìnhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thôngTinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTimer = new System.Windows.Forms.Label();
            this.progressTime = new System.Windows.Forms.ProgressBar();
            this.panelTime = new System.Windows.Forms.Panel();
            this.btnRadom = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.panelTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tròChơiMớiToolStripMenuItem,
            this.toànMànHìnhToolStripMenuItem,
            this.thôngTinToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1108, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            // 
            // tròChơiMớiToolStripMenuItem
            // 
            this.tròChơiMớiToolStripMenuItem.Name = "tròChơiMớiToolStripMenuItem";
            this.tròChơiMớiToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.tròChơiMớiToolStripMenuItem.Text = "Trò chơi mới";
            this.tròChơiMớiToolStripMenuItem.Click += new System.EventHandler(this.tròChơiMớiToolStripMenuItem_Click);
            // 
            // toànMànHìnhToolStripMenuItem
            // 
            this.toànMànHìnhToolStripMenuItem.Name = "toànMànHìnhToolStripMenuItem";
            this.toànMànHìnhToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.toànMànHìnhToolStripMenuItem.Text = "Toàn màn hình";
            this.toànMànHìnhToolStripMenuItem.Click += new System.EventHandler(this.toànMànHìnhToolStripMenuItem_Click);
            // 
            // thôngTinToolStripMenuItem
            // 
            this.thôngTinToolStripMenuItem.Name = "thôngTinToolStripMenuItem";
            this.thôngTinToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.thôngTinToolStripMenuItem.Text = "Thông tin";
            this.thôngTinToolStripMenuItem.Click += new System.EventHandler(this.thôngTinToolStripMenuItem_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.Font = new System.Drawing.Font("Microsoft JhengHei UI Light", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTimer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTimer.Location = new System.Drawing.Point(2, 1);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(102, 34);
            this.lblTimer.TabIndex = 1;
            this.lblTimer.Text = "00:00";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressTime
            // 
            this.progressTime.Location = new System.Drawing.Point(110, 7);
            this.progressTime.Name = "progressTime";
            this.progressTime.Size = new System.Drawing.Size(629, 23);
            this.progressTime.TabIndex = 2;
            // 
            // panelTime
            // 
            this.panelTime.Controls.Add(this.btnRadom);
            this.panelTime.Controls.Add(this.lblTimer);
            this.panelTime.Controls.Add(this.progressTime);
            this.panelTime.Location = new System.Drawing.Point(246, 27);
            this.panelTime.Name = "panelTime";
            this.panelTime.Size = new System.Drawing.Size(841, 38);
            this.panelTime.TabIndex = 3;
            // 
            // btnRadom
            // 
            this.btnRadom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRadom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRadom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnRadom.ForeColor = System.Drawing.SystemColors.Window;
            this.btnRadom.Location = new System.Drawing.Point(758, 7);
            this.btnRadom.Name = "btnRadom";
            this.btnRadom.Size = new System.Drawing.Size(75, 23);
            this.btnRadom.TabIndex = 3;
            this.btnRadom.Text = "Random: 1";
            this.btnRadom.UseVisualStyleBackColor = true;
            this.btnRadom.Click += new System.EventHandler(this.btnRadom_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1108, 406);
            this.Controls.Add(this.panelTime);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "frmMain";
            this.Text = "1760056 _ Pikachu";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panelTime.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem tròChơiMớiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thôngTinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toànMànHìnhToolStripMenuItem;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.ProgressBar progressTime;
        private System.Windows.Forms.Panel panelTime;
        private System.Windows.Forms.Button btnRadom;
    }
}

