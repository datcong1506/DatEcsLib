using Unity.Entities;
using UnityEngine;

namespace DatECSLib.Authoring
{
    public class MainCam_Authoring : MonoBehaviour
    {

    }

    public class MainCam_AuthoringBaker : Baker<MainCam_Authoring>
    {
        public override void Bake(MainCam_Authoring authoring)
        {
            AddComponent<MainCam_Component>();
        }
    }
}
