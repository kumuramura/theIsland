using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordLog : MonoBehaviour
{
    public static List<string> historyLog;
    private GameObject theLog;
    private GameObject theName;

    void Awake()
    {
        historyLog = new List<string>();
        historyLog.Add("0");
        
    }

    void Start()
    {
         for (int i = 1; i <= 11; i++)
            {
               theLog = GameObject.Find("r" + i);
               Visiable.setInvisible(theLog.GetComponent<CanvasGroup>());
            
             }
    }

    void Update()
    {

        
        while(historyLog.Count>=12)
        {
            historyLog.RemoveAt(1);
        }

        for(int i=1;i<=11;i++)
        {

            theLog = GameObject.Find("r" + i);
            theName = GameObject.Find("name" + i);
            Visiable.setVisible(theLog.GetComponent<CanvasGroup>());    
            
            string[] datas = historyLog[i].Split(':');//用:分开，0为名字，1为对话

            theName.GetComponent<Text>().text = datas[0];
            theLog.GetComponent<Text>().text = datas[1];

        }
        
        
    }

    
}
