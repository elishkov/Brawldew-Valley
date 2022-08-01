using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePhysicalCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        print("OnCollisionEnter");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            print("hi!");
            //Physics.IgnoreCollision(theobjectToIgnore.collider, collider);
        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
