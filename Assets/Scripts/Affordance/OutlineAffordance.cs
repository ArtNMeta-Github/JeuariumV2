using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineAffordance : Outline
{
    public XRBaseInteractable interactable;

    private bool isGrabbed = false;

    public override void Start()
    {
        base.Start();
        interactable ??= GetComponent<XRBaseInteractable>();

        interactable.firstHoverEntered.AddListener(x => TryOnOutline());
        interactable.lastHoverExited.AddListener(x => enabled = false);

        interactable.selectEntered.AddListener(x => SelectEntered());
        interactable.selectExited.AddListener(x => isGrabbed = false);

        enabled = false;
    }

    private void SelectEntered()
    {
        isGrabbed = true;
        enabled = false;
    }
    private void TryOnOutline()
    {
        if (isGrabbed)
            return;

        enabled = true;
    }

}
