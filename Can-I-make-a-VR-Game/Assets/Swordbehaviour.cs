using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Swordbehaviour : XRGrabInteractable
{
    public Transform objectToVibrate;


    private Vector3 initialPosition;

    public float amplitude; // the amount it moves
    public float frequency; // the period of the earthquake
    public Material normalMaterial;
    public Material hoveredMaterial;
    public MeshRenderer swordObjectRenderer;

    void Start()
    {
        initialPosition = objectToVibrate.position; // store this to avoid floating point error drift
}



    void Update()
    {
        initialPosition = objectToVibrate.position;
        if (isHovered && !isSelected)
        { 
        objectToVibrate.position = initialPosition + (objectToVibrate.forward * Mathf.Sin(frequency * Time.time) * amplitude) + objectToVibrate.right * Mathf.Sin(frequency/2 * Time.time) * amplitude + objectToVibrate.up * Mathf.Sin(frequency /3  * Time.time) * amplitude;
        }
        if (isSelected && swordObjectRenderer.material != normalMaterial)
        {
            swordObjectRenderer.material = normalMaterial;
        }
    }

    protected override void OnHoverEnter(XRBaseInteractor interactor)
    {
        base.OnHoverEnter(interactor);
        swordObjectRenderer.material = hoveredMaterial;
        



    }
    protected override void OnHoverExit(XRBaseInteractor interactor)
    {
        base.OnHoverExit(interactor);
        swordObjectRenderer.material = normalMaterial;

    }
     




}
