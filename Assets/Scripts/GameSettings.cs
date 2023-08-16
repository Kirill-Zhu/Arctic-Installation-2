using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/GameSettings")]
public class GameSettings : ScriptableObject
{
    public float SecToSleepMode { get { return _secToSleepMode; } }
   
    public float FadeBetwenPagesSec { get { return _fadeBetwenPagesSec; } }
    public float IconsAnimationSpeed { get { return _iconsAnimationSpeed; } }

    [SerializeField] private float _secToSleepMode = 90;
    [Range(0.1f, 5)]
    [SerializeField] private float _fadeBetwenPagesSec = 0.3f;
    [Range(0.1f, 3)]
    [SerializeField] private float _iconsAnimationSpeed = 1f;
    
}
