using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ItemData))]
public class GoToScene : MonoBehaviour
{
    private ItemData itemData;
    [SerializeField] private string sceneName;
    [SerializeField] private string failedSceneName;
    [SerializeField] private bool shouldAskConfirmation;
    [SerializeField] private GameObject textLabelObj;
    [SerializeField] private GameObject textConfirmationObj;

    private bool hasAsked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (shouldAskConfirmation)
        {
            textLabelObj.SetActive(true);
            textConfirmationObj.SetActive(false);
        }
        itemData = GetComponent<ItemData>();
        itemData.Activate += GoToNextScene;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GoToNextScene()
    {
        if (shouldAskConfirmation)
        {
            if (!hasAsked && PlayerWallet.Instance.RemaningSpins <= 0)
            {
                AskConfirmation();
                hasAsked = true;
                return;
            }
        }
        LoadScene();
    }

    void LoadScene()
    {
        if (shouldAskConfirmation && PlayerWallet.Instance.RemaningSpins <= 0)
        {
            SceneManager.LoadScene(failedSceneName);
            return;
        }
        SceneManager.LoadScene(sceneName);
    }

    void AskConfirmation()
    {
        textLabelObj.SetActive(false);
        textConfirmationObj.SetActive(true);
    }
}
