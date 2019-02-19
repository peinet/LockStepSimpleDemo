using System;
using System.Net.Sockets;
using Protocol;
using Google.Protobuf;
using System.IO;

public class ClientUdpMgr : Singleton<ClientUdpMgr>
{
    private ASynKcpUdpClientSocket _socket = null;

    public override void Init()
    {
        _socket = new ASynKcpUdpClientSocket(Config.PlayerId, Config.ServerAddress, Config.Port, RecHandler);
    }

    private void RecHandler(byte[] buf)
    {
        MsgID msgId = (MsgID)BitConverter.ToInt32(buf, 0);
        MemoryStream stream = new MemoryStream(buf, 4, buf.Length - 4);
        IMessage msg = null;
        switch (msgId)
        {
            case MsgID.LoginRsp:
                msg = LoginRsp.Parser.ParseFrom(stream);
                break;
            case MsgID.SteerPositionRsp:
                msg = SteerPositionRsp.Parser.ParseFrom(stream);
                break;
            case MsgID.LockStepEnd:
                msg = LockStepEnd.Parser.ParseFrom(stream);
                break;
            default:
                Log4U.LogDebug("SimulateClientUDP:RecHandler Not handler msgId=", msgId);
                break;
        }
        stream.Dispose();
        if(msgId != MsgID.LoginRsp)
        {
            Log4U.LogDebug("SimulateClientUDP:RecHandler<<<<<< msgId=", msgId, " msg=", msg);
        }
        MessageDispatcher.GetInstance().DispatchMessageAsyn(msgId, msg);
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

    public void SendMsg(MsgID msgId, IMessage msg)
    {
        //Log4U.LogDebug("SimulateClientUDP:SendMsg>>>>>> msgId=", msgId, " msg=", msg);
        byte[] playerIdBytes = BitConverter.GetBytes(Config.PlayerId);
        byte[] msgIdBytes = BitConverter.GetBytes((int)msgId);
        byte[] msgBytes = msg.ToByteArray();
        int bufferSize = playerIdBytes.Length + msgIdBytes.Length + msgBytes.Length;
        byte[] sendBuf = new byte[bufferSize];
        int offset = 0;
        Array.Copy(playerIdBytes, 0, sendBuf, offset, playerIdBytes.Length);
        offset += playerIdBytes.Length;
        Array.Copy(msgIdBytes, 0, sendBuf, offset, msgIdBytes.Length);
        offset += msgIdBytes.Length;
        Array.Copy(msgBytes, 0, sendBuf, offset, msgBytes.Length);
        _socket.Send(sendBuf);
    }
}