//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentsLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class GameComponentsLookup {

    public const int Direction = 0;
    public const int DirectionVec2 = 1;
    public const int GameObject = 2;
    public const int Move = 3;
    public const int PlayerId = 4;
    public const int Position = 5;
    public const int SteerPosition = 6;

    public const int TotalComponents = 7;

    public static readonly string[] componentNames = {
        "Direction",
        "DirectionVec2",
        "GameObject",
        "Move",
        "PlayerId",
        "Position",
        "SteerPosition"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(DirectionComponent),
        typeof(DirectionVec2Component),
        typeof(GameObjectComponent),
        typeof(MoveComponent),
        typeof(PlayerIdComponent),
        typeof(PositionComponent),
        typeof(SteerPositionComponent)
    };
}
