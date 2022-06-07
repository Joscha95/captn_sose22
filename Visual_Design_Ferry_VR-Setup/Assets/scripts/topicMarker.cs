using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class topicMarker : MonoBehaviour
{
    Material _mat;
    public float speed = .001f;
    float val = 0;

    [HideInInspector]
    public topicManager _topicmanager;

    private void Start()
    {
        GetComponent<MeshCollider>().convex = true;
        GetComponent<MeshCollider>().isTrigger = true;

        _mat = GetComponent<Renderer>().material;
    }
    private void OnMouseDown()
    {
        _topicmanager.markerClicked();
    }

    private void OnEnable()
    {
        if (!_mat) return;
        Show();
    }

    public void Show()
    {
        StartCoroutine("fadeIn");
    }

    public void Hide()
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine("fadeOut");
    }

    IEnumerator fadeOut()
    {
        while (val>0f)
        {
            val-=speed;
            val = Mathf.Max(val, 0);
            _mat.SetFloat("_fadein", val);
            yield return new WaitForEndOfFrame();
        }

        gameObject.SetActive(false);
    }

    IEnumerator fadeIn()
    {
        while (val < 1f)
        {
            val += speed;
            val = Mathf.Min(val, 1);
            _mat.SetFloat("_fadein", val);
            yield return new WaitForEndOfFrame();
        }

    }
}
