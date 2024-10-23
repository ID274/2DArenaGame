using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [Header("These values are passed from the gun that fires the bullet")]
    public int damage;
    public float lifetime;
    public GameObject shooter; // Object (gun) firing the projectile
    //public Sprite sprite;



    // Bullet speed is passed in when instantiated
    private void Start()
    {
        lifetime = shooter.GetComponent<Player>().gun.bulletLifetime;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //do stuff
        Destroy(gameObject);
    }
}
