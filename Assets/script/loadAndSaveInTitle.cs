using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
//标题中的加载跟保存
public class loadAndSaveInTitle : MonoBehaviour
{

    public int index =0;
    public static ScriptData currentLogInTitle;
    public static int JumpFromTitle = 0;
     void Start()
    {
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

                GameObject savePic = GameObject.Find(i.ToString());
                //偷出背景
                savePic.GetComponent<Image>().sprite = Resources.Load("picture/island/background/" + save.backpic, typeof(Sprite)) as Sprite;

                GameObject savelog = GameObject.Find("存档说明" + i);
                savelog.GetComponent<Text>().text = "index=" + save.index + "\r\n" + save.log+ "\r\n" + save.SaveTime;               
            }
        }
        
    }

    void Update()
    {
        //LoadGame();
    }

    public void LoadGame()
    {
        
        
            var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (File.Exists("./data"
                + "/gamesave" + button.name + ".save"))
            {

                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open("./data"
                + "/gamesave" + button.name + ".save", FileMode.Open);
                Save save = (Save)bf.Deserialize(file);
                file.Close();


            GameManager.scriptName = save.scriptName;
            LoadScript.index = save.index;
                switch (save.type)
                {
                    case 1:
                    currentLogInTitle = new ScriptData(save.type, save.name, save.log, save.picname, save.backpic);
                        break;
                    case 3:
                    currentLogInTitle = new ScriptData(save.type, save.option1, save.option2, save.JumpTo1,
                            save.JumpTo2, save.name, save.log, save.picname, save.backpic);
                        break;
                    case 4:
                    currentLogInTitle = new ScriptData(save.type, save.name, save.log, save.picname);
                        break;
                }


                Debug.Log("Game Loaded, index is " + LoadScript.index + " , scriptName is " + save.scriptName);
                JumpFromTitle = 1;
                SceneManager.LoadScene(1);


        }
            else
            {
                Debug.Log("No game saved!");
            }
    }


}
