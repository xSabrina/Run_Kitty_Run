using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartScript : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //assigns player variable and moves player to levelstart object position
        player = GameObject.Find("Player");
        player.transform.position = gameObject.transform.position;
    }

}
