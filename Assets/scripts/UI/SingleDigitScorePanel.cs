using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleDigitScorePanel : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Sprite[] numbers;
    [SerializeField] private Image digitImage;
    [SerializeField] private int score = 0;

    public void IncreaseScore()
    {
        IncreaseScore(1);
    }

    public void IncreaseScore(int increaseBy)
    {
        if (score + increaseBy > 9)
        {
            Debug.LogWarning($"single digit score panel increase ignored. Had score {score} and was raised by {increaseBy}");
            return;
        }
        if (score + increaseBy > numbers.Length + 1)
        {
            Debug.LogWarning($"single digit score panel increase ignored. No sprite provided for score {score + increaseBy}");
            return;
        }

        score += increaseBy;
        digitImage.sprite = numbers[score];
    }
    void Start()
    {
        digitImage.sprite = numbers[score];
    }
}
