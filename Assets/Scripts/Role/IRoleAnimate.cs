using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    // 角色动画接口
    public interface IRoleAnimate
    {
        // idle动画
        void animateIdle();

        // 说话动画
        void animateSpeak();

        // 移动动画
        void animateMove();
    }
}