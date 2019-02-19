using Protocol;
using Google.Protobuf;

public struct LockStepServerMsgItem
{
    public int sendPlayerId;
    public MsgID msgId;
    public IMessage msg;
    

    public LockStepServerMsgItem(int sendPlayerId, MsgID msgId, IMessage msg)
    {
        this.sendPlayerId = sendPlayerId;
        this.msgId = msgId;
        this.msg = msg;
    }

}
