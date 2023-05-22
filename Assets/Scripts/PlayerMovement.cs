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
    [SerializeField] private float gravityValue = -15f;

    public float JumpPower => jumpPower;
    public float GravityValue => gravityValue;

    public PlayerBaseState currentState;
    public PlayerBaseState jumpState = new PlayerJumpState();
    public PlayerBaseState slideState = new PlayerSlideState();

    [HideInInspector] public Vector3 playerVelocity;

    private Animator anim;
    public Animator Anim => anim;
    private CharacterController controller;
    public CharacterController Controller => controller;
    private CameraMovement cameraMov;
    public CameraMovement CameraMov => cameraMov;
    private UIManager uiManager;
    private Transform player;
    private GroundCheck gCheck;

    [Header("Line")]
    public float lineDistance = 4;
    private int lineToMove = 1;

    private bool isGround;
    public bool IsGround => isGround;
    private bool isLose;
    public bool IsLose => isLose;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gCheck = GetComponent<GroundCheck>();
        controller = GetComponent<CharacterController>();
        player = GetComponent<Transform>();
        cameraMov = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>();
        uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();

        StartCoroutine(SpeedIncrease());
    }
    private void Update()
    {
        isGround = gCheck.groundCheck;
        //����������
        //if (playerVelocity.y < 0)
            //playerVelocity.y = 0;
        //else
        playerVelocity.y += gravityValue * Time.deltaTime;
        //������������ ������
        if (!isLose)
            playerVelocity.z = movementSpeed;
        controller.Move(playerVelocity * Time.deltaTime);
        //������� �����, ������
        lineToMove = Mathf.Clamp(lineToMove, 0, 2);
        if (PlayerInput.swipeRight && isLose == false)
        {
            if (lineToMove < 2)
            {
                lineToMove++;
                anim.SetTrigger("MoveRight");
            }
        }
        if (PlayerInput.swipeLeft && isLose == false)
        {
            if (lineToMove > 0)
            {
                lineToMove--;
                anim.SetTrigger("MoveLeft");
            }
        }
        if (PlayerInput.swipeUp || Input.GetKeyDown(KeyCode.Space) && isLose == false)
        {
            Debug.Log("Jump");
            if (isGround && !isLose)
                SwitchState(jumpState);
        }
        if (PlayerInput.swipeDown && isLose == false)
        {
            SwitchState(slideState);
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, switchSpeed * Time.deltaTime);

        if (player.position.y < -2)
            GameOver();
        //���� �� ����� �� �����. ����� �������� ���� �������� �� ��������� ����� � ������������ �����������
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
            GameOver();
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

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        StartCoroutine(state.UpdateState(this));
    }
}
