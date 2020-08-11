using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFading : MonoBehaviour
{

    public float delay = 0f;
    public float duration = 0.6f;

    void Start ()
    {
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1f, duration).setEase(LeanTweenType.easeInQuad).setDelay(delay);
    }
}
