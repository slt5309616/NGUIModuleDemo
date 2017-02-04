using UnityEngine;
using System.Collections;

public class TestRotation : MonoBehaviour {
    public Transform targetPos;
    public Transform fromPos;
    public float time = 5.0f;
	// Use this for initialization
    public float speed = 0.1F;
    private Vector3 distanceTotal;
    private Vector3 distance=new Vector3(0,0,0);
	void Start () {
        transform.LookAt(fromPos.position);
	}
	
	// Update is called once per frame
	void Update () {
        distanceTotal = fromPos.position - targetPos.position;

        distance += (distanceTotal / (time*60));
        var dif = fromPos.position - distance;
        transform.LookAt(distance.sqrMagnitude < distanceTotal.sqrMagnitude ? dif : targetPos.position);
//        transform.LookAt(targetPos.position);
//        var relativeRotate = targetPos.position - transform.position;
//        transform.rotation = Quaternion.LookRotation(relativeRotate);
//        transform.rotation = Quaternion.FromToRotation(fromPos.position, targetPos.position);
//        transform.rotation = Quaternion.Lerp(fromPos.rotation, targetPos.rotation, Time.time*speed);
//        transform.rotation = Quaternion.identity;
	}
}
