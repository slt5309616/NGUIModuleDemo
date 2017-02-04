using UnityEngine;
using System.Collections;

public class TestCoroutine : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("1:"+Time.time);
        StartCoroutine(IamCoroutine());
        print("2:" + Time.time);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator IamCoroutine()
    {
        print("3:" + Time.time);
        yield return new WaitForSeconds(2);
        print("4:" + Time.time);
    }
}
