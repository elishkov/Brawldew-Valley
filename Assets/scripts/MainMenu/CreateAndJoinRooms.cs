using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public TMP_InputField roomNameInput;
    public TMP_InputField playerNameInput;
    public TextMeshProUGUI messageTextField;
    

    public void CreateRoom()
    {
        RoomOptions roomOptions = new();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomNameInput.text);
    }

    public void JoinRoom()
    {        
        PhotonNetwork.JoinRoom(roomNameInput.text);
    }

    public override void OnJoinedRoom()
    {
        // check player name uniqueness
        foreach (var player in PhotonNetwork.PlayerList)
        {
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
