using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customer;


// 处理用户点击选项的行为
public class OptionClick : MonoBehaviour
{
    // 回答的内容
    private Answer Answer { set; get; }

    // 点击进行选择
    public void chooseOption()
    {
        if (Answer == null)
        {
            return;
        }
        Debug.Log("用户回答:" + Answer.Option);
        // todo 
        // todo 1隐藏对话框 2寻找正在对话的客户角色 3说下一句话
    }
}
