using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePointRotation : MonoBehaviour
{
    Rigidbody2D rbPlayer;

    Vector3 direction;
    float angle;

    Vector2 firePointYOffset = new Vector2(0, -0.2f);

    // Update is called once per frame
    void Update()
    {
        rbPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        direction = Input.mousePosition - Camera.main.WorldToScreenPoint(rbPlayer.position);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        UpdatePosition();
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void UpdatePosition()
    {
        transform.position = rbPlayer.position + firePointYOffset + (Vector2)direction.normalized;
    }
}
