using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothorbit : MonoBehaviour
{
    public Transform cam;
    public float distance = 2.0f;
    public float xSpeed = 20.0f;
    public float ySpeed = 20.0f;
    public float zSpeed = 20.0f;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    public float distanceMin = 10f;
    public float distanceMax = 10f;
    public float smoothTime = 2f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;
    float velocityZ = 0.0f;
    bool inTransition = false;
    float smoothtime = 0;
    float duration = 100;
    float time = 0;
    Vector3 oldPosCam, oldPos, targPos,targPosCam;
    Quaternion oldRot, targRot;
    Vector3 zoom = new Vector3();

    [System.Serializable]
    public struct TopicCameras
    {
        public Topics topic;
        public Transform cam;
    }

    [Space(50)]
    public AnimationCurve smoothcurve;
    public List<TopicCameras> topicCameras;


    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;

        zoom.z = distance;

        GameManager.Instance.OnTopicChange += onTopicChange; 

    }
    void LateUpdate()
    {
        if (inTransition)
        {
            transitionToTarget();
        }
        else
        {
            mouseControl();
        }
        
    }

    void mouseControl()
    {
        if (Input.GetMouseButton(0))
        {
            velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
            velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
        }

        rotationYAxis += velocityX; 
        rotationXAxis -= velocityY;
        rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
        Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
        Quaternion rotation = toRotation;

        velocityZ += Input.GetAxis("Mouse ScrollWheel") * zSpeed;
        zoom.z = velocityZ;

        GameManager.Instance.setXAngle(rotationXAxis);

        cam.Translate(zoom, Space.Self);

        transform.rotation = rotation;
        GameManager.Instance.setYAngle(transform.eulerAngles.y);

        velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
        velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        velocityZ = Mathf.Lerp(velocityZ, 0, Time.deltaTime * smoothTime);
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    void onTopicChange(Topics newTopic)
    {
        

        if (topicCameras.Exists(c => c.topic == newTopic))
        {
            oldPosCam = cam.localPosition;
            oldRot = transform.rotation;
            oldPos = transform.position;
            Transform targTrans = topicCameras.Find(c => c.topic == newTopic).cam;
            targPosCam = targTrans.GetChild(0).localPosition;
            targPos = targTrans.localPosition;
            targRot = targTrans.localRotation;
            inTransition = true;
            time = 0;
        }
        else if (newTopic==Topics.NONE && Vector3.Distance(transform.localPosition,Vector3.zero)>.1f)
        {
            //reset Camera
            oldPosCam = cam.localPosition;
            targPosCam = cam.localPosition;
            oldRot = transform.rotation;
            targRot = transform.localRotation;
            oldPos = transform.position;
            targPos = Vector3.zero;
            inTransition = true;
            time = 0;
        }
    }

    void transitionToTarget()
    {
        cam.localPosition = Vector3.Lerp(oldPosCam, targPosCam, smoothcurve.Evaluate(time/duration));
        transform.rotation = Quaternion.Lerp(oldRot, targRot, smoothcurve.Evaluate(time / duration));
        transform.localPosition = Vector3.Lerp(oldPos, targPos, smoothcurve.Evaluate(time / duration));
        time++;
        if(time>=duration)
        {
            inTransition = false;
            rotationXAxis = transform.localEulerAngles.x;
            rotationYAxis = transform.localEulerAngles.y;
            time = 0;
        }
    }
}
