using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    public class RoleDialogueImpl : IRoleDialogue
    {
        // 对话信息
        private RoleDialogueInfo roleDialogueInfo = null;
        // 当前对话的id
        private int dialogueId = 0;
        // 是否正在显示对话
        private bool isDialoguing = false;

        //是否已经对话完毕
        private bool isFinished = false;
        // 初始化
        public void init(RoleDialogueInfo roleDialogueInfo)
        {
            this.roleDialogueInfo = roleDialogueInfo;
            this.dialogueId = roleDialogueInfo.DialogueId;
        }

        private bool hasNext()
        {
            //todo 是否还有下一句话
            return false;
        }

        private void dialogue()
        {
            if (roleDialogueInfo != null)
            {
                DialogueInfo dialogue = roleDialogueInfo.GetDialogueDic()[dialogueId];
                if (dialogue == null)
                {
                    Debug.Log("获取对话信息失败，dialogueId=" + dialogueId);
                    return;
                }
                Debug.Log("客户说：" + dialogue.Text);
            }
        }

        // 说下一句话
        public void speakNext()
        {
            if (isDialoguing == true) //正在对话中，则不处理
            {
                return;
            }
            isDialoguing = true;
            dialogue();
        }

          //是否说完了
       public bool isFinish(){
           return this.isFinished;
       }
    }
}