using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class heartsManager : MonoBehaviour
{
    public Player pl;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    void FixedUpdate()
    {
        foreach(Image img in hearts)
        {
            img.sprite = emptyHeart;
        }

        for (int i = 0; i < pl.health; i++)
        {
            if(i % 2 == 0)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = halfHeart;
            }
        }
    }
}
