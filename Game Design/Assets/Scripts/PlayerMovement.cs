using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject player1ShotDirection;
    public GameObject player2ShotDirection;

    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    float p1xTranslation;

    public GameObject shot;
    public Transform p1ShotSpawn;
    public Transform p2ShotSpawn;
    public float fireRate;
    float p1yTranslation;

    private bool usingAxis;

    private float p1NextFire;
    private float p2NextFire;
    float xMin, xMax, yMin, yMax;

    Camera gameCamera;

    private void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        // Get the Fire-Buttons of both players.
        float p1Fire = Input.GetAxis("P1Fire");
        float p2Fire = Input.GetAxis("P2Fire");

        //if(p1Fire == 1 && Time.time > p1NextFire)
        //{
        //    p1NextFire = Time.time + fireRate;
        //    Instantiate(shot, p1ShotSpawn.position, p1ShotSpawn.rotation);
        //};

        //if(p2Fire == 1 && Time.time > p2NextFire)
        //{
        //    p2NextFire = Time.time + fireRate;
        //    Instantiate(shot, p2ShotSpawn.position, p2ShotSpawn.rotation);
        //}

        // Get the horizontal and vertical axis of both players.
        p1xTranslation = Input.GetAxis("P1Horizontal") * speed;
        p1yTranslation = Input.GetAxis("P1Vertical") * speed;

        

        float p2xTranslation = Input.GetAxis("P2Horizontal") * speed;
        float p2yTranslation = Input.GetAxis("P2Vertical") * speed;


        // Make it move 10 meters per second instead of 10 meters per frame...
        p1xTranslation *= Time.deltaTime;
        p1yTranslation *= Time.deltaTime;

        p2xTranslation *= Time.deltaTime;
        p2yTranslation *= Time.deltaTime;


        // Translate and rotate players along x-axis
        //player1.transform.Translate(Mathf.Clamp(player1.transform.position.x + p1xTranslation, xMin, xMax), 0, 0);
        //player2.transform.Translate(p2xTranslation, 0, 0);

        

        float newX2Pos = Mathf.Clamp(player2.transform.position.x + p2xTranslation, xMin, xMax);
        float newY2Pos = Mathf.Clamp(player2.transform.position.y + p2yTranslation, yMin, yMax);
        player2.transform.position = new Vector2(newX2Pos, newY2Pos);

        //player1.transform.Rotate(0, 0, rotation);


        // Translate and rotate players along y-axis
        //player1.transform.Translate(0, p1yTranslation, 0);
        //player2.transform.Translate(0, p2yTranslation, 0);

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
        float newX1Pos = Mathf.Clamp(player1.transform.position.x + p1xTranslation, xMin, xMax);
        float newY1Pos = Mathf.Clamp(player1.transform.position.y + p1yTranslation, yMin, yMax);
        player1.transform.position = new Vector2(newX1Pos, newY1Pos);
    }
}
