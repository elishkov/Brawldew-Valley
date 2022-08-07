using Photon.Pun;
using Photon.Realtime;
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
    

    void Start()
    {
        // do this only once
        if (PhotonNetwork.IsMasterClient)
        {
            SetupArena();
        }

        // do this for each player joining
        CreateAndSetupPlayer();

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            print(player.NickName);
        }
    }

    private void SetupArena()
    {
        PhotonNetwork.Instantiate($"Arena\\{gameManager.pickups.name}", Vector3.zero, Quaternion.identity);
        PhotonNetwork.Instantiate($"Arena\\{gameManager.destructibleTerrain.name}", Vector3.zero, Quaternion.identity);
    }

    private void CreateAndSetupPlayer()
    {
        Vector3 position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
        GameObject networkCharacterGO = PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        var networkCharacter = networkCharacterGO.GetComponent<NetworkCharacter>();
        var character = networkCharacter.GetComponent<Character>();
        character.mainHealthBar = gameManager.mainHealthBar;
        character.charName = PhotonNetwork.NickName;
        gameManager.vcam.Follow = networkCharacter.transform;
        gameManager.players.Add(networkCharacterGO);
    }
}
