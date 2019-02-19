using FixMath;
using System;

public class Config
{
    // 从config.txt中读取
    public static bool IsServer = true;
    public static string ServerAddress = "10.10.11.81";
    public static int PlayerId; // 玩家id，也是本客户端KCP会话id

    public const int SYN_RATE_SERVER = 2;  // 服务器每隔2帧同步一次，也就是1/30
    public const int QUICK_PLAY_GAP = 4;  // 与服务器相差多少帧就启动快进
    public const int QUICK_PLAY_SPEED_MAX = 10;  // 快进的最大速度，每次播10帧
    public const float FRAME_STEP = 0.0167f;    // 1/60物理帧频
    public static readonly int Port = 8001;
    public const string PrecisionFormat = "f8"; // 保留8位小数
    public static readonly Fix64 Speed = (Fix64)0.05f;
    public static readonly float UI_Speed = 0.05f;
}

[Serializable]
public class ConfigJson
{
    public bool IsServer;
    public string ServerAddress;
    public int PlayerId;
}
