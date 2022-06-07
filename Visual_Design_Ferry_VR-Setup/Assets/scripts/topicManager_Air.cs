using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class topicManager_Air : topicManager
{
    public VisualEffect airparticles;
    public GameObject[] txts;

    protected override void ExtendActivate() 
    {
        airparticles.SendEvent("Start");

        foreach (var txt in txts)
        {
            txt.SetActive(true);
        }
    }

    protected override void ExtendDeactivate()
    {
        airparticles.SendEvent("Stop");
        foreach (var txt in txts)
        {
            txt.SetActive(false);
        }
    }


}
