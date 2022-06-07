using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JB;

public class topicManager : MonoBehaviour, ITopicController
{
    public OrbitViewLevel cameraLevel;
    public OrbitViewQuarter cameraQuarter;
    public Topics topic;
    public GameObject marker;

    bool visible = false;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnOrbitViewLevelChange += onOrbitViewLevelChange;
        GameManager.Instance.OnOrbitViewQuarterChange += onOrbitViewQuarterChange;
        GameManager.Instance.OnTopicChange += onTopicChange;

        marker.GetComponent<topicMarker>()._topicmanager = this;
    }

    void onOrbitViewLevelChange(OrbitViewLevel newVal)
    {
        cameraSectorChanged();
    }

    void onOrbitViewQuarterChange(OrbitViewQuarter newVal)
    {
        cameraSectorChanged();
    }

    void onTopicChange(Topics  newTopic)
    {
        if (newTopic == topic)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }

    }

    void cameraSectorChanged()
    {
        visible = (GameManager.Instance.orbitViewLevel == cameraLevel && GameManager.Instance.orbitViewQuarter == cameraQuarter);
        
        if (visible)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Show()
    {
        marker.SetActive(visible);
    }

    public void Hide()
    {
        marker.GetComponent<topicMarker>().Hide();
    }

    public void Activate() 
    {
        marker.GetComponent<topicMarker>().Hide();
        ExtendActivate();
    }

    protected virtual void ExtendActivate() { }

    public void Deactivate() 
    {
        marker.SetActive(visible);
        ExtendDeactivate();
    }

    protected virtual void ExtendDeactivate() { }


    public void Reset()
    {

    }

    public void markerClicked()
    {
        if (!visible) return;
        GameManager.Instance.topic = topic;
    }
}
