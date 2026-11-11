using System.Collections.Generic;
using UnityEngine;

public class StreekMult : PersistentPerk
{
    float curMult = 1;
    const float INCREASE = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void ApplyAfterScoreEffect(ref float score, ref List<(ScoreType, float)> scoreChanges)
    {
        base.ApplyAfterScoreEffect(ref score, ref scoreChanges);
        score *= curMult;
        scoreChanges.Add((ScoreType.Mult, curMult));

    }

    public override void OnScoreComplete(Symbol[] gambaSymbols, float score)
    {
        base.OnScoreComplete(gambaSymbols, score);

        if (score > 0)
        {
            curMult *= INCREASE;
        }
        else
        {
            curMult = 1;
        }
    }
}
