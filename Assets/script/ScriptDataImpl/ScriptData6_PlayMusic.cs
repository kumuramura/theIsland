using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptData6_PlayMusic : ScriptData
{
    public ScriptData6_PlayMusic(int type, string music)
    {
        this.type = type;
        this.music = music;
    }//设定有音乐的构造函数，类型为6
     //在读到此类型的数据后，文本index+2，跳过该数据的文本的读取
     //music以三位数的方式命名，再用tostring变成string索取
     //后续会开发反向读取该对话的bgm，通过index--；
}
