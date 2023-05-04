using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float switchSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityValue = -9.81f;

    private Vector3 playerVelocity;

    private Animator anim;
    private CharacterController controller;
    private CapsuleCollider cpCol;
    private CameraMovement cameraMov;
    private SwipeController swControl;

    [Header("Line")]
    public float lineDistance = 4;
    private int lineToMove = 1;

    private bool isGround;
    private bool isLose;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        swControl = FindObjectOfType<SwipeController>().GetComponent<SwipeController>();
        cpCol = GetComponent<CapsuleCollider>();
        controller = GetComponent<CharacterController>();
        cameraMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();

        StartCoroutine(SpeedIncrease());
    }
    private void Update()
    {
        isGround = controller.isGrounded;
        //Гравитация
        if (isGround && playerVelocity.y < 0)
            playerVelocity.y = 0;
        else
            playerVelocity.y += gravityValue * Time.deltaTime;

        //Передвижение вперед
        if (!isLose)
        {
            playerVelocity.z = movementSpeed;
            controller.Move(playerVelocity * Time.deltaTime);
        }
        //Переход влево, вправо
        lineToMove = Mathf.Clamp(lineToMove, 0, 2);
        if (SwipeController.swipeRight && isLose == false)
        {
            if (lineToMove < 2)
            {
                lineToMove++;
                anim.SetTrigger("MoveRight");
            }
        }
        if (SwipeController.swipeLeft && isLose == false)
        {
            if (lineToMove > 0)
            {
                lineToMove--;
                anim.SetTrigger("MoveLeft");
            }
        }
        if (SwipeController.swipeUp && isLose == false)
        {
            if (isGround)
                Jump();
        }
        if (SwipeController.swipeDown && isLose == false)
        {
            StartCoroutine(Slide());
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, switchSpeed * Time.deltaTime);
    }

    private void GameOver()
    {
        isLose = true;
        anim.SetBool("isRunning", false);
        anim.SetBool("isLose", true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = true;
            anim.SetBool("isRunning", true);
        }
        if (collision.collider.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = false;
            anim.SetBool("isRunning", false);
        }
    }
    private void Jump()
    {
        playerVelocity.y = Mathf.Sqrt(jumpPower * -3.0f * gravityValue);
        anim.SetTrigger("Jump");
    }
    private IEnumerator Slide()
    {
        cpCol.center = new Vector3(0, 0.4f, -0.12f);
        cpCol.height = 0.8f;
        //isSliding = true;
        anim.SetTrigger("Slide");
        cameraMov.distance += new Vector3(0, -0.3f, 0);
        yield return new WaitForSeconds(1);

        cpCol.center = new Vector3(0, 0.83f, 0.03f);
        cpCol.height = 1.64f;
        cameraMov.distance += new Vector3(0, 0.3f, 0);
    }
    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(10);
        if (movementSpeed < maxSpeed)
        {
            movementSpeed += 1;
            Debug.Log(movementSpeed);
        }
        StartCoroutine(SpeedIncrease());
    }
}
