using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody2D rb;
    public float fireRate; // Fire delay = 1 / fireRate
    public int damage;
    protected float fireDelay;
    public Player holdingPlayer; // Player holding the weapon

    protected virtual void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
}
