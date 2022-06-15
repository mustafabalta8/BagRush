using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private static bool isPlaying = false;

    [Header("Movement")]
    [SerializeField] private Vector3 movementDirection;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float backwardSpeed = 5;
    [SerializeField] private float swerveSpeed;
    //[SerializeField] private Transform sideMovementRoot;
    [SerializeField] private float sideMovementLimit;


    private float lastFrameFingerPositionX;
    private float moveFactorX;

    private static Screens gameState;
    public static bool IsPlaying
    {
        get { return isPlaying; } set { isPlaying = value; }
    }

    public static Screens GameState { get => gameState; set => gameState = value; }

    public static PlayerMovement instance;
    private Rigidbody rigidbody;

    private bool isMovingForward = true;
    private void Awake()
    {
        Singelton();

        rigidbody = GetComponent<Rigidbody>();

        GameState = Screens.MainMenu;
    }
    private void Singelton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (gameState == Screens.InGame)
        {
            HandleSideMovement();
            if (isMovingForward)
            MoveForward();                      
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            CollideWithObstacle();
        }
    }

    private void CollideWithObstacle()
    {
        isMovingForward = false;
        rigidbody.AddForce(Vector3.back * backwardSpeed, ForceMode.VelocityChange);
        StartCoroutine(ResetVelocity());
    }

    IEnumerator ResetVelocity()
    {
        yield return new WaitForSeconds(0.3f);
        rigidbody.velocity = Vector3.zero;
        isMovingForward = true;
    }
    
    private void MoveForward()
    {
        transform.Translate(movementDirection * Time.deltaTime * forwardSpeed);        
    }
    private void HandleSideMovement()
    {
        GetInput();

        float swerveAmount = swerveSpeed * moveFactorX * Time.deltaTime;
        var currentPos = transform.position; //this.sideMovementRoot.localPosition;
        currentPos.x += swerveAmount;
        currentPos.x = Mathf.Clamp(currentPos.x, -sideMovementLimit, sideMovementLimit);

        transform.position = currentPos;
        //this.sideMovementRoot.localPosition = currentPos;
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            moveFactorX = Input.mousePosition.x - lastFrameFingerPositionX;
            lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            moveFactorX = 0f;
        }
    }
}
