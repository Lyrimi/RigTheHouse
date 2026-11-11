using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PerkManger : MonoBehaviour
{
    public static PerkManger Instance;

    [SerializeField] private StatStorage statStorage;

    [SerializeField] public Dictionary<PerkBase, int[]> PerkTable;
    [SerializeField] private int _maxCodeLength;

    InputAction R;
    InputAction B;
    InputAction G;
    InputAction Y;
    InputAction LR;

    public List<PerkBase> ActivePerks = new();

    public List<PerkBase> perkObjects = new();

    public bool IsActive;

    public Action<bool, GameObject> OnCompletion;

    public Action OnScoreComplete;

    public PerkBase PerkAtempt;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Destroy(this);
            return;
        }
        Instance = this;
        statStorage.Init();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ComboButtonIndicator.Instance.MaxCodeLen = _maxCodeLength;
        R = InputSystem.actions.FindAction("R");
        G = InputSystem.actions.FindAction("G");
        B = InputSystem.actions.FindAction("B");
        Y = InputSystem.actions.FindAction("Y");
        LR = InputSystem.actions.FindAction("Move");

        R.performed += RA;
        G.performed += GA;
        B.performed += BA;
        Y.performed += YA;
        LR.performed += LRA;

        DontDestroyOnLoad(gameObject);
    }

    void RA(InputAction.CallbackContext callbackContext)
    {
        AddInput(4);
    }

    void GA(InputAction.CallbackContext callbackContext)
    {
        AddInput(1);
    }

    void BA(InputAction.CallbackContext callbackContext)
    {
        AddInput(3);
    }

    void YA(InputAction.CallbackContext callbackContext)
    {
        AddInput(2);
    }

    void LRA(InputAction.CallbackContext callbackContext)
    {
        Vector2 value = callbackContext.ReadValue<Vector2>();
        int num = value.x switch
        {
            1 => 5,
            -1 => 0,
            _ => -1,
        };
        AddInput(num);
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
        ComboButtonIndicator.Instance.Reset();
    }

    void AddInput(int num)
    {
        if (!IsActive)
        {
            return;
        }
        if (ComboButtonIndicator.Instance == null)
        {
            if (gameObject == null)
            {
                Debug.LogWarning("WHAT THE FUCK");
            }
            else
            {
                Debug.LogWarning($"missing ComboButtonIndciator {gameObject.name}");
            }
        }
        else
        {
            ComboButtonIndicator.Instance.AddVisualIndicator(num);
        }


        _curentCombo.Add(num);
        CheckMatch();
        print("CheckedMach");
    }

    void CheckMatch()
    {
        int listLen = _curentCombo.Count;

        if (listLen > _maxCodeLength)
        {
            Failed();
        }

        var item = PerkTable[PerkAtempt];
        {
            if (listLen != item.Length)
            {
                return;
            }

            bool match = true;
            for (int i = 0; i < listLen; i++)
            {
                if (_curentCombo[i] != item[i])
                {
                    match = false;
                }
            }

            if (match)
            {
                CodeEntered(PerkAtempt);
            }
        }
    }

    void CodeEntered(PerkBase perk)
    {
        print(perk.gameObject.name);

        RestartInput();

        AddPerk(perk);
    }

    void AddPerk(PerkBase perk)
    {
        perkObjects.Add(perk);
        OnCompletion?.Invoke(true, perk.gameObject);
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
        OnCompletion?.Invoke(false, null);
    }
}

