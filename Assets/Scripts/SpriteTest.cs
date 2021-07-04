using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customer;

public class SpriteTest : MonoBehaviour
{

    //精灵的图片地址
    public string spritePath;
    // Start is called before the first frame update
    void Start()
    {
        GameObject sprite = getSpriteFromPath(spritePath, 200);
    }

    private GameObject getSpriteFromPath(string path, int width)
    {
        // 创建GameObject对象
        GameObject gameObj = new GameObject();
        // 获取SpriteRenderer对象
        SpriteRenderer spr = gameObj.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        spr.sprite = ResLoader.LoadSpriteFromPath(path);
        // 移动位置
        spr.transform.position = new Vector2(0, 0);
        float d = width / (float)spr.sprite.rect.width;
        spr.transform.localScale = new Vector2(d, d);
        return gameObj;
    }
}
