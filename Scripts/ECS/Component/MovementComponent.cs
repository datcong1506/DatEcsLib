using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
namespace DatECSLib.StateMachine.Example
{
    [System.Serializable]
    public struct MovementComponent : IComponentData
    {
        public float Speed;
    }

}
