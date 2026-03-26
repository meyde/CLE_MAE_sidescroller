using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool grounded= true;
    [SerializeField] private float maxMoveSpeed = 3f;
    [SerializeField] private float maxJumpSpeed = 5f;
    public float moveSpeed =1f;
    public float jumpSpeed = 1f;
    public LayerMask mask;
    public int temporality;
    private static PlayerControl _instance;

    void Awake()
    {
        this.InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != _instance)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("MoveHorizontal");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        float moveValue = moveAction.ReadValue<float>();
        checkGround();
        if (grounded)
        {
            rb.linearVelocityX += (moveValue * moveSpeed);
            float upValue = jumpAction.ReadValue<float>();
            rb.linearVelocityY+=( upValue * jumpSpeed);            
        }
        else 
        {    
            rb.linearVelocityX += (moveValue * moveSpeed/20);
        }
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocityX, -maxMoveSpeed, maxMoveSpeed), Mathf.Clamp(rb.linearVelocityY, -maxJumpSpeed, maxJumpSpeed));
    }
    private void checkGround() 
    {
        var rayCastHit = Physics2D.Raycast(transform.position, new Vector2(0, -1), 1.1f, mask);
        if (rayCastHit) { grounded=true; } else { grounded = false; };
        
    }
    public void OnTimeTravel()
    {
        transform.position= new Vector3 (transform.position.x, (transform.position.y + 100) % 300, transform.position.z);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawRay(transform.position, Vector3.down * 1.1f);
    }
}
