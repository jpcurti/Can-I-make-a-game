using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class BowShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;
    public Animator bowAnimator;
    public float speed = 40;
    public GameObject arrow;
    public Transform bowPivot; 
    float m_LastPressTime=0.5f;
    float m_PressDelay = 0.5f;

   
       
    void Start()
    {
        tryInitialize();
    }
    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            tryInitialize();
        }
        else {
            updateShootingAnimation();
        }
    }
    void tryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            targetDevice = devices[0];

        }
    }
    void updateShootingAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) )
        {
            bowAnimator.SetFloat("Arming", triggerValue);
            
            if (triggerValue > 0.95  )
            {   
                
                if (targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool triggerPressed)) {

                    if (triggerPressed)
                    {
                        FireArrow();
                       
                    }
                }
               
                    
            }
        }
    }
       

     void FireArrow()
    {

        // Quaternion arrowPosition = Quaternion.Euler(bowPivot.position.x, bowPivot.position.y , bowPivot.position.z);
        if (m_LastPressTime + m_PressDelay > Time.unscaledTime)
        {
            return;
        }
        else
        {
            m_LastPressTime = Time.unscaledTime;
            GameObject spawnedarrow = Instantiate(arrow, bowPivot.position, bowPivot.rotation);
            spawnedarrow.GetComponent<Rigidbody>().velocity = speed * bowPivot.forward;
            Destroy(spawnedarrow, 5);
        }    
       
    }


}
