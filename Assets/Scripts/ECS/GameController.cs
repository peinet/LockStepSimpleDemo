using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour
{
    private Systems _logicsystems;
    private Systems _renderSystems;

    private void Awake()
    {
    }

    private void Start()
    {
        var contexts = Contexts.sharedInstance;
        _logicsystems = CreateLogicSystems(contexts);
        _logicsystems.Initialize();

        _renderSystems = CreateRenderSystems(contexts);
        _renderSystems.Initialize();
    }

    public void Execute()
    {
        _logicsystems.Execute();
        _logicsystems.Cleanup();
    }

    private void Update()
    {
        _renderSystems.Execute();
        _renderSystems.Cleanup();
    }

    private void OnDestroy()
    {
        _logicsystems.TearDown();
        _logicsystems = null;
        _renderSystems.TearDown();
        _renderSystems = null;
    }

    Systems CreateLogicSystems(Contexts contexts)
    {
        return new Feature("Logic System")
            .Add(new InitializeSystem())
            .Add(new MovementSystem(contexts))
            .Add(new ViewSystem(contexts))
            //.Add(new CleanupSystem(contexts));
        ;
    }

    Systems CreateRenderSystems(Contexts contexts)
    {
        return new Feature("Render System")
            .Add(new ViewSystem(contexts))
        //.Add(new CleanupSystem(contexts));
        ;
    }

}
