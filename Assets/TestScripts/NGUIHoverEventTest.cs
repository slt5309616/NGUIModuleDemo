using UnityEngine;
using System.Collections;

public class NGUIHoverEventTest : UIWidgetContainer {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void OnClick()
    {
        print("111");
    }
    void OnHover(bool isOver)
    {
        print(isOver);
    }
}
