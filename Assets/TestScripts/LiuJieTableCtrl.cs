using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LiuJieTableCtrl : MonoBehaviour {

    public int _newIndex
    {
        get;
        set;
    }
    public int _myIndex
    {
        get;
        private set;
    }
	// Use this for initialization
    
    public bool _needUpdate
    {
        get;
        set;
    }
    public int _spriteNum
    {
        get;
        private set;
    }
    public float scaleRate;

    Color darkColor = new Color(0.3f,0.3f,0.3f);
    Color lightColor = new Color(1, 1, 1);
    UITable _table = null;





    [HideInInspector]public List<EventDelegate> onIndexChange = new List<EventDelegate>();
	void Start () {
        _newIndex = 0;
        _needUpdate = true;
        _table = this.GetComponentInParent<UITable>();
        _spriteNum = _table.GetChildList().Count;

        if (_table==null)
        {
            print("TableCtrl getting Table Error");
            return;
        }
        var itr = _table.GetChildList().GetEnumerator();
        for (int i=1;itr.MoveNext();i++)
        {
            changeColor(itr.Current.GetComponent<UISprite>(), darkColor);

            var tempSp =itr.Current.GetChild(0).GetComponent<UISprite>();
            tempSp.spriteName = i.ToString();
            var tempSprite = tempSp.atlas.GetSprite(i.ToString());
            if (tempSprite != null)
            {
                tempSp.width = tempSprite.width;
                tempSp.height = tempSprite.height;
            }

            changeColor(tempSp, darkColor);
        }

	}
	
	// Update is called once per frame
	void Update () {
        //if newindex overflows ,reset it to 0
        if (_newIndex!=_myIndex)
        {
            if (_newIndex >= _spriteNum)
            {
                _newIndex -= _spriteNum;
            }
            _needUpdate = true;
        }
        if (_needUpdate)
        {
            
            List<Transform> childList = _table.GetChildList();

            //reset old sprite
            childList[_myIndex].localScale = new Vector3(1.0f, 1.0f);
            changeColor(childList[_myIndex].GetComponent<UISprite>(), darkColor);
            changeColor(childList[_myIndex].GetChild(0).GetComponent<UISprite>(), darkColor);

            //update current Sprite
            childList[_newIndex].localScale = new Vector3(scaleRate, scaleRate);
            changeColor(childList[_newIndex].GetComponent<UISprite>(), lightColor);
            changeColor(childList[_newIndex].GetChild(0).GetComponent<UISprite>(), lightColor);

            _table.repositionNow = true;
            _needUpdate = false;
            _myIndex = _newIndex;
            EventDelegate.Execute(onIndexChange);
        }
	}
    public void OnSwitchBtnClick()
    {
        if (_needUpdate==false)
        {
        	_newIndex++;
        }
    }
    public void OnResetBtnClick()
    {
        if (_needUpdate==false)
        {
            _newIndex = 0;
        }
    }
    void changeColor(UISprite sprite,Color color)
    {
        if (sprite != null)
        {
            sprite.color = color;
        }
        else
        {
            print("TableCtrl getting UISprite Error");
        }
    }
}
