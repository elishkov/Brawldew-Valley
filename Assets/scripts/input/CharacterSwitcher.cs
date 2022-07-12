using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField] List<GameObject> characters = new List<GameObject>();
    PlayerInputManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        //manager.playerPrefab = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
