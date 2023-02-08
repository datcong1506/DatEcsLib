using DatMonoLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefManager : Singleton<RefManager>
{
    [SerializeField] private Joystick joystick;
    public Vector2 MoveDirec => joystick.Direction;


    protected override void OnCreateSingleton()
    {
        
    }
}
