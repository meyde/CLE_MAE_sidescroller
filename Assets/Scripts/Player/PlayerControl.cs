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
    [SerializeField] private float maxMoveSpeed = 3f;
    [SerializeField] private float maxJumpSpeed = 5f;
    public float moveSpeed =1f;
    public float jumpSpeed = 1f;
    public LayerMask mask;
    public int temporality;
    [SerializeField] GameObject[] TimelineFathers;
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
            rb.linearVelocityX += (moveValue * moveSpeed / 20);
        }
        rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocityX, -maxMoveSpeed, maxMoveSpeed), Mathf.Clamp(rb.linearVelocityY, -maxJumpSpeed, maxJumpSpeed));
    }
    private void CheckGround() 
    {
        var rayCastHit = Physics2D.Raycast(transform.position, new Vector2(0, -1), 1.1f, mask);
        if (rayCastHit) { grounded=true; } else { grounded = false; };
        
    }
    public void OnChangedTimeline(int timeline)
    {
        switch (timeline)
        {
            case 0:
                {
                    TimelineFathers[0].SetActive(true);
                    TimelineFathers[1].SetActive(false);
                    TimelineFathers[2].SetActive(false);
                    break;
                }
            case 1:
                {
                    TimelineFathers[0].SetActive(false);
                    TimelineFathers[1].SetActive(true);
                    TimelineFathers[2].SetActive(false);
                    break;
                }
            case 2:
                {
                    TimelineFathers[0].SetActive(false);
                    TimelineFathers[1].SetActive(false);
                    TimelineFathers[2].SetActive(true);
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
        Gizmos.DrawRay(transform.position, Vector3.down * 1.1f);
    }
}
