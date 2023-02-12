using SolidCinsGUI.Helpers;
using SolidCinsGUI.Packets;
using SolidCinsGUI.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidCinsGUI
{
    public partial class ClientPanelForm : Form
    {
        public static ClientPanelForm Instance { get; set; }
        public ClientPanelForm()
        {
            Instance = this;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            familyNameLabel.Text = $"Welcome, {ClientController.FamilyName}";
            datetimeTimer.Start();
            FormClosing += OnFormClosing;
        }

        private void OnFormClosing(Object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Uygulama kapandi");
            SocketHelper.Instance.Send(new ClientOfflinePacket { FamilyName = ClientController.FamilyName });
            Application.ExitThread();
            Environment.Exit(0);

        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            if (messageTextBox.TextLength == 0)
            {
                MessageBox.Show("Message cannot be empty!", "Alert!");
                return;
            }
            ThreadHelper.AddItem(this, chatListBox, $"{ClientController.FamilyName}: {messageTextBox.Text}");
            SocketHelper.Instance.Send(new ChatMessagePacket
            {
                SenderFamilyName = ClientController.FamilyName,
                Message = messageTextBox.Text
            });
            messageTextBox.Text = "";

        }

        private void datetimeTimer_Tick(object sender, EventArgs e)
        {
            this.timeLabel.Text = DateTime.Now.ToString("HH:mm");
        }

        private void messageTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                sendMessageButton.PerformClick();
                e.Handled = true;
            }
        }

    }
}
