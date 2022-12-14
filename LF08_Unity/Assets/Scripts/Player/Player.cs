using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public Transform ShootingPoint;
        public GameObject BulletPrefab;
        public Text DeathText;

        public float BulletForce = 30f;
        public float Firerate = 0.5f;
        public float MoveSpeed = 10f;
        public float Health = 100f;

        private Rigidbody2D _playerRigidbody;
        public Camera Cam;

        private Vector2 _movement;
        private Vector2 _mousePosition;
        private float _nextfire;

        private const string playerShootingSoundName = "playerShoot";

        private void Start()
        {
            _playerRigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            HandlePlayerInput();
            UpdatePlayerRotation();
            UpdateCamera();
        }

        private void FixedUpdate()
        {
            UpdatePlayerMovement();
        }

        private void HandlePlayerInput()
        {
            if (Input.GetButton("Fire1") && !PauseMenu.isGamePaused)
            {
                Shoot();
            }

            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            _mousePosition = Cam.ScreenToWorldPoint(Input.mousePosition);
        }

        private void Shoot()
        {
            if (!(Time.time > _nextfire)) return;
            _nextfire = Time.time + Firerate;
            GameObject bullet = Instantiate(BulletPrefab, ShootingPoint.position, ShootingPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.TransformDirection(Vector2.up) * BulletForce, ForceMode2D.Impulse);
            AudioManager.main.PlaySFX(playerShootingSoundName);
        }

        private void UpdatePlayerMovement()
        {
            _playerRigidbody.MovePosition(_playerRigidbody.position + MoveSpeed * Time.fixedDeltaTime * _movement);
        }

        private void CheckForCollision()
        {
            
        }

        private void UpdatePlayerRotation()
        {
            Vector2 lookDirection = _mousePosition - _playerRigidbody.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            _playerRigidbody.rotation = angle;
        }

        private void UpdateCamera()
        {
            Cam.transform.position = transform.position + new Vector3(0, 1, -5);
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            if (!(Health <= 0)) return;
            Destroy(gameObject);
            DeathText.gameObject.SetActive(true);

            StartCoroutine(RestartGame());
        }

        private IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(3);
            // Restart the game.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        
    }
}