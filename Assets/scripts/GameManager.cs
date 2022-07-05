using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> players; 
    [SerializeField] public OnScreenMessageSystem onScreenMessageSystem;

    public void Awake()
    {
        players = new List<GameObject>();
        instance = this;
    }

    
}
