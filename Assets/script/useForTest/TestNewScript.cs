using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNewScript
{
    void main()
    {
        ScriptData scriptData = new ScriptData1_NormalTalk(1,"2","3","4","5");
        Console.WriteLine(scriptData.name);
    }
}
