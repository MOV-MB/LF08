using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform ShootingPoint;
    public GameObject BulletPrefab;

    public float BulletForce = 30f;
    public float Firerate = 0.5f;
    
    private float _nextfire;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && !PauseMenu.isGamePaused)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (!(Time.time > _nextfire)) return;
        _nextfire = Time.time + Firerate;
        GameObject bullet = Instantiate(BulletPrefab, ShootingPoint.position, ShootingPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.TransformDirection(Vector2.up) * BulletForce, ForceMode2D.Impulse);
    }
}
