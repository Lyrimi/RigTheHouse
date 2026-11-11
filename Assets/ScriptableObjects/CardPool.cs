using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Cardpool", menuName = "Scriptable Objects/Cardpool")]
public class CardPool : ScriptableObject
{
    public SpawnAbleItem[] SpawnAbleItems;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}

[Serializable]
public class SpawnAbleItem
{
    public PerkBase Perk;
    public float Weight;
}