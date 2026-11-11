using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviewSpawner : MonoBehaviour
{
    private List<GameObject> _prefabs = new();
    [SerializeField] private float _distance;
    [SerializeField] private Transform _startPos;
    [SerializeField] private ItemManager _itemManager;

    [SerializeField] private GameObject prefab;
    [SerializeField] private int ToSpawn;
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
        _prefabs = PerkManger.Instance.perkObjects;


        float countToSpawn = _prefabs.Count;

        countToSpawn = ToSpawn;
        float spawnDif = _distance / (countToSpawn - 1);
        float startx = _startPos.position.x - (spawnDif / 2 * (countToSpawn - 1));
        for (int i = 0; i < countToSpawn; i++)
        {
            float cx = startx + spawnDif * i;
            Vector3 spawnPos = _startPos.position;
            spawnPos.x = cx;
            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
            ItemData item = obj.GetComponent<ItemData>();
            SpriteRenderer rend = obj.GetComponent<SpriteRenderer>();
            //rend.sprite = _prefabs[i].GetComponent<SpriteRenderer>().sprite;
            _itemManager._items.Insert(_itemManager._items.Count - 1, item);


        }
    }
}
