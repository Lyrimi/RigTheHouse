using System;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    public Camera MainCam;

    public ParticleSystem upMovPart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        MainCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LerpCamOnce(Vector2 pos, float time, Action callBack = null)
    {
        Vector3 Pos = pos;
        Pos.z = transform.position.z;
        StartCoroutine(CamLerp(Pos, time, callBack));
    }

    IEnumerator CamLerp(Vector3 pos, float time, Action callBack = null)
    {
        float curTime = 0f;
        Vector3 sPos = transform.position;
        while (curTime < time)
        {

            float frac = curTime / time;
            float value = frac;
            Vector3 lerpPos = Vector3.LerpUnclamped(sPos, pos, value);
            transform.position = lerpPos;
            yield return null;
            curTime += Time.deltaTime;

        }
        {
            float frac = 1;
            float value = frac;
            Vector3 lerpPos = Vector3.LerpUnclamped(sPos, pos, value);
            transform.position = lerpPos;
        }
        callBack?.Invoke();
    }
}
