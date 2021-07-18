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
            GameObject optionPanel = Object.Instantiate(Resources.Load(optionDialogPath), canvasTranform) as GameObject;
            GameObject option1 = GameObject.Find("btn_option1");
            GameObject option2 = GameObject.Find("btn_option2");
            GameObject option3 = GameObject.Find("btn_option3");
            GameObject option4 = GameObject.Find("btn_option4");
            List<Answer> answers = dialogueInfo.Answers;

            if (answers.Count > 3)
            {
                OptionClick optionClick4 = option4.GetComponent<OptionClick>();
                optionClick4.Answer = dialogueInfo.Answers[3];
            }
            else
            {
                GameObject.Destroy(option4);
            }
            if (answers.Count > 2)
            {
                OptionClick optionClick3 = option3.GetComponent<OptionClick>();
                optionClick3.Answer = dialogueInfo.Answers[2];
            }
            else
            {
                option3.SetActive(false);
            }
            if (answers.Count > 1)
            {
                OptionClick optionClick2 = option2.GetComponent<OptionClick>();
                optionClick2.Answer = dialogueInfo.Answers[1];
            }
            else
            {
                option2.SetActive(false);
            }
            if (answers.Count > 0)
            {
                OptionClick optionClick1 = option1.GetComponent<OptionClick>();
                optionClick1.Answer = dialogueInfo.Answers[0];
            }
            else
            {
                option1.SetActive(false);
            }
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