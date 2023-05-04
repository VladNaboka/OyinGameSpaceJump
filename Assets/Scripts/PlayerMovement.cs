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

    private Animator anim;
    private Rigidbody rb;
    private CapsuleCollider cpCol;
    private CameraMovement cameraMov;

    [Header("Scripts")]
    public SwipeController swControl;
    [Header("Line")]
    private int lineToMove = 1;
    public float lineDistance = 4;

    private bool isGround;
    private bool isLose;
    //private bool isSliding;

    //private bool isSliding;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cpCol = GetComponent<CapsuleCollider>();
        cameraMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        StartCoroutine(SpeedIncrease());
    }
    private void FixedUpdate()
    {
        //if (!isLose)
        //    rb.velocity = new Vector3(transform.position.x, transform.position.y, transform.position.z * movementSpeed * Time.deltaTime);

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

        transform.position = Vector3.Lerp(transform.position, targetPosition, switchSpeed * Time.fixedDeltaTime);
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
        //playerPos.Translate(Vector3.up * jumpPower * Time.fixedDeltaTime);
        if (isGround == true)
        {
            rb.AddForce(Vector3.up * jumpPower * Time.fixedDeltaTime, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
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
        yield return new WaitForSeconds(5);
        if (movementSpeed < maxSpeed)
        {
            movementSpeed += 1;
            Debug.Log(movementSpeed);
        }
        StartCoroutine(SpeedIncrease());
    }
}
