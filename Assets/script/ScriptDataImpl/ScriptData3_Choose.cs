using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptData3_Choose : ScriptData
{
    public ScriptData3_Choose(int type, string option1, string option2, int jump1, 
        int jump2, string name, string log, string picname, string backpic)
    {
        this.type = type;
        this.option1 = option1;
        this.option2 = option2;
        JumpTo1 = jump1;
        JumpTo2 = jump2;

        //新加内容
        this.name = name;
        this.log = log;
        this.picname = picname;
        this.backpic = backpic;

    }//设定处于选择时的函数，类型为3
}
