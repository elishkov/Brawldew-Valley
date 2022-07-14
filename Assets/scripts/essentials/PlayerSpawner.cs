using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public NetworkCharacter playerPrefab;

    public float minX = -1;
    public float maxX = 1;
    public float minY = -3;
    public float maxY = -1;

    public GameManager gameManager;
    public List<Text> healthBars;


    // Start is called before the first frame update
    void Start()
    {
        Vector2 position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        
        GameObject networkCharacterGO = PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        var networkCharacter = networkCharacterGO.GetComponent<NetworkCharacter>();
        
        var character = networkCharacter.GetComponent<Character>();
        character.mainHealthBar = gameManager.mainHealthBar;
        gameManager.vcam.Follow = networkCharacter.transform;

        gameManager.players.Add(networkCharacterGO);
    }
}
