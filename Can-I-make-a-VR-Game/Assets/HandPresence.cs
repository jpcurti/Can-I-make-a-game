using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    private InputDevice targetDevice;
    private GameObject spawnedController;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find controller model. A default model for the controller will be automatically set.");
                spawnedController = Instantiate(controllerPrefabs.Find(controller => controller.name == "Default Controller"), transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        {
            Debug.Log("Primary Button pressed!");
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerButtonValue) && triggerButtonValue > 0.1)
        {
            Debug.Log("Trigger Button pressed with value " + triggerButtonValue);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("Analog Button pressed with value x= " + primary2DAxisValue.x + " y=" + primary2DAxisValue.y);
        }

    }
}
