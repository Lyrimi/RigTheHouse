using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ItemData))]
public class ShopItem : MonoBehaviour
{
    private ItemData _itemData;
    [SerializeField] private TMP_Text _priceVisualiser;

    [SerializeField] private int _price;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _itemData = GetComponent<ItemData>();
        _itemData.Activate += Purchase;
        _priceVisualiser.text = $"{_price}";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Purchase()
    {
        YeetTheText(_priceVisualiser.transform.parent.gameObject, 3f);
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


}
