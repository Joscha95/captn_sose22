using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JB;

public class topicManager : MonoBehaviour, ITopicController
{
    public OrbitView CameraLevel;
    public GameObject[] elements;

    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnOrbitViewChange += onOrbitViewChange;
    }

    void onOrbitViewChange(OrbitView newVal)
    {
        active = newVal == CameraLevel;

        foreach (var ele in elements)
        {
            ele.SetActive(active);
        }

        if (active)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    public void Activate()
    {

    }

    public void Deactivate()
    {

    }

    public void Reset()
    {

    }
}
