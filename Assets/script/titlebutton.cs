using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//进入游戏的按钮互动
//完成注释
public class titlebutton : MonoBehaviour
{

    public CanvasGroup cgbackground;
    public CanvasGroup cg;
    public CanvasGroup files;

    public Image Fullcg;

  
    
    private bool filesIsOpen = false;

    void Update()
    {
        if(filesIsOpen==true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Visiable.setInvisible(files);
                filesIsOpen = false;
            }
        }
        
        
    }

    //进入游戏
    public void startGame()
    {

        SceneManager.LoadScene(1);
        
    }

    //打开存档界面
    public void loadGame()
    {
        Visiable.setVisible(files);
        filesIsOpen = true;
    }
    //关闭存档界面
    public void  CloseLoad()
    {
        Visiable.setInvisible(files);
        filesIsOpen = false;
    }
    //打开cg画面
    public void openCGmode()
    {
        Visiable.setVisible(cgbackground);
    }
    //关闭cg画面
    public void closeCGmode()
    {
        Visiable.setInvisible(cgbackground);
    }
    //打开某cg，cg名字根据按键名字决定
    public void openCG()
    {
        Visiable.setVisible(cg);
        //当前选中的部件
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        
        Fullcg.sprite = Resources.Load("picture/Illusion Island/cg/"+button.name, typeof(Sprite)) as Sprite;
      
        //print(button.name);
    }
    //关闭cg
    public void closeCG()
    {
        Visiable.setInvisible(cg);
    }

    public void exitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
