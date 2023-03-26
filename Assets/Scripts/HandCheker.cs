using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandCheker : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharecter;
    public List<GameObject> controllerprefabs;
    public GameObject handModelpref;
    public delegate void DeviceIsValidEvent();
    public event DeviceIsValidEvent DeviceIsValid;

    [HideInInspector]
    public InputDevice targetDevice;
    private GameObject spawnedController;

    public GameObject spawnedHandModel;
    private Animator handanimator;
    void Start()
    {
        TryInitialize();
    }
    public void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharecter, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerprefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                spawnedController = Instantiate(controllerprefabs[0], transform);
            }
            
            spawnedHandModel = Instantiate(handModelpref, transform);
            handanimator = spawnedHandModel.GetComponent<Animator>();
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
            }
            if (DeviceIsValid != null)
                DeviceIsValid();
        }
    }
    public void HandAnimUpdate(Animator handanimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigerValue))
            handanimator.SetFloat("Trigger", trigerValue);
        else
            handanimator.SetFloat("Trigger", 0);

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            handanimator.SetFloat("Grip", gripValue);
        else
            handanimator.SetFloat("Grip", 0);
    }

    void Update()
    {
        /*if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            Debug.Log("Pressing button yep");

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigerValue) && trigerValue > 0.1f)
            Debug.Log("Triger pressed" + trigerValue);

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2dAxisValue) && primary2dAxisValue != Vector2.zero)
            Debug.Log("Touchpad" + primary2dAxisValue); */

        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            HandAnimUpdate(handanimator);
        }
    }
}
