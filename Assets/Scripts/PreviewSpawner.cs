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

    public bool debugButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (debugButton)
        {
            SpawnItems();
            debugButton = false;
        }
    }

    void SpawnItems()
    {
        foreach (var item in PerkManger.Instance.perkObjects)
        {
            _prefabs.Add(item.gameObject);
        }


        float countToSpawn = _prefabs.Count;

        float startx = 0;
        for (int i = 0; i < countToSpawn; i++)
        {

        }
    }
}
