using UnityEngine;
using System.Collections;
using Entitas;

public class ViewSystem : Feature
{
    public ViewSystem(Contexts contexts):base("View System")
    {
        Add(new RenderSteerDirectionSystem(contexts)); 
        Add(new RenderPlayerDirectionSystem(contexts));
        Add(new RenderPlayerPositionSystem(contexts));
    }

}
