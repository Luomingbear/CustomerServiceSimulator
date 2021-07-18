using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Customer
{
    public class RoleDialogueImpl : MonoBehaviour, IRoleDialogue
    {
        // 客户对话框prefab地址
        private string customerDialogPath = "CustomerDialog";
        // 角色选项prefab地址
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

        //客户说话的panel
        private GameObject customerDialoguePanel = null;
        // 用户的选项panel
        private GameObject optionPanel;
        private int STATE_IDLE = 0;
        private int STATE_SHOW = 1;
        private int STATE_HIDE = 2;
        // 初始化
        public void init(RoleDialogueInfo roleDialogueInfo, GameObject roleObject)
        {
            this.roleDialogueInfo = roleDialogueInfo;
            this.dialogueId = roleDialogueInfo.DialogueId;
            this.roleObject = roleObject;
        }

        private void showCustomerPanel(DialogueInfo dialogue)
        {
            // 创建问题UI
            Camera camera = Camera.main;
            // Vector3 position = roleObject.GetComponent<Transform>().position;
            // Vector2 size = roleObject.GetComponent<SpriteRenderer>().size;
            // Vector3 vector3 = position + new Vector3(size.x, size.y, 0);
            // Debug.Log("vector3:" + vector3);
            if (customerDialoguePanel == null)
            {
                Transform canvasTranform = GameObject.Find("Canvas").GetComponent<Transform>();
                customerDialoguePanel = Object.Instantiate(Resources.Load(customerDialogPath), canvasTranform) as GameObject;
            }
            //执行显示动画
            Animator animator = customerDialoguePanel.GetComponent<Animator>();
            int state = animator.GetInteger("state");
            //如果现在对话框没有显示，则直接显示显示新的
            if (state == STATE_IDLE)
            {
                animator.SetInteger("state", STATE_SHOW);
            }
            // 如果对话框正在隐藏，则在隐藏之后再显示
            else if (state == STATE_HIDE)
            {
                animator.SetInteger("state", STATE_SHOW);
            }
            Text text = customerDialoguePanel.GetComponentInChildren<Text>(true);
            text.text = dialogue.Text;
            Debug.Log("客户[" + roleDialogueInfo.RoleName + "]说：" + dialogue.Text);
        }

        //显示选项，
        // optionObj： 选项的GameObject
        // position： 这是第几个选项
        // dialogueInfo: 对话信息
        private void showOption(GameObject optionObj, int position, DialogueInfo dialogueInfo)
        {
            if (optionObj == null)
            {
                return;
            }
            List<Answer> answers = dialogueInfo.Answers;
            if (answers.Count > position)
            {
                OptionClick optionClick = optionObj.GetComponent<OptionClick>();
                optionClick.Answer = dialogueInfo.Answers[position];
                // TMP_Text textMeshPro = optionObj.GetComponentInChildren<TMP_Text>(true) as TMP_Text;
                Text textComponent = optionObj.GetComponentInChildren<Text>(true) as Text;
                textComponent.text = optionClick.Answer.Option;
            }
            else
            {
                GameObject.Destroy(optionObj);
            }

        }

        private void showUserOptionPanel(DialogueInfo dialogueInfo)
        {
            Debug.Log("显示用户选项");
            if (optionPanel == null)
            {
                Transform canvasTranform = GameObject.Find("Canvas").GetComponent<Transform>();
                optionPanel = Object.Instantiate(Resources.Load(optionDialogPath), canvasTranform) as GameObject;
            }
            GameObject option1 = GameObject.Find("btn_option1");
            GameObject option2 = GameObject.Find("btn_option2");
            GameObject option3 = GameObject.Find("btn_option3");
            GameObject option4 = GameObject.Find("btn_option4");
            showOption(option1, 0, dialogueInfo);
            showOption(option2, 1, dialogueInfo);
            showOption(option3, 2, dialogueInfo);
            showOption(option4, 3, dialogueInfo);

            //执行显示动画
            Animator animator = optionPanel.GetComponent<Animator>();
            int state = animator.GetInteger("state");
            //如果现在对话框没有显示，则直接显示显示新的
            if (state == STATE_IDLE)
            {
                animator.SetInteger("state", STATE_SHOW);
            }
            // 如果对话框正在隐藏，则在隐藏之后再显示
            else if (state == STATE_HIDE)
            {
                animator.SetInteger("state", STATE_SHOW);
            }
        }

        private bool isAnimatingCustomerDialogue()
        {
            if (customerDialoguePanel == null)
            {
                return false;
            }
            Animator animator = customerDialoguePanel.GetComponent<Animator>();
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.normalizedTime >= 0.97f;
        }


        private bool isAnimatingOptionDialogue()
        {
            if (optionPanel == null)
            {
                return false;
            }
            Animator animator = optionPanel.GetComponent<Animator>();
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.normalizedTime >= 0.97f;
        }

        private void dialogue()
        {
            if (roleDialogueInfo != null)
            {
                DialogueInfo dialogue = roleDialogueInfo.GetDialogueDic()[dialogueId];
                if (dialogue == null)
                {
                    Debug.Log(roleDialogueInfo.RoleName + "-获取对话信息失败，dialogueId=" + dialogueId);
                    return;
                }
                if (!isAnimatingCustomerDialogue())
                {
                    showCustomerPanel(dialogue);
                }
                if (!isAnimatingOptionDialogue())
                {
                    showUserOptionPanel(dialogue);
                }
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

        // 隐藏对话和选项
        //return : 是否已经隐藏
        private bool hideDialogueAndOption()
        {
            if (customerDialoguePanel == null)
            {
                return true;
            }

            bool customerAnimationFinish = false;
            bool optionAnimationFinish = false;
            if (customerDialoguePanel != null)
            {
                Animator animator = customerDialoguePanel.GetComponent<Animator>();
                int state = animator.GetInteger("state");
                if (state == STATE_SHOW)
                {
                    animator.SetInteger("state", STATE_HIDE);
                }
                else if (state == STATE_HIDE)
                {
                    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                    if (stateInfo.normalizedTime >= 0.97f) //说明当前动画快结束了
                    {
                        Debug.Log("客户对话框已经隐藏");
                        optionAnimationFinish = true;
                    }
                }
            }

            if (optionPanel != null)
            {
                Animator animator = optionPanel.GetComponent<Animator>();
                int state = animator.GetInteger("state");
                if (state == STATE_SHOW)
                {
                    animator.SetInteger("state", STATE_HIDE);
                }
                else if (state == STATE_HIDE)
                {
                    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                    if (stateInfo.normalizedTime >= 0.97f) //说明当前动画快结束了
                    {
                        Debug.Log("选项对话框已经隐藏");
                        optionAnimationFinish = true;
                        GameObject.Destroy(optionPanel, 1);
                        optionPanel = null;
                    }
                }
            }
            return optionAnimationFinish && customerAnimationFinish;
        }

        // 选择具体的选项
        public void chooseOption(int dialogueId)
        {
            this.dialogueId = dialogueId;
            this.isDialoguing = false;
            if (dialogueId == 0)
            {
                this.isFinished = true;
            }
            if (hideDialogueAndOption() && !isFinished)
            {
                speakNext();
            }
        }

        //是否说完了
        public bool isFinish()
        {
            return this.isFinished;
        }
    }
}