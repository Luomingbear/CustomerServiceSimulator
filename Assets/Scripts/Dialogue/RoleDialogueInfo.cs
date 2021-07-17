using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customer;

namespace Customer
{
    //客户的对话信息
    public class RoleDialogueInfo
    {
        // 角色名
        public string RoleName { set; get; }
        // 当前的对话ID
        public int DialogueId { set; get; }

        private Dictionary<int, DialogueInfo> _dialogueDic = new Dictionary<int, DialogueInfo>();

        // 获取对话字典
        public Dictionary<int, DialogueInfo> GetDialogueDic()
        {
            return _dialogueDic;
        }

        // 添加对话信息
        public void AddDialogueInfo(DialogueInfo dialogueInfo)
        {
            _dialogueDic.Add(dialogueInfo.TextId, dialogueInfo);
        }

    }
}