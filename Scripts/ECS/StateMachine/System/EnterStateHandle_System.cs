using Unity.Entities;
using Unity.Burst;
using UnityEngine;

namespace DatECSLib.StateMachine
{
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(StateMachineSystemGroup))]
    [BurstCompile]
    public partial struct EnterStateHandle_System : ISystem
    {

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
/*            state.RequireForUpdate<EnterState_Component>();
*/            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var endSimulationBuffer = SystemAPI.GetSingletonRW<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = endSimulationBuffer.ValueRO.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

            var enterStateJob = new EnterStateJob
            {
                ECB = ecb,
            };
            enterStateJob.ScheduleParallel();
        }

        [BurstCompile]
        [WithAll(typeof(EnterState_Component))]
        private partial struct EnterStateJob : IJobEntity
        {
            public EntityCommandBuffer.ParallelWriter ECB;
            public void Execute(Entity entity, [EntityIndexInQuery] int index, RefRO<State_Component> state)
            {
                ECB.SetComponentEnabled<EnterState_Component>(index, entity, false);
            }
        }
    }
}