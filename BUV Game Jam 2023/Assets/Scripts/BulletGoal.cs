using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGoal : MonoBehaviour
{
    public GameObject mainGoal;
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
            Destroy(mainGoal.gameObject);
            Destroy(this.gameObject);
        }
    }
}
