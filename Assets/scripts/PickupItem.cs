using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    protected Transform player;
    protected Character character;

    [SerializeField] float speed = 3f;
    [SerializeField] float pickupDistance= 1.5f;
    [SerializeField] float ttl = 10f;

    private void Start()
    {
        player = GameManager.instance.player.transform;
        character = GameManager.instance.player.GetComponent<Character>();
    }

    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickupDistance)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position, 
            player.position,
            speed * Time.deltaTime
            );

        if (distance < 0.1)
        {
            Pickup();
        }
    }

    protected virtual void Pickup()
    {
        Destroy(gameObject);
    }

}
