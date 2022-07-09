using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHPBar : MonoBehaviour
{
    public GameObject follow;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = follow.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.transform.position;
    }
}
