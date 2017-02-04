using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LiuJieNumLabel : MonoBehaviour {

    public List<EventDelegate> onInSufficientNum = new List<EventDelegate>();
    public List<EventDelegate> onTimesIsFull = new List<EventDelegate>();
    public List<EventDelegate> onSufficientNum = new List<EventDelegate>();
    public UIButton switchBtn;
    public UILabel timeLabel;
    UILabel _numLabel;
    public int totalTimes;
    int _timesLeft;
	// Use this for initialization
	void Start () {
        var timeLabelCtrl = timeLabel.GetComponent<LiuJieTimeLabel>();
        if (timeLabelCtrl == null)
        {
            print("getting timeLabelCtrl error in " + this.name);
        }
        var switchBtnCtrl = switchBtn.GetComponent<LiuJieSwithBtn>();
        EventDelegate.Add(onTimesIsFull, timeLabelCtrl.OnTimesIsFull);
        EventDelegate.Add(onSufficientNum, timeLabelCtrl.OnTimesIsNotFull);
        EventDelegate.Add(onSufficientNum, switchBtnCtrl.OnSufficientNum);
        EventDelegate.Add(onInSufficientNum, switchBtnCtrl.OnInSufficientNum);

        _numLabel = GetComponent<UILabel>();
        _timesLeft = totalTimes;
        _numLabel.text = _timesLeft.ToString();
	}
	
	// Update is called once per frame
    void Update()
    {
        _numLabel.text = _timesLeft.ToString();
    }
    public void OnCountDownEnd()
    {
        _timesLeft++;
        if (_timesLeft >= totalTimes)
        {
            _timesLeft = totalTimes;
            EventDelegate.Execute(onTimesIsFull);
        }
        else
        {
            EventDelegate.Execute(onSufficientNum);
        }
    }
    public void OnSwitchBtnClicked()
    {
        _timesLeft--;
        if (_timesLeft<=0)
        {
            EventDelegate.Execute(onInSufficientNum);
        }
        else
        {
            EventDelegate.Execute(onSufficientNum);
        }
    }
}
