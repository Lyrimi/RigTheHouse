using UnityEngine;
using static GamblingManager;
public class DoubleValueGems : Perk
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();

        for (int i = 0; i < Instance.gameSymbols.Count; i++)
        {
            if (Instance.gameSymbols[i].SymbolIndex < 3)
            {
                Symbol symbol = Instance.gameSymbols[i];
                symbol.Value *= 2;
                Instance.gameSymbols[i] = symbol;
            }
        }
    }
}
