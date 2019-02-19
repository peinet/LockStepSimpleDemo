using System.Collections.Generic;

public class LockStepKeyFrameData
{
    private int _keyFrame = 0;
    private int _msgTotal = 0;
    private bool _isComplete = false;
    private List<LockStepClientMsgItem> _receiveMsgList;
   
    public LockStepKeyFrameData(int keyFrame)
    {
        _keyFrame = keyFrame;
        _receiveMsgList = new List<LockStepClientMsgItem>();
    }

    public void AddLockStepClientMsgItem(LockStepClientMsgItem msg)
    {
        _receiveMsgList.Add(msg);
        CheckComplete();
    }

    public void SetMsgTotal(int msgTotal)
    {
        _msgTotal = msgTotal;
        CheckComplete();
    }

    public void CheckComplete()
    {
        _isComplete = _msgTotal == _receiveMsgList.Count;
    }



    public int KeyFrame
    {
        get
        {
            return _keyFrame;
        }

        set
        {
            _keyFrame = value;
        }
    }

    public int MsgTotal
    {
        get
        {
            return _msgTotal;
        }

        set
        {
            _msgTotal = value;
        }
    }

    public bool IsComplete
    {
        get
        {
            return _isComplete;
        }
    }

    public List<LockStepClientMsgItem> ReceiveMsgList
    {
        get
        {
            return _receiveMsgList;
        }
    }
}