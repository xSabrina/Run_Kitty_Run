using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        
    private void OnTriggerEnter(Collider other)
    {
        GameManagerScript.instance.EndLevel();

    }
}
