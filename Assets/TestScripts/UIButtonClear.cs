using UnityEngine;
using UnityEngine.UI;

public class UIButtonClear : MonoBehaviour
{
    public InputField inputField=null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnClick()
    {
        if (inputField==null)
        {
            print("ParameterError");
            return;
        }

        inputField.text = "";
        
    }
}
