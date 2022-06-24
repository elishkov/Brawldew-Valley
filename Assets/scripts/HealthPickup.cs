using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupItem
{
    [SerializeField] long health_bonus = 20;
    protected override void Pickup()
    {        
        character.Heal(health_bonus);
        Destroy(gameObject);
    }
}
