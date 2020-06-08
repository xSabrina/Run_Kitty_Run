using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Rigidbody2D rb;

    public float x;
    public float y;

    public float speed;

    public float[] movementStartTimeInterval = new float[2];
    public float[] movementTimeInterval = new float[2];

    private Vector3 directionalVector;

    private float deltaTime;
    private bool isMoving;

    private bool invertXPos;
    private bool invertXNeg;
    private bool invertYPos;
    private bool invertYNeg;

    private bool isVisible;

    // Start is called before the first frame update
    void Start()
    {
        PlaceEnemy();
        InitValues();
}

    // Update is called once per frame
    void Update()
    {
        if (isVisible) {
            UpdatePositionCoordinates();
            HandleMovement();
        }
        if (Input.GetKey(KeyCode.A))
        {
            Camera.main.transform.Translate(new Vector3(-1, 0, 0)*Time.deltaTime);
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            Camera.main.transform.Translate(new Vector3(1, 0, 0)*Time.deltaTime);
        }
    }

    void PlaceEnemy()
    {
        this.transform.localPosition = new Vector3(x, y, 0);
    }

    void InitValues()
    {
        deltaTime = 0;
        isMoving = true;
        invertXPos = false;
        invertXNeg = false;
        invertYPos = false;
        invertYNeg = false;
    }

    void UpdatePositionCoordinates()
    {
        x = this.transform.localPosition.x;
        y = this.transform.localPosition.y;
    }

    void HandleMovement()
    {
        if (deltaTime <= 0 && isMoving) 
        {
            rb.velocity = new Vector3(0, 0, 0);
            System.Random r = new System.Random();
            deltaTime = movementStartTimeInterval[0] + ((movementStartTimeInterval[1] - movementStartTimeInterval[0]) * (float)r.NextDouble());
            isMoving = false;
        }
        deltaTime -= Time.deltaTime;
        if (deltaTime <= 0 && !isMoving)
        {
            System.Random r = new System.Random();
            deltaTime = movementTimeInterval[0] + ((movementTimeInterval[1] - movementTimeInterval[0]) * (float)r.NextDouble());
            isMoving = true;
            CreateRandomDirectionalVector();
        }
    }

    void CreateRandomDirectionalVector()
    {
        Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        directionalVector = Vector3.Normalize(randomVector);
        CheckCoordinateInvert();
        rb.velocity = directionalVector * speed;
    }

    void CheckCoordinateInvert()
    {
        if (invertXNeg)
        {
            if (directionalVector.x <= 0)
            {
                directionalVector.x = -directionalVector.x;
            }
            invertXNeg = false;
        }
        if (invertXPos)
        {
            if (directionalVector.x >= 0)
            {
                directionalVector.x = -directionalVector.x;
            }
            invertXPos = false;
        }
        if (invertYNeg)
        {
            if (directionalVector.y <= 0)
            {
                directionalVector.y = -directionalVector.y;
            }
            invertYNeg = false;
        }
        if (invertYPos)
        {
            if (directionalVector.y >= 0)
            {
                directionalVector.y = -directionalVector.y;
            }
            invertYPos = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            // enemy collision

            //collision.attachedRigidbody.velocity = speed * directionalVector;
        }
        else
        {
            CatchCoordinateInvert(collision.gameObject.name);
        }
    }

    void CatchCoordinateInvert(string wall)
    {
        rb.velocity = new Vector3(0, 0, 0);
        deltaTime = 0;
        if (wall.Equals("LeftWall"))
        {
            invertXNeg = true;
        }
        else if (wall.Equals("RightWall"))
        {
            invertXPos = true;
        }
        else if (wall.Equals("UpperWall"))
        {
            invertYPos = true;
        }
        else if (wall.Equals("LowerWall"))
        {
            invertYNeg = true;
        }
    }

    void OnBecameInvisible()
    {
        isVisible = false;
        rb.velocity = new Vector3(0, 0, 0);
        deltaTime = 0;
    }

    void OnBecameVisible()
    {
        isVisible = true;
    }
}
