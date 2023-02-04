using DatECSLib.StateMachine;
using DatECSLib.StateMachine.Example;
using Unity.Entities;
using UnityEngine;

namespace DatECSLib.Authoring
{
    public class StateMachineExample_Authoring : MonoBehaviour
    {
        
    }

    public class StateMachineExample_AuthoringBaker : Baker<StateMachineExample_Authoring>
    {
        public override void Bake(StateMachineExample_Authoring authoring)
        {
            this.SetupStateMachine();

            this.AddAndDisable<MoveState>();
            this.AddAndDisable<IdleState>();
            AddComponent<MovementComponent>();
        }
    }
}
