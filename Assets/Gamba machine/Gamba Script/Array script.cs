using System;
using UnityEngine;

public class Arrayscript : MonoBehaviour
{
    [SerializeField]
    EmblemMovementScript[] EmblemArray;


    public bool start;

    [SerializeField]
    int[] Array;


    void Start()
    {

    }
    private void Update()
    {
        if (start)
        {


            SlotSpriteManager.Instance.SetEndSprite(Array);
            start = false;
        }

    }

}
