using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public TMP_InputField roomNameInput;
    public TMP_InputField playerNameInput;
    public TextMeshProUGUI messageTextField;
    

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomNameInput.text);
    }

    public void JoinRoom()
    {        
        PhotonNetwork.JoinRoom(roomNameInput.text);
    }

    public override void OnJoinedRoom()
    {
        // check player name uniqueness
        print($"OnJoinedRoom: number of players {PhotonNetwork.PlayerList.Length}");
        foreach (var player in PhotonNetwork.PlayerList)
        {
            print($"player nick name: {player.NickName}");
            if (player.NickName == playerNameInput.text)
            {
                messageTextField.text = "player name isn't available, please choose a different name";
                PhotonNetwork.LeaveRoom();
                return;
            }
        }

        PhotonNetwork.NickName = playerNameInput.text;
        PhotonNetwork.LoadLevel("MultiplayerArena");
    }
}
