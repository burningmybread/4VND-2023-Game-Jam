using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public GameObject impactFx;
    public bool penetration = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 8);
        gameObject.layer = LayerMask.NameToLayer("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (penetration)
        {
            gameObject.layer = LayerMask.NameToLayer("Penetration");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(impactFx, this.transform.position, this.transform.rotation);

        //damages enemy and destroys bullet 
        if (collision.gameObject.tag == "Enemy")
        {
            var health = collision.gameObject.GetComponent<Health>();

            health.hp = health.hp - damage;

            Destroy(gameObject);
        }
        //destroys bullet if it hits a wall
        else if (collision.gameObject.tag == "Wall" && !penetration || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}