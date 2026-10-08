using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO.Ports;

public class CardManager : MonoBehaviour
{

    [SerializeField] public List<CardData> _cards = new();
    private InputAction _testAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _testAction = InputSystem.actions.FindAction("Test");
        _testAction.performed += Stuff;
        print("Start Ran");
    }

    // Update is called once per frame
    void Update()
    {
        print(_testAction.ReadValue<int>());
    }

    void Stuff(InputAction.CallbackContext callbackContext)
    {
        print("preformed");

    }
}
