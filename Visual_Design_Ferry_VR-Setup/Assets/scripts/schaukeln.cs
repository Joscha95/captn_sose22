using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class schaukeln : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = .1f;
    public float height = .1f;
    Vector3 rot = new Vector3();
    int counter=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter++;

        rot.x = map(Mathf.PerlinNoise(counter * speed, 1),0,1, -height, height);
        transform.eulerAngles = rot;
    }

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

}
