using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class topicManager_Air : topicManager
{
    public VisualEffect airparticles;
    

    protected override void ExtendActivate() 
    {
        airparticles.SendEvent("Start");
    }

    protected override void ExtendDeactivate()
    {
        airparticles.SendEvent("Stop");
    }


}
