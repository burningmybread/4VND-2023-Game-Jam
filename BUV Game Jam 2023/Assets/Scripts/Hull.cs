using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hull : MonoBehaviour
{
    public static event Action AddSphere;

    [HideInInspector] public Rigidbody2D rb;
    public float moveSpeed;
    public float rotateSpeed;
    private Vector2 moveDirection;
    private Turret turretCode;
    public GameObject turret;
    public Animator hullAnimator;
    public int numberOfSphere;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        turretCode = turret.GetComponent<Turret>();
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

        if(!turretCode.attach)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Hull");
        }
        else if(turretCode.attach)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    void ProcessMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            //AudioManager.Instance.PlayWalk("Walk");

            hullAnimator.SetTrigger("IsRunning");
        }
        else
        {
            hullAnimator.SetTrigger("IsIdle");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DataSphere")
        {
            numberOfSphere++;
            AddSphere?.Invoke();

            Destroy(collision.gameObject);
            Debug.Log(numberOfSphere);
        }
    }
}