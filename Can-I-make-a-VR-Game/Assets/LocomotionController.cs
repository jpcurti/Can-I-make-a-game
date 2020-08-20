using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class LocomotionController : MonoBehaviour
{
    public XRController rightTeleportRay;
    private XRRayInteractor rightRayInteractor;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        if (rightTeleportRay);
        {
            rightRayInteractor = rightTeleportRay.gameObject.GetComponent<XRRayInteractor>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (rightTeleportRay)
        {
            rightRayInteractor.allowSelect = checkIfActivated(rightTeleportRay);
            rightTeleportRay.gameObject.SetActive(checkIfActivated(rightTeleportRay));
        }
        
    }
    public bool checkIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }

}
