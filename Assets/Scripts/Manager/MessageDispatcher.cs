using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Protocol;
using Google.Protobuf;
using System.Collections;

public class MessageDispatcher:Singleton<MessageDispatcher>
{
    public struct MessageArgs
    {
        public MsgID msgId;
        public IMessage msg;
        public object ext;
    }

    private Dictionary<MsgID, List<Action<IMessage, object>>> _msgDic;
    private Queue<MessageArgs> _receiveMessageQueue;

    public override void Init()
    {
        _msgDic = new Dictionary<MsgID, List<Action<IMessage, object>>>();
        _receiveMessageQueue = new Queue<MessageArgs>();
        StartCoroutine(HandlerMessageAsyn());
    }

    public void AddMessageListener(MsgID msgId, Action<IMessage, object> handler)
    {
        if(_msgDic.ContainsKey(msgId))
        {
            List<Action<IMessage, object>> list = _msgDic[msgId];
            if(!list.Contains(handler))
            {
                list.Add(handler);
            }
        }
        else
        {
            List<Action<IMessage, object>> list = new List<Action<IMessage, object>>();
            list.Add(handler);
            _msgDic.Add(msgId, list);
        }
    }

    public void RemoveMessageListener(MsgID msgId, Action<IMessage, object> handler)
    {
        if (_msgDic.ContainsKey(msgId))
        {
            List<Action<IMessage, object>> list = _msgDic[msgId];
            if (list.Contains(handler))
            {
                list.Remove(handler);
            }
        }
    }

    public void DispatchMessage(MsgID msgId, IMessage msg, object ext=null)
    {
        if (_msgDic.ContainsKey(msgId))
        {
            List<Action<IMessage, object>> list = _msgDic[msgId];
            foreach(Action<IMessage, object> handler in list)
            {
                handler(msg, ext);
            }
        }
    }

    public void DispatchMessageAsyn(MsgID msgId, IMessage msg, object ext = null)
    {
        lock(_receiveMessageQueue)
        {
            MessageArgs args = new MessageArgs();
            args.msgId = msgId;
            args.msg = msg;
            args.ext = ext;
            _receiveMessageQueue.Enqueue(args);
        }
    }

    private IEnumerator HandlerMessageAsyn()
    {
        // 处理完当前_receiveMessageQueue后，代码再次进入yield语句，协程再次挂起，等待下一帧后继续执行
        while(true)
        {
            // 下一帧执行lock后面的语句
            yield return 0;
            lock (_receiveMessageQueue)
            {
                while (_receiveMessageQueue.Count != 0)
                {
                    MessageArgs args = _receiveMessageQueue.Dequeue();
                    DispatchMessage(args.msgId, args.msg, args.ext);
                }
            }
        }
    }

}