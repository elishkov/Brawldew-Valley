using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public NetworkCharacter playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public GameManager gameManager;
    public List<Text> healthBars;
    public GameObject fountain;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        GameObject networkCharacterGO = PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        var networkCharacter = networkCharacterGO.GetComponent<NetworkCharacter>();
        
        var character = networkCharacter.GetComponent<Character>();
        var dash = networkCharacter.GetComponent<DashAbility>();
        dash.cooldownIcon = gameManager.dashCooldownIcon;
        character.mainHealthBar = gameManager.mainHealthBar;
        character.fountain = fountain;
        gameManager.vcam.Follow = networkCharacter.transform;

        gameManager.players.Add(networkCharacterGO);
    }
}
