namespace Client
{
    partial class GrapicForm
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
            this.BackgroundPanel = new System.Windows.Forms.Panel();
            this.ColourShowPanel = new System.Windows.Forms.Panel();
            this.AlphaTrackBar = new System.Windows.Forms.TrackBar();
            this.GreenTrackBar = new System.Windows.Forms.TrackBar();
            this.BlueTrackBar = new System.Windows.Forms.TrackBar();
            this.RedTrackBar = new System.Windows.Forms.TrackBar();
            this.ClearButton = new System.Windows.Forms.Button();
            this.BackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // BackgroundPanel
            // 
            this.BackgroundPanel.BackColor = System.Drawing.Color.White;
            this.BackgroundPanel.Controls.Add(this.ColourShowPanel);
            this.BackgroundPanel.Controls.Add(this.AlphaTrackBar);
            this.BackgroundPanel.Controls.Add(this.GreenTrackBar);
            this.BackgroundPanel.Controls.Add(this.BlueTrackBar);
            this.BackgroundPanel.Controls.Add(this.RedTrackBar);
            this.BackgroundPanel.Controls.Add(this.ClearButton);
            this.BackgroundPanel.Cursor = System.Windows.Forms.Cursors.Cross;
            this.BackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BackgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.BackgroundPanel.Name = "BackgroundPanel";
            this.BackgroundPanel.Size = new System.Drawing.Size(914, 513);
            this.BackgroundPanel.TabIndex = 0;
            this.BackgroundPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BackgroundPanel_MouseDown);
            this.BackgroundPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BackgroundPanel_MouseMove);
            this.BackgroundPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BackgroundPanel_MouseUp);
            // 
            // ColourShowPanel
            // 
            this.ColourShowPanel.BackColor = System.Drawing.Color.Black;
            this.ColourShowPanel.Location = new System.Drawing.Point(698, 12);
            this.ColourShowPanel.Name = "ColourShowPanel";
            this.ColourShowPanel.Size = new System.Drawing.Size(204, 42);
            this.ColourShowPanel.TabIndex = 16;
            // 
            // AlphaTrackBar
            // 
            this.AlphaTrackBar.Location = new System.Drawing.Point(798, 114);
            this.AlphaTrackBar.Maximum = 255;
            this.AlphaTrackBar.Name = "AlphaTrackBar";
            this.AlphaTrackBar.Size = new System.Drawing.Size(104, 45);
            this.AlphaTrackBar.TabIndex = 15;
            this.AlphaTrackBar.Value = 255;
            this.AlphaTrackBar.Scroll += new System.EventHandler(this.AlphaTrackBar_Scroll);
            // 
            // GreenTrackBar
            // 
            this.GreenTrackBar.Location = new System.Drawing.Point(698, 114);
            this.GreenTrackBar.Maximum = 255;
            this.GreenTrackBar.Name = "GreenTrackBar";
            this.GreenTrackBar.Size = new System.Drawing.Size(104, 45);
            this.GreenTrackBar.TabIndex = 14;
            this.GreenTrackBar.Scroll += new System.EventHandler(this.GreenTrackBar_Scroll);
            // 
            // BlueTrackBar
            // 
            this.BlueTrackBar.Location = new System.Drawing.Point(798, 63);
            this.BlueTrackBar.Maximum = 255;
            this.BlueTrackBar.Name = "BlueTrackBar";
            this.BlueTrackBar.Size = new System.Drawing.Size(104, 45);
            this.BlueTrackBar.TabIndex = 13;
            this.BlueTrackBar.Scroll += new System.EventHandler(this.BlueTrackBar_Scroll);
            // 
            // RedTrackBar
            // 
            this.RedTrackBar.Location = new System.Drawing.Point(698, 63);
            this.RedTrackBar.Maximum = 255;
            this.RedTrackBar.Name = "RedTrackBar";
            this.RedTrackBar.Size = new System.Drawing.Size(104, 45);
            this.RedTrackBar.TabIndex = 12;
            this.RedTrackBar.Scroll += new System.EventHandler(this.RedTrackBar_Scroll);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(698, 165);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(204, 23);
            this.ClearButton.TabIndex = 6;
            this.ClearButton.Text = "Clear screen";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // GrapicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 513);
            this.Controls.Add(this.BackgroundPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "GrapicForm";
            this.Text = "Paint Window";
            this.BackgroundPanel.ResumeLayout(false);
            this.BackgroundPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BackgroundPanel;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.TrackBar RedTrackBar;
        private System.Windows.Forms.TrackBar BlueTrackBar;
        private System.Windows.Forms.TrackBar AlphaTrackBar;
        private System.Windows.Forms.TrackBar GreenTrackBar;
        private System.Windows.Forms.Panel ColourShowPanel;
    }
}