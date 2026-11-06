using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO.Ports;

public class ItemManager : MonoBehaviour
{

    [SerializeField] public List<ItemData> _items = new();
    [SerializeField] private Transform _cardPackMove;
    private InputAction _moveAction;
    private InputAction _sellectAction;

    private int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _sellectAction = InputSystem.actions.FindAction("Select");
        _moveAction.performed += Move;
        _sellectAction.performed += SelectItem;
        print("Start Ran");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnEnable()
    {
        if (_items[index].enabled)
        {
            _items[index].IsSelected = true;
        }

    }
    void OnDisable()
    {
        if (_items[index].enabled)
        {
            _items[index].IsSelected = false;
        }
    }



    void Move(InputAction.CallbackContext callbackContext)
    {
        Vector2 value = callbackContext.ReadValue<Vector2>();
        _items[index].IsSelected = false;
        index = value.x switch
        {
            1 => Mathf.Min(index + 1, _items.Count - 1),
            -1 => Mathf.Max(index - 1, 0),
            _ => index,
        };
        _items[index].IsSelected = true;
    }

    void SelectItem(InputAction.CallbackContext callbackContext)
    {
        print("Item Selcted");
        var selectItem = _items[index].GetComponent<CardPackSelect>();
        if (selectItem)
        {
            selectItem.MovePoint = _cardPackMove;
        }
        _items[index].Activate?.Invoke();
    }
}
