using UnityEngine;
using System.Collections;
using Entitas;
using FixMath;
using System.Collections.Generic;

public class CalculateDirectionSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    private GameContext _context;
    private FixVec2 _arrowCenterPos;
    private IGroup<GameEntity> _steerPosGroup;
    private IGroup<GameEntity>  _moveGroup; // 这个组中的实体都是持久化的

    public CalculateDirectionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
        GameObject arrowGo = GameObject.Find("ArrowImg");
        _arrowCenterPos = (FixVec2)(arrowGo.GetComponent<RectTransform>().position);
        _steerPosGroup = _context.GetGroup(GameMatcher.AllOf(GameMatcher.PlayerId, GameMatcher.SteerPosition));
        _moveGroup = _context.GetGroup(GameMatcher.AllOf(GameMatcher.PlayerId, GameMatcher.Move));
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity entity in entities)
        {
            int playerId = entity.playerId.value;
            Vector2 steerPos = entity.steerPosition.value;
            GameEntity findPosEntity = null;
            foreach (GameEntity posEntity in _moveGroup.GetEntities())
            {
                if (posEntity.playerId.value == playerId)
                {
                    findPosEntity = posEntity;
                    break;
                }
            }
            if (findPosEntity == null)
            {
                findPosEntity = _context.CreateEntity();
                findPosEntity.ReplacePlayerId(playerId);
            }
            if (steerPos == Vector2.zero)
            {
                // 停止移动
                findPosEntity.ReplaceMove(false);
            }
            else
            {
                findPosEntity.ReplaceMove(true);
                Fix64 steerDir;
                FixVec2 dirVec;
                FixVec2 fixSteerPos = (FixVec2)steerPos;
                dirVec = fixSteerPos - _arrowCenterPos;
                dirVec = dirVec.Normalize();
                Fix64 angle = Fix64.Atan2(dirVec.Y, dirVec.X);
                steerDir = angle * Fix64.Rad2Deg - (Fix64)90;
                findPosEntity.ReplaceDirection(steerDir);
                findPosEntity.ReplaceDirectionVec2(dirVec);
                Log4U.LogDebug("CalculateDirectionSystem:Execute playerId=" + playerId + " steerDir=" + steerDir);
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPlayerId && entity.hasSteerPosition;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector<GameEntity>(GameMatcher.AllOf(GameMatcher.PlayerId, GameMatcher.SteerPosition));
    }

    public void Cleanup()
    {
        foreach (GameEntity entity in _steerPosGroup.GetEntities())
        {
            entity.Destroy();
        }
    }
}

