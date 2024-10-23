using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class Gun : Weapon
{
    public int magSize; // Amount of bullets the gun can fire before "breaking"
    public int currentMagSize;
    public float accuracyOffset; // The lower the value, the less variation between shot angles (more accurate)
    public float bulletSpeed;
    protected Bullet bulletType;
    public float bulletLifetime;
    public GameObject bullet;
    public Transform barrel;
    public FireMode currentFireMode;
    public TextMeshPro ammoCount;

    public enum FireMode
    {
        Single,
        Burst,
        Auto
    }

    public enum WeaponType
    {
        Pistol,
        Rifle,
        Smg,
        Shotgun
    }

    public virtual void Shoot()
    {
        if (fireDelay <= 0)
        {
            Vector2 direction = transform.right;
            float accuracyVariance = Random.Range((0 - accuracyOffset), accuracyOffset);
            direction = Quaternion.Euler(0, 0, accuracyVariance) * direction;
            GameObject bulletInstance = Instantiate(bullet, barrel.position, transform.rotation);
            bulletInstance.GetComponentInChildren<Rigidbody2D>().velocity = direction * bulletSpeed;
            Bullet bulletReference = bulletInstance.GetComponent<Bullet>();
            bulletReference.damage = damage;
            bulletReference.shooter = holdingPlayer.gameObject;
            fireDelay = 1 / fireRate;
            currentMagSize--;
            UpdateAmmoText();
            if (currentMagSize <= 0)
            {
                Break();
            }
        }
    }

    public virtual void Break()
    {
        Destroy(gameObject);
    }

    protected override void Awake()
    {
        base.Awake();
        bulletType = bullet.GetComponent<Bullet>();
        fireDelay = 1 / fireRate;
        currentMagSize = magSize;
        Transform findConstraint = GameObject.FindGameObjectsWithTag("RotationConstraint")[0].transform;
        ammoCount.GetComponent<RotationConstraint>().SetSource(0, new ConstraintSource { sourceTransform = findConstraint , weight = 1 }); // Set source for rotation constraint at runtime so the text doesn't rotate with the object
        UpdateAmmoText();
    }
    private void Update()
    {
        fireDelay -= Time.deltaTime;
    }
    protected void UpdateAmmoText()
    {
        ammoCount.text = $"{currentMagSize}/{magSize}";
    }
}
