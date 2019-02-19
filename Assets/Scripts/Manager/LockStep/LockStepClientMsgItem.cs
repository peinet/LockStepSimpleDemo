using Protocol;
using Google.Protobuf;

public class LockStepClientMsgItem
{
    public MsgID msgId;
    public IMessage msg;

    public LockStepClientMsgItem(MsgID msgId, IMessage msg)
    {
        this.msgId = msgId;
        this.msg = msg;
    }

}
