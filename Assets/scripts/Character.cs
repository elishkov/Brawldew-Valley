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
    // Start is called before the first frame update
    void Start()
    {
        
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
}
