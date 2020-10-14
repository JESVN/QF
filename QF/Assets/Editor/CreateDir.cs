using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class CreateDir : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Assets/CreateFolder #&_b")]
    private static void GenerateFolder()
    {
        string prjPath = Application.dataPath + "/";
        Directory.CreateDirectory(prjPath + "Audio");
        Directory.CreateDirectory(prjPath + "Prefabs");
        Directory.CreateDirectory(prjPath + "Prefabs/Game");
        Directory.CreateDirectory(prjPath + "Prefabs/UIPrefabs");
        Directory.CreateDirectory(prjPath + "Materials");
        Directory.CreateDirectory(prjPath + "Resources");
        Directory.CreateDirectory(prjPath + "Scripts");
        Directory.CreateDirectory(prjPath + "Textures");
        Directory.CreateDirectory(prjPath + "Scenes");
        Directory.CreateDirectory(prjPath + "Animation");
        Directory.CreateDirectory(prjPath + "AnimationController");
        Directory.CreateDirectory(prjPath + "Plugins");
        Directory.CreateDirectory(prjPath + "Plugins/iOS");
        Directory.CreateDirectory(prjPath + "Plugins/Android");
        Directory.CreateDirectory(prjPath + "Editor");
        Directory.CreateDirectory(prjPath + "Effect");
        Directory.CreateDirectory(prjPath + "Meshes");
        Directory.CreateDirectory(prjPath + "Shaders");
        Directory.CreateDirectory(prjPath + "Config");
        Directory.CreateDirectory(prjPath + "Art");
        Directory.CreateDirectory(prjPath + "3rd");
        Directory.CreateDirectory(Application.streamingAssetsPath);
        AssetDatabase.Refresh();//刷新unity资源显示
        Debug.Log("Created Finished");
    }
#endif
}