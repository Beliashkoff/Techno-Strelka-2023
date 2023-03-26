using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class HandCheker : MonoBehaviour
{
    public GameObject handModelpref;
    public InputActionReference gripRef;
    public InputActionReference triggerRef;
    [HideInInspector]
    public GameObject spawnedHandModel;
    private Animator handanimator;
    void Start()
    {
        spawnedHandModel = Instantiate(handModelpref, transform);
        handanimator = spawnedHandModel.GetComponent<Animator>();
    }
    public void HandAnimUpdate(Animator handanimator)
    {
        float triggerValue = triggerRef.action.ReadValue<float>();
        float gripValue = gripRef.action.ReadValue<float>();
        handanimator.SetFloat("Trigger", triggerValue);
        handanimator.SetFloat("Grip", gripValue);
    }

    void Update()
    {
        HandAnimUpdate(handanimator);
    }
}
