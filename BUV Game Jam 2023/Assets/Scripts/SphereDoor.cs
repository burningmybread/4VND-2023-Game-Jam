using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDoor : MonoBehaviour
{
    private Hull hullCode;
    // Start is called before the first frame update
    void Start()
    {
        hullCode = GameObject.FindGameObjectWithTag("Player").GetComponent<Hull>();
    }

    // Update is called once per frame
    void Update()
    {
        openTheDoor(); 
    }

    void openTheDoor()
    {
        if(hullCode.numberOfSphere == 4)
        {
            Destroy(this.gameObject);
        }
    }
}
