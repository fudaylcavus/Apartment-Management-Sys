
using SolidCinsGUI.Controllers;
using SolidCinsGUI.Helpers;
using System.Text;

namespace SolidCinsGUI
{
    public partial class LoginForm : Form
    {
        public static Form globalForm { get; set; }
        public static LoginForm Instance { get; set; }

        public LoginForm()
        {
            Instance = this;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            TrySocketConnection();
            
        }

        public void TrySocketConnection()
        {
            SocketHelper.Instance.Connect("127.0.0.1", 8888).ContinueWith(t =>
            {
                if (t.Result)
                {
                    toolStripStatusLabel.Text = "Connected";
                    colorIndicatorLabel.BackColor = Color.Green;
                }
                else
                {
                    toolStripStatusLabel.Text = "Connection Problem! (Click to retry)";
                    colorIndicatorLabel.BackColor = Color.Red;
                }
            });
        }


        private void loginButton_Click(object sender, EventArgs e)
        {
            if (!SocketHelper.Instance.IsConnected())
            {
                MessageBox.Show("Server is currently unreachable, talk to your IT Room manager!");
                return;
            }
            if (familyNameTextBox.TextLength == 0)
            {
                MessageBox.Show("Family Name cannot be empty!", "Alert!");
                return;
            }

            ClientController.FamilyName = familyNameTextBox.Text;
            new ClientPanelForm(); //Initialize Next Possible Form Instance on background to speed up the process
            ClientController.SendLoginRequest(familyNameTextBox.Text, passwordTextBox.Text);


        }

        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {
            if (!SocketHelper.Instance.IsConnected())
            {
                colorIndicatorLabel.BackColor = System.Drawing.SystemColors.MenuHighlight;
                toolStripStatusLabel.Text = "Connecting...";
                TrySocketConnection();
            }
        }


    }
}