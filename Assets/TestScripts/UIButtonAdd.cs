using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonAdd : MonoBehaviour {
    public InputField inputField;
    public GameObject panel;
    public Text _text;

    private RectTransform content;
    float _areaWidth;
    float _areaHeight;
	// Use this for initialization
	void Start () {
        content = GameObject.Find("Panel").GetComponent<RectTransform>();
        _areaWidth = panel.GetComponent<RectTransform>().rect.width;
        _areaHeight = panel.GetComponent<RectTransform>().rect.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnClick()
    {
        if (content == null)
        {
            print("scrollViewContent is null");
            return;
        }
        if (inputField == null)
        {
            print("inputField is null");
            return;
        }
        if (panel == null)
        {
            print("scrollView is null");
            return;
        }

        var textTemp = (Text)Instantiate(_text);
        var go = new GameObject();
        textTemp.transform.SetParent(go.transform);
        var randomPostion = new Vector3(Random.Range(-_areaWidth / 2.0f, _areaWidth / 2.0f), Random.Range(-_areaHeight / 2.0f, _areaHeight / 2.0f), 0);
        print(randomPostion);
        go.GetComponent<Transform>().SetParent(content.GetComponent<Transform>(), true);
//        _text.transform.position = transform.TransformPoint(randomPostion);

        go.transform.localPosition = randomPostion;
        
    }
}
