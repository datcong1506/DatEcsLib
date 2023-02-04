using Unity.Entities;

namespace DatECSLib.StateMachine
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial class StateMachineSystemGroup : ComponentSystemGroup
    {

    }
}