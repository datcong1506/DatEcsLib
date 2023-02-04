using Unity.Entities;

public struct ChangeStateRequest_Component : IComponentData,IEnableableComponent
{
    public ComponentType NewState;
}