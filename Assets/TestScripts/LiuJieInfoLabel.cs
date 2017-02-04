using UnityEngine;
using System.Collections;
using UnityEditor;

public class LiuJieInfoLabel : MonoBehaviour {
    public Texture2D cursorTexture;
    public string destination;
	// Use this for initialization
	void Start () {
	}



    void OnHover(bool isOver)
    {
        if (isOver)
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    void OnClick()
    {
        print("Im "+destination);
    }
}
