using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using Packets;
using System.Net;
using System.Security.Cryptography;

namespace Client
{
    public class Client
    {
        TcpClient tcpClient;
        UdpClient udpClient;
        NetworkStream stream;
        //StreamWriter writer;
        //StreamReader reader;
        BinaryReader reader;
        BinaryWriter writer;
        BinaryFormatter formatter;
        ClientForm clientForm;

        string clientName;

        RSACryptoServiceProvider m_RSAProvider;
        RSAParameters m_PublicKey;
        RSAParameters m_PrivateKey;
        RSAParameters m_ClientKey;

        public Client()
        {
            tcpClient = new TcpClient();
            udpClient = new UdpClient();
            m_RSAProvider = new RSACryptoServiceProvider(1024);
            m_PublicKey = m_RSAProvider.ExportParameters(false);
            m_PrivateKey = m_RSAProvider.ExportParameters(true);
        }

        public bool Connect(string ipAddress, int port)
        {
            tcpClient.Connect(ipAddress, port);
            udpClient.Connect(ipAddress, port);
            if (tcpClient.Connected)
            {
                stream = tcpClient.GetStream();
                writer = new BinaryWriter(stream);
                reader = new BinaryReader(stream);

                formatter = new BinaryFormatter();
                return true;
            }

            else
                return false;
        }

        public void Run()
        {
            clientForm = new ClientForm(this);
            Thread TCPThread = new Thread(TCPProcessServerResponse);
            TCPThread.Start();
            Thread UDPThread = new Thread(UDPProcessServerResponse);
            UDPThread.Start();
            clientForm.ShowDialog();
            tcpClient.Close();
            udpClient.Close();
        }

        public IPEndPoint Login()
        {
            return (IPEndPoint)udpClient.Client.LocalEndPoint;
        }

        private void TCPProcessServerResponse()
        {
            //Packet message, clientName;
            //clientName = ReadClientName();
            //if (clientName == null)
            //{
            //    ClientNamePacket namePacket = (ClientNamePacket)clientName;
            //    while ((message = TCPReadMessage()) != null)
            //    {
            //        ChatMessagePacket chatPacket = (ChatMessagePacket)message;
            //        clientForm.UpdateChatWindow(namePacket._nickName + ": " + chatPacket._message); //Update this to have a name instead of server
            //    }
            //}

            try
            {
                while (reader != null)
                {
                    int numOfBytes;
                    if ((numOfBytes = reader.ReadInt32()) != -1)
                    {
                        byte[] buffer = reader.ReadBytes(numOfBytes);
                        MemoryStream mStream = new MemoryStream(buffer);
                        Packet clientPacket = formatter.Deserialize(mStream) as Packet;

                        switch (clientPacket.packetType)
                        {
                            case PacketType.ChatMessage:
                                ChatMessagePacket chatPacket = clientPacket as ChatMessagePacket;
                                clientForm.UpdateChatWindow(chatPacket._message);
                                break;
                            case PacketType.ClientName:
                                ClientNamePacket namePacket = clientPacket as ClientNamePacket;
                                clientForm.UpdateClientList(namePacket._nickName);
                                break;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void UDPProcessServerResponse()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                while(true)
                {
                    byte[] bytes = udpClient.Receive(ref endPoint);
                    MemoryStream memStream = new MemoryStream(bytes);
                    Packet udpPacket = formatter.Deserialize(memStream) as Packet;
                }
            }

            catch(SocketException e)
            {
                Console.WriteLine("Client UDP read method exception: ", e.Message);
            }
        }

        public void TCPSendMessage(Packet message)
        {
            MemoryStream memStream = new MemoryStream();
            formatter.Serialize(memStream, message);
            byte[] buffer = memStream.GetBuffer();
            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
        }

        public void UDPSendMessage(Packet packet)
        {
            MemoryStream memStream = new MemoryStream();
            formatter.Serialize(memStream, packet);
            byte[] buffer = memStream.GetBuffer();
            udpClient.Send(buffer, buffer.Length);
        }

        public Packet TCPReadMessage()
        {
            int numberOfBytes;
            if ((numberOfBytes = reader.ReadInt32()) != -1)
            {
                byte[] buffer = reader.ReadBytes(numberOfBytes);
                MemoryStream memStream = new MemoryStream(buffer);
                return formatter.Deserialize(memStream) as Packet;
            }
            else
                return null;
        }

        public void SetName(Packet name)
        {
            MemoryStream memStream = new MemoryStream();
            formatter.Serialize(memStream, name);
            byte[] buffer = memStream.GetBuffer();
            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
        }

        //public string GetName()
        //{
        //    return clientName;
        //}

        private byte[] Encrypt(byte[] data)
        {
            lock(m_RSAProvider)
            {
                m_RSAProvider.ImportParameters(m_ClientKey);
                return m_RSAProvider.Encrypt(data, true);
            }
        }

        private byte[] Decrypt(byte[] data)
        {
            lock(m_RSAProvider)
            {
                m_RSAProvider.ImportParameters(m_PrivateKey);
                return m_RSAProvider.Decrypt(data, true);
            }
        }

        private byte[] EncryptString(string message)
        {
            byte[] bytes;
            bytes = UTF8Encoding.UTF8.GetBytes(message);
            Encrypt(bytes);
            return bytes;
        }

        private string DecryptString(byte[] message)
        {
            string _message;
            Decrypt(message);
            _message = UTF8Encoding.UTF8.GetString(message);
            return _message;
        }
    }
}
