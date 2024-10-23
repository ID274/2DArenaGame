using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BoxCollider2D feetCollider;


    [Header("Weapon")]
    public Weapon equippedWeapon;
    public Gun gun = null;


    [Header("Movement")]
    public Rigidbody2D rb, weaponRb;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 movement;
    [SerializeField] private float throwForce = 1000f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Controls")]
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode shootBind = KeyCode.Mouse0;
    public KeyCode throwWeapon = KeyCode.Q;
    public KeyCode jump = KeyCode.Space;


    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movement = Vector2.zero;
        if (weaponRb != null)
        {
            weaponRb.velocity = Vector2.zero;
        }

        if (equippedWeapon != null)
        {
            if (equippedWeapon.GetComponent<Gun>())
            {
                gun = equippedWeapon as Gun;
            }
        }
        
        if (gun != null)
        {
            if (gun.currentFireMode == Gun.FireMode.Auto)
            {
                if (Input.GetKey(shootBind))
                {
                    gun.Shoot();
                }
            }
            else if (gun.currentFireMode == Gun.FireMode.Single)
            {
                if (Input.GetKeyDown(shootBind))
                {
                    gun.Shoot();
                }
            }
            else if (gun.currentFireMode == Gun.FireMode.Burst)
            {
                if (Input.GetKeyDown(shootBind))
                {
                    gun.Shoot();
                }
            }
            else
            {
                Debug.LogError("FireMode not set");
            }
        }

        if (Input.GetKeyDown(throwWeapon) && equippedWeapon != null)
        {
            ThrowWeapon();
        }

        if (Input.GetKey(moveLeft))
        {
            movement = new Vector2(-1, 0);
        }

        // Check if right key is pressed
        if (Input.GetKey(moveRight))
        {
            movement = new Vector2(1, 0);
        }

        if (Input.GetKeyDown(jump) && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(feetCollider.bounds.center, feetCollider.bounds.extents.x);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Terrain"))
            {
                return true;
            }
        }
        return false;
    }

    public void Move()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
    public void ThrowWeapon()
    {
        equippedWeapon.GetComponent<Pickup>().Drop();
        weaponRb.AddForce(weaponRb.transform.right * throwForce, ForceMode2D.Impulse);
        weaponRb = null;
        gun = null;
        equippedWeapon = null;
    }
}
