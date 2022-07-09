using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWorldSpace : MonoBehaviour
{
    public Canvas worldSpace;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(worldSpace.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
