using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Level : ScriptableObject
{
    //name of the level, must be the same name as the scene
    public string levelName = "Level";
    //Time the player needed to finish the level
    public int levelTime;
    //number of the level (starting with 1)
    public int levelNr;

}