using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using FixMath;

// 左下控制区域，点击的位置
[Game]
public class SteerPositionComponent : IComponent
{
    public Vector2 value;
}

// 移动组件
[Game]
public class MoveComponent : IComponent
{
    public bool isMove;
}

[Game]
public class DirectionComponent : IComponent
{
    public Fix64 value;
}

[Game]
public class DirectionVec2Component : IComponent
{
    public FixVec2 value;
}

[Game]
public class PositionComponent : IComponent
{
    public FixVec2 value;
}

[Game]
public class GameObjectComponent : IComponent
{
    public GameObject value;
}

[Game]
public class PlayerIdComponent : IComponent
{
    public int value;
}

