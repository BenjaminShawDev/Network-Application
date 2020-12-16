using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packets;

namespace Client
{
    public partial class ClientForm : Form
    {
        Client client;

        public ClientForm(Client tempClient)
        {
            InitializeComponent();
            SubmitButton.Enabled = false;
            GameButton.Enabled = false;
            MonoGameButton.Enabled = false;
            ListOfClients.SelectedIndex = -1;
            client = tempClient;
        }

        public void UpdateChatWindow(string message)
        {
            if(MessageWindow.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateChatWindow(message);
                }));
            }

            else
            {
                MessageWindow.Text += message + Environment.NewLine;
                MessageWindow.SelectionStart = MessageWindow.Text.Length;
                MessageWindow.ScrollToCaret();
            }
        }

        public void ClearChatWindow()
        {
            if (MessageWindow.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    MessageWindow.Clear();
                }));
            }
        }

        public void UpdateListOfClients(List<string> name)
        {
            if (ListOfClients.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateListOfClients(name);
                }));
            }

            else
            {
                ListOfClients.Items.Clear();
                foreach (string i in name)
                    ListOfClients.Items.Add(i);
                ListOfClients.ClearSelected();
            }
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (InputField.Text.Contains("/s"))
            {
                InputField.Text = InputField.Text.Substring(3, InputField.Text.Length - 3);
                client.TCPSendMessage(new EncryptMessagePacket(client.EncryptString(InputField.Text)));
            }
            else if (ListOfClients.SelectedIndex != -1)
            {
                int selectedClient = ListOfClients.SelectedIndex;
                client.TCPSendMessage(new ChatMessagePacket(InputField.Text, selectedClient));
            }
            else
            {
                client.TCPSendMessage(new ChatMessagePacket(InputField.Text, -1));
            }
            InputField.Clear();
        }

        private void NameButton_Click(object sender, EventArgs e)
        {
            NameInput.Text = NameInput.Text.Replace(" ", "");
            if (NameInput.Text != null && NameInput.Text != string.Empty)
            {
                client.SetName(new ClientNamePacket(NameInput.Text));
                SubmitButton.Enabled = true;
                GameButton.Enabled = true;
                MonoGameButton.Enabled = true;
                DeselectNamesButton.Enabled = true;
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            client.TCPSendMessage(new ChatMessagePacket("disconnected", -1));
            client.SetName(new ClientNamePacket(null));
            Close();
        }

        private void GameButton_Click(object sender, EventArgs e)
        {
            client.TCPSendMessage(new ChatMessagePacket("/game start", -1));
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            client.TCPSendMessage(new ChatMessagePacket("/help", -1));
        }

        private void MonoGameButton_Click(object sender, EventArgs e)
        {
            client.TCPSendMessage(new ChatMessagePacket("/monogame", -1));
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            client.TCPSendMessage(new ChatMessagePacket("/clear", -1));
        }

        private void DeselectNamesButton_Click(object sender, EventArgs e)
        {
            ListOfClients.SelectedIndex = -1;
        }
    }
}
