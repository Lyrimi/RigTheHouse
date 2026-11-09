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
        int[] symbols = new int[5];

        for (int i = 0; i < symbols.Length; i++)
        {
            symbols[i] = GetWeightedRandomSymbol();
        }

        string combined = "";

        foreach (var item in symbols)
        {
            combined += $"{item} | ";
        }

        Debug.Log(combined);
    }

    public void RecaculateSymbolList()
    {
        gameSymbols = _baseSymbols.ToList();
        foreach (var item in PerkManger.Instance.ActivePerks)
        {
            item.ApplyEffect();
        }
    }

    public int GetWeightedRandomSymbol()
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
                return item.SymbolIndex;
            }

        }
        return -1;
    }
}


[Serializable]
public struct Symbol
{
    public int SymbolIndex;
    public float Weight;

    public float Value;
}
