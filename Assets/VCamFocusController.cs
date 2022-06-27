using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamFocusController : MonoBehaviour
{
    [SerializeField] GameObject character1;
    [SerializeField] GameObject character2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position1 = character1.transform.position;
        var position2 = character2.transform.position;
        transform.position = new Vector3((position1.x + position2.x) / 2, (position1.y + position2.y) / 2);
    }
}
