using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Customer
{
    public class RoleDialogueImpl : IRoleDialogue
    {
        private string customerDialogPath = "CustomerDialog";
        private string optionDialogPath = "OptionPanel";
        // 对话信息
        private RoleDialogueInfo roleDialogueInfo = null;
        // 当前对话的id
        private int dialogueId = 0;
        // 是否正在显示对话
        private bool isDialoguing = false;

        //是否已经对话完毕
        private bool isFinished = false;
        //客户gameObject
        private GameObject roleObject;
        // 初始化
        public void init(RoleDialogueInfo roleDialogueInfo, GameObject roleObject)
        {
            this.roleDialogueInfo = roleDialogueInfo;
            this.dialogueId = roleDialogueInfo.DialogueId;
            this.roleObject = roleObject;
        }

        private bool hasNext()
        {
            //todo 是否还有下一句话
            return false;
        }

        private void showCustomerPanel(DialogueInfo dialogue)
        {
            // 创建问题UI
            Camera camera = Camera.main;
            // Vector3 position = roleObject.GetComponent<Transform>().position;
            // Vector2 size = roleObject.GetComponent<SpriteRenderer>().size;
            // Vector3 vector3 = position + new Vector3(size.x, size.y, 0);
            // Debug.Log("vector3:" + vector3);
            Transform canvasTranform = GameObject.Find("Canvas").GetComponent<Transform>();
            GameObject obj = Object.Instantiate(Resources.Load(customerDialogPath), canvasTranform) as GameObject;
            Text text = obj.GetComponentInChildren<Text>(true);
            text.text = dialogue.Text;
            Debug.Log("客户说：" + dialogue.Text);
        }

        private void showUserOptionPanel(DialogueInfo dialogueInfo)
        {
            Transform canvasTranform = GameObject.Find("Canvas").GetComponent<Transform>();
            GameObject obj = Object.Instantiate(Resources.Load(optionDialogPath), canvasTranform) as GameObject;
            Debug.Log("用户有" + dialogueInfo.Answers.Count + "个选择");
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
                showCustomerPanel(dialogue);
                showUserOptionPanel(dialogue);

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
        public bool isFinish()
        {
            return this.isFinished;
        }
    }
}