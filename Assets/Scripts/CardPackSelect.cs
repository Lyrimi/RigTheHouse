using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(ItemData))]
[RequireComponent(typeof(ItemShake))]
[RequireComponent(typeof(ShopItem))]
public class CardPackSelect : MonoBehaviour
{
    [SerializeField] private float _moveTime;
    [SerializeField] private float _openTime;
    [SerializeField] private float _disolveTime;
    [SerializeField] private float _spawnMoveTime;
    [SerializeField] private float _spawnThresehold;
    [SerializeField] private int _countToSpawn;
    [SerializeField] private float _distance;
    [SerializeField] private GameObject _prefab;
    [NonSerialized] public Transform StartPos;
    [NonSerialized] public ItemManager CarditemManager;
    [NonSerialized] public Transform MovePoint;
    [NonSerialized] public ItemManager ShopItemManager;
    private ItemData _itemData;
    private ItemShake _itemShake;
    private ShopItem _shopItem;
    private Material _disolve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _itemData = GetComponent<ItemData>();
        _itemShake = GetComponent<ItemShake>();
        _disolve = GetComponent<SpriteRenderer>().material;
        _shopItem = GetComponent<ShopItem>();
        _itemData.Activate += OnActivate;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnActivate()
    {

        if (_shopItem.AtemptPurchase())
        {
            ShopItemManager._items.Remove(_itemData);
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        ShopItemManager.gameObject.SetActive(false);
        CarditemManager.gameObject.SetActive(false);
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
        yield return Disolve();
    }
    bool spawned = false;
    IEnumerator Disolve()
    {
        _disolve.SetFloat("_DisolveAmount", 0);

        float curTime = 0f;
        while (curTime < _disolveTime)
        {
            float frac = curTime / _disolveTime;
            float value = frac;
            _disolve.SetFloat("_DisolveAmount", Mathf.Lerp(0, 1, value));
            curTime += Time.deltaTime;
            yield return null;
            if (frac > _spawnThresehold && !spawned)
            {
                spawned = true;
                SpawnItems();
            }
        }
        {
            float frac = 1;
            float value = frac;
            _disolve.SetFloat("_DisolveAmount", Mathf.Lerp(0, 1, value));
        }
        yield return null;

        if (!spawned)
        {
            spawned = true;
            SpawnItems();
        }

    }


    void SpawnItems()
    {
        CarditemManager._items = new();

        List<PerkBase> perks = _shopItem.GetNumRandomWeightedPerks(_countToSpawn);

        if (perks.Count < _countToSpawn)
        {
            _countToSpawn = perks.Count;
        }

        float startx = StartPos.position.x - (_distance / 2 * (_countToSpawn - 1));
        for (int i = 0; i < _countToSpawn; i++)
        {
            float cx = startx + _distance * i;
            Vector3 spawnPos = StartPos.position;
            spawnPos.x = cx;
            GameObject obj = Instantiate(_prefab, transform.position, Quaternion.identity);

            AsignSpriteFromPerk(obj, perks[i]);

            ItemData item = obj.GetComponent<ItemData>();

            PerkSelectVisual perkSelectVisual = obj.GetComponent<PerkSelectVisual>();
            perkSelectVisual.Perk = perks[i];

            PerkSelector perkSelector = obj.GetComponent<PerkSelector>();
            if (perkSelector)
            {
                perkSelector.ItemManager = CarditemManager;
            }
            CarditemManager._items.Add(item);

            StartCoroutine(MoveObjToPos(obj, transform.position, spawnPos, _spawnMoveTime));
        }
    }

    void AsignSpriteFromPerk(GameObject IObj, PerkBase perk)
    {
        SpriteRenderer iObjRend = IObj.GetComponent<SpriteRenderer>();
        if (iObjRend == null)
        {
            Debug.LogWarning("IGameObject Doesn't have a sprite render, adding one");
            iObjRend = gameObject.AddComponent<SpriteRenderer>();
        }

        SpriteRenderer perkRend = perk.gameObject.GetComponent<SpriteRenderer>();
        if (perkRend == null)
        {
            throw new Exception("Perk doesn't have a spritrender");
        }
        iObjRend.sprite = perkRend.sprite;
    }

    IEnumerator MoveObjToPos(GameObject obj, Vector3 startPos, Vector3 endPos, float moveTime)
    {
        float curTime = 0f;
        while (curTime < moveTime)
        {
            float frac = curTime / moveTime;
            float value = frac;
            obj.transform.position = Vector3.LerpUnclamped(startPos, endPos, value);
            curTime += Time.deltaTime;
            yield return null;
        }
        {
            float frac = 1;
            float value = frac;
            obj.transform.position = Vector3.LerpUnclamped(startPos, endPos, value);
        }
        CarditemManager.gameObject.SetActive(true);
    }
}
