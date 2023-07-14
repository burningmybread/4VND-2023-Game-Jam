using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    public Transform barrel;
    public GameObject projectilePrefab;
    public GameObject tetherPointPrefab;
    public float projectileSpeed;
    public GameObject hull;
    private bool attach = true;
    private LineRenderer tether;
    public int tetherLength = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tether = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CursorAiming();

        Shoot();

        AttachDetach();

        //tether.SetPosition(0, transform.position);
        //tether.SetPosition(4, hull.transform.position);
    }

    private void AttachDetach()
    {
        if (attach)
        {
            transform.position = hull.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (attach)
            {
                attach = false;

                rb.velocity = barrel.transform.up * 5f;

                //List<GameObject> tetherPoints = new List<GameObject>();

                //for (int i = 0; i <= tether.positionCount; i++)
                //{
                //    GameObject tetherPoint = Instantiate(tetherPointPrefab, transform.position, Quaternion.identity);

                //    tetherPoints.Add(tetherPoint);

                //    for (int y = 0; y <= tether.positionCount; y++)
                //    {
                //        if (y == tether.positionCount)
                //        {
                //            tether.SetPosition(y, hull.transform.position);
                //        }
                //        else if (y < tether.positionCount)
                //        {
                //            tether.SetPosition(y, tetherPoints[i].transform.position);
                //        }
                //    }
                //}

                //for (int i = 0; i <= tetherLength; i++)
                //{
                //    if (i <= tetherLength)
                //    {
                //        tether.SetPosition(i, transform.position);
                //    }
                //    else
                //    {
                //        tether.SetPosition(i, hull.transform.position);
                //    }
                //}
            }
            else if (!attach)
            {
                attach = true;
            }
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            projectileRb.AddForce(projectile.transform.up * projectileSpeed);

            Destroy(projectile, 8f);
        }
    }

    private void CursorAiming()
    {
        //Get direction of cursor
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = Camera.main.nearClipPlane;

        var direction = (mousePos - gameObject.transform.position).normalized;

        //Get the correct angle for the gun to rotate
        var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;

        //rotate the gun based on direction of cursor
        rb.rotation = angle;
    }
}
