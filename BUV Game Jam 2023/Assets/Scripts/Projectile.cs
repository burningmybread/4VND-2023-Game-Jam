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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //damages enemy and destroys bullet 
        if (collision.gameObject.tag == "Enemy")
        {

            Destroy(gameObject);
        }
        //destroys bullet if it hits a wall
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}