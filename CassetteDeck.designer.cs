namespace ZXCassetteDeck
{
    partial class CassetteDeck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CassetteDeck));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ejectButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.playButton = new System.Windows.Forms.ToolStripButton();
            this.pauseButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.previousBlockButton = new System.Windows.Forms.ToolStripButton();
            this.nextBlockButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.rewindButton = new System.Windows.Forms.ToolStripButton();
            this.fastForwardButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tapeView = new ZXCassetteDeck.TapeView();
            this.blockView = new ZXCassetteDeck.TapeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.class11 = new ZXCassetteDeck.Class1();
            this.listView1 = new System.Windows.Forms.ListView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.counter1 = new ZXCassetteDeck.Counter();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tapeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blockView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Black;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.ejectButton,
            this.toolStripLabel3,
            this.playButton,
            this.pauseButton,
            this.toolStripLabel1,
            this.previousBlockButton,
            this.nextBlockButton,
            this.toolStripLabel2,
            this.rewindButton,
            this.fastForwardButton,
            this.toolStripButton3,
            this.toolStripButton2,
            this.toolStripButton4,
            this.toolStripButton5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(861, 55);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 52);
            this.toolStripButton1.Text = "Load File";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // ejectButton
            // 
            this.ejectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ejectButton.Image = ((System.Drawing.Image)(resources.GetObject("ejectButton.Image")));
            this.ejectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ejectButton.Name = "ejectButton";
            this.ejectButton.Size = new System.Drawing.Size(36, 52);
            this.ejectButton.Text = "Eject";
            this.ejectButton.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.AutoSize = false;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(10, 52);
            // 
            // playButton
            // 
            this.playButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.playButton.Image = ((System.Drawing.Image)(resources.GetObject("playButton.Image")));
            this.playButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(36, 52);
            this.playButton.Text = "Play";
            this.playButton.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pauseButton.Image = ((System.Drawing.Image)(resources.GetObject("pauseButton.Image")));
            this.pauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(36, 52);
            this.pauseButton.Text = "Pause";
            this.pauseButton.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoSize = false;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(10, 52);
            // 
            // previousBlockButton
            // 
            this.previousBlockButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.previousBlockButton.Image = ((System.Drawing.Image)(resources.GetObject("previousBlockButton.Image")));
            this.previousBlockButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.previousBlockButton.Name = "previousBlockButton";
            this.previousBlockButton.Size = new System.Drawing.Size(36, 52);
            this.previousBlockButton.Text = "Previous Block";
            this.previousBlockButton.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // nextBlockButton
            // 
            this.nextBlockButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextBlockButton.Image = ((System.Drawing.Image)(resources.GetObject("nextBlockButton.Image")));
            this.nextBlockButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextBlockButton.Name = "nextBlockButton";
            this.nextBlockButton.Size = new System.Drawing.Size(36, 52);
            this.nextBlockButton.Text = "Next Block";
            this.nextBlockButton.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.AutoSize = false;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(10, 52);
            // 
            // rewindButton
            // 
            this.rewindButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rewindButton.Image = ((System.Drawing.Image)(resources.GetObject("rewindButton.Image")));
            this.rewindButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rewindButton.Name = "rewindButton";
            this.rewindButton.Size = new System.Drawing.Size(36, 52);
            this.rewindButton.Text = "Rewind";
            this.rewindButton.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // fastForwardButton
            // 
            this.fastForwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fastForwardButton.Image = ((System.Drawing.Image)(resources.GetObject("fastForwardButton.Image")));
            this.fastForwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fastForwardButton.Name = "fastForwardButton";
            this.fastForwardButton.Size = new System.Drawing.Size(36, 52);
            this.fastForwardButton.Text = "Fast Forward";
            this.fastForwardButton.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(6, 55);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 52);
            this.toolStripButton2.Text = "Toggle View Tape Progress";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click_1);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::ZXCassetteDeck.Properties.Resources.nav_18_512;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(36, 52);
            this.toolStripButton4.Text = "Show Details";
            this.toolStripButton4.Visible = false;
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click_1);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::ZXCassetteDeck.Properties.Resources.nav_18_512_Close;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(36, 52);
            this.toolStripButton5.Text = "Hide Details";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tapeView);
            this.panel1.Controls.Add(this.blockView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 50);
            this.panel1.TabIndex = 7;
            // 
            // tapeView
            // 
            this.tapeView.BackColor = System.Drawing.Color.Black;
            this.tapeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tapeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tapeView.Image = ((System.Drawing.Image)(resources.GetObject("tapeView.Image")));
            this.tapeView.Location = new System.Drawing.Point(0, 0);
            this.tapeView.Name = "tapeView";
            this.tapeView.Size = new System.Drawing.Size(861, 25);
            this.tapeView.TabIndex = 1;
            this.tapeView.TabStop = false;
            this.tapeView.TZXFile = null;
            // 
            // blockView
            // 
            this.blockView.BackColor = System.Drawing.Color.Black;
            this.blockView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blockView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.blockView.Image = ((System.Drawing.Image)(resources.GetObject("blockView.Image")));
            this.blockView.Location = new System.Drawing.Point(0, 25);
            this.blockView.Name = "blockView";
            this.blockView.Size = new System.Drawing.Size(861, 25);
            this.blockView.TabIndex = 0;
            this.blockView.TabStop = false;
            this.blockView.TZXFile = null;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 105);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.class11);
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Size = new System.Drawing.Size(861, 259);
            this.splitContainer1.SplitterDistance = 237;
            this.splitContainer1.TabIndex = 8;
            // 
            // class11
            // 
            this.class11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.class11.Location = new System.Drawing.Point(484, 0);
            this.class11.Name = "class11";
            this.class11.Size = new System.Drawing.Size(377, 237);
            this.class11.TabIndex = 2;
            this.class11.Text = "class11";
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.Color.Lime;
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(484, 237);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Silver;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 237);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(861, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Silver;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(846, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // counter1
            // 
            this.counter1.BackColor = System.Drawing.Color.White;
            this.counter1.DigitCount = 6;
            this.counter1.Location = new System.Drawing.Point(328, 18);
            this.counter1.Name = "counter1";
            this.counter1.Size = new System.Drawing.Size(90, 20);
            this.counter1.TabIndex = 1;
            this.counter1.ToValue = ((long)(0));
            this.counter1.Value = ((long)(0));
            // 
            // CassetteDeck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(861, 364);
            this.Controls.Add(this.counter1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CassetteDeck";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ZX Cassette Deck";
            this.Load += new System.EventHandler(this.Deck_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tapeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blockView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton playButton;
        private System.Windows.Forms.ToolStripButton pauseButton;
        private System.Windows.Forms.ToolStripButton previousBlockButton;
        private System.Windows.Forms.ToolStripButton nextBlockButton;
        private System.Windows.Forms.ToolStripButton rewindButton;
        private System.Windows.Forms.ToolStripButton fastForwardButton;
        private System.Windows.Forms.ToolStripButton ejectButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TapeView tapeView;
        private TapeView blockView;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ListView listView1;
        private Counter counter1;
        private System.Windows.Forms.ToolStripSeparator toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private Class1 class11;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
    }
}

