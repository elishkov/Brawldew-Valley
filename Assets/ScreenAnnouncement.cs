using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenAnnouncement : MonoBehaviour
{
    [SerializeField] private float ttl = 2f;
    private float ttlRemaining;

    void Start()
    {
        ttlRemaining = ttl;
    }

    // Update is called once per frame
    void Update()
    {
        if (ttlRemaining > 0)
        {
            ttlRemaining -= Time.deltaTime;

            TextMeshPro textMeshPro = GetComponent<TextMeshPro>();
            var color = textMeshPro.color;
            color.a -= 0.01f;
            textMeshPro.color = color;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
