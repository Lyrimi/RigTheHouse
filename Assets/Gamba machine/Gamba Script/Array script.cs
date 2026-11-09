using UnityEngine;

public class Arrayscript : MonoBehaviour
{


    public Rollingscript ToCall;

    public bool start;

    [SerializeField]
    int[] Array;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Update()
    {
        if (start)
        {
            ToCall.Ossamabinarray(Array);
            start = false;
        }
        
    }

}
