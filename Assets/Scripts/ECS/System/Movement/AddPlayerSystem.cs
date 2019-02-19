using UnityEngine;
using System.Collections;
using Entitas;
using System.Collections.Generic;

public class AddPlayerSystem : ReactiveSystem<GameEntity>
{
    private GameContext _context;
    private GameObject _playerContainer;
    private Transform _playerContainerT; 

    public AddPlayerSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
        _playerContainer = GameObject.Find("PlayerContainer");
        _playerContainerT = _playerContainer.GetComponent<Transform>();
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity entity in entities)
        {
            int playerId = entity.playerId.value;
            string name = "player" + playerId;
            GameObject player = GameObject.Find("PlayerContainer/"+ name);
            if(player == null)
            {
                player = new GameObject(name);
                SpriteRenderer spriteRenderer = player.AddComponent<SpriteRenderer>();
                Texture2D texture = Resources.Load<Texture2D>("Textures/res/ue/icon/npc/" + playerId);
                spriteRenderer.sprite = Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                spriteRenderer.sortingOrder = 1;
                Transform playerT = player.GetComponent<Transform>();
                playerT.SetParent(_playerContainerT, false);
                entity.AddGameObject(player);
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPlayerId && entity.hasMove;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector<GameEntity>(GameMatcher.AllOf(GameMatcher.PlayerId, GameMatcher.Move));
    }
}
