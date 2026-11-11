using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboScreen : MonoBehaviour
{
    public Vector3 StartPostion;

    public Transform RevelPos;

    [SerializeField] private GameObject WatingComboScreen;

    [SerializeField] private Transform _camResetPos;
    [SerializeField] private float _camResetTime = 3;

    private SpriteRenderer _spriteRenderer;

    public CodeVisualiser CodeVisualiser;

    public bool WatingAnyInput = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartPostion = transform.position;
        CodeVisualiser = GetComponent<CodeVisualiser>();
    }



    // Update is called once per frame
    void Update()
    {
        if (WatingAnyInput && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            WatingAnyInput = false;
            StartCoroutine(Activate());
        }
    }

    void Start()
    {
        animator = WatingComboScreen.GetComponentInChildren<Animator>();
    }

    public void LerpPos(Vector2 targetPos, float lerpTime, Action callbackAction = null, Transform overideTransform = null)
    {
        Vector3 pos = (Vector3)targetPos;
        pos.z = StartPostion.z;
        transform.position = StartPostion;
        StartCoroutine(LerpPostion(pos, lerpTime, overideTransform, callbackAction));
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    IEnumerator LerpPostion(Vector3 lerpPos, float lerpTime, Transform overideTransform = null, Action callbackAction = null)
    {
        float curTime = 0;
        Vector3 sPostion;
        if (overideTransform != null)
        {
            sPostion = overideTransform.position;
        }
        else
        {
            sPostion = transform.position;
        }
        while (curTime < lerpTime)
        {
            float frac = curTime / lerpTime;
            float value = Ease.EaseOutQuad(frac);
            Vector2 pos = Vector3.LerpUnclamped(sPostion, lerpPos, value);
            if (overideTransform != null)
            {
                overideTransform.position = pos;
            }
            else
            {
                transform.position = pos;
            }
            yield return null;
            curTime += Time.deltaTime;
        }
        {
            float frac = 1;
            float value = Ease.EaseOutQuad(frac);
            Vector2 pos = Vector3.LerpUnclamped(sPostion, lerpPos, value);
            if (overideTransform != null)
            {
                overideTransform.position = pos;
            }
            else
            {
                transform.position = pos;
            }
        }
        callbackAction?.Invoke();
    }

    public IEnumerator Activate()
    {
        //Some straight up bs right here
        yield return LerpPostion(StartPostion, 0.5f);
        yield return LerpPostion(RevelPos.position, 0.5f, WatingComboScreen.transform);
        CodeVisualiser.EndHightLight();
        PerkManger.Instance.IsActive = true;
        PerkManger.Instance.RestartInput();
        PerkManger.Instance.OnCompletion = Finished;
    }
    Animator animator;
    public void Finished(bool Completed, GameObject perkObj)
    {
        PerkManger.Instance.IsActive = false;
        StartCoroutine(FinishScreen(Completed, perkObj));
    }

    [SerializeField] private SpriteRenderer _finishCardRend;
    IEnumerator FinishScreen(bool Completed, GameObject perkObj)
    {

        if (Completed)
        {
            _finishCardRend.sprite = perkObj.GetComponent<SpriteRenderer>().sprite;
            animator.SetTrigger("CheatActivated");
        }
        else
        {
            _finishCardRend.sprite = CodeVisualiser.Perk.gameObject.GetComponent<SpriteRenderer>().sprite;
            animator.SetTrigger("CheatFailed");
        }
        yield return null;
        float time = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(time + 0.5f);

        animator.SetTrigger("Reset");

        yield return null;
        time = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(time - 0.3f);
        WatingComboScreen.transform.position = StartPostion;
        ResetCam();
    }

    public void ResetCam()
    {
        WatingComboScreen.transform.position = StartPostion;
        transform.position = StartPostion;
        Vector3 pos = _camResetPos.transform.position;
        pos.y = -50;
        pos.z = -10;
        CameraManager.Instance.MainCam.transform.position = pos;
        CameraManager.Instance.LerpCamOnce(_camResetPos.position, _camResetTime, RestartShop);
    }
    [SerializeField] private ItemManager _shopItemManager;
    void RestartShop()
    {
        CameraManager.Instance.upMovPart.gameObject.SetActive(false);
        _shopItemManager.gameObject.SetActive(true);
        _shopItemManager.Move(-1);
    }
}
