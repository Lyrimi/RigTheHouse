using System.Collections.Generic;
using UnityEngine;

public class PerkBase : MonoBehaviour
{

    //Perk
    //PreEvalPerk

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void ApplyStatChangeEffect()
    {

    }

    public virtual void ApplyAfterScoreEffect(ref float score, ref List<(ScoreType, float)> scoreChanges)
    {

    }
}
