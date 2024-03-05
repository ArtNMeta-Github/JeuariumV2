using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    public Animator animator;

    public void Start()
    {
        animator.SetTrigger("Trigger");
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!other.CompareTag("Player"))
    //        return;

    //    animator.SetTrigger("Trigger");
    //}
}
