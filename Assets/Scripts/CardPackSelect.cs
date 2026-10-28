using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(ItemData))]
[RequireComponent(typeof(ItemShake))]
public class CardPackSelect : MonoBehaviour
{
    public Transform MovePoint;
    [SerializeField] private float _moveTime;
    [SerializeField] private float _openTime;
    private ItemData _itemData;
    private ItemShake _itemShake;
    private Material _disolve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _itemData = GetComponent<ItemData>();
        _itemShake = GetComponent<ItemShake>();
        _disolve = GetComponent<SpriteRenderer>().material;
        _itemData.Activate += OnActivate;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnActivate()
    {
        print("CardDidStuff");
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        CameraManager.Instance.upMovPart.gameObject.SetActive(true);
        CameraManager.Instance.LerpCamOnce(MovePoint.position, _moveTime);
        float curTime = 0;
        Vector3 sPos = transform.position;
        Vector3 endPos = MovePoint.position;
        while (curTime < _moveTime)
        {

            float frac = curTime / _moveTime;

            float value = frac;
            Vector3 curPos = Vector3.LerpUnclamped(sPos, endPos, value);
            transform.position = curPos;
            curTime += Time.deltaTime;
            yield return null;
        }
        {
            float frac = 1;
            float value = frac;
            Vector3 curPos = Vector3.LerpUnclamped(sPos, endPos, value);
            transform.position = curPos;
        }
        _itemShake.enabled = true;
        yield return Open();
    }

    [SerializeField] private float _targetShakeSpeed = 0.1f;
    [SerializeField] private float _startShakeSpeed = 1f;

    IEnumerator Open()
    {
        float curTime = 0f;
        while (curTime < _openTime)
        {
            float frac = curTime / _openTime;
            float value = frac;
            _itemShake.LerpTime = Mathf.Lerp(_startShakeSpeed, _targetShakeSpeed, value);
            curTime += Time.deltaTime;
            yield return null;
        }
        {
            float frac = 1;
            float value = frac;
            _itemShake.LerpTime = Mathf.Lerp(_startShakeSpeed, _targetShakeSpeed, value);
        }
    }

    IEnumerator Disolve()
    {
        yield return null;
    }
}
