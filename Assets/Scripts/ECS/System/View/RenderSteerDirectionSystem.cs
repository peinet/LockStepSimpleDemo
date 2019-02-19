using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RenderSteerDirectionSystem : ReactiveSystem<GameEntity>
{
    private GameContext _context;
    private RectTransform _arrowT;

    public RenderSteerDirectionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
        _arrowT = GameObject.Find("ArrowImg").GetComponent<RectTransform>();
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity entity in entities)
        {
            float dir = (float)(entity.direction.value);
            _arrowT.rotation = Quaternion.identity;
            _arrowT.Rotate(0, 0, dir);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDirection && entity.hasPlayerId && entity.playerId.value == Config.PlayerId;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        // 需要收集Pause添加和移除事件
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Direction, GameMatcher.PlayerId));
    }
}

