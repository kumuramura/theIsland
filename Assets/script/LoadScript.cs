using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScript : MonoBehaviour{
    public static LoadScript instance;
    public Text speakingName;//改变名字颜色
    public static int index=0;
    bool SaveCheck = false;
    List<string> txt;//定义一个txt的list
    string[] datas;
    ScriptData currentLog;//记录当前Log
    public AudioSource nowMusic=new AudioSource();

    //用此初始化
     void Awake()
    {
        instance = this;
       
        string path = "./SaveData";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

    }

    void Update()
    {
        //print(index);
    }

    //加载txt文件中的剧本，根据换行符号进行分出一个个数据
     public void loadscripts(string txtFileName)
    {
        txt = new List<string>();

        TextAsset txtofscript = Resources.Load("writting/"+txtFileName) as TextAsset;
        datas = txtofscript.text.Split('\n');
        for (int i = 0; i < datas.Length; i++)
        {
            txt.Add(datas[i]);
        }

    }//加载txt文件

    public ScriptData loadNext()
    {
        if (index < txt.Count)
        //如果剧本还没到底
        {
            string[] datas = txt[index].Split('#');//第index个剧本，用#分开，放入datas数组

            int type = int.Parse(datas[0]);//parse对string强制转换，解析出对话类型

            if (SaveCheck == true)
            {
                SaveCheck = false;
                ChangeNameColor(currentLog.name);
                if(nowMusic.isPlaying==true)
                   {
                      nowMusic.Stop();
                   }
                return currentLog;
            }//保存,忘了savecheck有什么用了
            //改颜色就是用于保存（？后的改颜色
            else if(loadAndSaveInTitle.JumpFromTitle==1)
            {
                loadAndSaveInTitle.JumpFromTitle = 0;
                currentLog = loadAndSaveInTitle.currentLogInTitle;
                //index = loadAndSaveInTitle.index;
                ChangeNameColor(currentLog.name);
                //把聊天记录的内容删掉
                RecordLog.historyLog.Clear();
                RecordLog.historyLog.Add("0");
                //开始把存档中的save展示出来
                LoadScript.instance.loadscripts(GameManager.scriptName);
                

                return currentLog;
            }
            //该部分else if 的作用是
            //如果玩家的从标题读取存档，从而跳转到指定对话
            //那么我们把存档中的index跟log读取出来，返回到当前游戏界面
            
            else if(type==1)
            {
                string name = datas[1];
                string log = datas[2];
                string picname = datas[3];
                string backpic = datas[4];
                index++;
                currentLog = new ScriptData1_NormalTalk(type, name, log, picname, backpic);
                
                ChangeNameColor(currentLog.name);
                return currentLog;
                //为1就构造完整，建议所有对话，统一用1进行构造
            }
            else if(type==3)
            {
                string option1 = datas[1];//选项1的内容
                string option2 = datas[2];//选项2的内容
                int jump1 = int.Parse(datas[3]);//选了1，后跳多少行
                int jump2 = int.Parse(datas[4]);//选了2，后跳多少行
                string name = datas[5];//老四样
                string log = datas[6];
                string picname = datas[7];
                string backpic = datas[8];

                string[] nextLog1 = txt[index + jump1].Split('#');//跳转的文案
                string[] nextLog2 = txt[index + jump2].Split('#');//跳转的文案2

               ChangeNameColor(currentLog.name);//总感觉这里可以删
                
                //这里选项跳转的类型都为1
                if (GameManager.choosen == 1)
                {
                    index += jump1+1;//根据选项跳到指定位置，还要再+1
                    currentLog = new ScriptData1_NormalTalk(int.Parse(nextLog1[0]), nextLog1[1], nextLog1[2], nextLog1[3],nextLog1[4]);
                    
                    ChangeNameColor(nextLog1[1]);//再次改一次颜色

                    return new ScriptData1_NormalTalk(int.Parse(nextLog1[0]), nextLog1[1], nextLog1[2], nextLog1[3],nextLog1[4]);
                }
                else if(GameManager.choosen == 2)
                {
                    index += jump2+1;
                    currentLog = new ScriptData1_NormalTalk(int.Parse(nextLog2[0]), nextLog2[1], nextLog2[2], nextLog2[3],nextLog2[4]);
                    
                    ChangeNameColor(nextLog2[1]);

                    return new ScriptData1_NormalTalk(int.Parse(nextLog2[0]), nextLog2[1], nextLog2[2], nextLog2[3],nextLog2[4]);
                }

                currentLog = new ScriptData3_Choose(type, option1, option2, jump1, jump2,name,log,picname,backpic);
                
                return currentLog;
            }
            else if(type==5)
            {
                string txtName=datas[1];
                int jumpNumber=int.Parse(datas[2]);
                txt = new List<string>();

                //重写一下loadscript，我们自己再载入一次
                TextAsset txtofscript = Resources.Load("writting/" + txtName) as TextAsset;
                datas = txtofscript.text.Split('\n');
                for (int i = 0; i < datas.Length; i++)
                {
                    txt.Add(datas[i]);
                }
                //这里我们把index设置为后一位，是因为当前的jumpnumber的文案，已经被return了
                //重写的目的也是改这里，不然没有重写的意义
                index = jumpNumber+1;
                GameManager.scriptName = txtName;
                //我们再手动把数据分一下
                string[] newtxtdata = txt[jumpNumber].Split('#');//第jumpNumber个剧本，用#分开，放入datas数组
                string ttype= newtxtdata[0];
                switch(ttype)
                    {
                         
                case "1":
                        return new ScriptData1_NormalTalk(1,newtxtdata[1],newtxtdata[2],newtxtdata[3],newtxtdata[4]);
                                      

                    default:
                        return null;
                }
                //5的跳转就不要再跳到5了,6也别跳了,音乐跳来干嘛，3也是，别跳到选项去
                //好了，现在连4都不用跳了
                //保持switch是为了以后添加新的数字类型

            }
            else if(type==6)
            {
                
                string music = datas[1];
                index +=2;

                string[] nextLog = txt[index+1].Split('#');
                //6类型的下一个类型只能是1类型
                currentLog =new ScriptData1_NormalTalk(int.Parse(nextLog[0]), nextLog[1], nextLog[2], nextLog[3],nextLog[4]);

                AudioClip clip = Resources.Load<AudioClip>("music/" +music);
               //nowMusic播放
                if(nowMusic.isPlaying==true)
                   {
                      nowMusic.Stop();
                   }
                   nowMusic.clip = clip;
                   nowMusic.Play();

                return currentLog;
            }//返回一个音乐
            else
            {
                return null;
            }//返回空
           
                       
        }
        else
        {
            return null;
        }
    }
    

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.index = index;
        save.type = currentLog.type;
        save.name = currentLog.name;
        save.log = currentLog.log;
        save.picname = currentLog.picname;
        save.backpic = currentLog.backpic;
        save.option1 = currentLog.option1;
        save.option2 = currentLog.option2;
        save.JumpTo1 = currentLog.JumpTo1;
        save.JumpTo2 = currentLog.JumpTo2;
        save.afterJump = currentLog.afterJump;
        save.SaveTime=DateTime.Now.ToString();
        save.scriptName = GameManager.scriptName;

        return save;
    }

    public void SaveGame()//没有引用是因为被游戏界面中的存档按钮使用
    {
        if (ClickDect.savingmode == 1)
        { 
            Save save = CreateSaveGameObject();
            var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;


            button.GetComponent<Image>().sprite = Resources.Load("picture/demo/" + save.backpic, typeof(Sprite)) as Sprite;

            GameObject savelog = GameObject.Find("存档说明" + button.name);
            savelog.GetComponent<Text>().text = "index=" + index + "\r\n" + currentLog.log+ "\r\n"+DateTime.Now.ToString();
            
           
            
            BinaryFormatter bf = new BinaryFormatter();
            
            FileStream file = File.Create("./SaveData"
                + "/gamesave" + button.name + ".sav");

            bf.Serialize(file, save);
            file.Close();

            Debug.Log("Game has Saved, index is " + index+" , scriptName is "+save.scriptName);
            
        }
    }

    


    public void LoadGame()
    {
        if (ClickDect.savingmode == -1)
        {
            var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (File.Exists("./SaveData"
                + "/gamesave" + button.name + ".sav"))
            {

                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open("./SaveData"
                + "/gamesave" + button.name + ".sav", FileMode.Open);
                Save save = (Save)bf.Deserialize(file);
                file.Close();
                
                SaveCheck = true;

                RecordLog.historyLog.Clear();
                RecordLog.historyLog.Add("0");
                for (int i = 1; i <= 11; i++)
                {

                    GameObject theLog = GameObject.Find("r" + i);
                    GameObject theName = GameObject.Find("name" + i);
                    theName.GetComponent<Text>().text = "";
                    theLog.GetComponent<Text>().text = "";

                }

                //开始把存档中的save展示出来，这里有问题！！！要改！！！
                LoadScript.instance.loadscripts(save.scriptName);
                GameManager.scriptName = save.scriptName;
                index = save.index;
                switch (save.type)
                {
                    case 1:
                        currentLog = new ScriptData1_NormalTalk(save.type, save.name, save.log, save.picname, save.backpic);
                        break;
                    case 3:
                        currentLog = new ScriptData3_Choose(save.type, save.option1, save.option2, save.JumpTo1,
                            save.JumpTo2, save.name, save.log, save.picname, save.backpic);
                        break;
                }
                SmallHandleData(currentLog);//展示读档后的内容


                Debug.Log("Game Loaded, index is " + index + " , scriptName is " + save.scriptName);
                

            }
            else
            {
                Debug.Log("No game saved!");
            }
        }
    }


    private void ChangeNameColor(string CharacterName)
    {
        if (CharacterName == "少女")
         {
           speakingName.color=new Color32(149,49,135,255);
         }
        else if(CharacterName == "我")
         {
           speakingName.color=new Color32(16,58,145,255);
         }
    }

    //小的显示数据，用来显示读档后的显示
    private void SmallHandleData(ScriptData data)
    {
        if (data.type == 1)
        {

            
        }

        else if (data.type == 3)
        {
            
        }
    }
}
