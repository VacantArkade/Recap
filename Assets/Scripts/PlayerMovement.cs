using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    //References
    Rigidbody rb;
    [SerializeField] Animator animator;

    //Movement Values
    [Header("Movement Values")]
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float jumpSpeed = 10.0f;

    //Variables
    Vector3 movementVector;

    [SerializeField] HealthSO health;

    float DOTCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health.restoreHealth();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("walkSpeed", movementVector.magnitude);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            animator.SetTrigger("jump");
        }
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        Vector2 inputVector = ctx.ReadValue<Vector2>();
        movementVector = new Vector3(inputVector.x, 0, inputVector.y);

        animator.transform.forward = movementVector.normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(movementVector * moveSpeed, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Hurtbox>() != null)
        {
            health.takeDamage();
        }

        /*if (other.gameObject.GetComponentInParent<Door>() != null)
        {
            other.gameObject.transform.position = new Vector3(0, -1, 0);
            Debug.Log("touched door");
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent <Hurtbox>() != null)
        {
            DOTCounter += Time.deltaTime;
            if (DOTCounter >= 2)
            {
                health.takeDamage();
                DOTCounter = 0;
            }
        }
    }
}
