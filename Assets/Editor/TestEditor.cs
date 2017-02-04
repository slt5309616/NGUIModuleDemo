using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Runtime.InteropServices;
public class TestEditor : EditorWindow
{
    string _fileName = "";
    string _recordButtonStr = "Record";
    string _screenShotButtonStr = "ScreenShot";
    string _recordStatusStr = "";
    string _screenShotStatusStr = "";
    string _createAssetBundleStatusStr = "";
    string _loadABStatusStr = "";
    int _capturedFrame = 0;
    float _lastRecordFrameTime = 0.0f;
    float _lastScreenShotFrameTime = 0.0f;
    bool _isRecording = false;
    bool _sreenShoting = false;
    bool _hasNewScreenShots = false;
    Object[] _selectedAsset = null;
    [MenuItem ("Window/Do Something")]
    static void Initialize()
    {
        TestEditor window = EditorWindow.GetWindow<TestEditor>("My Empty Window");
    }
    void OnGUI()
    {
        _fileName = EditorGUILayout.TextField("FileName:", _fileName);

        if (GUILayout.Button(_recordButtonStr))
        {
            if (_isRecording)
            {
                _capturedFrame = 0;
                _recordButtonStr = "Recording";
                _isRecording = false;
            }
            else
            {
                _isRecording = true;
                _recordButtonStr = "Stop";
            }
        }
        EditorGUILayout.LabelField("Recording Status:", _recordStatusStr);

        if (GUILayout.Button(_screenShotButtonStr))
        {
            _sreenShoting = true;            
        }
        EditorGUILayout.LabelField("ScreenShot Status:", _screenShotStatusStr);

        if (GUILayout.Button("Create AssetBundle"))
        {
            _selectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            if (_selectedAsset.Length==0)
            {
                _createAssetBundleStatusStr = "No File Selected";
            }
            else
            {
                _createAssetBundleStatusStr = "";
                doCreateAssetBundle(_selectedAsset);
            }
        }
        EditorGUILayout.LabelField("Create Status:", _createAssetBundleStatusStr);

        if (GUILayout.Button("Load AssetBundle"))
        {
            OpenFileName ofn = new OpenFileName();
            ofn.structSize = Marshal.SizeOf(ofn);

            ofn.filter = "All Files\0*.*\0\0";

            ofn.file = new string(new char[256]);

            ofn.maxFile = ofn.file.Length;

            ofn.fileTitle = new string(new char[64]);

            ofn.maxFileTitle = ofn.fileTitle.Length;
            string path = Application.streamingAssetsPath;
            path = path.Replace('/', '\\');
            Debug.Log(path);
            //默认路径  
            ofn.InitialDirectory = path;
            //ofn.InitialDirectory = "D:\\MyProject\\UnityOpenCV\\Assets\\StreamingAssets";  
            ofn.title = "Open Project";

            ofn.defExt = "assetbundle";//显示文件的类型  
            //注意 一下项目不一定要全选 但是0x00000008项不要缺少  
            //OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;  

            if (OpenFileDll.GetOpenFileName(ofn))
            {
                Debug.Log("Selected file with full path: {0}" + ofn.file);
            }  
            _loadABStatusStr = "";
            doLoadAB(ofn.file);
        }
        EditorGUILayout.LabelField("Load Status:", _loadABStatusStr);
    }
    void Update()
    {
        if (_isRecording)
        {
            if (EditorApplication.isPlaying&&!EditorApplication.isPaused)
            {
                doRecord();
                Repaint();
            }
            else
            {
                _recordStatusStr = "Waiting Game to start";
            }
            
        }
        if (_sreenShoting)
        {
            if (EditorApplication.isPlaying && !EditorApplication.isPaused)
            {
                _screenShotStatusStr = "";
                doScreanShot();
                Repaint();
				_sreenShoting = false;
            }
            else
            {
                _screenShotStatusStr = "Game is not running";
                
            }

        }
        if (_hasNewScreenShots)
        {
            if (Time.time - _lastScreenShotFrameTime > 2)
            {
                _screenShotStatusStr = "";
                Repaint();
                _hasNewScreenShots = false;
            }
        }

    }
    void doRecord()
    {
        if (_lastRecordFrameTime<Time.time+Time.deltaTime)
        {
            _recordStatusStr = "CaupturedFrame" + _capturedFrame;
            Application.CaptureScreenshot(_fileName+" "+_capturedFrame+".png");
            _capturedFrame++;
            _lastRecordFrameTime = Time.time;
        }
    }
    void doScreanShot()
    {
        Application.CaptureScreenshot(_fileName + " " + _capturedFrame + ".png");
        _hasNewScreenShots = true;
        _screenShotStatusStr = "Complete";
        _lastScreenShotFrameTime = Time.time;
        _sreenShoting = false;
    }
    void doCreateAssetBundle(Object[] selectedAsset)
    {
        // one asset per bundle
        IEnumerator itr = selectedAsset.GetEnumerator();
        while (itr.MoveNext())
        {
            string sourcePath = AssetDatabase.GetAssetPath((Object)itr.Current);
            Debug.Log(sourcePath);

            string targetPath = Application.dataPath + "/StreamingAssets/" + ((Object)itr.Current).name + ".assetbundle";
            if (BuildPipeline.BuildAssetBundle((Object)itr.Current, null, targetPath,BuildAssetBundleOptions.UncompressedAssetBundle,BuildTarget.StandaloneWindows))
            {
                Debug.Log(((Object)itr.Current).name + "资源打包成功");
                _createAssetBundleStatusStr = "资源打包成功";
            }
            else
            {
                Debug.Log(((Object)itr.Current).name + "资源打包失败");
                _createAssetBundleStatusStr = "资源打包失败";
            }
        }
        //all in one bundle
        //string path = Application.dataPath + "/StreamingAssets/ALL.assetbundle";
        //if(BuildPipeline.BuildAssetBundle(null, selectedAsset, path, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows)){
        //    AssetDatabase.Refresh();
        //    Debug.Log("资源打包成功");
        //    _createAssetBundleStatusStr = "资源打包成功";
        //}
        //else
        //{
        //    Debug.Log(((Object)itr.Current).name + "资源打包失败");
        //    _createAssetBundleStatusStr = "资源打包失败";
        //}
    }
    void doLoadAB(string path)
    {
        AssetBundle.CreateFromFile(path);
    }
}
