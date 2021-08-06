using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_ : MonoBehaviour
{
    public Text text;
    private string str = "***************Unity文字逐字显示实现***************";

    public  static float waitTime = 10f; //字符间隔

    float speed;    //计时
    
    void Start()
    {
        speed = 0;
        
    }

    void Update()
    {
        setText();
    }

    void setText()
    {
           speed += Time.deltaTime * waitTime;

            text.text = str.Substring(0, (int)speed + 1);

        
    }

    public void Click()
    {
        speed = 0;
        str = "shut up";
    }
}
