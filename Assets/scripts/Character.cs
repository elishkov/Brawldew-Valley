using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Stat
{
    public int maxVal;
    public int curVal;

    public Stat(int max, int cur)
    {
        maxVal = max;
        curVal = cur;
    }

    internal void Subtract(int amount)
    {
        curVal -= amount;
        if (curVal < 0) curVal = 0;
    }

    internal void Add(int amount)
    {
        curVal += amount;
        if (curVal > maxVal) curVal = maxVal;
    }

    internal void SetToMax()
    {
        curVal = maxVal;
    }

    public override string ToString()
    {
        return $"current: {curVal}; max: {maxVal}";
    }
}

public class Character : MonoBehaviour
{
    public StatusBar hpBar;
    public Stat hp;
    public bool is_dead = false;
    public int recover_period_sec = 3;

    private Animator animator;
    private PhotonView view;

    public GameObject floatingHPBar;
    [SerializeField] float horizontal_floating_txt_scatter = 0.2f;
    [SerializeField] Color text_color;
    private float critFontSize = 7f;
    private float normalFontSize = 3f;
    private Color critColor = Color.yellow;
    public MainHealthBar mainHealthBar;

    public void ApplyHeal(int amount)
    {
        //network effects
        view.RPC("Heal", RpcTarget.AllBuffered, amount);

        //local effects
        UpdateHpBar();

    }

    [PunRPC]
    public void Heal(int amount)
    {
        hp.Add(amount);
    }

    public void FullHeal()
    {
        hp.SetToMax();
    }

    public void ApplyDamage(int amount, bool isCrit)
    {
        amount = OneHitKillProtectionAdjustment(amount);

        // network effect
        view.RPC("TakeDamage", RpcTarget.AllBuffered, amount, isCrit);

        // local effect
        ShowFloatingText(amount, isCrit);
    }

    private int OneHitKillProtectionAdjustment(int amount)
    {
        int max_allowed_dmg = hp.curVal;
        if ((float)hp.curVal / hp.maxVal > 0.1)
        {
            max_allowed_dmg = hp.curVal - 1;
        }
        amount = Mathf.Min(max_allowed_dmg, amount);
        return amount;
    }

    [PunRPC]
    public void TakeDamage(int amount, bool isCrit)
    {
        hp.Subtract(amount);

        UpdateHpBar();

        if (hp.curVal <= 0)
        {
            is_dead = true;            
            StartCoroutine(ScheduleRecover());            
        }

        if (is_dead == true)
        {
            animator.SetTrigger("Death");
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    IEnumerator ScheduleRecover()
    {
        print("start waiting at: " + Time.time);
        yield return new WaitForSecondsRealtime(recover_period_sec);
        print("finished waiting at: " + Time.time);
        Recover();
        
    }

    [PunRPC]
    public void Recover()
    {
        is_dead = false;
        hp.SetToMax();
        UpdateHpBar();
        animator.SetTrigger("Recover");
    }

    private void UpdateHpBar()
    {
        if (view is null || view.IsMine)
        {
            hpBar.Set(hp.maxVal, hp.curVal);
            mainHealthBar.Set(hp.maxVal, hp.curVal);
        }
        floatingHPBar.GetComponent<StatusBar>().Set(hp.maxVal, hp.curVal);
    }

    public void ShowFloatingText(long amount, bool isCrit)
    {
        // scatter x axis
        var text_pos_x = UnityEngine.Random.Range(
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
            isCrit ? critColor : text_color,
            isCrit ? critFontSize : normalFontSize);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        UpdateHpBar();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
