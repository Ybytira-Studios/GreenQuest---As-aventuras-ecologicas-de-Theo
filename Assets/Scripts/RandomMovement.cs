using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 2f;
    public float minDirectionChangeAngle = 30f;

    private Vector2 movementDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRandomDirection();
    }

    void Update()
    {
        rb.velocity = movementDirection * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SetRandomDirection();
    }

    void SetRandomDirection()
    {
        Vector2 newDirection;
        //do
        //{
            float angle = Random.Range(0f, 360f);
            newDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        //}
        //while (Vector2.Angle(movementDirection, newDirection) < minDirectionChangeAngle);

        Debug.Log("MovementDirection"+movementDirection);
        Debug.Log("NewDirection"+newDirection);
        Debug.Log("angulo entre movement direction e newDirection"+Vector2.Angle(movementDirection, newDirection));

        movementDirection = newDirection;
    }
}
