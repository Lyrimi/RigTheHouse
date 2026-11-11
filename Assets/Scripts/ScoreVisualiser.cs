using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreVisualiser : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private bool _debugButton;

    [SerializeField] private float score;

    public static ScoreVisualiser Instance;

    [SerializeField] private GameObject multObj;

    [SerializeField] private Transform playerWalletVisual;
    private TMP_Text multText;
    private Vector3 multTextSPos;

    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        text = GetComponent<TMP_Text>();
        multTextSPos = multObj.transform.position;
        multText = multObj.GetComponent<TMP_Text>();
        multObj.SetActive(false);
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_debugButton)
        {
            _debugButton = false;
            StartCoroutine(MultScoreAnim(2));
        }
    }

    IEnumerator ApplyScoreChangesAnim(List<(ScoreType, float)> scoreChanges)
    {
        score = 0;
        UpdateText();
        yield return new WaitForSeconds(0.2f);
        foreach (var item in scoreChanges)
        {
            if (item.Item1 == ScoreType.Addtion)
            {
                yield return AddScoreAnim(item.Item2);
            }
            else if (item.Item1 == ScoreType.Mult)
            {
                yield return MultScoreAnim(item.Item2);
            }
        }
        yield return AddScoreToWallet();
    }

    public void ApplyScoreChanges(List<(ScoreType, float)> scoreChanges)
    {
        StartCoroutine(ApplyScoreChangesAnim(scoreChanges));
    }



    IEnumerator AddScoreAnim(float value)
    {
        yield return null;
        score += value;
        UpdateText();

        yield return Bounce(transform, 1, 1.5f, 0.15f);

    }

    void UpdateText()
    {
        text.text = $"{score}";
    }

    IEnumerator MultScoreAnim(float value)
    {

        yield return null;


        multObj.transform.position = multTextSPos;
        multText.text = $"x {value}";
        multObj.SetActive(true);
        StartCoroutine(Bounce(multObj.transform, 0, 1, 0.4f));
        yield return new WaitForSeconds(0.2f);
        yield return Move(multObj.transform, multTextSPos, transform.position, 0.2f);
        score *= value;
        UpdateText();
        multObj.SetActive(false);
        yield return Bounce(transform, 1, 1.5f, 0.15f);
    }

    IEnumerator Move(Transform objTrans, Vector3 sStart, Vector3 sEnd, float lerpTime)
    {
        float curTime = 0;

        while (curTime < lerpTime)
        {
            float frac = curTime / (lerpTime);
            float value = frac;
            objTrans.position = Vector3.LerpUnclamped(sStart, sEnd, value);
            yield return null;
            curTime += Time.deltaTime;
        }
        {
            float frac = 1;
            float value = frac;
            objTrans.position = Vector3.LerpUnclamped(sStart, sEnd, value);
            yield return null;
        }
    }
    IEnumerator Bounce(Transform trans, float sStart, float sEnd, float lerpTime, bool skipSecoundPart = false)
    {
        float curTime = 0;
        if (skipSecoundPart)
        {
            lerpTime *= 2;
        }
        while (curTime < lerpTime / 2)
        {
            float frac = curTime / (lerpTime / 2);
            float value = frac;
            float cSize = Mathf.LerpUnclamped(sStart, sEnd, value);
            trans.localScale = new(cSize, cSize, cSize);
            yield return null;
            curTime += Time.deltaTime;
        }
        curTime = 0;
        if (skipSecoundPart)
        {
            {
                float frac = 1;
                float value = Ease.EaseOutElastic(frac);
                float cSize = Mathf.LerpUnclamped(sStart, sEnd, value);
                trans.localScale = new(cSize, cSize, cSize);
            }
            yield break;
        }
        while (curTime < lerpTime / 2)
        {
            float frac = curTime / (lerpTime / 2);
            float value = frac;
            float cSize = Mathf.LerpUnclamped(sEnd, sStart, value);
            trans.localScale = new(cSize, cSize, cSize);
            yield return null;
            curTime += Time.deltaTime;
        }
        {
            float frac = 1;
            float value = Ease.EaseOutElastic(frac);
            float cSize = Mathf.LerpUnclamped(sEnd, sStart, value);
            trans.localScale = new(cSize, cSize, cSize);
        }
    }

    IEnumerator AddScoreToWallet()
    {
        yield return null;
        StartCoroutine(Bounce(transform, 1, 0, 2, true));
        yield return Move(transform, transform.position, playerWalletVisual.position, 2f);
        PlayerWallet.Instance.Cash += score;
        transform.position = startPos;


    }
}
