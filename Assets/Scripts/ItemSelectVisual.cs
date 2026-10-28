using System.Collections;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
[RequireComponent(typeof(ItemData))]
public class ItemSelectVisual : MonoBehaviour
{
    //[SerializeField] private AnimationCurve _lerpCurve;
    [SerializeField] private bool _debugButton;
    [SerializeField] private float _lerpSize = 1;

    [SerializeField] private float _lerpTime = 1;
    ItemData _itemData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float _startSize;
    void Start()
    {
        _startSize = transform.localScale.x;
        _itemData = GetComponent<ItemData>();
        _itemData.OnSelect += OnSelect;
        _itemData.OnDeselect += OnDeselect;
    }

    void OnSelect()
    {
        StopAllCoroutines();
        StartCoroutine(SpringBounce(transform.localScale.x, _lerpSize));

    }
    void OnDeselect()
    {
        if (gameObject == null)
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(SpringBounce(transform.localScale.x, _startSize));
    }



    // Update is called once per frame
    void Update()
    {
        if (_debugButton)
        {
            _debugButton = false;

        }
    }

    IEnumerator SpringBounce(float sStart, float sEnd)
    {
        float curTime = 0;

        while (curTime < _lerpTime)
        {
            float frac = curTime / _lerpTime;
            float value = Ease.EaseOutElastic(frac);
            float cSize = Mathf.LerpUnclamped(sStart, sEnd, value);
            transform.localScale = new(cSize, cSize, cSize);
            yield return null;
            curTime += Time.deltaTime;
        }
        {
            float frac = 1;
            float value = Ease.EaseOutElastic(frac);
            float cSize = Mathf.LerpUnclamped(sStart, sEnd, value);
            transform.localScale = new(cSize, cSize, cSize);
        }
    }
}
