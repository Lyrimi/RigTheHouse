using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ItemData))]
public class ShopItem : MonoBehaviour
{
    private ItemData _itemData;
    [SerializeField] private TMP_Text _priceVisualiser;

    [SerializeField] private int _price;
    [SerializeField] Color _baseColor;
    [SerializeField] Color _poorColor;
    [SerializeField] private SpawnAbleItem[] _cards;
    public SpawnAbleItem[] Cards => _cards;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _itemData = GetComponent<ItemData>();
        _priceVisualiser.text = $"{_price}";
        PlayerWallet.Instance.OnCashChange += UpdateText;
        UpdateText();
    }

    void UpdateText()
    {
        if (PlayerWallet.Instance.HasEnoughCash(_price))
        {
            _priceVisualiser.color = _baseColor;
        }
        else
        {
            _priceVisualiser.color = _poorColor;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool AtemptPurchase()
    {
        if (PlayerWallet.Instance.HasEnoughCash(_price))
        {
            Purchase();
            return true;
        }
        return false;
    }

    void Purchase()
    {
        PlayerWallet.Instance.Cash -= _price;
        StartCoroutine(YeetTheText(_priceVisualiser.transform.parent.gameObject, 3f));
    }

    IEnumerator YeetTheText(GameObject obj, float despawnTime)
    {

        yield return null;

        Rigidbody2D rb = obj.AddComponent<Rigidbody2D>();

        rb.linearVelocity = Random.onUnitCircle * 50;
        rb.angularVelocity = Random.Range(-1024f, 1024f);

        if (despawnTime != 0)
        {
            yield return new WaitForSeconds(despawnTime);

            obj.SetActive(false);
        }
    }

    public List<PerkBase> GetNumRandomWeightedPerks(int num)
    {
        List<PerkBase> selectedPerks = new();

        List<SpawnAbleItem> cSpawnAbles = _cards.ToList();

        for (int i = 0; i < num; i++)
        {
            SpawnAbleItem item = GetRandomWeightedPerk(cSpawnAbles);
            if (item == null)
            {
                return selectedPerks;
            }
            selectedPerks.Add(item.Perk);
            cSpawnAbles.Remove(item);
        }



        return selectedPerks;
    }

    SpawnAbleItem GetRandomWeightedPerk(List<SpawnAbleItem> spawnAbleItems)
    {
        float totalWeight = 0;

        foreach (var item in spawnAbleItems)
        {
            totalWeight += item.Weight;
        }

        float random = Random.Range(0, totalWeight);

        foreach (var item in spawnAbleItems)
        {
            random -= item.Weight;

            if (random < 0)
            {
                return item;
            }

        }
        return null;
    }

}
[Serializable]
public class SpawnAbleItem
{
    public PerkBase Perk;
    public float Weight;
}
