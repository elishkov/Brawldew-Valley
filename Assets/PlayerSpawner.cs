using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public GameManager gameManager;
    public List<Text> healthBars;


    // Start is called before the first frame update
    void Start()
    {
        Vector2 position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        gameManager.players.Add(player);
        
        //player.GetComponent<Character>().cur_health_txt = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
