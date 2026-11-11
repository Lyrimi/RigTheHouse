using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ItemData))]
public class BuyRounds : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private ItemData itemData;

    [SerializeField] private StatStorage statStorage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemData = GetComponent<ItemData>();
        UpdateText();
        itemData.Activate += PurcahseRounds;
    }

    // Update is called once per frame
    void Update()
    {


    }
    void UpdateText()
    {
        text.text = $"{(int)statStorage.BuyRoundsPrice}";
    }
    void PurcahseRounds()
    {
        if (PlayerWallet.Instance.HasEnoughCash((int)statStorage.BuyRoundsPrice))
        {
            PlayerWallet.Instance.Cash -= (int)statStorage.BuyRoundsPrice;
            statStorage.BuyRoundsPrice *= 1.2f;
            UpdateText();
            PlayerWallet.Instance.RemaningSpins++;
        }
    }
}
