using UnityEngine;
using Entitas;
using Protocol;
using Google.Protobuf;
using System;

public class InitializeSystem: IInitializeSystem
{
    private GameContext _context;

    public void Initialize()
    {
        _context = Contexts.sharedInstance.game;
    }


}