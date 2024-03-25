using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagnifierLaser : MonoBehaviour
{
    [SerializeField] Transform lineStart;
    [SerializeField] Transform target;

    LineRenderer lineRenderer;
    bool enableLine = false;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.activated.AddListener(x => EnableLine());
        grabInteractable.deactivated.AddListener(x => DisableLine());
        grabInteractable.selectExited.AddListener(x => DisableLine());
    }
    private void Update()
    {
        if (!enableLine) return;

        bool isHit = Physics.Raycast(lineStart.position, transform.up, out RaycastHit hit);

        lineRenderer.enabled = isHit;

        if (!isHit)
            return;

        lineRenderer.SetPosition(0, lineStart.position);
        lineRenderer.SetPosition(1, hit.point);
        lineRenderer.SetPosition(2, hit.point);

        if(hit.transform.CompareTag("LaserReceiver"))
        {            
            lineRenderer.SetPosition(1, hit.transform.position);
            lineRenderer.SetPosition(2, target.position);
        }
    }

    void EnableLine()
    {
        enableLine = true;
        lineRenderer.enabled = true;
    }

    void DisableLine()
    {
        enableLine = false;
        lineRenderer.enabled = false;
    }
}
