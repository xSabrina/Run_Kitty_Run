using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointRotation : MonoBehaviour
{
    public Rigidbody2D rbPlayer;
    public Rigidbody2D rbFirePoint;
    public Camera cam;

    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rbFirePoint.position = rbPlayer.position;
        Vector2 lookDirection = mousePos - rbFirePoint.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rbFirePoint.rotation = angle;
    }
}
