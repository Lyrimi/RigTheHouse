using UnityEngine;
using UnityEngine.InputSystem;

public class BadToTheBone : MonoBehaviour
{
    InputAction BadTB;
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        BadTB = InputSystem.actions.FindAction("BTB");
        audioSource.Stop();
        BadTB.performed += PlayBTB;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayBTB(InputAction.CallbackContext callbackContext)
    {
        audioSource.Play();
    }


}
