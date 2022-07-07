using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public void Awake()
    {
        instance = this;
    }

    [SerializeField] public List<GameObject> players;
    [SerializeField] public OnScreenMessageSystem onScreenMessageSystem;
}
