using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace DatECSLib.StateMachine
{
    public static class StateMachineHelper
    {
        public static void SetupStateMachine(this IBaker baker)
        {
            baker.AddAndDisable<ExitState_Component>();
            baker.AddAndDisable<EnterState_Component>();
/*            baker.AddAndDisable<ChangeStateRequest_Component>();
*/            
            baker.AddComponent<State_Component>();
        }

        public static void ChangeState(this EntityCommandBuffer.ParallelWriter ecb,int index,Entity entity,ComponentType newState)
        {
            ecb.SetComponentEnabled<ChangeStateRequest_Component>(index, entity, true);
            ecb.SetComponent(index, entity, new ChangeStateRequest_Component
            {
                NewState=newState,
            });
        }
        public static void ChangeState(this EntityCommandBuffer ecb, Entity entity, ComponentType newState)
        {
            ecb.SetComponentEnabled<ChangeStateRequest_Component>( entity, true);
            ecb.SetComponent( entity, new ChangeStateRequest_Component
            {
                NewState = newState,
            });
        }
        public static void ChangeState(this EntityManager manager, Entity entity, ComponentType newState)
        {
            manager.SetComponentEnabled<ChangeStateRequest_Component>(entity, true);
            manager.SetComponentData(entity, new ChangeStateRequest_Component
            {
                NewState = newState,
            });
        }
    }
}

