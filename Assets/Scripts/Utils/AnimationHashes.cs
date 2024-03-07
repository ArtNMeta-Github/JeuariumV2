using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationHashes
{
    public static readonly int hashGreeting = Animator.StringToHash("Greeting");
    public static readonly int hashLeft = Animator.StringToHash("Left");
    public static readonly int hashRight = Animator.StringToHash("Right"); 
    public static readonly int hashIdle = Animator.StringToHash("Idle"); 
    public static readonly int hashWalking = Animator.StringToHash("Walking"); 
}
