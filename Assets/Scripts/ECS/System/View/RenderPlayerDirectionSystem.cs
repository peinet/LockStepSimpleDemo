using System.Collections.Generic;
using Entitas;
using UnityEngine;
using FixMath;

public class RenderPlayerDirectionSystem : ReactiveSystem<GameEntity>
{
    private GameContext _context;
    private IGroup<GameEntity> _listeners;

    public RenderPlayerDirectionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity entity in entities)
        {
            GameObject go = entity.gameObject.value;
            Fix64 direction = entity.direction.value;
            Transform transform = go.GetComponent<Transform>();
            transform.rotation = Quaternion.identity;
            transform.Rotate(0,0, (float)direction);
            //Log4U.LogDebug("RenderPlayerDirectionSystem:Execute direction=" + (float)direction);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDirection && entity.hasGameObject;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        // 需要收集Pause添加和移除事件
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Direction));
    }
}

