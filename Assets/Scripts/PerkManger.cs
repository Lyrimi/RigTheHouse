using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;

public class PerkManger : MonoBehaviour
{
    public static PerkManger Instance;

    [SerializeField] public Dictionary<Perk, int[]> PerkTable;
    [SerializeField] private int _maxCodeLength;
    [SerializeField] private ComboButtonIndicator comboButtonIndicator;

    public List<Perk> ActivePerks = new();

    public bool IsActive;

    public Action<bool> OnCompletion;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        comboButtonIndicator.MaxCodeLen = _maxCodeLength;
    }

    // Update is called once per frame

    [SerializeField] private int _debugNum;
    [SerializeField] private bool _debugbutton;
    List<int> _curentCombo = new();
    void Update()
    {
        if (_debugbutton)
        {
            _debugbutton = false;
            AddInput(_debugNum);
        }
    }

    public void RestartInput()
    {
        _curentCombo = new();
        comboButtonIndicator.Reset();
        print("Restarting");
    }

    void AddInput(int num)
    {
        if (!IsActive)
        {
            return;
        }
        comboButtonIndicator.AddVisualIndicator(num);
        _curentCombo.Add(num);
        CheckMatch();
    }

    void CheckMatch()
    {
        int listLen = _curentCombo.Count;

        if (listLen > _maxCodeLength)
        {
            Failed();
        }

        foreach (var item in PerkTable)
        {
            if (listLen != item.Value.Length)
            {
                continue;
            }

            bool match = true;
            for (int i = 0; i < listLen; i++)
            {
                if (_curentCombo[i] != item.Value[i])
                {
                    match = false;
                }
            }

            if (match)
            {
                CodeEntered(item.Key);
            }
        }
    }

    void CodeEntered(Perk perk)
    {
        print(perk.gameObject.name);
        ActivePerks.Add(perk);
        RestartInput();
        OnCompletion?.Invoke(true);
    }

    void Failed()
    {
        print("failed");
        RestartInput();
        OnCompletion?.Invoke(false);
    }
}

