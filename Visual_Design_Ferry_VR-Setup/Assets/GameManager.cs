using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using GD.MinMaxSlider;

public enum States { TOP, ORBIT };
public enum Topics {NONE, COMMUNICATION, lIDAR, DRIVETRAIN, BATTERY, RADAR, AIR };
public enum OrbitViewLevel { TOP, MIDDLE, BOTTOM };
public enum OrbitViewQuarter { Q1, Q2, Q3, Q4 };



public class GameManager : MonoBehaviour
{
    float xAngle = 0;
    float yAngle = 0;

    public GameObject closeBtn;

    public static GameManager Instance { get; private set; }

    [MinMaxSlider(-90,90)]
    public Vector2 orbitMiddleView = new Vector2(-10, 20);

    // events

    private OrbitViewLevel _orbitViewLevel;
    public OrbitViewLevel orbitViewLevel
    {
        get { return _orbitViewLevel; }
        set
        {
            if (_orbitViewLevel == value || topic != Topics.NONE) return;
            _orbitViewLevel = value;
            if (OnOrbitViewLevelChange != null )
                OnOrbitViewLevelChange(_orbitViewLevel);
        }
    }
    public delegate void OnOrbitViewLevelChangeDelegate(OrbitViewLevel newVal);
    public event OnOrbitViewLevelChangeDelegate OnOrbitViewLevelChange;

    private OrbitViewQuarter _orbitViewQuarter;
    public OrbitViewQuarter orbitViewQuarter
    {
        get { return _orbitViewQuarter; }
        set
        {
            if (_orbitViewQuarter == value || topic != Topics.NONE) return;
            _orbitViewQuarter = value;
            if (OnOrbitViewQuarterChange != null)
                OnOrbitViewQuarterChange(_orbitViewQuarter);
        }
    }
    public delegate void OnOrbitViewQuarterChangeDelegate(OrbitViewQuarter newVal);
    public event OnOrbitViewQuarterChangeDelegate OnOrbitViewQuarterChange;


    private Topics _topic;
    public Topics topic
    {
        get { return _topic; }
        set
        {
            if (_topic == value) return;
            _topic = value;
            closeBtn.SetActive(topic!=Topics.NONE);
            if (OnTopicChange != null)
                OnTopicChange(_topic);
        }
    }
    public delegate void OnTopicChangeDelegate(Topics newVal);
    public event OnTopicChangeDelegate OnTopicChange;

    void Awake()
    {
        if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }

    public void setXAngle(float _xAngle)
    {
        xAngle = _xAngle;
        if (xAngle > orbitMiddleView[0] && xAngle < orbitMiddleView[1])
        {
            orbitViewLevel = OrbitViewLevel.MIDDLE;
        }else if(xAngle< orbitMiddleView[0])
        {
            orbitViewLevel = OrbitViewLevel.BOTTOM;
        }
        else
        {
            orbitViewLevel = OrbitViewLevel.TOP;
        }
    }

    public void setYAngle(float _yAngle)
    {
        float q = 360 / 4;
        yAngle = _yAngle;
        orbitViewQuarter = (OrbitViewQuarter)Mathf.Floor(yAngle / q);
    }

    public void closeTopic()
    {
        topic = Topics.NONE;
    }
}
