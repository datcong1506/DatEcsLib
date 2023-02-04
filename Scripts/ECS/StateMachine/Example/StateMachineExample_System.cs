using Unity.Entities;
using Unity.Burst;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;

namespace DatECSLib.StateMachine.Example
{


    [BurstCompile]
    public partial struct StateMachineExample_System : ISystem
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
            var spaceInput = Input.GetKey(KeyCode.Space);



            if (spaceInput)
            {
                var player= SystemAPI.GetSingletonEntity<MovementComponent>();
                var isEnable = SystemAPI.IsComponentEnabled<MoveState>(player);
                if(isEnable)
                {
                    SystemAPI.SetComponentEnabled<ChangeStateRequest_Component>(player, true);
                    SystemAPI.SetComponent(player, new ChangeStateRequest_Component
                    {
                        NewState = new ComponentType(typeof(IdleState)),
                    });
                }
                else
                {
                    SystemAPI.SetComponentEnabled<ChangeStateRequest_Component>(player, true);
                    SystemAPI.SetComponent(player, new ChangeStateRequest_Component
                    {
                        NewState=new ComponentType(typeof(MoveState)),
                    });
                }
            }
        }
    }


    [BurstCompile]
    public partial struct StateMachineExample_Move_System : ISystem
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
            /*var deltaTime = SystemAPI.Time.DeltaTime;
            var t = new Test
            {
                DeltaTime= deltaTime,
            };
            t.ScheduleParallel();*/


            var t1 = new Test1();
            t1.Schedule();

            var t2 = new Test2();
            t2.Schedule();
        }

        [WithAll(typeof(MoveState))]
        [BurstCompile]
        private partial struct Test : IJobEntity
        {
            public float DeltaTime;
            public void Execute(TransformAspect transformAspect)
            { int up = 1;
                if (transformAspect.WorldPosition.y > 4)
                {
                    up = 0;
                }
                transformAspect.WorldPosition += math.up() * DeltaTime * up;
            }
        }

        [WithAll(typeof(MoveState),typeof(EnterState_Component))]
        [BurstCompile]
        private partial struct Test1 : IJobEntity
        {
            public void Execute()
            {
                Debug.Log("Enter MoveState");
            }
        }


        [WithAll(typeof(MoveState), typeof(ExitState_Component))]
        [BurstCompile]
        private partial struct Test2 : IJobEntity
        {
            public void Execute()
            {
                Debug.Log("Exit MoveState");
            }
        }
    }

    [BurstCompile]
    public partial struct StateMachineExample_Idle_System : ISystem
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

        }
    }




    public struct MoveState:IComponentData,IEnableableComponent
    {

    }

    public struct IdleState:IComponentData, IEnableableComponent
    {

    }
}