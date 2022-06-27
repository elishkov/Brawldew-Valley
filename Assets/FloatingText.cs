using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] long ttl = 0;
    long ttl_remaining;
    // Start is called before the first frame update
    void Start()
    {
        ttl_remaining = ttl;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ttl == 0 || ttl_remaining > 0)
        {
            ttl_remaining -= 1;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, -1);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
