using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    private Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] int jumps;
    [SerializeField] Animator animator;
    [SerializeField] GameObject steve;
    [SerializeField] Stat health;

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

        input = playerActions.Player.Move.ReadValue<Vector2>();     //controls player movement using new movement system

        animator.SetFloat("moveSpeed", input.magnitude);            //tells animator to animate moving animation

        playerActions.Player.Jump.performed += OnJump;              //calls jump function when space is pressed

        if(health.amount <= 0)
        {
            steve.transform.position = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {

        var newInput = GetCameraBasedInput(input, Camera.main);     //uses the camera position to adjust movement keys
        var newVelocity = new Vector3(newInput.x * speed * Time.fixedDeltaTime, rb.velocity.y, newInput.z * speed * Time.fixedDeltaTime);

        rb.velocity = newVelocity;
    }

    Vector3 GetCameraBasedInput(Vector2 input, Camera cam)          //camera based movement
    {
        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        return input.x * camRight + input.y * camForward;
    }

    public void OnJump(InputAction.CallbackContext ctx)             //jump function
    {
        if (ctx.performed && jumps > 0)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            animator.SetTrigger("jump");
            jumps--;
        }
    }

    private void OnCollisionEnter(Collision collision)              //collision detection
    {
        if(collision.gameObject.tag == "Ground" && jumps == 0)      //prevents player from infinitely jumping
        {
            jumps++;
        }

        if(collision.gameObject.tag == "Respawn")                   //respawns player at 0,0,0 if they fall off the map
        {
            steve.transform.position = Vector3.zero;
        }
    }
}
