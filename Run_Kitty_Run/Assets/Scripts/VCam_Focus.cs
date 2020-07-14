using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCam_Focus : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
}
