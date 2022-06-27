using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamZoomController : MonoBehaviour
{
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject vCam;
    [SerializeField] float minOrthographicSize = 3f;
    [SerializeField] float maxOrthographicSize = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);
        
        // just a rough estimation that seemed to work... O_o
        var newOrthographicSize = distance / 1.7f;
        if (newOrthographicSize > minOrthographicSize && newOrthographicSize < maxOrthographicSize)
        {
            var vc = vCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            print($"OrthographicSize changed to {newOrthographicSize}");
            vc.m_Lens.OrthographicSize = newOrthographicSize;
        }
    }
}
