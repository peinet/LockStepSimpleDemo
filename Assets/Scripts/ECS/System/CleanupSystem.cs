using UnityEngine;
using System.Collections;
using Entitas;

public class CleanupSystem : ICleanupSystem
{
    private GameContext _context;
    private IGroup<GameEntity> _playerIdGroup;

    public CleanupSystem(Contexts contexts)
    {
        _context = contexts.game;
        _playerIdGroup = _context.GetGroup(GameMatcher.PlayerId);
    }

    public void Cleanup()
    {
        foreach (GameEntity entity in _playerIdGroup.GetEntities())
        {
            entity.Destroy();
        }
    }

}
