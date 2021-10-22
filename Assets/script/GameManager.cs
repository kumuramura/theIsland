using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public Text names;
    public Text talk;
    public Image background;
    public Image character;
    public Image character2;//用于动态切换
    public Text choose1;
    public Text choose2;

    public CanvasGroup xuan1;//选项的canvas

    public static int choosen = 0;//用来选择的

    public static string scriptName= "0chapter";//设置当前的脚本名,在loadscript的读档中使用

    //public static int times = 1;//有什么用？

    private bool isActive = false;
    private int timer=0;//计时器,用于自动显示auto

    //下面为文字对话逐个显示
    public static float waitTime = 10f; //字符间隔,被clickdect的speedchange修改
    float speed;    //计时，跳转时置为0
    private string logPrint = null;//接收data.log的内容
    int use = 1;//决定文字是否逐个显示，用来改变显示选项的时候
                //

    //下面为图片动态切换数值
    int frontback = 1;
    string oldCharater="空";//上一个角色图片的名字
    //该功能成功转移到了ImageTransition
    //

    void Start()
    {
        

            LoadScript.instance.loadscripts(GameManager.scriptName);
            handleData(LoadScript.instance.loadNext());
 
        
        
        for (int i = 1; i <= 30; i++)
        {
            if (File.Exists("./data"
                + "/gamesave" + i + ".save"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open("./data"
                + "/gamesave" + i + ".save", FileMode.Open);
                Save save = (Save)bf.Deserialize(file);
                file.Close();

                

                GameObject savelog = GameObject.Find("存档说明" + i);
                savelog.GetComponent<Text>().text = "index=" + save.index + "\r\n" + save.log;

                GameObject savePic = GameObject.Find(i.ToString());
                savePic.GetComponent<Image>().sprite = Resources.Load("picture/island/background/" + save.backpic, typeof(Sprite)) as Sprite;
            }
        }

        speed = 0;//文字显示的speed，一开始设置为0


    }

    public void leftClick()
    {
        handleData(LoadScript.instance.loadNext());
        speed = 0;//文字对话的显示从头开始
    }

    public void MoreleftClick()
    {
        if(ClickDect.savingmode==-1)
        handleData(LoadScript.instance.loadNext());
    }//给读档用的

    public void chooseA()
    {
        choosen = 1;//选第一个       
        handleData(LoadScript.instance.loadNext());      
        choosen = 0;
    }

    public void chooseB()
    {
       choosen = 2;//选第二个
        handleData(LoadScript.instance.loadNext());
        choosen = 0;

    }

    public void setImage(Image image, string picName)
    {
        image.sprite = Resources.Load("picture/island/" + picName, typeof(Sprite)) as Sprite;
    }//设置角色图片和背景图片的函数

    public void setText(Text text, string content)
    {
        //单纯的设置文字
        text.text=content;
        
    }

    void textPrint(Text text,string str)//文字逐个显示，放在update中执行
    {
        speed += Time.deltaTime * waitTime;
        text.text = str.Substring(0, (int)speed + 1);
    }

    void Update() {

        print(scriptName);
        //暂停使用，后续优先处理
        if(ClickDect.skipsituation==1)
        {
            handleData(LoadScript.instance.loadNext());
            
        }
        //暂停使用
        if(ClickDect.autosituation==1)
        {
            if(timer==410)
            {
                handleData(LoadScript.instance.loadNext());
                timer=0;
            }
            else
            timer++;
            
        }
        if (use == 1)
            textPrint(talk, logPrint);//开始逐个显示
        else if (use == 0)
            setText(talk,logPrint);

        
    }

    

    public void handleData(ScriptData data)
    {
        if (data == null)
            return;
        if(data.type == 1)
        {

            Visiable.setInvisible(xuan1);
            if(oldCharater!= data.picname)
            {
                frontback = -frontback;
                oldCharater = data.picname;
                print("不是一张图");
            }

            setImage(background, "background/" + data.backpic);//设置背景
            if(frontback==1)//前景图生效
            {
                setImage(character, "fImage/" + data.picname);//设置人物立绘
                ImageTransition.alpha1 = 1f;
                ImageTransition.alpha2 = 0f;
                
            }
            if(frontback == -1)//后景图生效
            {
                setImage(character2, "fImage/" + data.picname);//设置人物立绘
                ImageTransition.alpha1 = 0f;
                ImageTransition.alpha2 = 1f;
            }
            
            setText(names, data.name);
            //setText(talk, data.log);
            logPrint = data.log;//把log的内容传出去
            use = 1;
            RecordLog.historyLog.Add(data.name+":"+ data.log);
        }
        else if (data.type == 4)
        {

            Visiable.setInvisible(xuan1);          
            setImage(character, "fImage/" + data.picname);
            setText(names, data.name);
            //setText(talk, data.log);
            logPrint = data.log;//把log的内容传出去
            use = 1;
            RecordLog.historyLog.Add(data.name + ":" + data.log);
        }
        else if (data.type == 3)
        {
            setText(choose1, data.option1);
            setText(choose2, data.option2);
            setImage(background, "background/" + data.backpic);
            setImage(character, "fImage/" + data.picname);
            setText(names, data.name);
            setText(talk, data.log);
            Visiable.setVisible(xuan1);
            RecordLog.historyLog.Add("选项1:"+data.option1);
            RecordLog.historyLog.Add("选项2:" + data.option2);
            use = 0;
        }
       

    }//用于设置数据

}  
