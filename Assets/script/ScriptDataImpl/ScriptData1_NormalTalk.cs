using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptData1_NormalTalk : ScriptData
{
    public ScriptData1_NormalTalk(int type, string name, string log, string picname, string backpic)
    {
        this.type = type;
        this.name = name;
        this.log = log;
        this.picname = picname;
        this.backpic = backpic;
    }//新场景下的对话构造函数，类型为1,要显示出来的
}
