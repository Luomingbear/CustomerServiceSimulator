using System;
using System.Collections;
using UnityEngine;
using System.IO;

namespace Customer
{
    public static class ResLoader
    {

        //同步下载资源
        public static WWW DownloadSync(string path, WWWForm form = null)
        {
            WWW www;
            if (form != null)
                www = new WWW(path, form);
            else
                www = new WWW(path);

            YieldToStop(www);

            return www;
        }

        //yield 下载资源
        private static void YieldToStop(WWW www)
        {
            var @enum = DownloadEnumerator(www);
            while (@enum.MoveNext()) ;
        }

        private static IEnumerator DownloadEnumerator(WWW www)
        {
            while (!www.isDone) ;
            yield return www;
        }

        /// <summary>
        /// 运行模式下Texture转换成Texture2D
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public static Texture2D TextureToTexture2D(Texture texture)
        {
            Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
            Graphics.Blit(texture, renderTexture);

            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture2D.Apply();

            RenderTexture.active = currentRT;
            RenderTexture.ReleaseTemporary(renderTexture);

            return texture2D;
        }

        //从path加载图片为sprite
        public static Sprite LoadSpriteFromPath(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
            {
                return null;
            }
            Texture texture = ResLoader.DownloadSync("file://" + fileInfo.FullName).texture;
            if (texture == null)
            {
                Debug.LogError("texture is NULL!");
                return null;
            }
            Texture2D texture2D = ResLoader.TextureToTexture2D(texture);
            Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
    }
}