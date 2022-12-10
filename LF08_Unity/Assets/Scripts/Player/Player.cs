using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform ShootingPoint;
    public GameObject BulletPrefab;
    public float BulletForce = 30f;
    public float Firerate = 0.5f;
    public float moveSpeed = 10f;
    private Rigidbody2D PlayerRigidbody;
    public Camera cam;

    private Vector2 movement;
    private Vector2 mousePosition;
    private float _nextfire;

    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        UpdateMovement();
        UpdateRotation();
        UpdateCamera();
    }

    void HandleInput()
    {
        if (Input.GetButton("Fire1") && !PauseMenu.isGamePaused)
        {
            Shoot();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void Shoot()
    {
        if (!(Time.time > _nextfire)) return;
        _nextfire = Time.time + Firerate;
        GameObject bullet = Instantiate(BulletPrefab, ShootingPoint.position, ShootingPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.TransformDirection(Vector2.up) * BulletForce, ForceMode2D.Impulse);
    }

    void UpdateMovement()
    {
        PlayerRigidbody.MovePosition(PlayerRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateRotation()
    {
        Vector2 lookDirection = mousePosition - PlayerRigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        PlayerRigidbody.rotation = angle;
    }

    void UpdateCamera()
    {
        cam.transform.position = transform.position + new Vector3(0, 1, -5);
    }
}