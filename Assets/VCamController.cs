using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamController : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject vCam;
    [SerializeField] float minOrthographicSize = 2.5f;
    [SerializeField] float maxOrthographicSize = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        var newOrthographicSize = distance / 2;
        if (newOrthographicSize > minOrthographicSize && newOrthographicSize < maxOrthographicSize)
        {
            var vc = vCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            print($"OrthographicSize changed to {newOrthographicSize}");
            vc.m_Lens.OrthographicSize = newOrthographicSize;
        }
    }
}
