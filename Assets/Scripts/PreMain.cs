using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FixMath;
using Protocol;
using Google.Protobuf;
using System.IO;
using UnityEditor;
using System;

/**
 * 启动类
 */
public class PreMain : MonoBehaviour
{
    private void Start()
    {
        // 设置UI帧频 在Edit/Project Settings/Quality  质量设置里把帧数设定关闭，关闭之后才能在代码中修改游戏运行的帧数。
        Application.targetFrameRate = 30;
        // 设置物理帧频 在Edit/Project Settings/Time/Fixed Timestep  1/60=0.0167
        StartCoroutine(LoadConfig());
    }

    private IEnumerator LoadConfig()
    {
        //读取config.txt
        //StreamReader reader = new StreamReader(Application.streamingAssetsPath + "/config.txt");
        //string config = reader.ReadLine();
        //reader.Close();

        // 重新导入
        ////AssetDatabase.ImportAsset("Assets/Resources/config.txt");
        //TextAsset textAsset = Resources.Load<TextAsset>("config");
        //string config = textAsset.text;

        WWW www = new WWW(Application.streamingAssetsPath + "/config.txt");
        yield return www;
        byte[] bytes = www.bytes;
        // BOM是“Byte Order Mark”标记文件的编码 EF BB BF     UTF-8保存的文本有，ANSI无
        byte[] configBytes = new byte[bytes.Length - 3];
        Array.Copy(bytes, 3, configBytes, 0, configBytes.Length);
        ConfigJson configJson = JsonUtility.FromJson<ConfigJson>(System.Text.Encoding.Default.GetString(configBytes));
        Config.IsServer = configJson.IsServer;
        Config.ServerAddress = configJson.ServerAddress;
        Config.PlayerId = configJson.PlayerId;
        Log4U.Init();
        Log4U.LogDebug("IsServer=", configJson.IsServer);
        Log4U.LogDebug("ServerAddress=", configJson.ServerAddress);
        Log4U.LogDebug("PlayerId=", configJson.PlayerId);
        InitGame();
    }

    private void InitGame()
    {
        Log4U.LogDebug("PreMain Start");
        this.gameObject.AddComponent<Main>();
    }

    private void Update()
    {

    }
}
