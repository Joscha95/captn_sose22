using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoothorbit : MonoBehaviour
{
    public Transform camera;
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
    Vector3 zoom = new Vector3();
    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;

        zoom.z = distance;

    }
    void LateUpdate()
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

        velocityZ += Input.GetAxis("Mouse ScrollWheel")*zSpeed;
        zoom.z = velocityZ;
        
        GameManager.Instance.setXAngle(rotationXAxis);

        //distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
        //RaycastHit hit;
        //if (Physics.Linecast(target.position, transform.position, out hit))
        //{
        //distance -= hit.distance;
        //}
        //Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        //Vector3 position = rotation * negDistance + target.position;
        camera.Translate(zoom, Space.Self);

        transform.rotation = rotation;
        //transform.position = position;
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
}
