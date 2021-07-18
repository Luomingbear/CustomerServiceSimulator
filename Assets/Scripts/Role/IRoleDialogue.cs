using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Customer
{
    public interface IRoleDialogue
    {

        // 初始化数据
        void init(RoleDialogueInfo roleDialogueInfo, GameObject roleObject);

        // 下一句话
        void speakNext();

        //选择了选项
        void chooseOption(int dialogueId);

        //是否说完了
        bool isFinish();
    }
}