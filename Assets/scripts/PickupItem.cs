using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    protected GameObject[] players;
    protected Character character;

    [SerializeField] float speed = 3f;
    [SerializeField] float pickupDistance= 1.5f;
    [SerializeField] float ttl = 10f;
    float ttl_remaining;

    private void Start()
    {
        players = GameManager.instance.players;
        ttl_remaining = ttl;
    }

    private void Update()
    {
        ttl_remaining -= Time.deltaTime;
        if (ttl!= 0 && ttl_remaining < 0)
        {
            Destroy(gameObject);
        }

        float minDistance = float.MaxValue;
        float distance;
        GameObject closestPlayer = null;
        foreach (var player in players)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlayer = player;
            }

        }
        if (minDistance > pickupDistance)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            closestPlayer.transform.position,
            speed * Time.deltaTime
            );

        if (minDistance < 0.1)
        {
            Pickup(closestPlayer.GetComponent<Character>());
        }
    }

    protected virtual void Pickup(Character character)
    {
        Destroy(gameObject);
    }

}
