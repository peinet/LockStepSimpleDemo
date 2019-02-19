using Entitas;

public class MovementSystem:Feature
{
    public MovementSystem(Contexts contexts):base("Movement System")
    {
        Add(new CalculateDirectionSystem(contexts));
        Add(new AddPlayerSystem(contexts));
        Add(new MoveSystem(contexts));
    }


}