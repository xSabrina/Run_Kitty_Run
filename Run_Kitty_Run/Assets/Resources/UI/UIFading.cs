using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFading : MonoBehaviour
{

    public CanvasGroup canvasGroup;

    public void FadeIn(Component comp)
    {
        StartCoroutine(FadeCanvas(canvasGroup, canvasGroup.alpha, 1));
    }

    public void FadeOut(Component comp)
    {
        StartCoroutine(FadeCanvas(canvasGroup, canvasGroup.alpha, 0));
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1)
            {
                if (end == 0)
                {
                    gameObject.SetActive(false);
                } else if (end == 1)
                {
                    gameObject.SetActive(true);
                }
                break;
            }

            yield return new WaitForEndOfFrame();
        }
        print("done fading");

    }
}
