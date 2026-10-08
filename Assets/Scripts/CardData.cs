using System;
using Unity.VisualScripting;
using UnityEngine;

public class CardData : MonoBehaviour
{
    public bool IsSelected
    {
        set
        {
            _isSelected = value;
            if (value)
            {
                OnSelect?.Invoke();
            }
            else
            {
                OnDeselect?.Invoke();
            }
        }
        get
        {
            return _isSelected;
        }

    }

    [SerializeField] private bool _debugOverideSlected;

    /// <summary>
    /// DON'T YOU FUCKING DARE MODIFY THIS BOOL IF YOU WANT TO CHANGE IT USE THE PUBLIC ONE
    /// </summary>
    private bool _isSelected;

    public Action OnSelect;
    public Action OnDeselect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_debugOverideSlected != IsSelected)
        {
            IsSelected = _debugOverideSlected;
        }
    }
}
