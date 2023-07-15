using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    public float ricochetSpeed = 1000;
    public float ricochetDegree;

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
}
