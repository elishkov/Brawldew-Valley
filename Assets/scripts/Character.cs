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
    [SerializeField] Color text_color;
    public bool is_dead = false;
    
    private Animator animator;
    private float critFontSize = 7f;
    private float normalFontSize = 3f;
    private Color critColor = Color.yellow;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //cur_health_txt.text = $"Health: {cur_health}/{max_health}";
    }

    public void Heal(long amount)
    {
        cur_health = (long)Mathf.Min(max_health, cur_health + amount);
    }

    public void TakeDamage(long amount, bool isCrit)
    {
        
        ShowFloatingText(amount, isCrit);

        cur_health = (long)Mathf.Max(0, cur_health - amount);

        var min_allowed_health = 0;

        // one hit kill protection
        if (cur_health > max_health / 10)
        {
            min_allowed_health = 1;
        }
        cur_health = (long)Mathf.Max(min_allowed_health, cur_health - amount);

        if (cur_health == 0)
        {
            is_dead = true;
            animator.SetTrigger("Death");
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    private void ShowFloatingText(long amount, bool isCrit)
    {
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
        GameManager.instance.onScreenMessageSystem.PostMessage(
            textPosition, 
            amount.ToString(), 
            isCrit? critColor : text_color,
            isCrit? critFontSize : normalFontSize);
    }
}
