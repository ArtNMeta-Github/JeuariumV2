using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Greeting : MonoBehaviour
{
    Animator animator;
    int hashGreeting = Animator.StringToHash("greeting");
    Coroutine corutine;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartGreetingCo(Transform player)
    {
        if(corutine == null)
            corutine = StartCoroutine(LookAtPlayer(player));
    }

    IEnumerator LookAtPlayer(Transform player)
    {
        Vector3 dir = player.position - transform.position;

        while (Vector3.Angle(transform.forward, dir) >= 5f)
        {
            dir = player.position - transform.position;
            dir.y = 0f; 

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
            yield return null;
        }

        animator.SetTrigger(hashGreeting);
        corutine = null;

        yield break;
    }
}
