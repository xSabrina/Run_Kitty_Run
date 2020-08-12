using UnityEngine;
using UnityEngine.InputSystem;

public class FirePointRotation : MonoBehaviour
{
    public Rigidbody2D rbPlayer;
    float angle;
    Vector2 firePointYOffset = new Vector2(0, -0.9f);

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        CalcAngle();
        UpdateAngle();
    }

    private void UpdateAngle()
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void UpdatePosition()
    {
        transform.position = rbPlayer.position + firePointYOffset;
    }

    private void CalcAngle()
    {
        Vector2 direction = Mouse.current.position.ReadValue();
        Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        direction.x = direction.x - objectPos.x;
        direction.y = direction.y - objectPos.y;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
    }
    
}
