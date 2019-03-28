using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] string horizontalCTRL = "P1Horizontal";
    [SerializeField] string verticalCTRL = "P1Vertical";
    [SerializeField] string shootCTRL = "P1Fire";

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
        float deltaX = Input.GetAxis(horizontalCTRL) * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis(verticalCTRL) * Time.deltaTime * moveSpeed;

        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);

        if (deltaX > 0 && !facingRight)
        {
            Flip();
        }
        else if(deltaX < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = false;

        transform.Rotate(0f, 180f, 0f);
    }
}
