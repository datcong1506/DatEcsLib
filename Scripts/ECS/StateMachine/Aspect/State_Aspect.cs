using Unity.Entities;
using Unity.Transforms;

readonly partial struct State_Aspect : IAspect
{
    public readonly Entity Self;

    readonly RefRO<State_Component> State;
   
    public void ChangeState(EntityManager manger,ComponentType newState)
    {
       

        var currentState = State.ValueRO.Value;
        if (currentState == newState)
        {
            return;
        }
        //init
        if (currentState.TypeIndex == TypeIndex.Null)
        {
            manger.SetComponentEnabled(Self, newState, true);
            manger.SetComponentEnabled<EnterState_Component>(Self, true);
        }
        else
        {
            manger.SetComponentEnabled(Self, currentState, false);
            manger.SetComponentEnabled(Self, newState, true);
            manger.SetComponentEnabled<ExitState_Component>(Self, true);
            manger.SetComponentEnabled<EnterState_Component>(Self, true);
        }
    }
}
