using DatMonoLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : Singleton<CameraManager>
{

    [SerializeField] private Camera mainCam;
    public Camera MainCa => mainCam;

    [SerializeField] private Transform mainCamTF;
    public Transform MainCamTF=> mainCamTF;


    protected override void OnCreateSingleton()
    {
        
    }
}
