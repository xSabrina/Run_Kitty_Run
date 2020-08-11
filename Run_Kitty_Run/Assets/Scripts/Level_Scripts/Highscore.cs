using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore
{
    //username and score in seconds, the lower the score the better
    public string userName;
    public string score;
    public Highscore(string userName, string score)
    {
        this.userName = userName;
        this.score = score;
    }

}
