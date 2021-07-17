using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customer;

//角色行为测试类
public class RoleTest : MonoBehaviour
{
    // 客户角色prefab的地址
    public string rolePrefabPath = "";
    // Start is called before the first frame update
    public ReadCsv readCsv = new ReadCsv();

    private void setDialogueInfo(RoleBehavior roleBehavior)
    {
        readCsv.filePath = "Assets/Dialogue/DialogueTest.csv";
        readCsv.start();
        List<RoleDialogueInfo> roleList = readCsv.GetDialogueList();
        RoleDialogueInfo roleDialogueInfo = roleList[0];
        roleBehavior.setDialogueInfos(roleDialogueInfo);
        Debug.Log("设置 RoleDialogueInfo：" + roleDialogueInfo.GetDialogueDic().Count);
    }

    void Start()
    {
        // 创建一个role
        Camera camera = Camera.main;
        Vector3 v3 = camera.ViewportToWorldPoint(new Vector3(-0.5F, 0, camera.nearClipPlane));
        GameObject obj = Instantiate(Resources.Load(rolePrefabPath, typeof(GameObject)), v3, Quaternion.identity) as GameObject;
        // 设置对话信息
        RoleBehavior roleBehavior = obj.GetComponentInChildren<RoleBehavior>(true) as RoleBehavior;
        setDialogueInfo(roleBehavior);
    }
}
