using System.Collections;
using TMPro;
using UnityEngine;

public class CodeVisualiser : MonoBehaviour
{
    [SerializeField] private TMP_Text[] numberTexts;
    [SerializeField] private SpriteRenderer[] _highlightRends;

    [SerializeField] private int[] _combo;

    public PerkBase Perk;

    bool _isVisuallising;

    [SerializeField] private float _hightlightTime;
    [SerializeField] private float _waitBeforeNextHighlight;
    [SerializeField] private float _waitBeforeFirstBlink;

    private float _timeToNext = 0;

    private int _curComboIndex = 0;
    [SerializeField] private float _blinkTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isVisuallising)
        {
            if (_timeToNext <= 0)
            {
                StartCoroutine(Blink(_highlightRends[_combo[_curComboIndex]], _blinkTime));
                _timeToNext = _hightlightTime;
                _curComboIndex++;

                if (_curComboIndex >= _combo.Length)
                {
                    _curComboIndex = 0;
                    _timeToNext = _waitBeforeNextHighlight;
                }
            }
            else
            {
                _timeToNext -= Time.deltaTime;
            }
        }
    }

    IEnumerator Blink(SpriteRenderer rend, float blinkTime)
    {
        float curTime = 0f;


        while (curTime < (blinkTime / 2))
        {
            float frac = curTime / (blinkTime / 2);
            float value = frac;
            float alpha = Mathf.LerpUnclamped(0, 1, value);
            curTime += Time.deltaTime;
            Color color = rend.color;
            color.a = alpha;
            rend.color = color;
            yield return null;
        }
        while (curTime < (blinkTime / 2))
        {
            float frac = curTime / (blinkTime / 2);
            float value = frac;
            float alpha = Mathf.LerpUnclamped(1, 0, value);
            curTime += Time.deltaTime;
            Color color = rend.color;
            color.a = alpha;
            rend.color = color;
            yield return null;
        }
        {
            float frac = 1;
            float value = frac;
            float alpha = Mathf.LerpUnclamped(1, 0, value);
            Color color = rend.color;
            color.a = alpha;
            rend.color = color;
        }
    }

    void FindCombo(PerkBase perk)
    {
        _combo = PerkManger.Instance.PerkTable[perk];

    }
    void UpdateText()
    {
        foreach (var item in numberTexts)
        {
            item.text = "";
        }
        for (int i = 0; i < _combo.Length; i++)
        {
            if (numberTexts[_combo[i]].text != "")
            {
                numberTexts[_combo[i]].text += $"\n{i + 1}";
            }
            else
            {
                numberTexts[_combo[i]].text += $"{i + 1}";
            }

        }
    }

    public void StartHightlight(PerkBase perk)
    {
        FindCombo(perk);
        Perk = perk;
        UpdateText();
        _curComboIndex = 0;
        _isVisuallising = true;
        _timeToNext = _waitBeforeFirstBlink;
    }

    public void EndHightLight()
    {
        _isVisuallising = false;
        StopAllCoroutines();
        foreach (var rend in _highlightRends)
        {
            Color color = rend.color;
            color.a = 0;
            rend.color = color;
        }
    }

}
