
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//游戏中的按钮交互，菜单，加速，自动，历史记录，设置界面等
public class ClickDect : MonoBehaviour
{
    public CanvasGroup menu;
    public CanvasGroup setting;//设置界面
    public CanvasGroup savingPage;//存档界面
    public CanvasGroup testRecords;//文字记录

    public static int savingmode = 0;
    public Text autoColor;
    public Text skipColor;

    public static int autosituation = 0;//0为手动，1为自动
    public static int skipsituation=0;//0为正常，1为加速

    public Dropdown speedChange;//设置中，文字显示速度调整


    void Update()
    {
        if (Input.GetMouseButtonDown(1) && menu.alpha == 0)
        {
            menuOn();
        }
        
        else if (Input.GetMouseButtonDown(1) && menu.alpha == 1&&setting.alpha==0&&savingPage.alpha==0)
        {
            menuOff();
        }

        printSpeed();//文字显示速度


    }

    public void printSpeed()
    {
        if(speedChange.value==0)
        {
            GameManager.waitTime = 4.0f;

        }
        else if(speedChange.value == 1)
        {
            GameManager.waitTime = 10.0f;
        }
        else if (speedChange.value == 2)
        {
            GameManager.waitTime = 20.0f;
        }
    }

    public void menuOn()
    {
        Visiable.setVisible(menu);    
    }

    public void menuOff()
    {
        Visiable.setInvisible(menu);
    }

    public void settingOn()
    {
        Visiable.setVisible(setting);
    }

    public void settingOff()
    {
        Visiable.setInvisible(setting);
    }

    public void exitToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void savingpageUp()
    {
        Visiable.setVisible(savingPage);
        savingmode = 1;
    }

    public void savingpageDown()
    {
        Visiable.setInvisible(savingPage);
        savingmode = 0;
    }

    public void loadingpageUp()
    {
        Visiable.setVisible(savingPage);
        savingmode = -1;
    }

    public void TextRecordUp()
    {
        Visiable.setVisible(testRecords);
    }
    public void TextRecordDown()
    {
        Visiable.setInvisible(testRecords);
    }

    //auto跟skip不能同时出现
    public void auto()
    {
        if(autosituation == 0)
        {
            autoColor.color = new Color32(80, 130, 230, 250);
            autosituation = 1;
            
            //TODO开启自动模式
        }
        else if (autosituation == 1)
        {
            autoColor.color = new Color32(144, 135, 135, 250);
            autosituation = 0;
            
            //TODO关闭自动模式
        }
    }

    public void skip()
    {
        if(skipsituation == 0)
        {
            skipColor.color = new Color32(80, 130, 230, 250);
            skipsituation = 1;
            
            //TODO开启加速模式
        }
        else if (skipsituation == 1)
        {
            skipColor.color = new Color32(144, 135, 135, 250);
            skipsituation = 0;
            
            //TODO关闭自动加速模式
        }
    }

}
