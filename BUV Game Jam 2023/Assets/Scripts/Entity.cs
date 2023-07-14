using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// base script for everything that can move by itself in a level
/// handles movement and has the base script for behaviours
/// holds health, speed, acceleration time and shits
/// </summary>

public class Entity : MonoBehaviour
{
    [SerializeField] int health = 1, currentHealth;
    [SerializeField] bool vulnerable = true; //if vulnerable, can be affected by attacks
    [SerializeField] string faction; //sets the entity's faction to determine what can harm it and other things

    [SerializeField] Vector4 linearAccelRates = new Vector4(1, 1, 1, 1) /*determines the acceleration rate in each of the entity direction, goes from up, right, down, left, respectively for each number*/;
    [SerializeField] float moveForce = 5 /*how much force this will use for moving*/;
    [SerializeField] float angularAccelRate = 1 /*how fast the entity can turn*/;

    [SerializeField] float drag, angularDrag; //drag of the entity, applies to the rigidbody at the start

    //receives the move destination
    //then converts it to local position
    //then normalize it
    //then multplies each axis with the corresponding accel rate
    Vector2 CalculatesMovement(Vector2 moveDestination)
    {
        return new Vector2();
    }

    //receives a move direction, then calculates the multiplier
    void Move(Vector2 moveDir)
    {

    }
}
