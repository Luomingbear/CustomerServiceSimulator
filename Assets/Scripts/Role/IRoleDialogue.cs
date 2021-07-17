using System;
using System.Collections;
using System.Collections.Generic;

namespace Customer{
    public interface IRoleDialogue{

        // 初始化数据
        void init(RoleDialogueInfo roleDialogueInfo);

        // 下一句话
        void speakNext();

        //是否说完了
        bool isFinish();
    }
}