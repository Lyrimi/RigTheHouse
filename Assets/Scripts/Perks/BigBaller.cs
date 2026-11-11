using System.Collections.Generic;
using UnityEngine;

public class BigBaller : PersistentPerk
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    const int SMYBOLINDEX = 2;
    public override void OnScoreComplete(Symbol[] gambaSymbols)
    {
        base.OnScoreComplete(gambaSymbols);

        bool match = true;
        foreach (var item in gambaSymbols)
        {
            if (item.SymbolIndex != SMYBOLINDEX)
            {
                match = false;
            }
        }

        if (match)
        {
            UpChanceAndValue();
        }
    }

    private float ChanceMod = 1;
    private float ValueMod = 1;

    void UpChanceAndValue()
    {
        ChanceMod += 0.2f;
        ValueMod *= 1.1f;
    }

    public override void ApplyStatChangeEffect(ref List<Symbol> gameSymbols)
    {
        for (int i = 0; i < gameSymbols.Count; i++)
        {
            if (gameSymbols[i].SymbolIndex == SMYBOLINDEX)
            {
                Symbol symbol = gameSymbols[i];
                symbol.Value *= ValueMod;
                symbol.Weight *= ChanceMod;
                gameSymbols[i] = symbol;
            }
        }
    }
}
