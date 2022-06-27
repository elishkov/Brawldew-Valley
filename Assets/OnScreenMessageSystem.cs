using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnScreenMessageSystem : MonoBehaviour
{
    [SerializeField] GameObject textPrefab;

    public void PostMessage(Vector3 worldPosition, string message)
    {
        worldPosition.z = -1f;

        GameObject textGO = Instantiate(textPrefab,transform);
        textGO.transform.position = worldPosition;

        TextMeshPro textMeshPro = textGO.GetComponent<TextMeshPro>();
        textMeshPro.text = message;

    }
}
