using DatECSLib.StateMachine;
using Unity.Entities;
using UnityEngine;

namespace DatECSLib.Authoring
{
    public class StateMachine_Authoring : MonoBehaviour
    {
        
    }

    public class StateMachine_AuthoringBaker : Baker<StateMachine_Authoring>
    {
        public override void Bake(StateMachine_Authoring authoring)
        {
            this.SetupStateMachine();
        }
    }
}
