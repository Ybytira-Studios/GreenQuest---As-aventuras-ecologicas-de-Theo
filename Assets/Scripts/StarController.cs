using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StarController : MonoBehaviour
{
    public Image starImage;
    public Sprite[] starSprites;
    private float[] starLimits = { 15f, 20f, 25f, 30f };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckStars(float time)
    {
        for (int i = 0; i < starLimits.Length; i++)
        {
            if (time <= starLimits[i])
            {
                starImage.sprite = starSprites[i];
                return;
            }
        }
        // Caso o tempo exceda o limite para todas as estrelas, você pode definir uma ação apropriada
        starImage.sprite = starSprites[2];
    }
}
