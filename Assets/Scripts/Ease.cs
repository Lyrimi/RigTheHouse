using System.Collections;
using UnityEngine;
public static class Ease
{
    public static float EaseOutElastic(float t)
    {
        const float c4 = 2f * Mathf.PI / 3f;
        return t == 0f ? 0f : t == 1f ? 1f
            : Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1f;
    }

    public static float EaseInOutCirc(float t)
    {
        return t < 0.5f
            ? (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * t, 2f))) / 2f
            : (Mathf.Sqrt(1f - Mathf.Pow(-2f * t + 2f, 2f)) + 1f) / 2f;
    }
}