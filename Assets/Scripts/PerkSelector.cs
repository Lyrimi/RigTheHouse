using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(ItemData))]
public class PerkSelector : MonoBehaviour
{
    private ItemData _itemData;
    public ItemManager ItemManager;



    [SerializeField] private Sprite CodeImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _itemData = GetComponent<ItemData>();
        _itemData.Activate += PerkChosen;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PerkChosen()
    {
        foreach (var item in ItemManager._items)
        {
            if (item != _itemData)
            {
                StartCoroutine(YeetTheCard(item, 10, Vector2.zero));
            }
        }
        StartCoroutine(RevelCombo());

        ItemManager.gameObject.SetActive(false);
    }

    List<GameObject> cardObjs = new();
    IEnumerator YeetTheCard(ItemData itemData, float despawnTime, Vector2 linerOveride)
    {
        GameObject cardObj = itemData.gameObject;
        cardObjs.Add(cardObj);
        //remove the card after one frame so we don't remove it while the for loop is running
        yield return null;
        ItemManager._items.Remove(itemData);

        Rigidbody2D rb = cardObj.AddComponent<Rigidbody2D>();


        if (linerOveride != Vector2.zero)
        {

            rb.linearVelocity = linerOveride;
        }
        else
        {
            rb.linearVelocity = Random.onUnitCircle * 50;

            rb.angularVelocity = Random.Range(-1024f, 1024f);
        }

        if (despawnTime != 0)
        {
            yield return new WaitForSeconds(despawnTime);

            Destroy(cardObj);
        }


    }

    IEnumerator RevelCombo()
    {
        yield return new WaitForSeconds(0.3f);

        StartCoroutine(YeetTheCard(_itemData, 0, Vector2.down * 40));

        yield return new WaitForSeconds(1f);

        ComboScreen comboScreen = ComboScreen.Instance;

        comboScreen.LerpPos(comboScreen.RevelPos.position, 0.5f, DestroyThis);

    }

    void DestroyThis()
    {
        ComboScreen.Instance.WatingAnyInput = true;
        foreach (var item in cardObjs)
        {
            if (!item)
            {
                return;
            }
            Destroy(item);
        }
        Destroy(this);
    }
}
