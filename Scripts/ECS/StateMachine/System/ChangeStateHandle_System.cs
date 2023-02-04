using Unity.Entities;
using Unity.Burst;
using UnityEngine;
using Unity.Collections;
using Unity.Transforms;

namespace DatECSLib.StateMachine
{
    // 
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(StateMachineSystemGroup))]
    [UpdateAfter(typeof(EnterStateHandle_System))]
    [BurstCompile]
    public partial struct ChangeStateHandle_System : ISystem
    {


        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
/*            state.RequireForUpdate<ChangeStateRequest_Component>();
*/        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var beginSimalateGR = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecbBegin = beginSimalateGR.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            var endSimulateGR = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var ecbEnd=endSimulateGR.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

            var changeStateJob = new ChangeStateJob
            {
                ECBFirst = ecbBegin,
                ECBSecond = ecbEnd,
            };
            changeStateJob.ScheduleParallel();
        }
        [BurstCompile]
        private partial struct ChangeStateJob : IJobEntity
        {
            public EntityCommandBuffer.ParallelWriter ECBFirst; // begin
            public EntityCommandBuffer.ParallelWriter ECBSecond; // end

            public void Execute(Entity enity, [EntityIndexInQuery]int index,RefRW<State_Component> state, RefRO<ChangeStateRequest_Component> newState)
            {
                ECBFirst.SetComponentEnabled<ChangeStateRequest_Component>(index, enity, false);

                if (newState.ValueRO.NewState == state.ValueRO.Value)
                {
                    return;
                }

                if (state.ValueRO.Value.TypeIndex == TypeIndex.Null)
                {
                    // This should be run one time in initalize state
                    ECBFirst.SetComponentEnabled<EnterState_Component>(index, enity, true);
                    ECBFirst.SetComponentEnabled(index, enity, newState.ValueRO.NewState, true);
                    ECBSecond.SetComponentEnabled<EnterState_Component>(index, enity, false);
                }
                else
                {
                    ECBFirst.SetComponentEnabled<ExitState_Component>(index, enity, true);
                    ECBSecond.SetComponentEnabled<ExitState_Component>(index, enity, false);
                    ECBSecond.SetComponentEnabled<EnterState_Component>(index, enity, true);
                    ECBSecond.SetComponentEnabled(index, enity, state.ValueRO.Value, false);
                    ECBSecond.SetComponentEnabled(index, enity, newState.ValueRO.NewState, true);
                }

                state.ValueRW.Value = newState.ValueRO.NewState;
            }
        }
    }
}
