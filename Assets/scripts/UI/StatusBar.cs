using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Text text;
    public Image bar;

    public void Set(int maxVal, int curVal)
    {
        bar.fillAmount = (float)curVal / maxVal;
        if (text is not null)
        {
            text.text = $"{curVal}/{maxVal}";
        }
    }
}
