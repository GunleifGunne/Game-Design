using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float moveSpeed = 50.0f;
    [SerializeField] string horizontalCTRL = "P1Horizontal";
    [SerializeField] string verticalCTRL = "P1Vertical";

    private float xMin, xMax, yMin, yMax;
    private bool facingRight = true;

    Camera gameCamera;

    private void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
    }

    private void SetUpMoveBoundaries()
    {
        gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void Move()
    {
        float moveX = Input.GetAxis(horizontalCTRL) * Time.deltaTime * moveSpeed;
        float moveY = Input.GetAxis(verticalCTRL) * Time.deltaTime * moveSpeed;

        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }

        float newXPos = Mathf.Clamp(transform.position.x + moveX, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + moveY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    public void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
