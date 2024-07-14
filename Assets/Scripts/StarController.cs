using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarController : MonoBehaviour
{
    public Image starImage;
    public Sprite[] starSprites;
    public float[] starLimits = { 20f, 15f, 10f };

    public void CheckStars(float remainingTime)
    {
        for (int i = 0; i < starLimits.Length; i++)
        {
            if (remainingTime > starLimits[i])
            {
                starImage.sprite = starSprites[i];
                return;
            }
        }
        starImage.sprite = starSprites[2];
    }
}
