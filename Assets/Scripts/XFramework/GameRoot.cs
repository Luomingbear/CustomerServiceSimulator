using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework.Extend;

namespace XFramework
{
    [RequireComponent(typeof(AudioManager))]
    public class GameRoot : MonoBehaviour
    {
        private static GameRoot instance;
        public static GameRoot Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<GameRoot>();
                return instance;
            }
        }
        /// <summary>
        /// 场景管理器
        /// </summary>
        private SceneSystem sceneSystem;
        /// <summary>
        /// 面板管理器
        /// </summary>
        private PanelManager panelManager;
        /// <summary>
        /// 加载场景时显示的进度条面板名称
        /// </summary>
        [Header("加载场景时显示的进度条面板名称")]
        public string loadPanelName = "AsyncLoadPanel";
        /// <summary>
        /// 面板管理器
        /// </summary>
        public PanelManager PanelManager { get => panelManager; }

        protected virtual void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
            sceneSystem = new SceneSystem();
            DontDestroyOnLoad(gameObject);
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            GameObject asyncLoadPanel = GameObject.Find(loadPanelName);
            asyncLoadPanel?.transform.PanelAppearance(false);
            LoadScene(new MainScence());
        }

        /// <summary>
        /// 加载一个场景
        /// </summary>
        /// <param name="sceneState"></param>
        public void LoadScene(SceneState sceneState)
        {
            sceneSystem?.SetScene(sceneState);
        }

        /// <summary>
        /// 初始化面板管理器
        /// </summary>
        /// <param name="manager"></param>
        public void Initialize(PanelManager manager)
        {
            panelManager = manager;
        }
    }
}
