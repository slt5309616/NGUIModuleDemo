using UnityEngine;
using System.Collections;

public class TestCharactorControl : MonoBehaviour
{
    Animator animator;
    AnimatorStateInfo stateinfo;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        stateinfo = animator.GetCurrentAnimatorStateInfo(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            print("W1");
            animator.CrossFade("walk",0.1f);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            print("W2");
            animator.CrossFade("Idle", 0.1f);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            print("S1");
            animator.SetBool("Dead",true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetFloat("Direction", 0.1f);
            animator.CrossFade("getHit", 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetFloat("Direction", 0.9f);
            animator.CrossFade("getHit", 0.1f);
        }
	}
}
