using UnityEngine;
using System.Collections;

public class LiuJieSwithBtn : MonoBehaviour {
    UIButton _btn; 
	// Use this for initialization
	void Start () {
        _btn = GetComponent<UIButton>();
        if (_btn==null)
        {
            print("getting UIButton error in "+this.name);
        }
	}
    public void OnInSufficientNum()
    {
        _btn.isEnabled = false;
    }
    public void OnSufficientNum()
    {
        _btn.isEnabled = true;
    }
}
