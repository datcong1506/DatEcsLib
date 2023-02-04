using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public static class BakerHelper
{

    public static void AddAndDisable<T>(this IBaker baker) where T : unmanaged, IComponentData, IEnableableComponent
    {
        baker.AddComponent<T>();
        baker.SetComponentEnabled<T>(baker.GetEntityWithoutDependency(), false);
    }

}
