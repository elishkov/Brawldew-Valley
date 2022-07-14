using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public NetworkCharacterGO playerPrefab;

    public float minX = -1;
    public float maxX = 1;
    public float minY = -1;
    public float maxY = 1;

    public GameManager gameManager;
    public List<Text> healthBars;


    // Start is called before the first frame update
    void Start()
    {
        Vector2 position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        
        GameObject networkCharacter = PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        var networkCharacterGO = networkCharacter.GetComponent<NetworkCharacterGO>();
        
        var character = networkCharacterGO.player.GetComponent<Character>();
        character.mainHealthBar = gameManager.mainHealthBar;
        gameManager.vcam.Follow = networkCharacterGO.player.transform;

        gameManager.players.Add(networkCharacterGO.player);
    }
}
