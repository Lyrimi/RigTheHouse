using System.Collections.Generic;
using UnityEngine;

public class Pitty : PersistentPerk
{
    int currentMisses = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnScoreComplete(Symbol[] gambaSymbols, float score)
    {
        base.OnScoreComplete(gambaSymbols, score);

        if (score > 0)
        {
            currentMisses = 1;
        }
        else
        {
            currentMisses++;
        }
    }

    public override void ApplyStatChangeEffect(ref List<Symbol> gameSymbols)
    {
        base.ApplyStatChangeEffect(ref gameSymbols);

        float highestWeight = 0;
        Symbol hightestSymbol = new();
        int index = -1;

        for (int i = 0; i < gameSymbols.Count; i++)
        {
            var item = gameSymbols[i];
            if (highestWeight < item.Weight || (highestWeight <= item.Weight && hightestSymbol.Value < item.Value))
            {
                highestWeight = item.Weight;
                hightestSymbol = item;
                index = i;

            }
        }

        hightestSymbol.Weight *= currentMisses;
        gameSymbols[index] = hightestSymbol;


    }
}
