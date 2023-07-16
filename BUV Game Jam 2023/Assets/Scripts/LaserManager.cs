using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public List<GameObject> lasers = new List<GameObject>();
    public bool active = false;
    public int loopAmount = 10;
    public Transform laserSpawn;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {

    }

    private void LaserCreate()
    {
        int randomLaser = Random.Range(0, lasers.Count);
        var laserWave = Instantiate(lasers[randomLaser], laserSpawn.position, Quaternion.identity);
        Destroy(laserWave, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.tag == "Player" && active)
        {
            for (int i = 0; i < loopAmount; i++)
            {
                LaserCreate();
            }
            active = false;
        }
    }
}
