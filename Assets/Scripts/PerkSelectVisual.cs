using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ItemData))]
public class PerkSelectVisual : MonoBehaviour
{
    private ItemData _itemData;
    [SerializeField] private GameObject _textCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _itemData = GetComponent<ItemData>();
        _itemData.OnSelect += OnSelect;
        _itemData.OnDeselect += OnDeselect;
        _textCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSelect()
    {
        _textCanvas.SetActive(true);
    }

    void OnDeselect()
    {
        _textCanvas.SetActive(false);
    }
}
