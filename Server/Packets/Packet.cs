using System;
using System.Net;
using System.Collections.Generic;

namespace Packets
{
    public enum PacketType
    {
        ChatMessage,
        PrivateMessage,
        ClientName,
        ClientList,
        Login,
        EncryptedMessage,
        Disconnect
    }

    [Serializable]
    public class Packet
    {
        public PacketType packetType
        {
            get; protected set;
        }
    }

    [Serializable]
    public class ChatMessagePacket : Packet
    {
        public string _message;
        public int _clientListNum;

        public ChatMessagePacket(string message, int clientListNum)
        {
            _message = message;
            _clientListNum = clientListNum;
            packetType = PacketType.ChatMessage;
        }
    }

    [Serializable]
    public class ClientNamePacket : Packet
    {
        public string _nickName;
        
        public ClientNamePacket(string nickname)
        {
            _nickName = nickname;
            packetType = PacketType.ClientName;
        }
    }

    [Serializable]
    public class ClientListPacket : Packet
    {
        //public string _nickName;
        public List<string> _nickName = new List<string>();

        public ClientListPacket(List<string> nickname)
        {
            _nickName = nickname;
            packetType = PacketType.ClientList;
        }
    }

    [Serializable]
    public class LoginPacket : Packet
    {
        public IPEndPoint _endPoint;

        public LoginPacket(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
            packetType = PacketType.Login;
        }
    }

    [Serializable]
    public class EncryptMessagePacket : Packet
    {
        public byte[] _encryptedMessage;

        public EncryptMessagePacket(byte[] message)
        {
            _encryptedMessage = message;
            packetType = PacketType.EncryptedMessage;
        }
    }
}
