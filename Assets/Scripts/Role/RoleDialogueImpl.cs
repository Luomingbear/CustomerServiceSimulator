using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    //角色对话实现类
    public class RoleDialogueImpl : IRoleDialogue
    {
        // 角色的GameObject
        public GameObject GameObject { set; private get; }

        // 角色对话信息
        private DialgueInfo _dialogue = null;

        public void setDialogue(DialgueInfo dialgueInfo)
        {
            this._dialogue = dialgueInfo;
        }

        // 把问题显示在客户的右上角
        private void showTextDialog()
        {
            Debug.Log(_dialogue.RoleName + " 说 :" + _dialogue.Text);
        }

        // 显示答案选项
        private void showAnwsers()
        {
            Debug.Log("你的回答可以是 :" + _dialogue.Answers);
        }

        //下一句话
        public bool speakNext()
        {
            Debug.Log(GameObject + "speak next");
            //todo 讲下一句话
            if (_dialogue != null)
            {
                // 1. 把问题显示在客户的右上角
                showTextDialog();
                // 2. 显示回答选项
                showAnwsers();
            }
            return false;
        }
    }
}