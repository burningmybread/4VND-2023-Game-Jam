using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    public float moveSpeed;
    public float rotateSpeed;
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();

        MovementInputs();
    }

    private void MovementInputs()
    {
        //if body's direction isn't the same as move direction, rotates towards move direction
        //if body's direction is the same as move direction, move

        if (moveDirection != Vector2.zero)
        {
            rb.velocity = moveDirection * moveSpeed;

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }

        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        //{
        //    rb.velocity = moveDirection * moveSpeed;
        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    if (rb.rotation != 0f)
        //    {
        //        rb.rotation -= rotateSpeed;
        //    }
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    if (rb.rotation != -90f)
        //    {
        //        rb.rotation -= rotateSpeed;
        //    }
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    if (rb.rotation != 180f)
        //    {
        //        rb.rotation += rotateSpeed;
        //    }
        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    if (rb.rotation != 90f)
        //    {
        //        rb.rotation += rotateSpeed;
        //    }
        //}    


    }

    void ProcessMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

    }
}