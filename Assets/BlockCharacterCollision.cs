using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider2D characterCollider;
    public BoxCollider2D characterBlockerCollider;
    void Start()
    {
        Physics2D.IgnoreCollision(characterCollider, characterBlockerCollider, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
