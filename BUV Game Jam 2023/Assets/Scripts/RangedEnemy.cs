using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float firerate;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public Transform barrel;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        Behavior();
    }

    public void RangedDamage()
    {
        if (canAttack && canShoot)
        {
            GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            projectileRb.AddForce(projectile.transform.up * projectileSpeed);

            Destroy(projectile, 8f);

            canShoot = false;

            Invoke("DoFirerate", firerate);
        }
    }

    void DoFirerate()
    {
        canShoot = true;
    }

    public override void Behavior()
    {
        if (!health.dead)
        {
            if (chasing == true)
            {
                if (Vector2.Distance(transform.parent.position, player.transform.position) <= attackDistance)
                {
                    canAttack = true;
                }
                else
                {
                    animator.SetTrigger("Idle");

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
            AudioManager.Instance.PlayEffect("Enemydeath");
            Instantiate(deathDecal, this.transform.position, this.transform.rotation);
            Destroy(gameObject);
        }
    }
}
