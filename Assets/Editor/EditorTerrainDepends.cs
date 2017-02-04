using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;
public class EditorTerrainDepends  {

    [MenuItem("Assets/Show Texture Property")]
    private static void ShowTerrainTextureProperty() {
        var tarObj = Selection.activeObject;

        var objs = AssetDatabase.GetDependencies(new string []{AssetDatabase.GetAssetPath(tarObj)});
        var itr = objs.GetEnumerator();
        for (int i = 0; itr.MoveNext();i++ ) {
            
            if (itr.Current.ToString().IndexOf(".tga") >= 0) {
                TextureImporter ti = (TextureImporter)TextureImporter.GetAtPath(itr.Current.ToString());
                Debug.Log(itr.Current.ToString()+" is "+ti.isReadable);
            }
            
        }

    }
    [MenuItem("Assets/Show Texture Property", true)]
    private static bool NewMenuOptionValidation() {
        if (Selection.activeObject==null){
            return false;
        }
        bool isPrefab = PrefabUtility.GetPrefabType(Selection.activeObject) == PrefabType.Prefab;
        bool isTerrain = Selection.activeObject.name.IndexOf("Terrain")>=0;
        return isPrefab && isTerrain;
    }
    [MenuItem("Edit/Export Current Scene")]
    private static void ExportScene() {
        GameObject[] goList = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject));;
        var itr = goList.GetEnumerator();
        Debug.Log(goList.Length);
        List<Object> prefabList = new List<Object>();
        for (int i = 0; itr.MoveNext(); i++) {
            var go = (GameObject)itr.Current;
            if (PrefabUtility.GetPrefabType(go)==PrefabType.PrefabInstance) {
                var obj = PrefabUtility.GetPrefabParent(go);
                if (prefabList.Contains(obj)) {
                    continue;
                }
                prefabList.Add(obj);
            }
        }

        
        List<string> prefabPathList = new List<string>();
        var itrPrefabList = prefabList.GetEnumerator();
        while (itrPrefabList.MoveNext()) {
            var prefabPath = AssetDatabase.GetAssetPath((Object)itrPrefabList.Current);
            prefabPathList.Add(prefabPath);
        }
       
        var dependPathList = AssetDatabase.GetDependencies(prefabPathList.ToArray());
        Debug.Log(prefabList.Count);
        Debug.Log(dependPathList.Length);
        
        string prefix = "E:/Test";
        var itr3 = dependPathList.GetEnumerator();
        while (itr3.MoveNext()) {
            var filePath = (string)itr3.Current;
            filePath =filePath.Remove(0, filePath.IndexOf("/"));
            var folderPath = filePath.Remove(filePath.LastIndexOf("/"),filePath.Length-filePath.LastIndexOf("/"));
            if (!Directory.Exists(prefix+folderPath)) {
                Directory.CreateDirectory(prefix + folderPath);
            }
            File.Copy(Application.dataPath + filePath, prefix + filePath, true);
            File.Copy(Application.dataPath + filePath + ".meta", prefix + filePath + ".meta", true);
        }
    }
}
