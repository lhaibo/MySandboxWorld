using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

using static System.IO.Directory;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;

namespace SWFrameWork.Tools
{
    public static class Setup
    {
        [MenuItem("SWFrameWork/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Folders.CreateDefault("_Project", "Animation", "Art", "Materials", "Prefabs", "ScriptableObjects",
                "_Scripts", "Settings", "Scene","Fonts");

            Refresh();
        }
        
        static class Folders
        {
            public static void CreateDefault(string root,params string[] folders)
            {
                string fullPath = Combine(Application.dataPath, root);
                foreach (var folder in folders)
                {
                    string path = Combine(fullPath, folder);
                    if (!Exists(path))
                    {
                        CreateDirectory(path);
                    }
                }
            }
        }
        
        [MenuItem("SWFrameWork/Setup/Create Default Fonts Txt")]
        public static void CreateDefaultFont()
        {
            string path = Combine(Application.dataPath, "_Project/Fonts/ChineseCharacters.txt");
            if (!Exists(path))
            {
                CreateCnFontAsset(path);
                Refresh();
            }
        }

        private static void CreateCnFontAsset(string path)
        {
            // 设置文件保存路径

            // 使用StringBuilder来构建字符集
            StringBuilder sb = new StringBuilder();

            // 添加基本汉字区块的字符
            for (int code = 0x4E00; code <= 0x9FFF; code++)
            {
                sb.Append(char.ConvertFromUtf32(code));
            }

            // 添加扩展-A区块的字符
            for (int code = 0x3400; code <= 0x4DBF; code++)
            {
                sb.Append(char.ConvertFromUtf32(code));
            }

            // 将字符集写入文件
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
            
            Refresh();
        }
    }

}
