using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    [SerializeField] private TimeManager tm;
    private InputAction moveAction;
    private InputAction jumpAction;
    [SerializeField] private InputActionAsset iua;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool grounded= true;
    [SerializeField] private bool wallHug = false;
    [SerializeField] private float raycastVertical = 1.1f;
    [SerializeField] private float raycastHorizontal = 0.5f;
    [SerializeField] private float maxMoveSpeed = 3f;
    [SerializeField] private float maxJumpSpeed = 5f;
    public float moveSpeed =1f;
    public float jumpSpeed = 1f;
    public LayerMask mask;
    public int temporality;
    [SerializeField] GameObject[] timelineFathers;
    [SerializeField] GameObject[] backgrounds;
    public Interactable interactingWith = null;
    public float level1LeverActive = 0f;
    public float level3State; //0f: start, 1f: seed picked up; 2f: seed planted

    void Start()
    {
        moveAction = iua.FindAction("Move");
        jumpAction = iua.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (tm.paused || tm.rewinding) return;
        float moveValue = moveAction.ReadValue<float>();
        CheckGround();
        if (grounded)
        {
            rb.linearVelocityX += (moveValue * moveSpeed);
            float upValue = jumpAction.ReadValue<float>();
            rb.linearVelocityY += (upValue * jumpSpeed);
        }
        else
        {
            rb.linearVelocityX += (moveValue * moveSpeed);
        }
        if (!wallHug)
        {
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocityX, -maxMoveSpeed, maxMoveSpeed), Mathf.Clamp(rb.linearVelocityY, -maxJumpSpeed, maxJumpSpeed));
        }
        else
        {
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocityX/20, -maxMoveSpeed, maxMoveSpeed), Mathf.Clamp(rb.linearVelocityY, -maxJumpSpeed, maxJumpSpeed));
        }
    }
    private void CheckGround() 
    {
        var rayCastHit1 = Physics2D.BoxCast(transform.position, new Vector2(2f, 0.1f),180, new Vector2(0, -1), raycastVertical, mask);

        if (rayCastHit1) { grounded=true; } else { grounded = false; };
        
    }
    private void CheckWall()
    {
        var rayCastHit1 = Physics2D.Raycast(transform.position, new Vector2(1, 0), raycastHorizontal, mask);
        var rayCastHit2 = Physics2D.Raycast(transform.position, new Vector2(-1, 0), raycastHorizontal, mask);
        if (rayCastHit1 | rayCastHit2)  { wallHug = true; } else { wallHug = false; }
        ;
    }
    public void OnChangedTimeline(int timeline)
    {
        switch (timeline)
        {
            case 0:
                {
                    timelineFathers[0].SetActive(true);
                    backgrounds[0].SetActive(true);
                    timelineFathers[1].SetActive(false);
                    backgrounds[1].SetActive(false);
                    timelineFathers[2].SetActive(false);
                    backgrounds[2].SetActive(false);
                    break;
                }
            case 1:
                {
                    timelineFathers[0].SetActive(false);
                    backgrounds[0].SetActive(false);
                    timelineFathers[1].SetActive(true);
                    backgrounds[1].SetActive(true);
                    timelineFathers[2].SetActive(false);
                    backgrounds[2].SetActive(false);
                    break;
                }
            case 2:
                {
                    timelineFathers[0].SetActive(false);
                    backgrounds[0].SetActive(false);
                    timelineFathers[1].SetActive(false);
                    backgrounds[1].SetActive(false);
                    timelineFathers[2].SetActive(true);
                    backgrounds[2].SetActive(true);
                    break;
                }
            default:
                {
                    Debug.Log("temporalité non existente"); break;
                }
        }
    }
    public void OnTimeTravel()
    {
        if (tm.paused || tm.rewinding) return;
        Debug.Log("Switching timeline");
        temporality = (temporality + 1) % 3;
        OnChangedTimeline(temporality);
    }

    public void OnInteract()
    {
        if (tm.paused || tm.rewinding || interactingWith == null) return;
        Debug.Log("Interactiontried");
        interactingWith.Interaction();
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawRay(transform.position, Vector3.down * raycastVertical);
        Gizmos.DrawRay(transform.position, new Vector3(0.8f,-raycastVertical,0));
        Gizmos.DrawRay(transform.position, new Vector3(-0.8f, -raycastVertical, 0));
        Gizmos.DrawRay(transform.position, Vector3.down * raycastVertical);
        Gizmos.DrawRay(transform.position, Vector3.right *raycastHorizontal);
        Gizmos.DrawRay(transform.position, Vector3.left * raycastHorizontal);
    }
}
