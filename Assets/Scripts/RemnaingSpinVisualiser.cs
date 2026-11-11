using TMPro;
using UnityEngine;

public class RemnaingSpinVisualiser : MonoBehaviour
{
    TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TMP_Text>();
        PlayerWallet.Instance.OnSpinsChange += UpdateText;
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateText()
    {
        text.text = $"{PlayerWallet.Instance.RemaningSpins}";
    }
}
