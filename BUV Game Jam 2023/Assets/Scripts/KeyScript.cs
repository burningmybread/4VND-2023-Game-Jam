using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject door;
    public GameObject spawnLocation;
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
        if(collision.gameObject.tag == "Player")
        {
            Destroy(door.gameObject);
            Destroy(this.gameObject);
            GameObject enemy = Instantiate(enemyPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
        }
    }
}
