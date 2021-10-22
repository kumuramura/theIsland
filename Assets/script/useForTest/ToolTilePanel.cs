using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolTilePanel : MonoBehaviour
{
    private float alpha = 0f;
    public float alphaSpeed = 2.0f;

    private CanvasGroup cg;

    void Start()
    {
        cg = this.transform.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (alpha != cg.alpha)
        {
            cg.alpha = Mathf.Lerp(cg.alpha, alpha, alphaSpeed * Time.deltaTime);
            //This would mean the change to cg.alpha would happen per second instead of per frame.
            if (Mathf.Abs(alpha - cg.alpha) <= 0.01)//差值小于0.01的时候
            {
                cg.alpha = alpha;
            }
        }
    }

    public void Show()
    {
        alpha = 1;
    }

    public void Hide()
    {
        alpha = 0;
    }
}

