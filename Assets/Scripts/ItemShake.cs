using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
[RequireComponent(typeof(ItemData))]
public class ItemShake : MonoBehaviour
{
    bool _dirtyPoint = true;
    [SerializeField] public float LerpTime = 1f;
    [SerializeField] private float _maxAngle = 45;

    Quaternion tRot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tRot = Quaternion.Euler(0, 0, _maxAngle);
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
        if (gameObject.activeSelf)
        {
            StartCoroutine(Wiggle(Quaternion.identity));
        }

    }

    Vector3 axis = new(0, 0, 1);
    void UpdateWigglePoint()
    {
        axis.Scale(new(-1, -1, -1));
        _dirtyPoint = false;

        StartCoroutine(Wiggle(Quaternion.AngleAxis(_maxAngle, axis)));
    }

    IEnumerator Wiggle(Quaternion targetRot)
    {
        Quaternion startRot = transform.rotation;
        float curTime = 0;

        while (curTime < LerpTime)
        {

            float frac = curTime / LerpTime;
            float value = Mathf.SmoothStep(0, 1, frac);
            Quaternion lerpRot = Quaternion.Lerp(startRot, targetRot, value);
            transform.rotation = lerpRot;
            curTime += Time.deltaTime;
            yield return null;
        }
        _dirtyPoint = true;
    }
}
