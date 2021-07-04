using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer{

    //客户行为的接口
    public interface IRoleBehavior{

        // 获取客户obj
        RoleGameObject GetRoleObject();

        //加入队伍
        void JoinQueue();

        //离开队伍
        void LeaveQueue();

        // 每一帧的刷新回调
        void Update();
    }
}