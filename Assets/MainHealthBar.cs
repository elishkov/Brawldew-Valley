using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void Set(int maxVal, int curVal)
    {
        slider.value = curVal;
        slider.maxValue = maxVal;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
