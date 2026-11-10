using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class SlotSpriteManager : MonoBehaviour
{
    public static SlotSpriteManager Instance;
    [SerializeField]
    EmblemMovementScript[] EmblemArray;

    public List<Sprite> Sprites;
    public List<EmblemMovementScript> Middleslots;
    public Action CallBack;

    public void SetEndSprite(int[] Array, Action callBack = null)
    {
        foreach (EmblemMovementScript emblem in EmblemArray)
        {
            emblem.Restart();
        }
        if (Array.Length != Middleslots.Count)
        {
            throw new Exception("More or less than 5 elements");
        }
        CallBack = callBack;
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
