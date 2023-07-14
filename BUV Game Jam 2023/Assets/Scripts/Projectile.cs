using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //damages enemy and destroys bullet 
        if (collision.gameObject.tag == "Enemy")
        {
            var health = collision.gameObject.GetComponent<Health>();

            health.hp = health.hp - damage;

            Destroy(gameObject);
        }
        //destroys bullet if it hits a wall
        else if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}