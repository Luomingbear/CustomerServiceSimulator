using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Customer
{

    // 角色行为的实现类
    public class RoleBehavior : MonoBehaviour
    {
        // 移动的快慢
        public float MoveScale = 1;
        //相机
        private Camera cam;
        // 是否正在对话

        //角色对话实现接口
        private IRoleDialogue roleDialogue = new RoleDialogueImpl();

        // 设置对话信息
        public void setDialogueInfos(RoleDialogueInfo roleDialogueInfo)
        {
            if (roleDialogue != null)
            {
                roleDialogue.init(roleDialogueInfo, gameObject);
            }
        }


        private void Start()
        {
            cam = Camera.main;
        }

        //说一句话
        private void speakNext()
        {
            if (roleDialogue != null)
            {
                roleDialogue.speakNext();
            }
        }

        // 向右走，需要判断右边是否有人，要保持一定的距离
        private void moveRight()
        {
            transform.Translate(0.1F * MoveScale, 0, 0);
        }

        private void FixedUpdate()
        {
            Vector3 viewPoint = cam.WorldToViewportPoint(transform.position);
            if (viewPoint.x > 1.1) //客户已经走出了屏幕
            {
                // 回收当前对象
                Debug.Log("需要回收当前客户");
            }
            else if (viewPoint.x >= 0.5)
            {
                if (roleDialogue != null && roleDialogue.isFinish()) //对话结束，走出屏幕
                {
                    moveRight();
                }
                else
                {
                    //进行对话
                    speakNext();
                }
            }
            else // 需要向右走进行排队
            {
                //向右走
                moveRight();
            }
        }
    }
}