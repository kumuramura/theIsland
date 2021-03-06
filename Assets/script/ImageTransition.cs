using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTransition : MonoBehaviour
{
    //下面为图片动态切换数值
    public static float alpha1 = 0f;
    public static float alpha2 = 0f;
    public float alphaSpeed = 6.0f;
    public CanvasGroup cg1;
    public CanvasGroup cg2;

    void Update()
    {
        //图片动态显示
        if (alpha1 != cg1.alpha)
        {

            cg1.alpha = Mathf.Lerp(cg1.alpha, alpha1, alphaSpeed * Time.deltaTime);
            //This would mean the change to cg.alpha would happen per second instead of per frame.
            if (Mathf.Abs(alpha1 - cg1.alpha) <= 0.01)//差值小于0.01的时候
            {
                cg1.alpha = alpha1;
            }
        }
        if (alpha2 != cg2.alpha)
        {

            cg2.alpha = Mathf.Lerp(cg2.alpha, alpha2, alphaSpeed * Time.deltaTime);
            //This would mean the change to cg.alpha would happen per second instead of per frame.
            if (Mathf.Abs(alpha2 - cg2.alpha) <= 0.01)//差值小于0.01的时候
            {
                cg2.alpha = alpha2;
            }
        }
        
    }
}
