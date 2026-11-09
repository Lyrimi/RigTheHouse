using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SlotSpriteManager : MonoBehaviour
{
    public static SlotSpriteManager Instance;

    public List<Sprite> Sprites;
    public List<GameObject> Middleslots;
    
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
