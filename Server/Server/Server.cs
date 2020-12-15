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

        //Hangman variables
        static bool gameStarted = false;
        static int m_Lives = 6;
        static List<char> m_IncorrectLetters;
        static List<char> m_CorrectLetters;
        static string[] m_Words = { "server", "client", "socket", "serialisation", "address", "programming", "network", "concurrency" };
        static string m_Word;
        static string m_WordAnswer;

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

        private void SendPacketToAll(string message)
        {
            ChatMessagePacket outPacket = new ChatMessagePacket(message);
            foreach (Client i in m_Clients.Values)
                i.Send(outPacket);
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
                        case PacketType.ChatMessage:
                            ChatMessagePacket chatPacket = packet as ChatMessagePacket;
                            //Help command
                            if (chatPacket._message == "/help")
                            {
                                ChatMessagePacket outPacket = new ChatMessagePacket("You need to set a name before you can do anything, to do that simply type a name into the textbox above and then click the Enter Name button." + Environment.NewLine + 
                                    "To send a message you need to type a message in the textbox at the bottom and then either click the send message button or press the enter key on your keyboard." + Environment.NewLine + 
                                    "/w [recipient] [message] - Whisper to another client" + Environment.NewLine + "/game [command] or /g [command] - commands for the hangman game " + Environment.NewLine +
                                    "Game commands: start - starts a game of hangman, end - ends the game, guess - guess a letter from the word" + Environment.NewLine + "/help - brings up this text" + Environment.NewLine);
                                m_Clients[index].Send(outPacket);
                                break;
                            }
                            //Check for client name
                            if (m_Clients[index].GetName() == null || m_Clients[index].GetName() == string.Empty && chatPacket._message != "/help")
                            {
                                ChatMessagePacket outPacket = new ChatMessagePacket("Please enter a name");
                                m_Clients[index].Send(outPacket);
                                break;
                            }
                            else if (chatPacket._message.Contains("/clear"))
                            {
                                ChatMessagePacket outPacket = new ChatMessagePacket("/clear");
                                m_Clients[index].Send(outPacket);
                            }
                            //Whisper command
                            else if (chatPacket._message.Contains("/w"))
                            {
                                bool findName = false;
                                string senderName = m_Clients[index].GetName();
                                string recipientName = string.Empty;
                                ChatMessagePacket outPacket = new ChatMessagePacket(string.Empty);
                                foreach (Client i in m_Clients.Values)
                                {
                                    try
                                    {
                                        if (chatPacket._message.Contains("/w " + i.GetName()) && i.GetName() != null && senderName != i.GetName())
                                        {
                                            findName = true;
                                            recipientName = i.GetName();
                                            chatPacket._message = chatPacket._message.Substring(recipientName.Length + 3, chatPacket._message.Length - (recipientName.Length + 3));
                                            outPacket = new ChatMessagePacket(senderName + " whispers:" + chatPacket._message);
                                            i.Send(outPacket);
                                        }
                                    }

                                    catch
                                    {

                                    }
                                }
                                
                                if (findName == true)
                                {
                                    outPacket = new ChatMessagePacket("You whispered to " + recipientName + ": " + chatPacket._message);
                                    m_Clients[index].Send(outPacket);
                                }
                                else
                                {
                                    outPacket = new ChatMessagePacket("Incorrect syntax for a private message. It should be /w [recipient] [message]");
                                    m_Clients[index].Send(outPacket);
                                }
                            }
                            //Game commands
                            else if (chatPacket._message.Contains("/game") || chatPacket._message.Contains("/g"))
                            {
                                if (chatPacket._message.Contains("start") && !gameStarted)
                                {
                                    SendPacketToAll("Game: A game of hangman was started by " + m_Clients[index].GetName() + ". To participate type /game guess [letter] to guess a letter");
                                    gameStarted = true;
                                    GameStart();
                                    SendPacketToAll(ShowHangman());
                                }

                                else if (chatPacket._message.Contains("end") && gameStarted)
                                {
                                    SendPacketToAll("The game has been ended by " + m_Clients[index].GetName() + ", the word was " + m_Word + ".");
                                    gameStarted = false;
                                }

                                else if (chatPacket._message.Contains("guess") && gameStarted && m_Lives > 0)
                                {
                                    if (chatPacket._message.Contains("/game"))
                                        chatPacket._message = chatPacket._message.Substring(12, 1);
                                    if (chatPacket._message.Contains("/g"))
                                        chatPacket._message = chatPacket._message.Substring(9, 1);
                                    SendPacketToAll("Game: " + m_Clients[index].GetName() + " guessed " + chatPacket._message + ".");
                                    if (m_Word.Contains(chatPacket._message))
                                        m_CorrectLetters.Add(chatPacket._message[0]);
                                    else if (m_IncorrectLetters.Contains(chatPacket._message[0]))
                                    {
                                        SendPacketToAll(chatPacket._message + " has already been guessed.");
                                    }
                                    else if (!m_Word.Contains(chatPacket._message[0]))
                                    {
                                        m_Lives--;
                                        m_IncorrectLetters.Add(chatPacket._message[0]);
                                    }

                                    SendPacketToAll(ShowHangman());
                                }

                                if (m_Lives == 0 && gameStarted)
                                {
                                    gameStarted = false;
                                    SendPacketToAll("GAME OVER!" + Environment.NewLine + "The word was " + m_Word + Environment.NewLine + "Type /game start or press the hangman button if you want to play again.");
                                }

                                if (m_Word == m_WordAnswer && gameStarted)
                                {
                                    gameStarted = false;
                                    SendPacketToAll("YOU WIN" + Environment.NewLine + "You guessed " + m_Word + " correctly" + Environment.NewLine + "Type /game start or press the hangman button if you want to play again.");
                                }
                            }
                            //Normal message
                            else
                            {
                                if (chatPacket._message != string.Empty)
                                {
                                    SendPacketToAll(m_Clients[index].GetName() + ": " + chatPacket._message);
                                }
                            }
                            break;
                        case PacketType.ClientName:
                            ClientNamePacket namePacket = packet as ClientNamePacket;
                            m_Clients[index].SetName(namePacket._nickName);
                            //Check for duplicate name
                            if (m_Clients[index].GetName() != null || m_Clients[index].GetName() != string.Empty)
                            {
                                int numberOfDuplicateNames = 0;
                                string inputtedName = m_Clients[index].GetName();
                                foreach (Client i in m_Clients.Values)
                                {
                                    if (inputtedName == i.GetName())
                                        numberOfDuplicateNames++;
                                }

                                if (numberOfDuplicateNames > 1)
                                {
                                    ChatMessagePacket outPacket = new ChatMessagePacket("That name is already taken, please try another name.");
                                    m_Clients[index].Send(outPacket);
                                    m_Clients[index].SetName(null);
                                    break;
                                }

                                else
                                {
                                    string listOfNames = "Client List:";
                                    foreach (Client i in m_Clients.Values)
                                    {
                                        if (i.GetName() != null)
                                            listOfNames = listOfNames + Environment.NewLine + i.GetName();
                                    }
                                    ClientNamePacket listPacket = new ClientNamePacket(listOfNames);
                                    foreach (Client i in m_Clients.Values)
                                        i.Send(listPacket);
                                }
                            }
                            break;
                        case PacketType.EncryptedMessage:
                            EncryptMessagePacket encryptedPacket = packet as EncryptMessagePacket;
                            foreach (Client i in m_Clients.Values)
                                i.Send(encryptedPacket);
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

        static void GameStart()
        {
            m_Lives = 6;
            m_IncorrectLetters = new List<char>();
            m_CorrectLetters = new List<char>();
            Random rand = new Random();
            m_Word = m_Words[rand.Next(0, m_Words.Length)];
            Console.WriteLine(m_Word);
        }

        static string ShowHangman()
        {
            string returnWord = "";
            for (int i = 0; i < m_Word.Length; i++)
            {
                if (m_CorrectLetters.Contains(Char.ToLower(m_Word[i])))
                    returnWord = returnWord + m_Word[i];
                else
                    returnWord = returnWord + "_";
            }

            m_WordAnswer = returnWord;

    //        ChatMessagePacket outPacket = new ChatMessagePacket(Environment.NewLine + "     |------+  " + Environment.NewLine + "     |      |  " + Environment.NewLine + "     |      " + (m_Lives < 6 ? "O" : "") + Environment.NewLine +
    //"     |     " + (m_Lives < 4 ? "/" : "") + (m_Lives < 5 ? "|" : "") + (m_Lives < 3 ? @"\" : "") + Environment.NewLine + "     |     " + (m_Lives < 2 ? "/" : "") + " " + (m_Lives < 1 ? @"\" : "") + Environment.NewLine +
    //"     |         " + Environment.NewLine + "===============" + Environment.NewLine + returnWord + Environment.NewLine);

            returnWord = Environment.NewLine + "     |------+  " + Environment.NewLine + "     |      |  " + Environment.NewLine + "     |      " + (m_Lives < 6 ? "O" : "") + Environment.NewLine +
    "     |     " + (m_Lives < 4 ? "/" : "") + (m_Lives < 5 ? "|" : "") + (m_Lives < 3 ? @"\" : "") + Environment.NewLine + "     |     " + (m_Lives < 2 ? "/" : "") + " " + (m_Lives < 1 ? @"\" : "") + Environment.NewLine +
    "     |         " + Environment.NewLine + "===============" + Environment.NewLine + returnWord + Environment.NewLine;

            return returnWord;
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

        public void SetName(string name)
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
