using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BG : MonoBehaviour
{

    public float fadetime;
    public Image[] images;
    public int second;
    private float flag;
    private float time;
    private int index = 0;


    void Start()
    {

        for (int i = 1; i < images.Length; i++)
        {
            images[i].color = new Color(1, 1, 1, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > second)
        {
            if (second >= fadetime * 2)
            {
                if (index < images.Length - 1)
                {
                    images[index + 1].DOFade(1, fadetime);
                    images[index].DOFade(0, fadetime);
                    index++;
                    time = 0;
                }
                else
                {
                    images[index].DOFade(0, fadetime);
                    images[0].DOFade(1, fadetime);
                    index = 0;

                }
            }
            else if (second < fadetime * 2)
            {
                Debug.Log("切换时间小于图片渐变时间");
            }

        }



    }
}

