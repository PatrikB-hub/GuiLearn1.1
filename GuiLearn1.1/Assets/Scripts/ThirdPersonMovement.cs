using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Third person Movement
/// </summary>
public class ThirdPersonMovement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Player player;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] public float gravity = -9.82f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private Vector3 playerVelocity;

    private bool isGrounded;

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
    }

    void Update()
    {
        Jump();
        Movement();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float movementSpeed = player.playerStats.stats.speed;
            if (player.playerStats.stats.currentStamina > 0 && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                player.disableStaminaRegenTime = Time.time;
                player.playerStats.stats.currentStamina -= player.staminaDegen * Time.deltaTime;
                movementSpeed = player.playerStats.stats.sprintSpeed;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                movementSpeed = player.playerStats.stats.crouchSpeed;
            }

            controller.Move(moveDir * movementSpeed * Time.deltaTime);

        }
    }


    void Jump()
    {
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(player.playerStats.stats.jumpHeight * -3.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    bool IsGrounded()
    {
        //draw raycast
        Debug.DrawRay(transform.position, -Vector3.up * ((controller.height * 0.5f) * 1.1f), Color.red);

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        // this would cast rays only against colliders in layer 8
        // But instead we wnt to collide against everything except layer 8. the ~ operator does this, it inverts a bit mask.

        layerMask = ~layerMask;

        RaycastHit hit;
        /*if (Physics.Raycast(transform.position, -Vector3.up, out hit, (controller.height * 0.5f) * 1.1f, layerMask))
        {
            return true;
        }*/

        if (Physics.SphereCast(transform.position, controller.radius, -Vector3.up, out hit, controller.bounds.extents.y + 0.1f - controller.bounds.extents.x, layerMask))
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (-Vector3.up * (controller.bounds.extents.y + 0.1f - controller.bounds.extents.x)), controller.radius);
    }
}
