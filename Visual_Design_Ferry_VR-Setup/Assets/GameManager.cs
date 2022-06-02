using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using GD.MinMaxSlider;

public enum States { TOP, ORBIT };
public enum OrbitView { TOP, MIDDLE, BOTTOM };



public class GameManager : MonoBehaviour
{
    float xAngle = 0;

    public static GameManager Instance { get; private set; }

    [MinMaxSlider(-90,90)]
    public Vector2 orbitMiddleView = new Vector2(-10, 20);
    
    private OrbitView _orbitView;
    public OrbitView orbitView
    {
        get { return _orbitView; }
        set
        {
            if (_orbitView == value) return;
            _orbitView = value;
            if (OnOrbitViewChange != null)
                OnOrbitViewChange(_orbitView);
        }
    }
    public delegate void OnOrbitViewChangeDelegate(OrbitView newVal);
    public event OnOrbitViewChangeDelegate OnOrbitViewChange;

    void Awake()
    {
        if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }

    public void setXAngle(float _xAngle)
    {
        xAngle = _xAngle;
        if (xAngle > orbitMiddleView[0] && xAngle < orbitMiddleView[1])
        {
            orbitView = OrbitView.MIDDLE;
        }else if(xAngle< orbitMiddleView[0])
        {
            orbitView = OrbitView.BOTTOM;
        }
        else
        {
            orbitView = OrbitView.TOP;
        }
    }
}
