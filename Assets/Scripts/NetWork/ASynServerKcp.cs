using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class ASynServerKcp
{
    private KCP _kcp;
    private UdpClient _socket;
    private IPEndPoint _remoteEP;
    private Action<byte[], ASynServerKcp> _recHandler;
    private int _playerId = -1;

    public ASynServerKcp(UInt32 conv_, UdpClient socket, IPEndPoint ep, Action<byte[], ASynServerKcp> recHandler)
    {
        _socket = socket;
        _recHandler = recHandler;
        _remoteEP = new IPEndPoint(new IPAddress(ep.Address.GetAddressBytes()), ep.Port);
        _kcp = new KCP(conv_, KcpOutput);
        // fast mode.
        _kcp.NoDelay(1, 10, 2, 1);
        _kcp.WndSize(128, 128);
    }

    public void Send(byte[] buff)
    {
        _kcp.Send(buff);
    }

    public void Update(UInt32 current)
    {
        _kcp.Update(current);
    }

    public void Input(byte[] rcvBuf)
    {
        _kcp.Input(rcvBuf);
        for (var size = _kcp.PeekSize(); size > 0; size = _kcp.PeekSize())
        {
            byte[] buf = new byte[size];
            if (_kcp.Recv(buf) > 0)
            {
                //Log4U.LogDebug("ASynServerKcp:Input Message receive data=", Encoding.ASCII.GetString(buf));
                _recHandler(buf, this);
            }
        }
    }

    public IPEndPoint RemoteEP { get { return _remoteEP; } }

    public int PlayerId
    {
        get
        {
            return _playerId;
        }

        set
        {
            if(_playerId == -1)
            {
                _playerId = value;
            }
        }
    }

    public void Dispose()
    {
        _kcp = null;
        _socket = null;
        _remoteEP = null;
        _recHandler = null;
    }

    // 服务器发包到客户端的流程：kcp->udp->client
    private void KcpOutput(byte[] buf, int size)
    {
        //Log4U.LogDebug("ASynServerKcp:KcpOutput Message send data=", Encoding.ASCII.GetString(buf, 0, size));
        _socket.Send(buf, size, _remoteEP);
    }

}
