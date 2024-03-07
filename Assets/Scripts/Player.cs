using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Player Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC") && other.TryGetComponent(out NPCBase npc))
        {
            npc.GreetingPlayer(transform);
        }
    }
}
