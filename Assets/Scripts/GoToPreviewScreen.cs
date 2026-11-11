using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ItemData))]
public class GoToScene : MonoBehaviour
{
    private ItemData itemData;
    [SerializeField] private string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemData = GetComponent<ItemData>();
        itemData.Activate += GoToNextScene;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GoToNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
