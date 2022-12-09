using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    public float bulletForce = 30f;

    public float firerate = 0.5f;
    float nextfire;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootingPoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}
