using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public LayerMask groundLayer;
    public float gravity = -9.8f;
    private float fallingSpeed;
    private XRRig rig;
    public float speed = 1.0f;
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;
    public float adittionalHeigth = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

    }
    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y,0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0 , inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);

        //gravity implementation
        if (CheckIfGrounded())
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);

    }
    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + adittionalHeigth;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height /2 + character.skinWidth, capsuleCenter.z);
    }
    bool CheckIfGrounded()
    {

        //check if the palyer is on ground
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;

        Boolean hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitinfo,
            groundLayer);
        return hasHit;
    }
}
