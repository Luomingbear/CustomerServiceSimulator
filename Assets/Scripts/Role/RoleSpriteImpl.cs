using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customer;

namespace Customer
{
    // 橘色Sprite的接口
    public class RoleSpriteImpl : IRoleSprite
    {
        private string _roleName = "";

        //获取本地图片访问地址
        private string getFullPath(string roleName)
        {
            return "/Users/bear/Downloads/" + roleName + ".png";
        }

        public void setRoleName(string roleName)
        {
            this._roleName = roleName;
        }

        // 获取 Sprite 对象
        public Sprite getSprite()
        {
            return ResLoader.LoadSpriteFromPath(getFullPath(_roleName));
        }
    }
}