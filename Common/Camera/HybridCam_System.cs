using Unity.Entities;
using Unity.Transforms;

[RequireMatchingQueriesForUpdate]
public partial class HybridCam_System : SystemBase
{

    private UnityEngine.Transform mainCamTF;
    protected override void OnCreate()
    {
        mainCamTF = CameraManager.Ins.MainCamTF;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    protected override void OnUpdate()
    {
        

        Entities
            .ForEach((ref TransformAspect tfAs,in MainCam_Component mainCamC_Component) =>
            {
                //sync 

                var mainCamTf = mainCamTF;

                mainCamTf.position = tfAs.WorldPosition;
                mainCamTf.rotation = tfAs.WorldRotation;



            }).WithoutBurst().Run();
    }
}