using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    [SerializeField] private StatStorage statStorage;
    [SerializeField] private TMP_Text highScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScoreText.text = $"Highest Score:\n{statStorage.HighestScore}";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
