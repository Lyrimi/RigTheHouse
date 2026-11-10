using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamblingManager : MonoBehaviour
{
    [SerializeField] Symbol[] _baseSymbols;

    public List<Symbol> gameSymbols;

    public static GamblingManager Instance;

    public bool _debugButton;
    public bool _debugButton2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        RecaculateSymbolList();
    }

    // Update is called once per frame
    void Update()
    {
        if (_debugButton)
        {
            _debugButton = false;
            Gamble();
        }
        if (_debugButton2)
        {
            _debugButton2 = false;
            RecaculateSymbolList();
        }
    }

    public void Gamble()
    {
        Symbol[] symbols = new Symbol[5];

        for (int i = 0; i < symbols.Length; i++)
        {
            symbols[i] = GetWeightedRandomSymbol();
        }

        string combined = "";

        foreach (var item in symbols)
        {
            combined += $"{item.SymbolIndex} | ";
        }

        Debug.Log(combined);
    }

    public void RecaculateSymbolList()
    {
        gameSymbols = _baseSymbols.ToList();
        foreach (var item in PerkManger.Instance.ActivePerks)
        {
            item.ApplyStatChangeEffect();
        }
    }

    public Symbol GetWeightedRandomSymbol()
    {
        float totalWeight = 0;

        foreach (var item in gameSymbols)
        {
            totalWeight += item.Weight;
        }

        float random = Random.Range(0, totalWeight);

        foreach (var item in gameSymbols)
        {
            random -= item.Weight;

            if (random < 0)
            {
                return item;
            }

        }
        throw new Exception("No symbols found");
    }
    void PreHandleSymbols(Symbol[] symbols)
    {
        foreach (PerkBase item in PerkManger.Instance.ActivePerks)
        {
            if (item is PreEvalPerk)
            {
                ((PreEvalPerk)item).ApplyPreCaclEffect(ref symbols);
            }
        }
        CaculateScore(symbols);
    }
    void CaculateScore(Symbol[] symbols)
    {
        int longestStreak = 0;
        Symbol lSSymbol;
        float LStreakValue = 0;
        float CStreakValue = 0;
        int lastSymbol = -1;
        int currentStreak = 0;

        for (int i = 0; i < symbols.Length; i++)
        {
            var item = symbols[i];
            if (item.SymbolIndex != lastSymbol)
            {
                lastSymbol = item.SymbolIndex;
                currentStreak = 0;
                CStreakValue = 0;
            }
            currentStreak++;
            CStreakValue += item.Value;

            if (currentStreak > longestStreak)
            {
                longestStreak = currentStreak;
                lSSymbol = item;
                LStreakValue = CStreakValue;
            }
        }

        float paternMult = longestStreak switch
        {
            5 => 27,
            4 => 9,
            3 => 3,
            _ => 0,
        };

        float score = LStreakValue * paternMult;
        ScorePostProcesing(score);
    }

    void ScorePostProcesing(float score)
    {
        foreach (var item in PerkManger.Instance.ActivePerks)
        {
            item.ApplyAfterScoreEffect(ref score);
        }
    }
}


[Serializable]
public struct Symbol
{
    public int SymbolIndex;
    public float Weight;

    public float Value;
}
