using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGoal : MonoBehaviour
{
    public GameObject mainGoal;
    public Sprite pressed;
    public Vector2 lerpDestination;
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
            mainGoal.transform.position = Vector2.Lerp(mainGoal.transform.position, lerpDestination, 10.0f);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pressed;
        }
    }
}
