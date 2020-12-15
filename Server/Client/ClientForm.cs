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

        public void UpdateClientList(string name)
        {
            if (ClientList.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateClientList(name);
                }));
            }

            else
            {
                ClientList.Clear();
                ClientList.Text += name + Environment.NewLine;
                ClientList.SelectionStart = ClientList.Text.Length;
                ClientList.ScrollToCaret();
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
            else
                client.TCPSendMessage(new ChatMessagePacket(InputField.Text));
            InputField.Clear();
        }

        private void NameButton_Click(object sender, EventArgs e)
        {
            if (NameInput.Text != null && NameInput.Text != " ")
            {
                client.SetName(new ClientNamePacket(NameInput.Text));
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            client.TCPSendMessage(new ChatMessagePacket("disconnected"));
            client.SetName(new ClientNamePacket(null));
            Close();
        }

        private void GameButton_Click(object sender, EventArgs e)
        {
            client.TCPSendMessage(new ChatMessagePacket("/game start"));
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            client.TCPSendMessage(new ChatMessagePacket("/help"));
        }
    }
}
