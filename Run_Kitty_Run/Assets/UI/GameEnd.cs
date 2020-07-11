using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{

    public Text timeText;
    private int completeTime;
    private int completeMinutes = 0;
    private int completeSeconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManagerScript.instance.levels.Count; i++)
        {
            completeTime += GameManagerScript.instance.levels[i].levelTime;
        }

        completeMinutes = completeTime / 60;
        completeSeconds = completeTime % 60;
        
        timeText.text = completeMinutes.ToString("00") + ":" + completeSeconds.ToString("00.00");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
