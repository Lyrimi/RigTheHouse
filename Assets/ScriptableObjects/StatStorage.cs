
using UnityEngine;

[CreateAssetMenu(fileName = "StatStorage", menuName = "Scriptable Objects/StatStorage")]
public class StatStorage : ScriptableObject
{
    [SerializeField] private float StartBuyRoundsPrice;
    public void Init()
    {
        BuyRoundsPrice = StartBuyRoundsPrice;
        IsFirstSpin = true;
        HighestScore = 0;
        RoundsBought = 0;
    }
    public float BuyRoundsPrice;

    public int RoundsBought = 0;

    public float HighestScore = 0;

    public bool IsFirstSpin = true;
}
