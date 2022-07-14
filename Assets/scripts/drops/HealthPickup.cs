using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupItem
{
    [SerializeField] int health_bonus = 20;
    protected override void Pickup(Character character)
    {        
        character.Heal(health_bonus);
        Destroy(gameObject);
    }
}
