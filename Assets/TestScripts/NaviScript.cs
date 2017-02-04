using UnityEngine;
using System.Collections;

public class NaviScript : MonoBehaviour {
    public Transform targetObj = null;
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
            if (targetObj != null)
            {
                print("A");
                
                agent.areaMask = 9;
                agent.destination = targetObj.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            
            if (targetObj != null)
            {
                print("D");
                GetComponent<NavMeshAgent>().areaMask = 17;
                GetComponent<NavMeshAgent>().destination = targetObj.position;
            }
        }
	}
}
