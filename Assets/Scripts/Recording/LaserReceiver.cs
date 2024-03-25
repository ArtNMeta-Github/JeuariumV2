using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiver : MonoBehaviour
{
    [SerializeField] GameObject target;
    public void ReceiveLaser()
    {
        var line = GetComponent<LineRenderer>();
        
        target.SetActive(true);

        line.enabled = true;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, target.transform.position);
    }
}
