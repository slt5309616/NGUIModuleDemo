using UnityEngine;
using System.Collections;
using System.Text;

public class LiuJieLevelSp : MonoBehaviour {

	// Use this for initialization
    LiuJieTableCtrl _ctrl;
    UISprite _sprite;

    public UIPanel panel;
	void Start () {
        _ctrl = panel.GetComponentInChildren<LiuJieTableCtrl>();
        _sprite = GetComponent<UISprite>();
        if (_ctrl == null)
	    {
		    print("Getting LiuJieTableCtrl in LevelSp Error");
	    }
        EventDelegate.Add(_ctrl.onIndexChange, OnIndexChange);
	}

    public void OnIndexChange()
    {
        var temp =  _ctrl._myIndex+1;
        _sprite.spriteName = temp.ToString();

        var tempSprite = _sprite.atlas.GetSprite(temp.ToString());
        if (tempSprite!=null)
        {
            _sprite.width = tempSprite.width;
            _sprite.height = tempSprite.height;
        }
        else
        {
            print("Get Sprite in LevelSp fail");
        }
    }
}
