using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Collections.Concurrent;
using System.Runtime.Serialization.Formatters.Binary;
using Packets;
using System.Security.Cryptography;

namespace Server
{
    class Server
    {
        TcpListener tcpListener;
        UdpClient udpListener;
        ConcurrentDictionary<int, Client> m_Clients;
        IPEndPoint endPoint;

        RSACryptoServiceProvider m_RSAProvider;
        RSAParameters m_PublicKey;
        RSAParameters m_PrivateKey;
        RSAParameters m_ServerKey;

        public Server(string ipAddress, int port)
        {
            IPAddress localAddress = IPAddress.Parse(ipAddress);
            tcpListener = new TcpListener(localAddress, port);
            udpListener = new UdpClient(port);
            Thread thread = new Thread(() => { UDPListen(); });
            thread.Start();

            m_RSAProvider = new RSACryptoServiceProvider(1024);
            m_PublicKey = m_RSAProvider.ExportParameters(false);
            m_PrivateKey = m_RSAProvider.ExportParameters(true);
        }

        public void Start()
        {
            m_Clients = new ConcurrentDictionary<int, Client>();
            int clientIndex = 0;
            tcpListener.Start();
            while (true)
            {
                int index = clientIndex;
                clientIndex++;

                Socket socket = tcpListener.AcceptSocket();
                Client client = new Client(socket);
                m_Clients.TryAdd(index, client);
                Thread thread = new Thread(() => { TCPClientMethod(index); });
                thread.Start();
            }
        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        private void TCPClientMethod(int index)
        {
            Packet packet;

            while ((packet = m_Clients[index].Read()) != null)
            {
                Console.WriteLine("Received...");

                try
                {
                    switch (packet.packetType)
                    {
                        //case PacketType.ChatMessage:
                        //    ChatMessagePacket chatPacket = (ChatMessagePacket)packet;
                        //    for (int i = 0; i < m_Clients.Count; i++)
                        //        m_Clients[i].Send(chatPacket);
                        //    break;
                        //case PacketType.ClientName:
                        //    ClientNamePacket clientName = (ClientNamePacket)packet;
                        //    m_Clients[index].SendName(clientName);
                        //    break;
                        case PacketType.ChatMessage:
                            ChatMessagePacket chatPacket = packet as ChatMessagePacket;
                            if (m_Clients[index].GetName() == null)
                                Console.WriteLine("No name inputted");
                            else
                            {
                                ChatMessagePacket outPacket = new ChatMessagePacket(m_Clients[index].GetName() + ": " + chatPacket._message);
                                //for (int i = 0; i < m_Clients.Count; i++)
                                foreach (Client i in m_Clients.Values)
                                {
                                    //if (m_Clients[i].GetName() != null)
                                    i.Send(outPacket);
                                }
                            }
                            break;
                        case PacketType.ClientName:
                            ClientNamePacket namePacket = packet as ClientNamePacket;
                            m_Clients[index].SetName(index, namePacket._nickName);
                            string listOfNames = "Client List:";
                            foreach (Client i in m_Clients.Values)
                            {
                                if (i.GetName() != null)
                                    listOfNames = listOfNames + Environment.NewLine + i.GetName();
                            }
                            ClientNamePacket listPacket = new ClientNamePacket(listOfNames);
                            foreach (Client i in m_Clients.Values)
                                i.Send(listPacket);
                            break;
                        case PacketType.Login:
                            LoginPacket loginPacket = (LoginPacket)packet;
                            endPoint = loginPacket._endPoint;
                            break;
                        default:
                            break;
                    }
                }

                catch
                {

                }
            }
            m_Clients[index].Close();
            Client c;
            m_Clients.TryRemove(index, out c);
        }

        private byte[] Encrypt(byte[] data)
        {
            lock (m_RSAProvider)
            {
                m_RSAProvider.ImportParameters(m_ServerKey);
                return m_RSAProvider.Encrypt(data, true);
            }
        }

        private byte[] Decrypt(byte[] data)
        {
            lock (m_RSAProvider)
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
            string messageString;
            Decrypt(message);
            messageString = UTF8Encoding.UTF8.GetString(message);
            return messageString;
        }

        //private Packet GetReturnMessage(Packet code)
        //{
        //    string messageToReturn;
        //    if (code.ToLower() == "hi" || code.ToLower() == "hello")
        //        messageToReturn = "Hello";
        //    else if (code.ToLower() == "random")
        //        messageToReturn = "asijhdbiusbd9123gr";
        //    else
        //        messageToReturn = "Unknown input";
        //    return messageToReturn;
        //}

        private void UDPListen()
        {
            IPEndPoint udpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] bytes = udpListener.Receive(ref udpEndPoint);
            MemoryStream memStream = new MemoryStream(bytes);

            for (int i = 0; i < m_Clients.Count; i++)
            {
                if(endPoint.ToString() == udpEndPoint.ToString())
                {
                    udpListener.Send(bytes, bytes.Length, udpEndPoint);
                }
            }
        }
    }

    class Client
    {
        Socket socket;
        NetworkStream stream;
        BinaryReader reader;
        BinaryWriter writer;
        BinaryFormatter formatter;
        //IPEndPoint endPoint;
        object readLock;
        object writeLock;

        string clientName;

        public Client(Socket mSocket)
        {
            writeLock = new object();
            readLock = new object();
            socket = mSocket;

            stream = new NetworkStream(socket);
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
            formatter = new BinaryFormatter();
        }

        public void Close()
        {
            stream.Close();
            reader.Close();
            writer.Close();
            socket.Close();
        }

        public Packet Read()
        {
            lock (readLock)
            {
                try
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
                catch
                {
                    return null;
                }
            }
        }

        public void Send(Packet message)
        {
            try
            {
                lock (writeLock)
                {
                    MemoryStream memStream = new MemoryStream();
                    formatter.Serialize(memStream, message);
                    byte[] buffer = memStream.GetBuffer();
                    writer.Write(buffer.Length);
                    writer.Write(buffer);
                    writer.Flush();
                }
            }

            catch
            {

            }
        }

        public string GetName()
        {
            return clientName;
        }

        public void SetName(int i, string name)
        {
            clientName = name;
            //Packet clientNamePacket = new ClientNamePacket(name);
            //MemoryStream memStream = new MemoryStream();
            //formatter.Serialize(memStream, clientNamePacket);
            //byte[] buffer = memStream.GetBuffer();
            //writer.Write(buffer.Length);
            //writer.Write(buffer);
            //writer.Flush();
        }

        //public string SetClientName(Packet name)
        //{
        //    int numberOfBytes;
        //    if ((numberOfBytes = reader.ReadInt32()) != -1)
        //    {
        //        byte[] buffer = reader.ReadBytes(numberOfBytes);
        //        MemoryStream memStream = new MemoryStream(buffer);
        //        return formatter.Deserialize(memStream) as Packet;
        //    }
        //    else
        //        return null;
        //}
    }
}
