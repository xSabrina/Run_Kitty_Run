﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFading : MonoBehaviour
{

    public float delay = 0f;

    void Start ()
    {
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1f, 1f).setDelay(delay);
    }
}
