using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Rigidbody2D rb;
    //public PolygonCollider2D polyCollider;
    public BoxCollider2D boxCollider;
    public bool playerCollideable = true;
    public TextMeshPro ammoCount;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        boxCollider = this.GetComponent<BoxCollider2D>();
        ammoCount = GetComponentInChildren<TextMeshPro>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        PickUp(collision);
    }

    public void PickUp(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponentInParent<Player>();
        if (player != null && playerCollideable && player.equippedWeapon == null)
        {
            rb.isKinematic = true;
            boxCollider.isTrigger = true;
            player.equippedWeapon = this.GetComponent<Weapon>();
            GetComponent<Weapon>().holdingPlayer = player;
            Transform hand = collision.transform.Find("Hand");
            transform.parent = hand;
            transform.position = hand.position;
            transform.rotation = hand.rotation;
            player.weaponRb = rb;
            boxCollider.enabled = false;
            ammoCount.enabled = false;
            //UI ammo count enabled
        }
    }
    public void Drop() 
    {
        transform.parent = null;
        rb.isKinematic = false;
        boxCollider.isTrigger = false;
        boxCollider.enabled = true;
        StartCoroutine(PlayerCollisionDelay());
        ammoCount.enabled = true;
        //UI ammo count enabled
    }

    public IEnumerator PlayerCollisionDelay()
    {
        playerCollideable = false;
        yield return new WaitForSeconds(0.5f);
        playerCollideable = true;
    }
}
