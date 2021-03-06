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
    public Stat hp;
    public bool is_dead = false;
    public int recover_period_sec = 3;

    private Animator animator;
    private PhotonView view;

    [SerializeField] private GameObject floatingHPBar;
    [SerializeField] public MainHealthBar mainHealthBar;
    [SerializeField] float horizontal_floating_txt_scatter = 0.2f;
    [SerializeField] Color text_color;

    private readonly float critFontSize = 7f;
    private readonly float normalFontSize = 3f;
    private Color critColor = Color.yellow;
    internal string charName;

    public void ApplyHeal(int amount)
    {
        //network effects
        view.RPC("Heal", RpcTarget.AllBuffered, amount);

        //local effects
        UpdateHpBars();
    }

    [PunRPC]
    public void Heal(int amount)
    {
        hp.Add(amount);
        UpdateHpBars();
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

        // local effects
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

    public void ReportKilled()
    {
        if (!view.IsMine)
        {
            GameManager.instance.singleDigitScorePanel.IncreaseScore();
        }
    }

    [PunRPC]
    public void TakeDamage(int amount, bool isCrit)
    {
        hp.Subtract(amount);
        UpdateHpBars();        

        if (hp.curVal <= 0)
        {
            is_dead = true;            
            StartCoroutine(ScheduleRecover());            
        }

        if (is_dead == true)
        {
            animator.SetTrigger("Death");            
            ReportKilled();
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    IEnumerator ScheduleRecover()
    {
        yield return new WaitForSecondsRealtime(recover_period_sec);
        Recover();
        
    }

    public void Recover()
    {
        is_dead = false;
        hp.SetToMax();
        UpdateHpBars();
        animator.SetTrigger("Recover");
    }

    private void UpdateHpBars()
    {
        if (mainHealthBar != null) mainHealthBar.Set(hp.maxVal, hp.curVal);
        if (floatingHPBar != null) floatingHPBar.GetComponent<StatusBar>().Set(hp.maxVal, hp.curVal);
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
        UpdateHpBars();
    }
}
