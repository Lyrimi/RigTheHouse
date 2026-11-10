using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private int _startingCash;
    private int cash;

    public Action OnCashChange;

    public int Cash
    {
        get
        {
            return cash;
        }
        set
        {
            cash = value;
            OnCashChange?.Invoke();
        }
    }
    public static PlayerWallet Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Cash = _startingCash;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool HasEnoughCash(int cost)
    {
        return cost < Cash;
    }
}
