using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{
    public float cleanupTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, cleanupTime);
    }
}
