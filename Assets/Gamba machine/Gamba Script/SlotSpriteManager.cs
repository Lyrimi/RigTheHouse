using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SlotSpriteManager : MonoBehaviour
{
    public static SlotSpriteManager Instance;

    public List<Sprite> Sprites;
    public List<EmblemMovementScript> Middleslots;
    
    public void SetEndSprite(int[] Array)
    {
        if (Array.Length != Middleslots.Count) 
        {
            Debug.LogError("More or less than 5 elements");
            return;
        }

        for (int i = 0; i < Middleslots.Count; i++)
        {
            Middleslots[i].endsprite = Sprites[Array[i]];
        }
    }

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
