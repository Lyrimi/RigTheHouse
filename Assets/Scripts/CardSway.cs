using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardSway : MonoBehaviour
{
    bool _dirtyPoint = true;
    [SerializeField] private float _lerpTime = 1f;
    [SerializeField] private float _maxAngle = 45;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_dirtyPoint)
        {
            UpdateWigglePoint();
        }
    }

    void OnEnable()
    {
        _dirtyPoint = true;
        StopAllCoroutines();
    }

    void OnDisable()
    {
        StopAllCoroutines();
        StartCoroutine(Wiggle(Quaternion.identity));
    }


    void UpdateWigglePoint()
    {
        Vector3 axis = Random.onUnitSphere;
        _dirtyPoint = false;
        StartCoroutine(Wiggle(Quaternion.AngleAxis(_maxAngle, axis)));
    }

    IEnumerator Wiggle(Quaternion targetRot)
    {
        Quaternion startRot = transform.rotation;
        float curTime = 0;

        while (curTime < _lerpTime)
        {

            float frac = curTime / _lerpTime;
            float value = Mathf.SmoothStep(0, 1, frac);
            Quaternion lerpRot = Quaternion.Lerp(startRot, targetRot, value);
            transform.rotation = lerpRot;
            curTime += Time.deltaTime;
            yield return null;
        }
        _dirtyPoint = true;
    }
}
