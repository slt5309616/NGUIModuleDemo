using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LiuJieTimeLabel : MonoBehaviour {

    public float timeInterval;
    float _timeLeft;
    float _clickTime;
    bool _isCounting;
    bool _isOneTimeCountingEnd;
    UILabel _timeLabel;
    public UILabel _numLabel;

    [HideInInspector]
    public List<EventDelegate> onCountDownEnd = new List<EventDelegate>();
	// Use this for initialization
	void Start () {
        timeInterval += 0.9f;
        _timeLeft = timeInterval;
        _isCounting = false;
        _isOneTimeCountingEnd = true;
        _timeLabel = GetComponent<UILabel>();
        if (_timeLabel==null)
	    {
		    print("getting _timeLabel error in "+this.name);
    	}
        var _numLabelCtrl = _numLabel.GetComponent<LiuJieNumLabel>();
        if (_numLabel==null)
	    {
		    print("getting numLabel error in "+this.name);
	    }
        EventDelegate.Add(onCountDownEnd, _numLabelCtrl.OnCountDownEnd);
	}
	
	// Update is called once per frame
	void Update () {
        if (_isCounting)
        {
            if (_timeLeft>0)
            {
                DoCountDown();
            }
            else
            {
                EventDelegate.Execute(onCountDownEnd);
                _timeLeft = timeInterval;
                _clickTime = Time.time;
                _isOneTimeCountingEnd = true;
            }
        }
	}
    public void OnTimesIsNotFull()
    {
        _isCounting = true;
    }
    void DoCountDown()
    {
        _timeLabel.text = ((int)(_timeLeft / (60 * 60))).ToString() + ":"
                            + ((int)((_timeLeft / 60) % 60)).ToString() + ":"
                            + ((int)(_timeLeft % 60)).ToString();
        _timeLeft = timeInterval -(Time.time - _clickTime);
    }
    public void OnTimesIsFull()
    {
        _isCounting = false;
        _timeLabel.text = "";
    }
}
