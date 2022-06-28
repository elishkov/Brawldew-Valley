using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] public long cur_health = 100;
    [SerializeField] public long max_health = 100;
    [SerializeField] Text cur_health_txt;
    [SerializeField] Text max_health_txt;
    [SerializeField] float horizontal_floating_txt_scatter = 0.2f;

    private bool is_dead = false;
    
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cur_health_txt.text = $"Health: {cur_health}/{max_health}";
    }

    public void Heal(long amount)
    {
        cur_health = (long)Mathf.Min(max_health, cur_health + amount);
    }

    public void TakeDamage(long amount)
    {
        animator.SetTrigger("Hurt");

        
        // scatter x axis
        var text_pos_x = Random.Range(
            transform.position.x - horizontal_floating_txt_scatter,
            transform.position.x + horizontal_floating_txt_scatter
            );

        // raise above character
        var text_pos_y = transform.position.y + transform.localScale.y;

        var textPosition = new Vector3(
            text_pos_x,
            text_pos_y
            );
        GameManager.instance.onScreenMessageSystem.PostMessage(textPosition, amount.ToString());

        cur_health = (long)Mathf.Max(0, cur_health - amount);
        
        if (cur_health == 0)
            is_dead = true;
    }
}
