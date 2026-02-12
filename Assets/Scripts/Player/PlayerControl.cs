using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed =10f;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("MoveHorizontal");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        float moveValue = moveAction.ReadValue<float>();
        rb.linearVelocityX= moveValue* moveSpeed;
    }
}
