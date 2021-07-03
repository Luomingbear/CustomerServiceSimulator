
// 单个回答的数据结构
public class Answer
{
    //Option1,Option1Jump,Option1Mood
    //选项文本内容
    public string Option { get; set; }

    //选择之后跳转的地址
    public int Jump { get; set; }

    //该回答对客户心情对影响
    public int MoodNum { set; get; }
}