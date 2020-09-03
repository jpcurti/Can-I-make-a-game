using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    GameObject arrow;
    Rigidbody arrowBody;
    private float lifeTImer = 2f;
    private float timer;
    private bool hitSomething = false;
    // Start is called before the first frame update
    void Start()
    {
        arrow = GetComponent<GameObject>();

        arrowBody =GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //TryGetComponent<Rigidbody>(out Rigidbody arrowBody);
        /*if(!hitSomething)
        {
            transform.rotation = Quaternion.LookRotation(arrowBody.velocity);
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Arrow" && collision.collider.tag != "Bow") // avoid that it collides with arrows and bows
        {
            hitSomething = true;
            StickArrow(collision);
        }
       
        
    }
    private void StickArrow(Collision col) 
    {
       
        // Make the arrow a child of the thing it's stuck to
        transform.parent = col.transform;
        //Destroy the arrow's rigidbody
      
        arrowBody.constraints = RigidbodyConstraints.FreezeAll;
        Destroy(arrowBody);
      
    }
}
