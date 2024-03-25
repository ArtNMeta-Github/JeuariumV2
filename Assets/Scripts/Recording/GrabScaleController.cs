using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class GrabScaleController : MonoBehaviour
{
    Vector3 sourScale;
    [SerializeField] Vector3 destScale;
    bool isActived = false;

    public float timer = 0f;
    [SerializeField] float speed;

    private void Start()
    {
        sourScale = transform.localScale;

        var grabInteractor = GetComponent<XRGrabInteractable>();

        grabInteractor.activated.AddListener(OnActive);
        grabInteractor.deactivated.AddListener(OnDeactive);
    }

    private void Update()
    {
        if (isActived)
            timer += speed * Time.deltaTime;

        transform.localScale = Vector3.Lerp(sourScale, destScale, timer);
    }
    void OnActive(ActivateEventArgs args)
    {
        isActived = true;
        //GetComponent<XRGeneralGrabTransformer>().enabled = false;
    }

    void OnDeactive(DeactivateEventArgs args)
    {
        isActived = false;
        //GetComponent<XRGeneralGrabTransformer>().enabled = true;
    }
}
