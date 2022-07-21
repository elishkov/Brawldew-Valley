using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupItem
{
    [SerializeField] int health_bonus = 20;
    
    protected override void Pickup(Character character)
    {
        // state change
        character.ApplyHeal(health_bonus);
        // network effect
        view.RPC("DeletePickup", RpcTarget.AllBuffered);
    }

    [PunRPC]
    protected virtual void DeletePickup()
    {
        Destroy(gameObject);
    }
}
