using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GamblingManager : MonoBehaviour
{
    [SerializeField] Symbol[] _baseSymbols;

    public List<Symbol> gameSymbols;

    public static GamblingManager Instance;

    public bool _debugButton;
    public bool _debugButton2;

    InputAction selectAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectAction = InputSystem.actions.FindAction("Select");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Destroy(this);
        }
        Instance = this;
        RecaculateSymbolList();
        selectAction.performed += Input;
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

    void Input(InputAction.CallbackContext callbackContext)
    {
        Gamble();
    }
    Symbol[] gambaSymbols;

    public bool isSpining;
    public void Gamble()
    {
        RecaculateSymbolList();
        if (isSpining)
        {
            return;
        }
        if (PlayerWallet.Instance.RemaningSpins <= 0)
        {
            return;
        }
        PlayerWallet.Instance.RemaningSpins -= 1;
        isSpining = true;

        gambaSymbols = new Symbol[5];

        for (int i = 0; i < gambaSymbols.Length; i++)
        {
            gambaSymbols[i] = GetWeightedRandomSymbol();
        }

        string combined = "";

        foreach (var item in gambaSymbols)
        {
            combined += $"{item.SymbolIndex} | ";
        }

        Debug.Log(combined);
        PreHandleSymbols(gambaSymbols);
    }

    public void RecaculateSymbolList()
    {
        gameSymbols = _baseSymbols.ToList();
        foreach (var item in PerkManger.Instance.ActivePerks)
        {
            item.ApplyStatChangeEffect(ref gameSymbols);
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
        int[] spriteIndexs = new int[symbols.Length];

        for (int i = 0; i < spriteIndexs.Length; i++)
        {
            spriteIndexs[i] = symbols[i].SymbolIndex;
        }
        SlotSpriteManager.Instance.SetEndSprite(spriteIndexs, FinishedGambaAnim);
        CaculateScore(symbols);
    }
    List<(ScoreType, float)> ScoreChanges = new();

    void CaculateScore(Symbol[] symbols)
    {
        int longestStreak = 0;
        Symbol lSSymbol;
        float LStreakValue = 0;
        float CStreakValue = 0;

        ScoreChanges = new();
        List<(ScoreType, float)> cScoreAddtions = new();

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
                cScoreAddtions = new();
            }
            currentStreak++;
            CStreakValue += item.Value;
            cScoreAddtions.Add((ScoreType.Addtion, item.Value));

            if (currentStreak > longestStreak)
            {
                longestStreak = currentStreak;
                lSSymbol = item;
                LStreakValue = CStreakValue;
                ScoreChanges = cScoreAddtions;
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
        ScoreChanges.Add((ScoreType.Mult, paternMult));

        ScorePostProcesing(score);
    }

    void ScorePostProcesing(float score)
    {
        foreach (var item in PerkManger.Instance.ActivePerks)
        {
            item.ApplyAfterScoreEffect(ref score, ref ScoreChanges);
            if (item is PersistentPerk)
            {
                ((PersistentPerk)item).OnScoreComplete(gambaSymbols, score);
            }
        }
        foreach (var item in ScoreChanges)
        {
            print($"{item.Item1} | {item.Item2}");
        }
        print(score);





    }

    void FinishedGambaAnim()
    {
        ScoreVisualiser.Instance.ApplyScoreChanges(ScoreChanges);
        print("Finished Gambling runing Score anims");
    }

    [SerializeField] private GameObject FinishedText;
    public void CheckIfFinsihed()
    {
        if (PlayerWallet.Instance.RemaningSpins <= 0)
        {
            FinishedText.SetActive(true);
        }
    }
}


[Serializable]
public struct Symbol
{
    public int SymbolIndex;
    public float Weight;

    public float Value;

    public string Identifyer;
}

[Serializable]
public enum ScoreType
{
    Addtion,
    Mult,
}
