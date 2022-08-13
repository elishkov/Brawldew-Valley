using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiDigitScorePanel : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Sprite[] numbers;
    [SerializeField] private Image[] digitImage;
    [SerializeField] private int score = 0;

    public void IncreaseScore()
    {
        IncreaseScore(1);
    }

    public void IncreaseScore(int increaseBy)
    {        
        score += increaseBy;
        UpdateDigitImages();
    }

    private void UpdateDigitImages()
    {
        if (score < 0 || score > Mathf.Pow(10, digitImage.Length) - 1)
        {
            Debug.LogWarning($"score panel increase ignored. score {score} can't be displayed");
            return;
        }

        int intermediateScore = score;
        int division;
        for (int i = digitImage.Length; i > 0; i--)
        {
            division = (int)(intermediateScore / Mathf.Pow(10, i - 1));
            if (division >= 1)
            {
                intermediateScore %= (int)Mathf.Pow(10, i - 1);
            }
            digitImage[i - 1].sprite = numbers[division];
            digitImage[i - 1].enabled = true;
        }

        for (int i = digitImage.Length; i > 1; i--)
        {
            if (digitImage[i - 1].sprite == numbers[0])
            {
                digitImage[i - 1].enabled = false;
            }
            else
            {
                break;
            }
        }


    }

    void Start()
    {
        UpdateDigitImages();
    }
    void Update()
    {
        UpdateDigitImages();
    }

    void Awake()
    {
        UpdateDigitImages();
    }
}
