using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customer;

namespace Customer
{
    //客户角色的GameObject
    public class RoleGameObject : MonoBehaviour, IRoleAnimate, IRoleDialogue, IRoleSprite
    {
        // 回收池
        private static RoleGameObject pool = null;
        RoleGameObject next;

        // 动画的代理
        private IRoleAnimate roleAnimate = new RoleAnimateImpl();

        // 对话的代理
        private IRoleDialogue roleDialogue = new RoleDialogueImpl();

        // 角色Sprite代理
        private IRoleSprite roleSprite = new RoleSpriteImpl();

        //对话信息
        private DialogueInfo _dialogue;

        private RoleGameObject()
        {

        }

        // 创建一个新的 角色Object
        public static RoleGameObject Create(DialogueInfo dialgueInfo)
        {
            if (pool != null)
            {
                RoleGameObject obj = pool;
                pool = obj.next;
                obj.next = null;
                obj.setDialogue(dialgueInfo);
                return obj;
            }
            RoleGameObject objNew = new RoleGameObject();
            objNew.setDialogue(dialgueInfo);
            return objNew;
        }

        // 回收当前对象
        public void Recycle()
        {
            next = pool;
            pool = this;
        }

        //设置对话信息
        public void setDialogue(DialogueInfo dialgueInfo)
        {
            this._dialogue = dialgueInfo;
            if (roleDialogue != null)
            {
                roleDialogue.setDialogue(dialgueInfo);
            }
        }

        // idle动画
        public void animateIdle()
        {
            if (roleAnimate != null)
            {
                roleAnimate.animateIdle();
            }
        }
        // 说话动画
        public void animateSpeak()
        {
            if (roleAnimate != null)
            {
                roleAnimate.animateSpeak();
            }
        }
        // 移动动画
        public void animateMove()
        {
            if (roleAnimate != null)
            {
                roleAnimate.animateMove();
            }
        }
        // 下一句话
        public bool speakNext()
        {
            if (roleDialogue != null)
            {
                return roleDialogue.speakNext();
            }
            return false;
        }

        public Sprite getSprite()
        {
            if (roleSprite != null)
            {
                return roleSprite.getSprite();
            }
            return null;
        }

    }
}