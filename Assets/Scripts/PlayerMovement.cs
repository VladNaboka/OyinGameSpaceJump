using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float switchSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityValue = -15f;

    private Vector3 playerVelocity;

    private Animator anim;
    private CharacterController controller;
    private CameraMovement cameraMov;
    private UIManager uiManager;
    private Transform player;

    [Header("Line")]
    public float lineDistance = 4;
    private int lineToMove = 1;

    private bool isGround;
    private bool isLose;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        player = GetComponent<Transform>();
        cameraMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();

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
            playerVelocity.z = movementSpeed;
        controller.Move(playerVelocity * Time.deltaTime);
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
        if (SwipeController.swipeUp || Input.GetKeyDown(KeyCode.Space) && isLose == false)
        {
            Debug.Log("Jump");
            if (isGround && !isLose)
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

        if (player.position.y < -2)
            GameOver();
        //Если на земле то бежит. Потом наверное надо заменить на отдельный метод с вовзращаемым результатом
        if (isGround)
            anim.SetBool("isRunning", true);
    }

    public void GameOver()
    {
        playerVelocity.z = 0;
        isLose = true;
        anim.SetBool("isRunning", false);
        anim.SetBool("isLose", true);
        uiManager.GameOverScreen();
    }
    private void Jump()
    {
        playerVelocity.y = Mathf.Sqrt(jumpPower * -3.0f * gravityValue);
        anim.SetBool("isRunning", false);
        anim.SetTrigger("Jump");
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
            GameOver();
    }
    private IEnumerator Slide()
    {
        controller.height = 0;
        controller.center = new Vector3(0, 0.49f, 0);

        anim.SetTrigger("Slide");
        cameraMov.distance += new Vector3(0, -0.3f, 0);
        yield return new WaitForSeconds(1);

        controller.height = 1.75f;
        controller.center = new Vector3(0, 0.86f, 0);

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
