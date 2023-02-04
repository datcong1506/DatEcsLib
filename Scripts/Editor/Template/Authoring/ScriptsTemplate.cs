using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace DatECSLib
{
    internal class BakerScriptsTemplate
    {
        private static readonly string tailName = "_Authoring";
        private static readonly string _path = "Assets/DatECSLib/Scripts/Editor/Template/Authoring/AuthoringTXT.txt";

        [MenuItem("Assets/Create/ECS/Authoring", false, 0)]
        public static void CreateComponent()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                ScriptableObject.CreateInstance<DoCreateFactoryScriptAsset>(),
                "New" + tailName + ".cs",
                (Texture2D)EditorGUIUtility.IconContent("cs Script Icon").image,
                _path);
        }

        private class DoCreateFactoryScriptAsset : EndNameEditAction
        {



            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                string text = File.ReadAllText(resourceFile);
                string fileName = Path.GetFileName(pathName);
                {
                    string newName = fileName.Replace(" ", "");
                    if (!newName.Contains(tailName))
                        newName = newName.Insert(fileName.Length - 3, tailName);
                    pathName = pathName.Replace(fileName, newName);
                    fileName = newName;
                }

                string fileNameWithoutExtension = fileName.Substring(0, fileName.Length - 3);
                text = text.Replace("#SCRIPTNAME#", fileNameWithoutExtension);

                string runtimeName = fileNameWithoutExtension.Replace(tailName, "");
                text = text.Replace("#RUNTIMENAME#", runtimeName);

                for (int i = runtimeName.Length - 1; i > 0; i--)
                    if (char.IsUpper(runtimeName[i]) && char.IsLower(runtimeName[i - 1]))
                        runtimeName = runtimeName.Insert(i, " ");

                text = text.Replace("#RUNTIMENAME_WITH_SPACES#", runtimeName);

                string fullPath = Path.GetFullPath(pathName);
                var encoding = new UTF8Encoding(true);
                File.WriteAllText(fullPath, text, encoding);
                AssetDatabase.ImportAsset(pathName);
                ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object)));
            }
        }

    }
}

