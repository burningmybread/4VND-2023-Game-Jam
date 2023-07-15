using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private bool chasing = false;
    public float moveSpeed;
    public float damage;
    public float attackDistance;
    public bool canAttack = false;
    private Health health;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            chasing = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Behavior();

        if (canAttack)
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    public void Damage()
    {
        if (canAttack)
        {
            var playerHealth = player.GetComponent<Health>();

            playerHealth.hp = playerHealth.hp - damage;
        }
    }

    private void Behavior()
    {
        if (!health.dead)
        {
            if (chasing == true)
            {
                if (Vector2.Distance(transform.position, player.transform.position) > attackDistance)
                {
                    canAttack = false;

                    //Move the enemy toward a given position which is player
                    transform.parent.position = Vector2.MoveTowards(transform.parent.position, player.transform.position, moveSpeed * Time.deltaTime);
                }
                else if (Vector2.Distance(transform.parent.position, player.transform.position) <= attackDistance)
                {
                    canAttack = true;
                }
                else
                {
                    canAttack = false;
                }

                Vector2 direction = player.transform.position - transform.parent.position;
                direction.Normalize();

                //Mathf to find angle between 2 points in radians, multiply to change it to degree
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.parent.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            }
        }
        else if (health.dead)
        {
            Destroy(gameObject);
        }
    }
}