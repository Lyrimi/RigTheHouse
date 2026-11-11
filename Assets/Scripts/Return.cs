using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    [SerializeField] private string SceneName;
    InputAction enter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enter = InputSystem.actions.FindAction("Select");
        enter.performed += GoToShop;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GoToShop(InputAction.CallbackContext callbackContext)
    {
        if (gameObject.activeInHierarchy == false || gameObject.activeSelf == false)
        {
            return;
        }
        SceneManager.LoadScene(SceneName);
    }
}
