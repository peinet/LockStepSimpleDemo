using Unity;
using UnityEngine;
using Protocol;
using Google.Protobuf;
using System;
using System.Collections.Generic;

public class LockStepServerMgr : Singleton<LockStepServerMgr>
{
    private int _currFrame = 0;
    private int _currKeyFrame = 0;
    private int _nextKeyFrame = 0;
    private int _msgIndex = 0; // 每个关键帧中，消息的序号
    private List<LockStepServerMsgItem> _msgQueue;
    private int _loginInPlayerCount = 0;

    public override void Init()
    {
        _msgQueue = new List<LockStepServerMsgItem>();
        MessageDispatcher.GetInstance().AddMessageListener(MsgID.LoginReq, OnLoginReq);
        MessageDispatcher.GetInstance().AddMessageListener(MsgID.SteerPositionReq, OnSteerPositionReq);
    }

    private void OnLoginReq(IMessage msg, object ext)
    {
        int playerId = (int)ext;
        LoginRsp rsp = new LoginRsp();
        rsp.PlayerId = playerId;
        ServerUDPMgr.GetInstance().SendMsgToAll(MsgID.LoginRsp, rsp);
        _loginInPlayerCount++;
    }

    private void OnSteerPositionReq(IMessage msg, object ext)
    {
        LockStepServerMsgItem serverMsgItem = new LockStepServerMsgItem((int)ext, MsgID.SteerPositionReq, msg);
        _msgQueue.Add(serverMsgItem);
    }

    private void FixedUpdate()
    {
        //  2个客户端都登录后，才开始帧同步
        if(_loginInPlayerCount < 2)
        {
            return;
        }
        _nextKeyFrame = _currKeyFrame + Config.SYN_RATE_SERVER;
        if (_currFrame % Config.SYN_RATE_SERVER == 0)
        {
            // 当前关键帧结束
            LockStepEnd lockStepEnd = new LockStepEnd();
            lockStepEnd.KeyFrame = _nextKeyFrame;
            lockStepEnd.MsgTotal = _msgIndex;
            ServerUDPMgr.GetInstance().SendMsgToAll(MsgID.LockStepEnd, lockStepEnd);
            _currKeyFrame = _currFrame;
            _msgIndex = 0;
        }
        if (_msgQueue.Count != 0)
        {
            foreach (LockStepServerMsgItem serverMsgItem in _msgQueue)
            {
                if (serverMsgItem.msgId == MsgID.SteerPositionReq)
                {
                    SteerPositionReq steerPositionReq = (SteerPositionReq)serverMsgItem.msg;
                    SteerPositionRsp steerPositionRsp = new SteerPositionRsp();
                    steerPositionRsp.PlayerId = serverMsgItem.sendPlayerId;
                    steerPositionRsp.X = steerPositionReq.X;
                    steerPositionRsp.Y = steerPositionReq.Y;
                    // 客户端将要执行该命令的帧
                    steerPositionRsp.KeyFrame = _nextKeyFrame;
                    steerPositionRsp.MsgIndex = _msgIndex;
                    ServerUDPMgr.GetInstance().SendMsgToAll(MsgID.SteerPositionRsp, steerPositionRsp);
                    _msgIndex++;
                }
            }
            _msgQueue.Clear();
        }
        _currFrame++;
    }

    public override void Release()
    {
        MessageDispatcher.GetInstance().RemoveMessageListener(MsgID.LoginReq, OnLoginReq);
        MessageDispatcher.GetInstance().RemoveMessageListener(MsgID.SteerPositionReq, OnSteerPositionReq);
    }
}