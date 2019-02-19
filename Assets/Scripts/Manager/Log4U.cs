using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class Log4U
{
    public enum LogLevel
    {
        DEBUG = 0,
        INFO,
        WARNING,
        ERROR,
        NOLOG,
    }

    private static LogLevel _currentLevel = LogLevel.DEBUG;

    public Log4U.LogLevel Level
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    public static void Init()
    {
        Application.logMessageReceived += (msg, stack, type) => {
            LogToFile(msg + "\n", true);
        };
        LogToFile("", false); // 清空log_unknow.log
    }

    public static void LogDebug(params object[] msgParams)
    {
        if (_currentLevel <= LogLevel.DEBUG)
        {
            Debug.Log(GetLogStr(LogLevel.DEBUG, msgParams));
        }
    }

    public static void LogInfo(params object[] msgParams)
    {
        if (_currentLevel <= LogLevel.INFO)
        {
            Debug.Log(GetLogStr(LogLevel.INFO, msgParams));
        }
    }

    public static void LogWarning(params object[] msgParams)
    {
        if (_currentLevel <= LogLevel.WARNING)
        {
            Debug.LogWarning(GetLogStr(LogLevel.WARNING, msgParams));
        }
    }

    public static void LogError(params object[] msgParams)
    {
        if (_currentLevel <= LogLevel.ERROR)
        {
            Debug.LogError(GetLogStr(LogLevel.ERROR, msgParams));
        }
    }

    public static string GetLogStr(LogLevel level, params object[] msgParams)
    {
        String str = String.Format("[{0}] [{1}]", DateTime.Now.ToString("HH:mm:ss.ffff"), level);
        foreach (var msg in msgParams)
        {
            str += String.Format(" {0} ", msg);
        }
        return str;
    }

    // 写log文件
    public static void LogToFile(string msg, bool append)
    {
        string fullPath = GetLogPath();
        string dir = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        using (
            FileStream fileStream = new FileStream(fullPath, append ? FileMode.Append : FileMode.Create,
                FileAccess.Write, FileShare.ReadWrite)) // 不会锁死, 允许其它程序打开
        {
            lock (fileStream)
            {
                StreamWriter writer = new StreamWriter(fileStream);
                writer.Write(msg);
                writer.Flush();
                writer.Close();
            }
        }
    }

    // 用于写日志的可写目录
    public static string GetLogPath()
    {
        string logPath;
        if (Application.isEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer)
            logPath = "Logs/";
        else
            logPath = Path.Combine(Application.persistentDataPath, "Logs/");
        string logName;
        if (!Config.IsServer)
        {
            logName = string.Format("log_{0}.log", Config.PlayerId);
        }
        else
        {
            logName = "log_server.log";
        }
        return logPath + logName;
    }
}


