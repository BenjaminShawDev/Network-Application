namespace Client
{
    partial class ClientForm
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
            this.SubmitButton = new System.Windows.Forms.Button();
            this.MessageWindow = new System.Windows.Forms.TextBox();
            this.InputField = new System.Windows.Forms.TextBox();
            this.NameInput = new System.Windows.Forms.TextBox();
            this.NameButton = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.ClientList = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SubmitButton
            // 
            this.SubmitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SubmitButton.AutoSize = true;
            this.SubmitButton.Location = new System.Drawing.Point(666, 392);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(93, 23);
            this.SubmitButton.TabIndex = 5;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // MessageWindow
            // 
            this.MessageWindow.Location = new System.Drawing.Point(13, 31);
            this.MessageWindow.Multiline = true;
            this.MessageWindow.Name = "MessageWindow";
            this.MessageWindow.ReadOnly = true;
            this.MessageWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MessageWindow.Size = new System.Drawing.Size(548, 355);
            this.MessageWindow.TabIndex = 2;
            this.MessageWindow.TabStop = false;
            // 
            // InputField
            // 
            this.InputField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InputField.Location = new System.Drawing.Point(12, 394);
            this.InputField.Name = "InputField";
            this.InputField.Size = new System.Drawing.Size(648, 20);
            this.InputField.TabIndex = 4;
            // 
            // NameInput
            // 
            this.NameInput.Location = new System.Drawing.Point(12, 5);
            this.NameInput.Name = "NameInput";
            this.NameInput.Size = new System.Drawing.Size(549, 20);
            this.NameInput.TabIndex = 0;
            // 
            // NameButton
            // 
            this.NameButton.Location = new System.Drawing.Point(567, 3);
            this.NameButton.Name = "NameButton";
            this.NameButton.Size = new System.Drawing.Size(93, 23);
            this.NameButton.TabIndex = 1;
            this.NameButton.Text = "Enter Name";
            this.NameButton.UseVisualStyleBackColor = true;
            this.NameButton.Click += new System.EventHandler(this.NameButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(666, 3);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(93, 23);
            this.DisconnectButton.TabIndex = 2;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // ClientList
            // 
            this.ClientList.Location = new System.Drawing.Point(567, 31);
            this.ClientList.Multiline = true;
            this.ClientList.Name = "ClientList";
            this.ClientList.ReadOnly = true;
            this.ClientList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ClientList.ShortcutsEnabled = false;
            this.ClientList.Size = new System.Drawing.Size(192, 355);
            this.ClientList.TabIndex = 7;
            this.ClientList.TabStop = false;
            this.ClientList.WordWrap = false;
            // 
            // ClientForm
            // 
            this.AcceptButton = this.SubmitButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 426);
            this.Controls.Add(this.ClientList);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.NameButton);
            this.Controls.Add(this.NameInput);
            this.Controls.Add(this.InputField);
            this.Controls.Add(this.MessageWindow);
            this.Controls.Add(this.SubmitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ClientForm";
            this.Text = "Chat Window";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.TextBox MessageWindow;
        private System.Windows.Forms.TextBox InputField;
        private System.Windows.Forms.TextBox NameInput;
        private System.Windows.Forms.Button NameButton;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.TextBox ClientList;
    }
}