using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptData5_NextTxt : ScriptData
{
    public ScriptData5_NextTxt(int type, string txtName, int newJump)
    {
        this.type = type;
        this.txtName = txtName;
        this.newJump = newJump;
    }//设定跳转的文本名，用于换文本，类型为5
}
