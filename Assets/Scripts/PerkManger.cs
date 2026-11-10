using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerkManger : MonoBehaviour
{
    public static PerkManger Instance;

    [SerializeField] public Dictionary<PerkBase, int[]> PerkTable;
    [SerializeField] private int _maxCodeLength;
    [SerializeField] private ComboButtonIndicator comboButtonIndicator;

    InputAction R;
    InputAction B;
    InputAction G;
    InputAction Y;

    public List<PerkBase> ActivePerks = new();

    public bool IsActive;

    public Action<bool> OnCompletion;

    public Action OnScoreComplete;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        comboButtonIndicator.MaxCodeLen = _maxCodeLength;
        R = InputSystem.actions.FindAction("R");
        G = InputSystem.actions.FindAction("G");
        B = InputSystem.actions.FindAction("B");
        Y = InputSystem.actions.FindAction("Y");

        R.performed += RA;
        G.performed += GA;
        B.performed += BA;
        Y.performed += YA;
    }

    void RA(InputAction.CallbackContext callbackContext)
    {
        AddInput(0);
    }

    void GA(InputAction.CallbackContext callbackContext)
    {
        AddInput(1);
    }

    void BA(InputAction.CallbackContext callbackContext)
    {
        AddInput(2);
    }

    void YA(InputAction.CallbackContext callbackContext)
    {
        AddInput(3);
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

    void CodeEntered(PerkBase perk)
    {
        print(perk.gameObject.name);

        RestartInput();
        OnCompletion?.Invoke(true);
        AddPerk(perk);
    }

    void AddPerk(PerkBase perk)
    {

        if (perk is PersistentPerk)
        {
            Type perkType = perk.GetType();
            PersistentPerk addedperk = gameObject.AddComponent(perkType) as PersistentPerk;
            ActivePerks.Add(addedperk);
            return;
        }
        ActivePerks.Add(perk);
    }

    void Failed()
    {
        print("failed");
        RestartInput();
        OnCompletion?.Invoke(false);
    }
}

