using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
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
        float startx = _startPos.position.x - (_distance / 2 * (_countToSpawn - 1));
        for (int i = 0; i < _countToSpawn; i++)
        {
            float cx = startx + _distance * i;
            Vector3 spawnPos = _startPos.position;
            spawnPos.x = cx;
            ItemData item = Instantiate(_prefabs[0], spawnPos, Quaternion.identity).GetComponent<ItemData>();
            _itemManager._items.Insert(_itemManager._items.Count - 1, item);

        }
    }
}
