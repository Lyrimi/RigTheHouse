using UnityEngine;
using System.Collections.Generic;
public class DoubleChanceItems : PerkBase
{
    [SerializeField] private int[] symbolsToAffect;
    [SerializeField] private float mult = 1.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ApplyStatChangeEffect(ref List<Symbol> gameSymbols)
    {

        for (int i = 0; i < gameSymbols.Count; i++)
        {
            foreach (var item in symbolsToAffect)
            {
                if (gameSymbols[i].SymbolIndex == item)
                {
                    Symbol symbol = gameSymbols[i];
                    symbol.Weight *= mult;
                    gameSymbols[i] = symbol;
                }
            }
        }
    }
}
