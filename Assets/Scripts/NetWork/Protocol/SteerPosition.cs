// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: SteerPosition.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Protocol {

  /// <summary>Holder for reflection information generated from SteerPosition.proto</summary>
  public static partial class SteerPositionReflection {

    #region Descriptor
    /// <summary>File descriptor for SteerPosition.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SteerPositionReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNTdGVlclBvc2l0aW9uLnByb3RvEghQcm90b2NvbCIoChBTdGVlclBvc2l0",
            "aW9uUmVxEgkKAXgYASABKAISCQoBeRgCIAEoAiJeChBTdGVlclBvc2l0aW9u",
            "UnNwEhAKCGtleUZyYW1lGAEgASgFEhAKCG1zZ0luZGV4GAIgASgFEhAKCHBs",
            "YXllcklkGAMgASgFEgkKAXgYBCABKAISCQoBeRgFIAEoAmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Protocol.SteerPositionReq), global::Protocol.SteerPositionReq.Parser, new[]{ "X", "Y" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Protocol.SteerPositionRsp), global::Protocol.SteerPositionRsp.Parser, new[]{ "KeyFrame", "MsgIndex", "PlayerId", "X", "Y" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class SteerPositionReq : pb::IMessage<SteerPositionReq> {
    private static readonly pb::MessageParser<SteerPositionReq> _parser = new pb::MessageParser<SteerPositionReq>(() => new SteerPositionReq());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SteerPositionReq> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Protocol.SteerPositionReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SteerPositionReq() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SteerPositionReq(SteerPositionReq other) : this() {
      x_ = other.x_;
      y_ = other.y_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SteerPositionReq Clone() {
      return new SteerPositionReq(this);
    }

    /// <summary>Field number for the "x" field.</summary>
    public const int XFieldNumber = 1;
    private float x_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float X {
      get { return x_; }
      set {
        x_ = value;
      }
    }

    /// <summary>Field number for the "y" field.</summary>
    public const int YFieldNumber = 2;
    private float y_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Y {
      get { return y_; }
      set {
        y_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SteerPositionReq);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SteerPositionReq other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (X != other.X) return false;
      if (Y != other.Y) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (X != 0F) hash ^= X.GetHashCode();
      if (Y != 0F) hash ^= Y.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (X != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(Y);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (X != 0F) {
        size += 1 + 4;
      }
      if (Y != 0F) {
        size += 1 + 4;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SteerPositionReq other) {
      if (other == null) {
        return;
      }
      if (other.X != 0F) {
        X = other.X;
      }
      if (other.Y != 0F) {
        Y = other.Y;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 13: {
            X = input.ReadFloat();
            break;
          }
          case 21: {
            Y = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  public sealed partial class SteerPositionRsp : pb::IMessage<SteerPositionRsp> {
    private static readonly pb::MessageParser<SteerPositionRsp> _parser = new pb::MessageParser<SteerPositionRsp>(() => new SteerPositionRsp());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SteerPositionRsp> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Protocol.SteerPositionReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SteerPositionRsp() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SteerPositionRsp(SteerPositionRsp other) : this() {
      keyFrame_ = other.keyFrame_;
      msgIndex_ = other.msgIndex_;
      playerId_ = other.playerId_;
      x_ = other.x_;
      y_ = other.y_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SteerPositionRsp Clone() {
      return new SteerPositionRsp(this);
    }

    /// <summary>Field number for the "keyFrame" field.</summary>
    public const int KeyFrameFieldNumber = 1;
    private int keyFrame_;
    /// <summary>
    /// �ĸ��ؼ�֡�ϴ�������Ϣ���ùؼ�֡�ǵڼ�֡
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int KeyFrame {
      get { return keyFrame_; }
      set {
        keyFrame_ = value;
      }
    }

    /// <summary>Field number for the "msgIndex" field.</summary>
    public const int MsgIndexFieldNumber = 2;
    private int msgIndex_;
    /// <summary>
    /// ����Ϣ����ţ���0��ʼ
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MsgIndex {
      get { return msgIndex_; }
      set {
        msgIndex_ = value;
      }
    }

    /// <summary>Field number for the "playerId" field.</summary>
    public const int PlayerIdFieldNumber = 3;
    private int playerId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int PlayerId {
      get { return playerId_; }
      set {
        playerId_ = value;
      }
    }

    /// <summary>Field number for the "x" field.</summary>
    public const int XFieldNumber = 4;
    private float x_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float X {
      get { return x_; }
      set {
        x_ = value;
      }
    }

    /// <summary>Field number for the "y" field.</summary>
    public const int YFieldNumber = 5;
    private float y_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Y {
      get { return y_; }
      set {
        y_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SteerPositionRsp);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SteerPositionRsp other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (KeyFrame != other.KeyFrame) return false;
      if (MsgIndex != other.MsgIndex) return false;
      if (PlayerId != other.PlayerId) return false;
      if (X != other.X) return false;
      if (Y != other.Y) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (KeyFrame != 0) hash ^= KeyFrame.GetHashCode();
      if (MsgIndex != 0) hash ^= MsgIndex.GetHashCode();
      if (PlayerId != 0) hash ^= PlayerId.GetHashCode();
      if (X != 0F) hash ^= X.GetHashCode();
      if (Y != 0F) hash ^= Y.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (KeyFrame != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(KeyFrame);
      }
      if (MsgIndex != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(MsgIndex);
      }
      if (PlayerId != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(PlayerId);
      }
      if (X != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(45);
        output.WriteFloat(Y);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (KeyFrame != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(KeyFrame);
      }
      if (MsgIndex != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MsgIndex);
      }
      if (PlayerId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(PlayerId);
      }
      if (X != 0F) {
        size += 1 + 4;
      }
      if (Y != 0F) {
        size += 1 + 4;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SteerPositionRsp other) {
      if (other == null) {
        return;
      }
      if (other.KeyFrame != 0) {
        KeyFrame = other.KeyFrame;
      }
      if (other.MsgIndex != 0) {
        MsgIndex = other.MsgIndex;
      }
      if (other.PlayerId != 0) {
        PlayerId = other.PlayerId;
      }
      if (other.X != 0F) {
        X = other.X;
      }
      if (other.Y != 0F) {
        Y = other.Y;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            KeyFrame = input.ReadInt32();
            break;
          }
          case 16: {
            MsgIndex = input.ReadInt32();
            break;
          }
          case 24: {
            PlayerId = input.ReadInt32();
            break;
          }
          case 37: {
            X = input.ReadFloat();
            break;
          }
          case 45: {
            Y = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code