using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TreeCuttable : ToolHit
{
    [SerializeField] GameObject pickupDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] int dropCountVariance= 8;
    [SerializeField] float spread = 2.7f;
    PhotonView view;

    public void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public override void Hit()
    {
        // range is (dropCount - dropCountVariance/2, dropCount + dropCountVariance/2)
        int drops = (int)(dropCount + (dropCountVariance * UnityEngine.Random.value - dropCountVariance / 2));
        while (drops > 0)
        {
            drops -= 1;
            Vector3 drop_position = transform.position;
            drop_position.x += spread * UnityEngine.Random.value - spread / 2;
            drop_position.y += spread * UnityEngine.Random.value - spread / 2;
            PhotonNetwork.Instantiate(Path.Combine("Arena", pickupDrop.name), drop_position, Quaternion.identity);            
        }
        view.RPC("RemoteDestroy", RpcTarget.AllBuffered);
    }

    [PunRPC]
    protected virtual void RemoteDestroy()
    {
        Destroy(gameObject);
    }
}
