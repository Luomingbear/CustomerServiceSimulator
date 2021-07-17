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

        //是否说完了
        bool isFinish();
    }
}