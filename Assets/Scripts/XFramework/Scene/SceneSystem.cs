using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework
{
    /// <summary>
    /// 场景状态管理系统
    /// </summary>
    public class SceneSystem
    {
        SceneState state;

        /// <summary>
        /// 设置场景
        /// </summary>
        /// <param name="sceneState"></param>
        public void SetScene(SceneState sceneState)
        {
            state?.OnExit();
            state = sceneState;
            state?.OnEnter();
        }
    }
}
