using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInFadeOutLayer : MonoBehaviour
{
    [SerializeField] private float durationS = 0.05f;

    [SerializeField] private bool visible;


    private IEnumerator FadeIn(SpriteRenderer renderer)
    {
        float alphaVal = renderer.color.a;
        Color tmp = renderer.color;

        while (renderer.color.a < 1)
        {
            alphaVal += 0.01f;
            tmp.a = alphaVal;
            renderer.color = tmp;

            yield return new WaitForSeconds(durationS); // update interval
        }
        visible = true;
    }

    private IEnumerator FadeOut(SpriteRenderer renderer)
    {
        float alphaVal = renderer.color.a;
        Color tmp = renderer.color;

        while (renderer.color.a > 0)
        {
            alphaVal -= 0.01f;
            tmp.a = alphaVal;
            renderer.color = tmp;

            yield return new WaitForSeconds(durationS); // update interval
        }


        visible = false;
    }
    public void ExecuteFadeIN()
    {
        //if 2d with sprite
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            StartCoroutine(FadeIn(sprites[i]));
        }
    }
    public void ExecuteFadeOUT()
    {
        //if 2d with sprite
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            StartCoroutine(FadeOut(sprites[i]));
        }
    }
}
