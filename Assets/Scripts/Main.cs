using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protocol;

public class Main : MonoBehaviour {

    private static GameObject _rootObj;
    private static List<Action> _singletonReleaseList = new List<Action>();
    private int _updateFrame = 0;
    private int _fixUpdateFrame = 0;

    public void Awake () {
        // Fix64测试
        //this.gameObject.AddComponent<Fix64Test>();
        // 早一点挂GameController组件，因为被LockStepClientMgr引用
        if (!Config.IsServer)
        {
            this.gameObject.AddComponent<GameController>();
        }
        _rootObj = gameObject;
        GameObject.DontDestroyOnLoad(_rootObj);
        InitSingletons();
        InitGame();
    }

    /// <summary>
    /// 在这里进行所有单例的初始化
    /// </summary>
    /// <returns></returns>
    private void InitSingletons()
    {
        AddSingleton<MessageDispatcher>();
        if (!Config.IsServer)
        {
            AddSingleton<ClientUdpMgr>();
            AddSingleton<LockStepClientMgr>();
        }
        else
        {
            AddSingleton<ServerUDPMgr>();
            AddSingleton<LockStepServerMgr>();
        }
    }

    private static void AddSingleton<T>() where T : Singleton<T>
    {
        if (_rootObj.GetComponent<T>() == null)
        {
            T t = _rootObj.AddComponent<T>();
            t.SetInstance(t);
            t.Init();

            _singletonReleaseList.Add(delegate ()
            {
                t.Release();
            });
        }
    }

    public static T GetSingleton<T>() where T : Singleton<T>
    {
        T t = _rootObj.GetComponent<T>();

        if (t == null)
        {
            AddSingleton<T>();
        }

        return t;
    }

    private void InitGame()
    {
        if (!Config.IsServer)
        {
           
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/GameUI"));
            GameObject popUpLayer = GameObject.Find("Canvas/PopUpLayer");
            go.GetComponent<RectTransform>().SetParent(popUpLayer.GetComponent<RectTransform>(), false);
            // 发送登录请求
            ClientUdpMgr.GetInstance().SendMsg(MsgID.LoginReq, new LoginReq());
        }
    }

    public void Update()
    {
        Log4U.LogDebug("Main:Update _updateFrame=", _updateFrame);
        _updateFrame++;
    }

    public void FixedUpdate()
    {
        Log4U.LogDebug("Main:FixedUpdate _fixUpdateFrame=", _fixUpdateFrame);
        _fixUpdateFrame++;
    }

    // Main销毁单例和GameController销毁ECS都用OnDestroy方法
    //public void OnApplicationQuit()
    public void OnDestroy()
    {
        for (int i = _singletonReleaseList.Count - 1; i >= 0; i--)
        {
            _singletonReleaseList[i]();
        }
    }

}
