using System;
using System.Net;

namespace Packets
{
    public enum PacketType
    {
        ChatMessage,
        PrivateMessage,
        ClientName,
        Login,
        EncryptedMessage
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

        public ChatMessagePacket(string message)
        {
            _message = message;
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
        public string _message;

        public EncryptMessagePacket(string message)
        {
            _message = message;
            packetType = PacketType.EncryptedMessage;
        }
    }
}
