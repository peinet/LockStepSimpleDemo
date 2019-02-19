using System;
using System.Net.Sockets;
using Protocol;
using Google.Protobuf;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class ServerUDPMgr : Singleton<ServerUDPMgr>
{
    private ASynKcpUdpServerSocket _socket;

    public override void Init()
    {
        _socket = new ASynKcpUdpServerSocket(Config.Port, RecHandler);
    }

    private void RecHandler(byte[] buf, ASynServerKcp serverKcp)
    {
        int playerId = BitConverter.ToInt32(buf, 0);
        serverKcp.PlayerId = playerId;
        MsgID msgId = (MsgID)BitConverter.ToInt32(buf, 4);
        MemoryStream stream = new MemoryStream(buf, 8, buf.Length - 8);
        IMessage msg = null;
        switch (msgId)
        {
            case MsgID.LoginReq:
                msg = LoginReq.Parser.ParseFrom(stream);
                break;
            case MsgID.LoginOutReq:
                msg = LoginOutReq.Parser.ParseFrom(stream);
                break;
            case MsgID.SteerPositionReq:
                //msg = SteerPositionReq.Descriptor.Parser.ParseFrom(stream);
                msg = SteerPositionReq.Parser.ParseFrom(stream);
                break;
            default:
                Log4U.LogDebug("SimulateServerUDP:RecHandler Not handler msgId=", msgId);
                break;
        }
        stream.Dispose();
        Log4U.LogDebug("SimulateServerUDP:RecHandler<<<<<< playerId=", playerId, " msgId=", msgId, " msg=", msg);
        MessageDispatcher.GetInstance().DispatchMessageAsyn(msgId, msg, playerId);
    }

    public void Update()
    {
        _socket.Update();
    }

    public override void Release()
    {
        _socket.Dispose();
        _socket = null;
    }

    public void RemovePlayer(int playerId)
    {
        _socket.RemovePlayer(playerId);
    }

    public void SendMsgToAll(MsgID msgId, IMessage msg)
    {
        Log4U.LogDebug("SimulateServerUDP:SendMsgToAll>>>>>> msgId=", msgId, " msg=", msg);
        byte[] msgIdBytes = BitConverter.GetBytes((int)msgId);
        byte[] msgBytes = msg.ToByteArray();
        int bufferSize = msgIdBytes.Length + msgBytes.Length;
        byte[] sendBuf = new byte[bufferSize];
        int offset = 0;
        Array.Copy(msgIdBytes, 0, sendBuf, offset, msgIdBytes.Length);
        offset += msgIdBytes.Length;
        Array.Copy(msgBytes, 0, sendBuf, offset, msgBytes.Length);
        _socket.SendToAll(sendBuf);
    }
}