using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ItemData))]
public class ShopItem : MonoBehaviour
{
    private ItemData _itemData;
    [SerializeField] private TMP_Text _priceVisualiser;

    [SerializeField] private int _price;
    [SerializeField] Color _baseColor;
    [SerializeField] Color _poorColor;
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
        print("Yeeted The text");
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


}
