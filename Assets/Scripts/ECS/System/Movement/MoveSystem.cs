using UnityEngine;
using System.Collections;
using Entitas;
using FixMath;
using System.Collections.Generic;

public class MoveSystem : IExecuteSystem
{
    private GameContext _context;
    private IGroup<GameEntity> _moveGroup;

    public MoveSystem(Contexts contexts)
    {
        _context = contexts.game;
        _moveGroup = _context.GetGroup(GameMatcher.AllOf(GameMatcher.Move, GameMatcher.GameObject));
    }

    public void Execute()
    {
        foreach(GameEntity entity in _moveGroup.GetEntities())
        {
            if(!entity.move.isMove)
            {
                continue;
            }
            FixVec2 dirVec2 = entity.directionVec2.value;
            dirVec2 *= Config.Speed;
            FixVec2 oldPos;
            if(entity.hasPosition)
            {
                oldPos = entity.position.value;
            }
            else
            {
                oldPos = new FixVec2();
            }
            entity.ReplacePosition(oldPos + dirVec2);
            Log4U.LogDebug("MoveSystem:Execute newPositionFixVec2=", oldPos + dirVec2);
        }
    }

}

