using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
[Serializable]
public class WeightedPrefab
{
    public GameObject Prefab;
    public float Weight;
}
public class ShopManager : MonoBehaviour
{
    [SerializeField] private WeightedPrefab[] _prefabs;
    [SerializeField] private int _countToSpawn;
    [SerializeField] private float _distance;
    [SerializeField] private Transform _startPos;
    [SerializeField] private ItemManager _itemManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnItems();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnItems()
    {
        foreach (var item in PerkManger.Instance.ActivePerks)
        {
            if (item is ShopExpansion)
            {
                _countToSpawn++;
            }
        }

        float startx = _startPos.position.x - (_distance / 2 * (_countToSpawn - 1));
        for (int i = 0; i < _countToSpawn; i++)
        {
            float cx = startx + _distance * i;
            Vector3 spawnPos = _startPos.position;
            spawnPos.x = cx;
            ItemData item = Instantiate(GetRandomWeightedPrefab(_prefabs), spawnPos, Quaternion.identity).GetComponent<ItemData>();
            _itemManager._items.Insert(_itemManager._items.Count - 1, item);


        }
    }
    GameObject GetRandomWeightedPrefab(WeightedPrefab[] weightedPrefabs)
    {
        float totalWeight = 0;

        foreach (var item in weightedPrefabs)
        {
            totalWeight += item.Weight;
        }

        float random = Random.Range(0, totalWeight);

        foreach (var item in weightedPrefabs)
        {
            random -= item.Weight;

            if (random < 0)
            {
                return item.Prefab;
            }

        }
        Debug.LogWarning("Couldn't Find any perk");
        return null;
    }
}
