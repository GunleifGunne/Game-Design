using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] float moveSpeed = 10.0f;
  
    float xMin, xMax, yMin, yMax;

    Camera gameCamera;

    private void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        MovePlayer1();
        MovePlayer2();
    }

    private void SetUpMoveBoundaries()
    {
        gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void MovePlayer1()
    {
        float deltaX = Input.GetAxis("P1Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("P1Vertical") * Time.deltaTime * moveSpeed;

        float newXPos = Mathf.Clamp(player1.transform.position.x + deltaX, xMin, xMax);
        float newYPos = Mathf.Clamp(player1.transform.position.y + deltaY, yMin, yMax);
        player1.transform.position = new Vector2(newXPos, newYPos);
    }

    private void MovePlayer2()
    {
        float deltaX = Input.GetAxis("P2Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("P2Vertical") * Time.deltaTime * moveSpeed;

        float newXPos = Mathf.Clamp(player2.transform.position.x + deltaX, xMin, xMax);
        float newYPos = Mathf.Clamp(player2.transform.position.y + deltaY, yMin, yMax);
        player2.transform.position = new Vector2(newXPos, newYPos);
    }
}
