using System;
using TMPro;
using Unity.Mathematics;
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
            print("Bought");
            PlayerWallet.Instance.Cash -= (int)statStorage.BuyRoundsPrice;
            statStorage.RoundsBought += 1;
            statStorage.BuyRoundsPrice = MathF.Pow(1.01f, statStorage.RoundsBought) * 2 * statStorage.RoundsBought + 1;
            UpdateText();
            PlayerWallet.Instance.RemaningSpins++;
        }
    }
}
