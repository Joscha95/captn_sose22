using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCam : MonoBehaviour
{
    public Transform rightController;
    public GameObject marker;
    bool triggered=false;

    void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(rightController.position, rightController.TransformDirection(Vector3.forward) * 10, Color.green);
        print(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));

        if (Physics.Raycast(rightController.position, rightController.TransformDirection(Vector3.forward), out hit))
        {
            marker.SetActive(true);
            marker.transform.position = hit.point;
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > .5f)
            {
                if (!triggered)
                {
                    transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    triggered = true;
                }
                
            }
            else
            {
                triggered = false;
            }
        }else
        {
            marker.SetActive(false);
        }
    }
}
