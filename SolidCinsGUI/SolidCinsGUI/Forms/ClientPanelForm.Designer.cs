namespace SolidCinsGUI
{
    partial class ClientPanelForm
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
            this.components = new System.ComponentModel.Container();
            this.onlineClientListBox = new System.Windows.Forms.ListBox();
            this.gateLogListBox = new System.Windows.Forms.ListBox();
            this.familyNameLabel = new System.Windows.Forms.Label();
            this.chatListBox = new System.Windows.Forms.ListBox();
            this.onlineClientsLabel = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.dollarLabel = new System.Windows.Forms.Label();
            this.euroLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.weatherLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.poundLabel = new System.Windows.Forms.Label();
            this.datetimeTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // onlineClientListBox
            // 
            this.onlineClientListBox.FormattingEnabled = true;
            this.onlineClientListBox.ItemHeight = 15;
            this.onlineClientListBox.Location = new System.Drawing.Point(12, 57);
            this.onlineClientListBox.Name = "onlineClientListBox";
            this.onlineClientListBox.Size = new System.Drawing.Size(227, 289);
            this.onlineClientListBox.TabIndex = 0;
            // 
            // gateLogListBox
            // 
            this.gateLogListBox.FormattingEnabled = true;
            this.gateLogListBox.ItemHeight = 15;
            this.gateLogListBox.Location = new System.Drawing.Point(12, 359);
            this.gateLogListBox.Name = "gateLogListBox";
            this.gateLogListBox.Size = new System.Drawing.Size(227, 79);
            this.gateLogListBox.TabIndex = 1;
            // 
            // familyNameLabel
            // 
            this.familyNameLabel.AutoSize = true;
            this.familyNameLabel.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.familyNameLabel.Location = new System.Drawing.Point(347, 9);
            this.familyNameLabel.Name = "familyNameLabel";
            this.familyNameLabel.Size = new System.Drawing.Size(184, 45);
            this.familyNameLabel.TabIndex = 2;
            this.familyNameLabel.Text = "SN. ÇAVUŞ";
            // 
            // chatListBox
            // 
            this.chatListBox.FormattingEnabled = true;
            this.chatListBox.ItemHeight = 15;
            this.chatListBox.Location = new System.Drawing.Point(270, 58);
            this.chatListBox.Name = "chatListBox";
            this.chatListBox.Size = new System.Drawing.Size(490, 259);
            this.chatListBox.TabIndex = 3;
            // 
            // onlineClientsLabel
            // 
            this.onlineClientsLabel.AutoSize = true;
            this.onlineClientsLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.onlineClientsLabel.Location = new System.Drawing.Point(12, 21);
            this.onlineClientsLabel.Name = "onlineClientsLabel";
            this.onlineClientsLabel.Size = new System.Drawing.Size(147, 21);
            this.onlineClientsLabel.TabIndex = 4;
            this.onlineClientsLabel.Text = "Online Neighbours";
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(270, 323);
            this.messageTextBox.MaxLength = 120;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.PlaceholderText = "Enter your message here...";
            this.messageTextBox.Size = new System.Drawing.Size(409, 23);
            this.messageTextBox.TabIndex = 5;
            this.messageTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.messageTextBox_KeyPress);
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(685, 323);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(75, 23);
            this.sendMessageButton.TabIndex = 6;
            this.sendMessageButton.Text = "Send";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.timeLabel.Location = new System.Drawing.Point(477, 384);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(84, 32);
            this.timeLabel.TabIndex = 7;
            this.timeLabel.Text = "17:18";
            // 
            // dollarLabel
            // 
            this.dollarLabel.AutoSize = true;
            this.dollarLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dollarLabel.Location = new System.Drawing.Point(294, 363);
            this.dollarLabel.Name = "dollarLabel";
            this.dollarLabel.Size = new System.Drawing.Size(49, 21);
            this.dollarLabel.TabIndex = 8;
            this.dollarLabel.Text = "19.68";
            // 
            // euroLabel
            // 
            this.euroLabel.AutoSize = true;
            this.euroLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.euroLabel.Location = new System.Drawing.Point(294, 388);
            this.euroLabel.Name = "euroLabel";
            this.euroLabel.Size = new System.Drawing.Size(49, 21);
            this.euroLabel.TabIndex = 9;
            this.euroLabel.Text = "20.01";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(269, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "€";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(269, 363);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "$";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(756, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "°C";
            // 
            // weatherLabel
            // 
            this.weatherLabel.AutoSize = true;
            this.weatherLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.weatherLabel.Location = new System.Drawing.Point(688, 379);
            this.weatherLabel.Name = "weatherLabel";
            this.weatherLabel.Size = new System.Drawing.Size(72, 37);
            this.weatherLabel.TabIndex = 13;
            this.weatherLabel.Text = "24.8";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(269, 412);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 21);
            this.label5.TabIndex = 14;
            this.label5.Text = "£";
            // 
            // poundLabel
            // 
            this.poundLabel.AutoSize = true;
            this.poundLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.poundLabel.Location = new System.Drawing.Point(294, 412);
            this.poundLabel.Name = "poundLabel";
            this.poundLabel.Size = new System.Drawing.Size(49, 21);
            this.poundLabel.TabIndex = 15;
            this.poundLabel.Text = "22.68";
            // 
            // datetimeTimer
            // 
            this.datetimeTimer.Enabled = true;
            this.datetimeTimer.Interval = 1000;
            this.datetimeTimer.Tick += new System.EventHandler(this.datetimeTimer_Tick);
            // 
            // ClientPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.poundLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.weatherLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.euroLabel);
            this.Controls.Add(this.dollarLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.sendMessageButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.onlineClientsLabel);
            this.Controls.Add(this.chatListBox);
            this.Controls.Add(this.familyNameLabel);
            this.Controls.Add(this.gateLogListBox);
            this.Controls.Add(this.onlineClientListBox);
            this.Name = "ClientPanelForm";
            this.Text = "CinsApartmentClient - Solidet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        public ListBox onlineClientListBox;
        public ListBox gateLogListBox;
        private Label familyNameLabel;
        public ListBox chatListBox;
        private Label onlineClientsLabel;
        private TextBox messageTextBox;
        private Button sendMessageButton;
        private Label timeLabel;
        public Label dollarLabel;
        public Label euroLabel;
        private Label label2;
        private Label label3;
        private Label label4;
        public Label weatherLabel;
        private Label label5;
        public Label poundLabel;
        private System.Windows.Forms.Timer datetimeTimer;
    }
}