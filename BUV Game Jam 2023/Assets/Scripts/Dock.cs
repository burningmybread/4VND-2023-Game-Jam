using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    public bool wallPenetration = false;
    public GameObject normalBullet;
    public GameObject penetratingBullet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Turret")
        {
            var turret = collision.GetComponent<Turret>();
            
            if (!turret.attach)
            {
                if (wallPenetration)
                {
                    turret.projectilePrefab = penetratingBullet;
                }
                else
                {
                    return;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Turret")
        {
            var turret = collision.GetComponent<Turret>();

            if (!turret.attach)
            {
                if (wallPenetration)
                {
                    turret.projectilePrefab = normalBullet;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
