using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topicManager_Drivetrain : topicManager
{
    float val = 0;
    public Material[] _mats;
    public float speed = .05f;

    AnimationCurve _smoothcurve;
    protected override void ExtendActivate()
    {
        StartCoroutine("fadeIn");
    }

    protected override void ExtendDeactivate()
    {
        StartCoroutine("fadeOut");
    }

    protected override void ExtendStart()
    {
        foreach (var _mat in _mats)
        {
            _mat.SetFloat("_fadeIn", 0);
        }

        _smoothcurve = GameManager.Instance.smoothcurve;
    }

    IEnumerator fadeOut()
    {
        
        while (val > 0f)
        {
            val -= speed;
            val = Mathf.Max(val, 0);
            foreach (var _mat in _mats)
            {
                _mat.SetFloat("_fadeIn", _smoothcurve.Evaluate(val));
            }
            yield return new WaitForFixedUpdate();
        }

    }

    IEnumerator fadeIn()
    {
        yield return new WaitForSecondsRealtime(1);

        while (val < 1f)
        {
            val += speed;
            val = Mathf.Min(val, 1);
            foreach (var _mat in _mats)
            {
                _mat.SetFloat("_fadeIn", _smoothcurve.Evaluate(val));
            }
            yield return new WaitForFixedUpdate();
        }

    }
}
