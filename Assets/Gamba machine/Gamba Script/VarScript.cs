using UnityEngine;

public class VarScript : MonoBehaviour
{
    public static VarScript instance;

    public float particleYOffSet;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            print(instance.gameObject.name);
            print(gameObject.name);
            throw new System.Exception("ERror");
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
