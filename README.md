# LockStepSimpleDemo
一个简单的帧同步Demo，使用的库有：ECS实体组件系统、ProtocolBuffer、KCP可靠UDP库、Fix64定点数库，支持多客户端和服务器通信。

Assets\StreamingAssets\config.txt文件字段说明
ServerAddress远程服务器ip
IsServer是否是服务器,
PlayerId玩家id

以2个客户端连接一个服务器为例：
客户端1的配置为{"ServerAddress":"10.10.11.64", "IsServer":false, "PlayerId":1}
客户端1的配置为{"ServerAddress":"10.10.11.64", "IsServer":false, "PlayerId":2}
服务器的配置为{"IsServer":true}
