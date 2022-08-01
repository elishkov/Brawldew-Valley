using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{    
    [SerializeField] float speed = 3f;
    [SerializeField] float pickupDistance= 1.5f;
    [SerializeField] float ttl = 10f;

    protected PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        StartCoroutine(DestroyAfterTtl());
    }

    private IEnumerator DestroyAfterTtl()
    {
        if (ttl > 0)
        {
            yield return new WaitForSeconds(ttl);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        float minDistance = float.MaxValue;
        float distance;
        GameObject closestPlayer = null;
        foreach (var player in GameManager.instance.players)
        {
            if (player.GetComponent<Character>().is_dead)
                continue;

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
