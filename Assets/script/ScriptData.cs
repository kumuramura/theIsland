public abstract class ScriptData
{
    public int type;
    public string name;
    public string log;
    public string picname;
    public string backpic;
    public string option1;
    public string option2;
    public int JumpTo1;//选项1跳转
    public int JumpTo2;//选项2跳转
    public int afterJump;//到达选项后再跳转到正确文本行
    public string music;//在此处使用什么短铃声
    public string txtName;//跳转的下一个剧本的名字
    public int newJump;//跳转到下一个剧本之后从第几行开始


}
