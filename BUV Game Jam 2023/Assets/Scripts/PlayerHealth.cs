using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//hud is ammo count, health, and then hologram count
//pause menu for restart level and return to main menu

public class PlayerHealth : MonoBehaviour
{
    public float hp;
    public float maxHp = 100f;
    public bool dead = false;

    [SerializeField] private Image healthbar;

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

    public void UpdateHP(int damagedAmount)
    {
        hp -= damagedAmount;
        healthbar.fillAmount = hp / maxHp;
    }
}
