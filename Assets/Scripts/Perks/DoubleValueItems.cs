using System.Collections.Generic;
using UnityEngine;
using static GamblingManager;
public class DoubleValueItems : PerkBase
{
    [SerializeField] private int[] symbolsToAffect;
    [SerializeField] private float aditon = 2;
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

        for (int i = 0; i < Instance.gameSymbols.Count; i++)
        {
            foreach (var item in symbolsToAffect)
            {
                if (Instance.gameSymbols[i].SymbolIndex == item)
                {
                    Symbol symbol = Instance.gameSymbols[i];
                    symbol.Value += aditon;
                    Instance.gameSymbols[i] = symbol;
                }
            }
        }
    }
}
