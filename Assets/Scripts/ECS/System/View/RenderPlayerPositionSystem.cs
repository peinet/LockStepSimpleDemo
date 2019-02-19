using UnityEngine;
using System.Collections;
using Entitas;
using System.Collections.Generic;

public class RenderPlayerPositionSystem : ReactiveSystem<GameEntity>
{
    private GameContext _context;

    public RenderPlayerPositionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity entity in entities)
        {
            GameObject go = entity.gameObject.value;
            Transform transform = go.GetComponent<Transform>();
            transform.position = entity.position.value.ToVector2();

            // 客户端在追赶服务器帧的过程中，可能在某个物理帧中运行了多次 _gameController.Execute();导致位置跳跃了。
            // 这时需要渲染层平滑地追赶最新的位置。
            // 实际测试中发现，这么做移动抖动更厉害。。。
            //Vector2 currPos = transform.position;
            //Vector2 newPos = entity.position.value.ToVector2();
            //Vector2 nowPos = Vector2.Lerp(currPos, newPos, Vector2.Distance(currPos, newPos)/Config.UI_Speed);
            //transform.position = nowPos;

            //Log4U.LogDebug("RenderPlayerPositionSystem:Execute position=" + transform.position)
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasGameObject && entity.hasPosition;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector<GameEntity>(GameMatcher.Position);
    }
}
