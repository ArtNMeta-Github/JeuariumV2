using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllFillAmount : MonoBehaviour
{
    [SerializeField] private Image image;

    [SerializeField] private bool toggleFillControl;

    [SerializeField] float speed;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            toggleFillControl = !toggleFillControl;
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            toggleFillControl = false;
            image.fillAmount = 1f;
        }

        if (toggleFillControl)
            image.fillAmount -= speed * Time.deltaTime;
    }
}
