using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XFramework.Extend;

namespace XFramework
{
    /// <summary>
    /// 场景状态
    /// </summary>
    public abstract class SceneState
    {
        protected PanelManager panelManager;
        /// <summary>
        /// 场景名称
        /// </summary>
        protected string sceneName = "";
        /// <summary>
        /// 场景id
        /// </summary>
        protected int sceneIndex = -1;
        /// <summary>
        /// 是否异步加载
        /// </summary>
        protected bool async = false;
        /// <summary>
        /// 是否显示加载界面
        /// </summary>
        protected bool loadPanel = false;
        /// <summary>
        /// 加载界面
        /// </summary>
        protected GameRoot Game { get => GameRoot.Instance; }
        /// <summary>
        /// 加载界面名称
        /// </summary>
        protected string LoadPanelName { get => Game.loadPanelName; }

        public SceneState()
        {
            panelManager = new PanelManager();
        }

        /// <summary>
        /// 场景进入时
        /// </summary>
        public virtual void OnEnter()
        {
            if (!async)
                LoadScene();
            else
                LoadSceneAsync();
        }

        /// <summary>
        /// 场景退出
        /// </summary>
        public virtual void OnExit()
        {
            panelManager.PopAll();
        }

        /// <summary>
        /// 加载场景完毕后执行的方法
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        protected virtual void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Game.Initialize(panelManager);
            SceneManager.sceneLoaded -= SceneLoaded;
            Debug.Log($"{sceneName}场景加载完毕！");
        }

        /// <summary>
        /// 同步加载场景
        /// </summary>
        protected virtual void LoadScene()
        {
            if (sceneIndex < 0)
                SceneManager.LoadScene(sceneName);
            else
                SceneManager.LoadScene(sceneIndex);
            SceneManager.sceneLoaded += SceneLoaded;
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        protected virtual void LoadSceneAsync()
        {
            GameRoot.Instance.StartCoroutine(AsyncLoad());
            SceneManager.sceneLoaded += SceneLoaded;
        }
        
        /// <summary>
        /// 异步加载场景的实现
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator AsyncLoad()
        {
            AsyncOperation operation;

            if (sceneIndex < 0)
                operation = SceneManager.LoadSceneAsync(sceneName);
            else
                operation = SceneManager.LoadSceneAsync(sceneIndex);

            if (loadPanel)
            {
                operation.allowSceneActivation = false;
                GameObject panel = GameObject.Find(LoadPanelName);
                if (panel == null)
                {
                    Debug.LogError($"{LoadPanelName}面板不存在");
                    yield break;
                }
                panel.transform.PanelAppearance(true);
                Slider slider = panel.GetComponentInChildren<Slider>();
                slider.value = 0;
                float progressValue;

                while (!operation.isDone)
                {
                    if (operation.progress < 0.9f)
                        progressValue = operation.progress;
                    else
                        progressValue = 1.0f;

                    slider.value = progressValue;
                    if (progressValue >= 0.9f)
                    {
                        slider.value = 1f;
                        operation.allowSceneActivation = true;
                    }
                    yield return null;
                }
                panel.transform.PanelAppearance(false);
            }
            else
                operation.allowSceneActivation = true;
        }
    }
}
