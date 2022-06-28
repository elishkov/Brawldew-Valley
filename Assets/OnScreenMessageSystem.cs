using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnScreenMessageSystem : MonoBehaviour
{
    [SerializeField] GameObject textPrefab;

    public void PostMessage(Vector3 worldPosition, string message)
    {
        PostMessage(worldPosition, message, Color.white);
    }


    public void PostMessage(Vector3 worldPosition, string message, Color color, float fontSize = 3f)
    {
        worldPosition.z = -1f;

        GameObject textGO = Instantiate(textPrefab,transform);
        textGO.transform.position = worldPosition;

        TextMeshPro textMeshPro = textGO.GetComponent<TextMeshPro>();
        textMeshPro.text = message;
        textMeshPro.color = color;
        textMeshPro.fontSize = fontSize;

    }
}
