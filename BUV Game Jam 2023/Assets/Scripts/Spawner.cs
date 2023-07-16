using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnedObject;
    public List<Transform> spawnLocations = new List<Transform>();
    public int spawnAmount;
    public bool active = true;
    public bool infinite = false;

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
        if (collision.gameObject.tag == "Player" && active)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                int randomLocation = Random.Range(0, spawnLocations.Count);
                Instantiate(spawnedObject, spawnLocations[randomLocation].position, Quaternion.identity);
                spawnLocations.Remove(spawnLocations[randomLocation]);
            }
            active = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && infinite && active)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                int randomLocation = Random.Range(0, spawnLocations.Count);
                Instantiate(spawnedObject, spawnLocations[randomLocation].position, Quaternion.identity);
                active = false;
                Invoke("Delay", 10f);
            }
        }
    }

    void Delay()
    {
        active = true;
    }
}