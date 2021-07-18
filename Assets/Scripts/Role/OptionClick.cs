using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customer;


namespace Customer
{
    // 处理用户点击选项的行为
    public class OptionClick : MonoBehaviour
    {
        private string TAG_SPEAKING_CUSTOMER = "SpeakCustomer";

        // 回答的内容
        public Answer Answer { set; get; }

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
            // 
            GameObject[] speakingRoles = GameObject.FindGameObjectsWithTag(TAG_SPEAKING_CUSTOMER);
            if (speakingRoles == null || speakingRoles.Length == 0)
            {
                Debug.Log("没有新的客户了！");
                return;
            }
            GameObject speakingRole = speakingRoles[0];
            IRoleDialogue roleDialogue = speakingRole.GetComponent<IRoleDialogue>();
            roleDialogue.chooseOption(Answer.Jump);
        }
    }
}
