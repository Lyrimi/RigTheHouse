using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ComboButtonIndicator : MonoBehaviour
{
    [SerializeField] private GameObject[] VisualObjectPrefabs;
    [SerializeField] private float _distance;
    public int MaxCodeLen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    int _count = 0;
    List<GameObject> _items = new();
    public void AddVisualIndicator(int index)
    {
        float startx = transform.position.x - (_distance / 2 * (MaxCodeLen - 1));
        float cx = startx + _distance * _count;
        Vector3 spawnPos = transform.position;
        spawnPos.x = cx;
        spawnPos.z = 0;
        GameObject obj = Instantiate(VisualObjectPrefabs[index], spawnPos, Quaternion.identity);
        _items.Add(obj);
        _count++;
    }

    public void Reset()
    {
        _count = 0;
        foreach (var item in _items)
        {
            Destroy(item);
        }
        _items = new();
    }
}
