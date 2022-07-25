using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownIcon : MonoBehaviour
{
    public Image fill;

    private float cooldownDuration;
    private float cooldownUntil;

    void Start()
    {
        fill.fillAmount = 1f;
    }

    public void StartCooldownAnimation(float duration)
    {
        fill.fillAmount = 0f;
        cooldownDuration = duration;
        cooldownUntil = Time.time + duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownUntil > Time.time)
        {
            fill.fillAmount += Time.deltaTime / cooldownDuration;
        }
    }
}
