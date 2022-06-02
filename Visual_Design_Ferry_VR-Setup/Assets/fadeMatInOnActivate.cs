using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeMatInOnActivate : MonoBehaviour
{
    Material _mat;
    float val = 0;
    float time = 0;
    public float speed = .01f;
    // Start is called before the first frame update
    void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (val<.9999f)
        {
            time++;
            val = Mathf.Lerp(val, 1, time * speed);
            _mat.SetFloat("_fadein", val);
        }

    }

    private void OnEnable()
    {
        val = time = 0;
        print(val);
    }
}
