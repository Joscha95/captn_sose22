using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JB;


public class OrbitControl : MonoBehaviour, ICameraController
{
    public Transform target;

    float _zoom = 5.0f;
    public float zoom {
        get => _zoom;
        set => _zoom = value;
    }

    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    float zoomMin;
    float zoomMax;

    Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    Vector3 startPos, startRot;

    bool mousedown;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        startPos = transform.position;
        startRot = transform.eulerAngles;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }

    }

    void LateUpdate()
    {

        if (Input.GetMouseButtonDown(0))
            mousedown = true;
        else if (Input.GetMouseButtonUp(0))
            mousedown = false;

        if (mousedown)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * zoom * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        zoom = Mathf.Clamp(zoom - Input.GetAxis("Mouse ScrollWheel") * 5, zoomMin, zoomMax);

        RaycastHit hit;
        if (Physics.Linecast(target.position, transform.position, out hit))
        {
            zoom -= hit.distance;
        }
        Vector3 negzoom = new Vector3(0.0f, 0.0f, -zoom);
        Vector3 position = rotation * negzoom + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void Reset(bool soft = false)
    {
        transform.position = startPos;
        transform.eulerAngles = startRot;
    }
}
