using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 11);
        Physics2D.IgnoreLayerCollision(3, 12);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(impactFx, this.transform.position, this.transform.rotation);

        if (collision.gameObject.tag == "Player")
        {
            var health = collision.gameObject.GetComponent<Health>();

            health.hp = health.hp - damage;

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
