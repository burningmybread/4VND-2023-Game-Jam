using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float hp;
    public float maxHp = 100f;
    public bool dead = false;


    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > maxHp)
        {
            hp = maxHp;
        }

        if (hp <= 0f)
        {
            dead = true;
        }
    }
}
