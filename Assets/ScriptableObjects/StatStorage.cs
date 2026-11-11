
using UnityEngine;

[CreateAssetMenu(fileName = "StatStorage", menuName = "Scriptable Objects/StatStorage")]
public class StatStorage : ScriptableObject
{
    [SerializeField] private float StartBuyRoundsPrice;
    public void Init()
    {
        BuyRoundsPrice = StartBuyRoundsPrice;
    }
    public float BuyRoundsPrice;
}
