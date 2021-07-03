using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 对话的数据结构
public class DialgueInfo
{
    //Role,IsReturnGoods,TextId,Text,AddRage
    //角色名
    public string RoleName { set; get; }

    // 是否需要退货
    public bool IsReturnGoods { set; get; }

    // 对话文案id
    public int TextId { set; get; }

    // 文本内容
    public string Text { set; get; }

    //愤怒值
    public int RageNum { set; get; }

    //回答选项的列表
    public List<Answer> Answers { set; get; }

    public Answer NoOptionAnwser { set; get; }

    public DialgueInfo()
    {

    }
}
