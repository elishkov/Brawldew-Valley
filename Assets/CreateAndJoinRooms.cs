using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public TMP_InputField roomNameInput;
    
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
        PhotonNetwork.LoadLevel("MultiplayerArena");
    }
}
