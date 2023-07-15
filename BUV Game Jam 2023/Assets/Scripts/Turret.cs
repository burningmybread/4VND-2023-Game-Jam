using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Turret : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    public Transform barrel;
    public GameObject projectilePrefab;
    public GameObject tetherPointPrefab;
    private List<GameObject> tetherPoints = new List<GameObject>();
    //private int tetherPoint;
    public int tetherLength = 5;
    public float fireDelay;
    public float firerate;
    private bool canShoot = true;
    public float projectileSpeed;
    public int magazineSize;
    public float reloadSpeed;
    [SerializeField] int currentAmmo;
    public float detachSpeed;
    public GameObject hull;
    public bool attach = true;
    private bool reattach = false;
    private LineRenderer tether;
    private Hull hullCode;
    private Vector2 velocity;
    public Animator turretAnimator;
    private float divider;
    private int currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tether = GetComponent<LineRenderer>();
        Physics2D.IgnoreCollision(hull.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        hullCode = hull.GetComponent<Hull>();
        currentAmmo = magazineSize;
        currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CursorAiming();

        Shoot();

        Reload();

        if (currentAmmo < 0)
        {
            currentAmmo = 0;
        }
        else if (currentAmmo > magazineSize)
        {
            currentAmmo = magazineSize;
        }

        tether.SetPosition(0, hull.transform.position);

        tether.SetPosition(tether.positionCount - 1, transform.position);

        AttachDetach();

        //if (Vector2.Distance(transform.position, tetherPoints[currentPoint].transform.position) > 5f && tetherPoints.Count > 0)
        //{
        //    GameObject tetherPoint = Instantiate(tetherPointPrefab, transform.position, Quaternion.identity);

        //    tether.positionCount++;

        //    tether.SetPosition(tether.positionCount - 1, tetherPoint.transform.position);

        //    tetherPoints.Add(tetherPoint);

        //    if (currentPoint < tetherLength)
        //    {
        //        currentPoint++;
        //    }
        //}
    }

    private void AttachDetach()
    {

        if (attach)
        {
            transform.position = hull.transform.position;
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else if (!attach)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Turret");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (attach)
            {
                attach = false;

                if (hull.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
                {
                    rb.velocity = barrel.transform.up * detachSpeed;
                }

                //if (tether.positionCount != tetherLength)
                //{
                //    GameObject firstPoint = Instantiate(tetherPointPrefab, transform.position, Quaternion.identity);

                //    tether.positionCount++;
               
                //    tether.SetPosition(tether.positionCount - 1, firstPoint.transform.position);

                //    tetherPoints.Add(firstPoint);
                //}


                hullCode.moveSpeed = hullCode.moveSpeed + 5f;
            }
            else if (!attach)
            {
                //this.gameObject.layer = LayerMask.NameToLayer("Hull");
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, hull.transform.position - this.gameObject.transform.position);
                if (hit.collider.gameObject == hull)
                {
                    Debug.Log("hit hull");
                    reattach = true;
                }
                else
                {
                    Debug.Log("did not hit");
                    reattach = false;
                }
            }
        }

        if (reattach)
        {
            //if (tetherPoints.Count > 0)
            //{
            //    foreach (var tether in tetherPoints)
            //    {
            //        if (transform.position != tether.transform.position)
            //        {
            //            transform.position = Vector2.MoveTowards(transform.position, tether.transform.position, Time.deltaTime * 5f);
            //        }
            //    }
            //}

            transform.position = Vector2.SmoothDamp(transform.position, hull.transform.position, ref velocity, 0.3f);

            if (Vector2.Distance(transform.position, hull.transform.position) <= 1f)
            {
                reattach = false;

                attach = true;

                hullCode.moveSpeed = 5f;
            }
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0 && canShoot)
        {
            turretAnimator.SetTrigger("Fire");
            AudioManager.Instance.PlayEffect("Turretshoot");
            Invoke("FireProjectile", fireDelay);
        }
    }

    private void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

        projectileRb.AddForce(projectile.transform.up * projectileSpeed);

        Destroy(projectile, 8f);

        currentAmmo--;

        canShoot = false;

        Invoke("DoFirerate", firerate);
    }

    void DoFirerate()
    {
        canShoot = true;

        turretAnimator.SetTrigger("Stop");
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize && attach && !reattach)
        {
            currentAmmo = 0;
            Invoke("Reloading", reloadSpeed);
        }
    }

    void Reloading()
    {
        currentAmmo = magazineSize;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TurretDock")
        {
            if (!attach)
            {
                this.transform.position = collision.transform.position;
                rb.velocity = barrel.transform.up * 0;
            }
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
