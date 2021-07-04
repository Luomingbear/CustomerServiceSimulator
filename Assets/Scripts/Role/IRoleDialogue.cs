using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    //角色对话接口
    public interface IRoleDialogue
    {

        //设置对话信息
        void setDialogue(DialogueInfo dialgueInfo);

        //下一句话
        // 返回值 true：讲了下一句 ，false：已经说完了
        bool speakNext();
    }
}