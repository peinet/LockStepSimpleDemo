using Unity;
using UnityEngine;
using Protocol;
using Google.Protobuf;
using System;
using System.Collections.Generic;

public class LockStepClientMgr : Singleton<LockStepClientMgr>
{
    private int _clientFrame = 0;
    private int _nearstServerKeyFrame = 0;
    private int _serverKeyFrame = 0;
    // 这个共享资源，要不要上锁
    private SortedList<int, LockStepKeyFrameData> _lockStepKeyFrameDataList;
    private GameContext _context;
    private GameController _gameController;

    public override void Init()
    {
        _context = Contexts.sharedInstance.game;
        _gameController =  this.gameObject.GetComponent<GameController>();
        MessageDispatcher.GetInstance().AddMessageListener(MsgID.LockStepEnd, OnLockStepEnd);
        MessageDispatcher.GetInstance().AddMessageListener(MsgID.SteerPositionRsp, OnSteerPositionRsp);
        _lockStepKeyFrameDataList = new SortedList<int, LockStepKeyFrameData>();
        _nearstServerKeyFrame = _clientFrame + Config.SYN_RATE_SERVER;
    }

    public void SendMsg(MsgID msgId, IMessage msg)
    {
        ClientUdpMgr.GetInstance().SendMsg(msgId, msg);
    }

    private void FixedUpdate()
    {
        Log4U.LogDebug("LockStepClientMgr:FixedUpdate _clientFrame=", _clientFrame);
        if (_clientFrame + Config.QUICK_PLAY_GAP <  _serverKeyFrame)
        {
            // 追平服务器
            int targetFrame = Math.Min(_serverKeyFrame, _clientFrame + Config.QUICK_PLAY_SPEED_MAX);
            while (_clientFrame < targetFrame)
            {
                if (_clientFrame == _nearstServerKeyFrame)
                {
                    // 关键帧
                    if (!PlayKeyFrame())
                    {
                        break;
                    }
                }
                else
                {
                    // 非关键帧
                    _gameController.Execute();
                    _clientFrame++;
                }
            }
        }
        else
        {
            // 正常走
            if (_clientFrame == _nearstServerKeyFrame)
            {
                // 关键帧
                PlayKeyFrame();
            }
            else if (_clientFrame < _nearstServerKeyFrame)
            {
                // 非关键帧
                _gameController.Execute();
                _clientFrame++;
            }
        }
    }

    private bool PlayKeyFrame()
    {
        if (_lockStepKeyFrameDataList.Count ==0)
        {
            // 数据还未接收完毕，继续等待
            return false;
        }
        LockStepKeyFrameData lockStepKeyFrameData = _lockStepKeyFrameDataList.Values[0] ;
        if (lockStepKeyFrameData.KeyFrame == _clientFrame)
        {
            foreach (LockStepClientMsgItem LockStepClientMsgItem in lockStepKeyFrameData.ReceiveMsgList)
            {
                if (LockStepClientMsgItem.msgId == MsgID.SteerPositionRsp)
                {
                    SteerPositionRsp resp = (SteerPositionRsp)LockStepClientMsgItem.msg;
                    GameEntity entity = _context.CreateEntity();
                    entity.AddPlayerId(resp.PlayerId);
                    entity.AddSteerPosition(new Vector2(resp.X, resp.Y));
                    Log4U.LogDebug("LockStepClientMgr:FixedUpdate _clientFrame=", _clientFrame, " resp=", resp.ToString());
                }
            }
            _lockStepKeyFrameDataList.RemoveAt(0);
            _nearstServerKeyFrame = _clientFrame + Config.SYN_RATE_SERVER;
            _gameController.Execute();
            _clientFrame++;
        }
        return true;
    }

    private void OnLockStepEnd(IMessage msg, object ext)
    {
        LockStepEnd rsp = (LockStepEnd)msg;
        int serverKeyFrame = rsp.KeyFrame;
        _serverKeyFrame = Math.Max(_serverKeyFrame, serverKeyFrame);
        int msgTotal = rsp.MsgTotal;
        LockStepKeyFrameData lockStepKeyFrameData;
        if (_lockStepKeyFrameDataList.ContainsKey(serverKeyFrame))
        {
            lockStepKeyFrameData = _lockStepKeyFrameDataList[serverKeyFrame];
        }
        else
        {
            lockStepKeyFrameData = new LockStepKeyFrameData(serverKeyFrame);
            _lockStepKeyFrameDataList.Add(serverKeyFrame, lockStepKeyFrameData);
        }
        lockStepKeyFrameData.SetMsgTotal(msgTotal);
    }

    private void OnSteerPositionRsp(IMessage msg, object ext)
    {
        SteerPositionRsp steerPositionRsp = (SteerPositionRsp)msg;
        int serverKeyFrame = steerPositionRsp.KeyFrame;
        LockStepClientMsgItem lockStepClientMsgItem = new LockStepClientMsgItem(MsgID.SteerPositionRsp, msg);
        LockStepKeyFrameData lockStepKeyFrameData;
        if (_lockStepKeyFrameDataList.ContainsKey(serverKeyFrame))
        {
            lockStepKeyFrameData = _lockStepKeyFrameDataList[serverKeyFrame];
        }
        else
        {
            lockStepKeyFrameData = new LockStepKeyFrameData(serverKeyFrame);
            _lockStepKeyFrameDataList.Add(serverKeyFrame, lockStepKeyFrameData);
        }
        lockStepKeyFrameData.AddLockStepClientMsgItem(lockStepClientMsgItem);
    }

  

    public override void Release()
    {
        MessageDispatcher.GetInstance().RemoveMessageListener(MsgID.LockStepEnd, OnLockStepEnd);
        MessageDispatcher.GetInstance().RemoveMessageListener(MsgID.SteerPositionRsp, OnSteerPositionRsp);
    }

    public int ClientKeyFrame
    {
        get { return _clientFrame; }
    }


}