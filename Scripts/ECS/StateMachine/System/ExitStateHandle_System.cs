/*using Unity.Entities;
using Unity.Burst;
using DatECSLib.StateMachine;

[UpdateInGroup(typeof(StateMachineSystemGroup))]
[UpdateAfter(typeof(ChangeStateHandle_System))]
[BurstCompile]
public partial struct ExitStateHandle_System : ISystem
{
    
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
       
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var beginSimulationBuffer = SystemAPI.GetSingletonRW<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = beginSimulationBuffer.ValueRW.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

        var exitStateJob = new ExitStateJob
        {
            ECB = ecb,
        };
        exitStateJob.ScheduleParallel();

    }

    [BurstCompile]
    [WithAll(typeof(ExitState_Component))]
    private partial struct ExitStateJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
        public void Execute(Entity entity, [EntityIndexInQuery] int index, RefRO<State_Component> state)
        {
            // Disable ExitState Component
            ECB.SetComponentEnabled<ExitState_Component>(index, entity, false);
            // Enable Enter State Component
            ECB.SetComponentEnabled<EnterState_Component>(index, entity, true);
        }
    }
}*/