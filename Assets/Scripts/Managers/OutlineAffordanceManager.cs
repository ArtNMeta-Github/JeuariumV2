using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineAffordanceManager : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]
    public float width;
    public Color color;

    public static OutlineAffordanceManager instance;
    private void Awake()
    {
        instance = this;
    }
}
