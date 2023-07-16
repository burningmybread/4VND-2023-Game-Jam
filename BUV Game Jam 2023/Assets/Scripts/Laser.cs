using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damage; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var health = collision.GetComponent<PlayerHealth>();

            health.hp = health.hp - damage;
        }
    }
}
