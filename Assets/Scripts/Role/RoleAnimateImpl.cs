using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    // 角色动画实现
    public class RoleAnimateImpl : IRoleAnimate
    {
        public GameObject GameObject { set; get; }

        // idle动画
        public void animateIdle()
        {
            Debug.Log(GameObject + "animateIdle");
        }

        // 说话动画
        public void animateSpeak()
        {
            Debug.Log(GameObject + "animateSpeak");
        }

        // 移动动画
        public void animateMove()
        {
            Debug.Log(GameObject + "animateMove");
        }
    }
}