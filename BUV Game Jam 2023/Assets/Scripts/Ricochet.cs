using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    public float ricochetSpeed = 1000;
    public float ricochetDegree = 90f;

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
        if (collision.gameObject.tag == "Projectile")
        {
            
            Debug.Log("Hit ramp");

            collision.attachedRigidbody.velocity = Vector2.zero;

            collision.transform.rotation = Quaternion.Euler(Vector3.forward * ricochetDegree);

            collision.attachedRigidbody.AddForce(collision.transform.up * ricochetSpeed);

           
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    ContactPoint2D[] contacts = new ContactPoint2D[10];
    //    collision.GetContacts(contacts);
    //    Vector3 norm = contacts[0].normal;

    //    collision.transform.rotation = Vector3.Reflect(collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized);
    //}
}
