using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private float _startingCash;
    private float cash;

    public Action OnCashChange;
    public Action OnSpinsChange;

    [SerializeField] int remaningSpins;

    public int RemaningSpins
    {
        get
        {
            return remaningSpins;
        }
        set
        {
            remaningSpins = value;
            OnSpinsChange?.Invoke();
        }
    }

    public float Cash
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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Destroy(this);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        Cash = _startingCash;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool HasEnoughCash(float cost)
    {
        return cost < Cash;
    }
}
