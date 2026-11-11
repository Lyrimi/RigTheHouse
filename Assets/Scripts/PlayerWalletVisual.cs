using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class PlayerWalletVisual : MonoBehaviour
{
    private TMP_Text _text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        PlayerWallet.Instance.OnCashChange += UpdateText;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBecameVisible()
    {
        UpdateText();
    }

    void UpdateText()
    {
        int cashAmount = (int)PlayerWallet.Instance.Cash;
        _text.text = $"{cashAmount}";
    }
}
