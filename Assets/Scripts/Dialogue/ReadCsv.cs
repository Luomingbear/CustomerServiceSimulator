using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Customer
{
    public class ReadCsv
    {
        //csv文件的地址
        public string filePath;

        //Role,IsReturnGoods,TextId,Text,AddRage,Option1,Option1Jump,Option1Mood,Option2,Option2Jump,Option2Mood,Option3,Option3Jump,Option3Mood,Option4,Option4Jump,Option4Mood,NoOption,NoOptionJump,NoOptionMood
        private string KEY_Role = "Role", KEY_IsReturnGoods = "IsReturnGoods", KEY_TextId = "TextId", KEY_Text = "Text",
        KEY_AddRage = "AddRage", KEY_Option1 = "Option1", KEY_Option1Jump = "Option1Jump", KEY_Option1Mood = "Option1Mood",
         KEY_Option2 = "Option2", KEY_Option2Jump = "Option2Jump", KEY_Option2Mood = "Option2Mood", KEY_Option3 = "Option3",
         KEY_Option3Jump = "Option3Jump", KEY_Option3Mood = "Option3Mood", KEY_Option4 = "Option4", KEY_Option4Jump = "Option4Jump",
         KEY_Option4Mood = "Option4Mood", KEY_NoOption = "NoOption", KEY_NoOptionJump = "NoOptionJump", KEY_NoOptionMood = "NoOptionMood";

        // 默认标题位置
        private Dictionary<string, int> DEFAULT_TITLE_DIC = new Dictionary<string, int>
            {
                {"ole",0},{"IsReturnGoods",1},{"TextId",2},{"Text",3},{"AddRage",4},{"Option1",5},
                {"Option1Jump",6},{"Option1Mood",7},{"Option2",8},{"Option2Jump",9},{"Option2Mood",10},
                {"Option3",11},{"Option3Jump",12},{"Option3Mood",13},{"Option4",14},{"Option4Jump",15},
                {"Option4Mood",16},{"NoOption",17},{"NoOptionJump",18},{"NoOptionMood",19}
            };

        // 读取的标题位置
        private Dictionary<string, int> titleDic = new Dictionary<string, int>();

        // 对话列表
        private List<RoleDialogueInfo> dialogueList = new List<RoleDialogueInfo>();

        //游戏开始的时候就去读取配置信息
        private void Start()
        {
            start();
        }

        //获取解析到到对话信息
        public List<RoleDialogueInfo> GetDialogueList()
        {
            return dialogueList;
        }

        public void setFilePath(string filePath)
        {
            this.filePath = filePath;
        }

        //读取标题栏
        private Dictionary<string, int> readTitles(StreamReader streamReader)
        {
            Debug.Log("开始解析标题");
            string line = streamReader.ReadLine();
            string[] splits = line.Split(',');
            for (int i = 0; i < splits.Length; i++)
            {
                titleDic.Add(splits[i], i);
            }
            Debug.Log("标题解析完毕:" + line);
            return titleDic;
        }

        //读取一条回答信息
        private Answer getAnswer(string[] splits, string keyOption, string keyJump, string keyMood)
        {
            //option
            Answer answer = new Answer();
            int index = getTitleIndex(keyOption);
            if (index == -1)
            {
                return null;
            }
            else
            {
                string option = splits[index];
                if (option == null || option.Length == 0)
                {
                    return null;
                }
                answer.Option = option;
            }
            //jump
            index = getTitleIndex(keyJump);
            if (index != -1)
            {
                int jump = 0;
                int.TryParse(splits[index], out jump);
                if (jump != -1)
                {
                    answer.Jump = jump;
                }
            }
            // mood
            index = getTitleIndex(keyMood);
            if (index == -1)
            {
                answer.MoodNum = 0;
            }
            else
            {
                int mood = 0;
                int.TryParse(splits[index], out mood);
                answer.MoodNum = mood;
            }
            return answer;
        }

        //获取某一个标题在第几列
        private int getTitleIndex(string key)
        {
            if (titleDic.ContainsKey(key))
            {
                return titleDic[key];
            }
            return -1;
        }

        private DialogueInfo readLine(string line)
        {
            string[] splits = line.Split(',');

            DialogueInfo dialgueInfo = new DialogueInfo();
            // 角色信息
            int index = getTitleIndex(KEY_Role);
            if (index == -1)
            {
                Debug.LogError("没有找到角色信息:" + line);
                return null;
            }
            dialgueInfo.RoleName = splits[index];
            // textId
            index = getTitleIndex(KEY_TextId);
            if (index == -1)
            {
                Debug.LogError("没有找到TextId信息:" + line);
                return null;
            }
            int textId = 0;
            int.TryParse(splits[index], out textId);
            dialgueInfo.TextId = textId;
            // 问题信息
            index = getTitleIndex(KEY_Text);
            if (index == -1)
            {
                Debug.LogError("没有找到问题text信息:" + line);
                return null;
            }
            dialgueInfo.Text = splits[index];
            // 是否需要退货
            index = getTitleIndex(KEY_IsReturnGoods);
            if (index == -1)
            {
                Debug.LogWarning("没有找到退货信息:" + line);
                dialgueInfo.IsReturnGoods = false;
            }
            else
            {
                bool returnGoods = false;
                bool.TryParse(splits[index], out returnGoods);
                dialgueInfo.IsReturnGoods = returnGoods;
            }
            // 愤怒值
            index = getTitleIndex(KEY_AddRage);
            if (index == -1)
            {
                dialgueInfo.RageNum = 0;
                Debug.LogWarning("没有找到愤怒值信息:" + line);
            }
            else
            {
                int rage = 0;
                int.TryParse(splits[index], out rage);
                dialgueInfo.RageNum = rage;
            }
            List<Answer> answers = new List<Answer>();
            // 回答选项1
            Answer answer1 = getAnswer(splits, KEY_Option1, KEY_Option1Jump, KEY_Option1Mood);
            if (answer1 != null)
            {
                answers.Add(answer1);
            }
            // 回答选项2
            Answer answer2 = getAnswer(splits, KEY_Option2, KEY_Option2Jump, KEY_Option2Mood);
            if (answer2 != null)
            {
                answers.Add(answer2);
            }
            // 回答选项3
            Answer answer3 = getAnswer(splits, KEY_Option3, KEY_Option3Jump, KEY_Option3Mood);
            if (answer3 != null)
            {
                answers.Add(answer3);
            }
            // 回答选项4
            Answer answer4 = getAnswer(splits, KEY_Option4, KEY_Option4Jump, KEY_Option4Mood);
            if (answer4 != null)
            {
                answers.Add(answer4);
            }
            dialgueInfo.Answers = answers;
            // 无回答选项
            Answer answerNo = getAnswer(splits, KEY_NoOption, KEY_NoOptionJump, KEY_NoOptionMood);
            if (answer1 != null)
            {
                dialgueInfo.NoOptionAnwser = answerNo;
            }
            return dialgueInfo;
        }

        // 开始读取文件
        public void start()
        {
            try
            {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                StreamReader sr = new StreamReader(filePath);
                {
                    string line;
                    // 从文件读取并显示行，直到文件的末尾 
                    readTitles(sr);
                    Debug.Log("对话开始解析");
                    RoleDialogueInfo roleDialogueInfo = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        DialogueInfo dialgueInfo = readLine(line);
                        if (dialgueInfo == null)
                        {
                            continue;
                        }

                        // 如果roleDialogueInfo=null，或者角色名字有变化，则添加一个新角色
                        if (roleDialogueInfo == null || roleDialogueInfo.RoleName != dialgueInfo.RoleName)
                        {
                            roleDialogueInfo = new RoleDialogueInfo();
                            roleDialogueInfo.RoleName = dialgueInfo.RoleName;
                            roleDialogueInfo.AddDialogueInfo(dialgueInfo);
                            roleDialogueInfo.DialogueId = dialgueInfo.TextId;
                            dialogueList.Add(roleDialogueInfo);
                        }
                        // 否则说明是同一个角色
                        else
                        {
                            roleDialogueInfo.AddDialogueInfo(dialgueInfo);
                        }
                        // Debug.Log(line);
                    }
                    Debug.Log("对话解析完毕");
                }
            }
            catch (System.Exception e)
            {
                // 向用户显示出错消息
                Debug.LogError("The file could not be read:");
                Debug.LogError(e.Message);
            }
        }
    }
}