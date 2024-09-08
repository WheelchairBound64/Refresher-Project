using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    private Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] Animator animator;
    [SerializeField] int jumps;

    IA_PlayerActions playerActions;
    // Start is called before the first frame update
    void Start()
    {
        playerActions = new IA_PlayerActions();
        playerActions.Player.Enable();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        input = playerActions.Player.Move.ReadValue<Vector2>();

        animator.SetFloat("moveSpeed", input.magnitude);

        playerActions.Player.Jump.performed += OnJump;
    }

    private void FixedUpdate()
    {

        var newInput = GetCameraBasedInput(input, Camera.main);
        var newVelocity = new Vector3(newInput.x * speed * Time.fixedDeltaTime, rb.velocity.y, newInput.z * speed * Time.fixedDeltaTime);

        rb.velocity = newVelocity;
    }

    Vector3 GetCameraBasedInput(Vector2 input, Camera cam)
    {
        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        return input.x * camRight + input.y * camForward;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && jumps > 0)
        {
            Debug.Log("Pressed Jump");
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            jumps--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" && jumps == 0)
        {
            jumps++;
        }
    }
}
