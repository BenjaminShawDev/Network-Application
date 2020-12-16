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
            this.GameButton = new System.Windows.Forms.Button();
            this.HelpButton = new System.Windows.Forms.Button();
            this.MonoGameButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.ListOfClients = new System.Windows.Forms.ListBox();
            this.DeselectNamesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SubmitButton
            // 
            this.SubmitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SubmitButton.AutoSize = true;
            this.SubmitButton.Location = new System.Drawing.Point(567, 392);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(93, 23);
            this.SubmitButton.TabIndex = 9;
            this.SubmitButton.Text = "Send Message";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // MessageWindow
            // 
            this.MessageWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageWindow.Location = new System.Drawing.Point(12, 31);
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
            this.InputField.Size = new System.Drawing.Size(548, 20);
            this.InputField.TabIndex = 6;
            // 
            // NameInput
            // 
            this.NameInput.Location = new System.Drawing.Point(12, 5);
            this.NameInput.Name = "NameInput";
            this.NameInput.Size = new System.Drawing.Size(252, 20);
            this.NameInput.TabIndex = 0;
            // 
            // NameButton
            // 
            this.NameButton.Location = new System.Drawing.Point(270, 3);
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
            this.DisconnectButton.TabIndex = 5;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // GameButton
            // 
            this.GameButton.Location = new System.Drawing.Point(369, 3);
            this.GameButton.Name = "GameButton";
            this.GameButton.Size = new System.Drawing.Size(93, 23);
            this.GameButton.TabIndex = 2;
            this.GameButton.Text = "Play Hangman";
            this.GameButton.UseVisualStyleBackColor = true;
            this.GameButton.Click += new System.EventHandler(this.GameButton_Click);
            // 
            // HelpButton
            // 
            this.HelpButton.Location = new System.Drawing.Point(568, 3);
            this.HelpButton.Name = "HelpButton";
            this.HelpButton.Size = new System.Drawing.Size(92, 23);
            this.HelpButton.TabIndex = 4;
            this.HelpButton.Text = "Help";
            this.HelpButton.UseVisualStyleBackColor = true;
            this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // MonoGameButton
            // 
            this.MonoGameButton.Location = new System.Drawing.Point(468, 3);
            this.MonoGameButton.Name = "MonoGameButton";
            this.MonoGameButton.Size = new System.Drawing.Size(92, 23);
            this.MonoGameButton.TabIndex = 3;
            this.MonoGameButton.Text = "2D Game";
            this.MonoGameButton.UseVisualStyleBackColor = true;
            this.MonoGameButton.Click += new System.EventHandler(this.MonoGameButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(666, 392);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(93, 23);
            this.ClearButton.TabIndex = 10;
            this.ClearButton.Text = "Clear Text";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // ListOfClients
            // 
            this.ListOfClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListOfClients.FormattingEnabled = true;
            this.ListOfClients.ItemHeight = 20;
            this.ListOfClients.Location = new System.Drawing.Point(568, 31);
            this.ListOfClients.Name = "ListOfClients";
            this.ListOfClients.Size = new System.Drawing.Size(191, 324);
            this.ListOfClients.TabIndex = 7;
            // 
            // DeselectNamesButton
            // 
            this.DeselectNamesButton.Location = new System.Drawing.Point(568, 363);
            this.DeselectNamesButton.Name = "DeselectNamesButton";
            this.DeselectNamesButton.Size = new System.Drawing.Size(191, 23);
            this.DeselectNamesButton.TabIndex = 8;
            this.DeselectNamesButton.Text = "Deselect Name";
            this.DeselectNamesButton.UseVisualStyleBackColor = true;
            this.DeselectNamesButton.Click += new System.EventHandler(this.DeselectNamesButton_Click);
            // 
            // ClientForm
            // 
            this.AcceptButton = this.SubmitButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 426);
            this.Controls.Add(this.DeselectNamesButton);
            this.Controls.Add(this.ListOfClients);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.MonoGameButton);
            this.Controls.Add(this.HelpButton);
            this.Controls.Add(this.GameButton);
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
        private System.Windows.Forms.Button GameButton;
        private System.Windows.Forms.Button HelpButton;
        private System.Windows.Forms.Button MonoGameButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.ListBox ListOfClients;
        private System.Windows.Forms.Button DeselectNamesButton;
    }
}